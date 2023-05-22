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
    Sum sum = new();
    for (int i = 0; i < this.funcs.Count(); i++)
    {
      Mult mult = new();
      for (int j = 0; j < this.funcs.Count(); j++)
      {
        if (i == j)
          mult.Add(this.funcs[j].Derive());
        else
          mult.Add(this.funcs[j]);
      }
      sum.Add(mult);
    }
    return sum;
  }
  
  public override string ToString()
  {
    string str = "";

    foreach (var f in this.funcs)
      str += f.ToString() + " * ";

    return str.Substring(0, str.Length - 3);
  }
}