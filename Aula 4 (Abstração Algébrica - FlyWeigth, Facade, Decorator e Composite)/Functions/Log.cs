namespace Functions;
using static FunctionUtil;

public class Log : Function
{
  private Function u;
  private Function a;
  public Log(Function a, Function u)
  {
    this.u = u;
    this.a = a;
  }
  
  protected override double get(double x) 
    => Math.Log(u[x], a[x]);
  
  public override Function Derive()
    => u.Derive() / u * log(a, e);
  
  public override string ToString() 
    => $"log({a} {u})";
}