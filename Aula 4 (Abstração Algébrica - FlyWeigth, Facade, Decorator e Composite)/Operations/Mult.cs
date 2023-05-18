namespace Operations;
using Functions;

public class Mult : Function
{
  private List<Function> funcs = new List<Function>();
  public void Add(Function func)
    => this.funcs.Add(func);

  protected override double get(double x)
  {
    double result = 1;
    
    foreach (var f in this.funcs)
      result *= f[x];
      
    return result;
  }
  
  public override Function Derive()
  {
    return null; // ainda não fiz kk (ainda hua hua)
  }
  
  public override string ToString()
  {
    string str = "";

    foreach (var f in this.funcs)
      str += f.ToString() + " * ";

    return str.Substring(0, str.Length - 3);
  }
}