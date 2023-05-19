namespace Functions;

public class Ln : Function
{
  private Function inner;
  public Ln(Function inner)
    => this.inner = inner;
  
  protected override double get(double x) 
    => Math.Log(inner[x]);
  
  public override Function Derive()
    => inner.Derive() / inner;
  
  public override string ToString() 
    => $"ln({inner})";
}