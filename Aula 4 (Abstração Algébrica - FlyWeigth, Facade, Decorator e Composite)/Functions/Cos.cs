namespace Functions;
using static FunctionUtil;

public class Cos : Function
{
  private Function inner;
  public Cos(Function inner)
    => this.inner = inner;
  
  protected override double get(double x) 
    => Math.Cos(inner[x]);
  
  public override Function Derive()
    => inner.Derive() * sin(inner); //espera
  
  public override string ToString() 
    => $"cos({inner})";
}