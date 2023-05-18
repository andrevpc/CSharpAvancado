namespace Functions;
using static FunctionUtil;

public class Sin : Function
{
  private Function inner;
  public Sin(Function inner)
    => this.inner = inner;
  
  protected override double get(double x) 
    => Math.Sin(inner[x]);
  
  public override Function Derive()
    => inner.Derive() * (-cos(inner));
  
  public override string ToString() 
    => $"sin({inner})";
}