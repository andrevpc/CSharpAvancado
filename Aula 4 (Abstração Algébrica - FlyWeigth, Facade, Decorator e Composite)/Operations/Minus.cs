namespace Operations;
using Functions;

public class Minus : Function //errado
{
  private Function f;
  public Minus(Function func)
    => this.f = func;

  protected override double get(double x) => -f[x];
  
  public override Function Derive()
    => -f.Derive();
  
  public override string ToString()
    => $"-({f})";
}