namespace Functions;
using Operations;

public abstract class Function
{
  public double this[double x] => get(x);

  protected abstract double get(double x);
  public abstract Function Derive();

  public static Function operator +(Function f, double n)
  {
    Sum sum = new Sum();
    sum.Add(f);
    sum.Add(new Constant(n));
    return sum;
  }
  public static Function operator +(Function f, Function g)
  {
    Sum sum = new Sum();
    sum.Add(f);
    sum.Add(g);
    return sum;
  }
  
  public static Function operator *(Function f, Function g)
  {
    Mult mult = new Mult();
    mult.Add(f);
    mult.Add(g);
    return mult;
  }
}