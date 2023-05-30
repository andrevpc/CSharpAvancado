using System.Collections.Generic;
using System.Threading.Tasks;
 
namespace DrawIo;
 
public class Project
{
    public VisualObject Selected { get; set; }
    public IEnumerable<VisualObject> Objects => this.objs;
    private VisualObject copied = null;
    private List<VisualObject> objs = new List<VisualObject>();
    private Stack<ICommand> history = new Stack<ICommand>();
    private Stack<ICommand> undone = new Stack<ICommand>();
 
    public Project()
    {
         
    }
 
    public void Execute(ICommand command)
    {
        command.Execute(this);
        history.Push(command);
        undone.Clear();
    }
 
    public void Undo()
    {
        if (history.Count < 1)
            return;
         
        var commmand = history.Pop();
        commmand.Undo(this);
        this.undone.Push(commmand);
    }
 
    public void Copy()
        => copied = Selected;
     
    public void Cut()
    {
        copied = Selected;
        Remove(Selected);
        Selected = null;
    }
     
    public VisualObject Paste()
    {
        if (copied == null)
            return null;
 
        var copy = copied.Clone();
        this.objs.Add(copy);
        copied = copy;
        return copy;
    }
     
    public VisualObject Delete()
    {
        Remove(Selected);
        var removed = Selected;
        Selected = null;
        return removed;
    }
 
    public void Remove(VisualObject obj)
        => this.objs.Remove(obj);
     
    public void Add(VisualObject obj)
        => this.objs.Add(obj);
 
    public void Redo()
    {
        if (undone.Count < 1)
            return;
         
        var commmand = undone.Pop();
        commmand.Execute(this);
        this.history.Push(commmand);
    }
 
    public async Task Draw()
    {
        foreach (var obj in this.objs)
        {
            await obj.Draw(obj == Selected);
        }
    }
}