namespace Operations;
using Functions;

public class Div : Function
{
  private List<Function> funcs = new List<Function>();
  public void Add(Function func)
    => this.funcs.Add(func);

  protected override double get(double x)
  {
    double result = 1;
    
    foreach (var f in this.funcs)
      result /= f[x];
      
    return result;
  }
  
  public override Function Derive()
  {
    if (this.funcs.Count() > 2)
        {
            Function v = funcs[funcs.Count() - 1];
            Div u = new Div();
            foreach (var f in this.funcs.SkipLast(1))
                u.Add(f);

            return ((v * u.Derive()) - (v.Derive() * u)) / (v ^ 2);
        }

        Function g = funcs[0];
        Function h = funcs[1];

        return ((g.Derive() * h) - (g * h.Derive())) / (h ^ 2);
  }
  
  public override string ToString()
  {
    string str = "";

    foreach (var f in this.funcs)
      str += f.ToString() + " / ";

    return str.Substring(0, str.Length - 3);
  }
}