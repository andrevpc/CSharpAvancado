using System.Drawing;
using System.Threading.Tasks;
 
namespace DrawIo;
 
public class ClassBox : VisualObject
{
    public RectangleF Rectangle { get; set; }
    public Color Color { get; set; }
    private string text;
 
    public ClassBox(IVisualBehavior g, PointF initial) : base(g)
    {
        this.text = "Classe";
        this.Color = Color.White;
        this.Rectangle = new RectangleF(initial, new SizeF(100, 100));
    }
 
    public override VisualObject Clone()
    {
        var crrPt = this.Rectangle.Location;
        var newPt = new PointF(crrPt.X + 20, crrPt.Y + 20);
 
        ClassBox box = new ClassBox(this.g, PointF.Empty);
        box.text = this.text;
        box.Color = this.Color;
        box.Rectangle = new RectangleF(newPt, this.Rectangle.Size);
 
        return box;
    }

    public override async Task Draw(bool selected)
    {
        await g.FillRectangle(Rectangle, Color);
        await g.DrawRectangle(Rectangle, 
            selected ? Color.Red : Color.Black);
        await g.DrawText(Rectangle, text);
    }
}