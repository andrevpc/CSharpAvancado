using System.Drawing;
using System.Threading.Tasks;
 
namespace DrawIo;
 
public interface IVisualBehavior
{
    Task FillRectangle(RectangleF rect, Color color);
    Task DrawRectangle(RectangleF rect, Color color);
    Task DrawText(RectangleF rect, string text);
    Task DrawLine(PointF p, PointF q, Color color, float width);
    Task DrawDottedLine(PointF p, PointF q, Color color, float width);
}