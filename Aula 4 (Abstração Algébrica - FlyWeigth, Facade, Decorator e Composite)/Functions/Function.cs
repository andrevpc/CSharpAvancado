namespace Functions;
using Operations;

public abstract class Function
{
  public double this[double x] => get(x);

  protected abstract double get(double x);
  public abstract Function Derive();

  public static Function operator +(Function f, double n)
  {
    Sum sum = new();
    sum.Add(f);
    sum.Add(new Constant(n));
    return sum;
  }
  public static Function operator +(Function f, Function g)
  {
    Sum sum = new();
    sum.Add(f);
    sum.Add(g);
    return sum;
  }
  public static Function operator -(Function f)
  {
    Minus minus = new();
    return minus;
  }
  public static Function operator -(Function f, Function g)
  {
    Sub sub = new();
    sub.Add(f);
    sub.Add(g);
    return sub;
  }
  
  public static Function operator *(Function f, Function g)
  {
    Mult mult = new();
    mult.Add(f);
    mult.Add(g);
    return mult;
  }
  public static Function operator /(Function f, Function g)
  {
    Div div = new();
    div.Add(f);
    div.Add(g);
    return div;
  }
}