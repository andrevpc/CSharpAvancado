using System;
using System.Threading.Tasks;
 
namespace DrawIo;
 
public abstract class VisualObject : ICloneable, IPrototype
{
    protected IVisualBehavior g;
 
    public VisualObject(IVisualBehavior g)
        => this.g = g;
     
    public abstract Task Draw(bool selected);
 
    public abstract VisualObject Clone();
 
    object ICloneable.Clone()
        => this.Clone();
}