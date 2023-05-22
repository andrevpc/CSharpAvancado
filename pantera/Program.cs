using Pamella;
using System.Drawing;
using System.Collections.Generic;
using System;

var view = new ExemploView();
App.Open(view);

public class ExemploView : View
{
    private List<Enemy> enemies = new List<Enemy>();
    private PointF player = PointF.Empty;
    protected override void OnRender(IGraphics g)
    {
        g.Clear(Color.DarkBlue);

        foreach (var enemy in this.enemies)
        {
            g.FillPolygon(enemy.GetFlashlight(),
                Brushes.Yellow);
            g.FillRectangle(
                enemy.Location.X - 20,
                enemy.Location.Y - 20,
                40, 40, Brushes.Green
            );
            enemy.Act();
        }

        g.FillRectangle(
            player.X - 20, player.Y - 20,
            40, 40, Brushes.HotPink
        );

        Invalidate();
    }

    protected override void OnStart(IGraphics g)
    {
        g.SubscribeKeyDownEvent(input =>
        {
            if (input == Input.Escape)
                App.Close();
            
            if (input == Input.D)
                player = new PointF(player.X + 5, player.Y);
            
            if (input == Input.A)
                player = new PointF(player.X - 5, player.Y);
            
            if (input == Input.W)
                player = new PointF(player.X, player.Y - 5);
            
            if (input == Input.S)
                player = new PointF(player.X, player.Y + 5);
        });

        var builder = new PatrollingBuilder();
        var state = builder
            .AddPatrolling(new PointF(
                g.Width / 2,
                g.Height / 2 + 400
            ))
            .AddSpinning(MathF.PI / 2)
            .AddPatrolling(new PointF(
                g.Width / 2 + 400,
                g.Height / 2 + 400
            ))
            .AddWait(2000)
            .AddSpinning(MathF.PI)
            .AddPatrolling(new PointF(
                g.Width / 2,
                g.Height / 2 + 400
            ))
            .AddSpinning(MathF.PI)
            .AddPatrolling(new PointF(
                g.Width / 2,
                g.Height / 2 - 400
            ))
            .AddWait(2000)
            .AddSpinning(MathF.PI)
            .Connect()
            .Build();

        Enemy enemy = new Enemy();
        enemy.SetState(state);
        enemy.Location = new PointF(
            g.Width / 2,
            g.Height / 2
        );
        this.enemies.Add(enemy);
    }
}

public class Enemy
{
    private State state;
    public void SetState(State state)
    {
        this.state = state;
        this.state.Enemy = this;
    }

    public PointF Location { get; set; }
    public float Rotation { get; set; }

    public void Act()
        => this.state.Act();

    public void Move(PointF target)
    {
        var dir = new SizeF(
            target.X - Location.X,
            target.Y - Location.Y
        );
        var dist = MathF.Sqrt(dir.Width * dir.Width + dir.Height * dir.Height);
        var unity = dir / dist;
        var vel = 5;
        this.Location = new PointF(
            this.Location.X + vel * unity.Width,
            this.Location.Y + vel * unity.Height
        );
    }

    public PointF[] GetFlashlight()
    {
        var u = new SizeF(
            MathF.Sin(Rotation),
            MathF.Cos(Rotation)
        );
        var v = new SizeF(
            -MathF.Cos(Rotation),
            MathF.Sin(Rotation)
        );
        return new PointF[]
        {
            Location,
            Location + 300 * u + 100 * v,
            Location + 300 * u - 100 * v,
        };
    }
}

public abstract class State
{
    public Enemy Enemy { get; set; }

    public abstract void Act();
}

public abstract class ConnectableState : State
{
    public State NextState { get; set; }
}

public class Waiting : ConnectableState
{
    public int Time { get; set; }
    private DateTime? start = null;
    public override void Act()
    {
        start ??= DateTime.Now;
        var span = DateTime.Now - start;
        if (span.Value.TotalMilliseconds > Time)
        {
            this.Enemy.SetState(NextState);
            this.start = null;
        }
    }
}

public class Patrolling : ConnectableState
{
    public PointF Target { get; set; }
    public override void Act()
    {
        var dx = this.Enemy.Location.X - Target.X;
        var dy = this.Enemy.Location.Y - Target.Y;
        var dist = dx * dx + dy * dy;
        if (dist < 100)
        {
            if (NextState is not null)
                this.Enemy.SetState(NextState);

            return;
        }

        this.Enemy.Move(Target);
    }
}

public class Spinnig : ConnectableState
{
    public float Rotation { get; set; }
    private float currentRotation = 0;
    private float startRotation = float.NaN;
    public override void Act()
    {
        if (startRotation == float.NaN)
        {
            startRotation = Enemy.Rotation % (2 * MathF.PI);
            currentRotation = startRotation;
        }
        var objective = startRotation + Rotation;
        var rotation = currentRotation < objective ?
            0.01f : -0.01f;

        this.Enemy.Rotation += rotation;
        this.currentRotation += rotation;

        if (MathF.Abs(currentRotation - objective) % (2 * MathF.PI) < 0.02f)
        {
            currentRotation = 0;
            this.Enemy.Rotation = objective;
            this.Enemy.SetState(NextState);
        }
    }
}

public class PatrollingBuilder
{   
    private ConnectableState state = null;
    private ConnectableState last = null;

    private void setNext(ConnectableState next)
    {
        if (state is null)
            last = state = next;
        else
        {
            last.NextState = next;
            last = next;
        }
    }

    public PatrollingBuilder AddPatrolling(PointF point)
    {
        var newState = new Patrolling();
        newState.Target = point;

        setNext(newState);

        return this;
    }
    public PatrollingBuilder AddWait(int milli)
    {
        var newState = new Waiting();
        newState.Time = milli;

        setNext(newState);

        return this;
    }
    public PatrollingBuilder AddSpinning(float angle)
    {
        var newState = new Spinnig();
        newState.Rotation = angle;

        setNext(newState);

        return this;
    }

    public PatrollingBuilder Connect()
    {
        last.NextState = state;
        return this;
    }

    public ConnectableState Build()
        => this.state;
}