namespace Operations;
using Functions;

public class Sum : Function
{
  private List<Function> funcs = new List<Function>();
  public void Add(Function func)
    => this.funcs.Add(func);

  protected override double get(double x)
    => this.funcs.Sum(f => f[x]);
  
  public override Function Derive()
  {
    Sum result = new Sum();
    
    foreach (var f in this.funcs)
      result.Add(f.Derive());
    
    return result;
  }
  
  public override string ToString()
  {
    string str = "(";

    foreach (var f in this.funcs)
      str += f.ToString() + " + ";

    return str.Substring(0, str.Length - 3) + ")";
  }
}
