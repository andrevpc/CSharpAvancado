namespace Functions;
using static FunctionUtil;

public class Pow : Function
{
  private Function u;
  private Function v;
  public Pow(Function u, Function v)
  {
    this.u = u;
    this.v = v;
  }
  protected override double get(double x) 
    => Math.Pow(u[x], v[x]);
  
  public override Function Derive()
    => v * (u ^ (v - 1)) * u.Derive() + (u ^ v) * ln(u) * v.Derive();
  
  public override string ToString() 
    => $"({u}^{v})";
}