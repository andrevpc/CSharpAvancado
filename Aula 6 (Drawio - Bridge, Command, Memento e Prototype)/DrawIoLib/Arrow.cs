using System.Drawing;
using System.Threading.Tasks;
 
namespace DrawIo;
 
public class Arrow : VisualObject
{
    private bool dotted = false;
    private ClassBox start;
    private ClassBox end;
 
    public Arrow(IVisualBehavior g, ClassBox start, ClassBox end, bool dotted) : base(g)
    {
        this.start = start;
        this.end = end;
        this.dotted = dotted;
    }
 
    public override VisualObject Clone()
    {
        Arrow arrow = new Arrow(g, start, end, dotted);
        return arrow;
    }
 
    public override async Task Draw(bool selected)
    {
        var left = start.Rectangle.Location.X > end.Rectangle.Location.X
            ? end : start;
        var right = left == start ? end : start;
 
        var leftPt = new PointF(
            left.Rectangle.Location.X + left.Rectangle.Width,
            left.Rectangle.Location.Y + left.Rectangle.Height / 2
        );
        var rightPt = new PointF(
            right.Rectangle.Location.X,
            right.Rectangle.Location.Y + right.Rectangle.Height / 2
        );
 
        float wid = rightPt.X - leftPt.X;
        float middle = leftPt.X + wid / 2;
        var middleLeftPt = new PointF(middle, leftPt.Y);
        var middleRightPt = new PointF(middle, rightPt.Y);
 
        var color = selected ? Color.Red : Color.Black;
 
        if (dotted)
        {
            await g.DrawDottedLine(leftPt, middleLeftPt, color, 2f);
            await g.DrawDottedLine(middleLeftPt, middleRightPt, color, 2f);
            await g.DrawDottedLine(middleRightPt, rightPt, color, 2f);
        }
        else
        {
            await g.DrawLine(leftPt, middleLeftPt, color, 2f);
            await g.DrawLine(middleLeftPt, middleRightPt, color, 2f);
            await g.DrawLine(middleRightPt, rightPt, color, 2f);
        }
 
        if (end == left)
        {
            await g.DrawLine(leftPt, new PointF(leftPt.X + 20, leftPt.Y + 10), color, 3f);
            await g.DrawLine(leftPt, new PointF(leftPt.X + 20, leftPt.Y - 10), color, 3f);
        }
        else
        {
            await g.DrawLine(rightPt, new PointF(rightPt.X - 20, rightPt.Y + 10), color, 3f);
            await g.DrawLine(rightPt, new PointF(rightPt.X - 20, rightPt.Y - 10), color, 3f);
        }
    }
}