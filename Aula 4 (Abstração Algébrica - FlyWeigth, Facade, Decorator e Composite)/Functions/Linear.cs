namespace Functions;

public class Linear : Function
{
  protected override double get(double x) => x;
  
  public override Function Derive()
    => new Constant(1);

  public override string ToString() => "x";
}