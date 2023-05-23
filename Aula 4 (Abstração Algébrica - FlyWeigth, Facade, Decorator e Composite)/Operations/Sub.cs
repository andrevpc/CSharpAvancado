namespace Operations;
using Functions;

public class Sub : Function
{
  private List<Function> funcs = new List<Function>();
  public void Add(Function func)
    => this.funcs.Add(func);

  protected override double get(double x)
  {
    var func = this.funcs;
    double result = func[0][x];
    
    for(int i = 1; i < func.Count(); i++)
      result -= func[i][x];
      
    return result;
  }
  
  public override Function Derive()
  {
    Sub result = new Sub();
    
    foreach (var f in this.funcs)
      result.Add(f.Derive());
    
    return result;
  }
  
  public override string ToString()
  {
    string str = "";

    foreach (var f in this.funcs)
      str += f.ToString() + " - ";

    return str.Substring(0, str.Length - 3);
  }
}