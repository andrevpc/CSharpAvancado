namespace Functions;
using Operations;

public abstract class Function
{
  public double this[double x] => get(x);

  protected abstract double get(double x);
  public abstract Function Derive();
  
  #region +
  public static Function operator +(Function f, Function g)
  {
    Sum sum = new();
    sum.Add(f);
    sum.Add(g);
    return sum;
  }
  public static Function operator +(Function f, double n)
  {
    Sum sum = new();
    sum.Add(f);
    sum.Add(new Constant(n));
    return sum;
  }
  public static Function operator +(double f, Function n)
  {
    Sum sum = new();
    sum.Add(new Constant(f));
    sum.Add(n);
    return sum;
  }
  #endregion

  #region -
  public static Function operator -(Function f)
  {
    Minus minus = new(f);
    return minus;
  }
  public static Function operator -(Function f, Function g)
  {
    Sub sub = new();
    sub.Add(f);
    sub.Add(g);
    return sub;
  }
  public static Function operator -(Function f, double g)
  {
    Sub sub = new();
    sub.Add(f);
    sub.Add(new Constant(g));
    return sub;
  }
  public static Function operator -(double f, Function g)
  {
    Sub sub = new();
    sub.Add(new Constant(f));
    sub.Add(g);
    return sub;
  }
  #endregion

  #region *
  public static Function operator *(Function f, Function g)
  {
    Mult mult = new();
    mult.Add(f);
    mult.Add(g);
    return mult;
  }
  public static Function operator *(Function f, double g)
  {
    Mult mult = new();
    mult.Add(f);
    mult.Add(new Constant(g));
    return mult;
  }
  public static Function operator *(double f, Function g)
  {
    Mult mult = new();
    mult.Add(new Constant(f));
    mult.Add(g);
    return mult;
  }
  #endregion

  #region /
  public static Function operator /(Function f, Function g)
  {
    Div div = new();
    div.Add(f);
    div.Add(g);
    return div;
  }
  public static Function operator /(Function f, double g)
  {
    Div div = new();
    div.Add(f);
    div.Add(new Constant(g));
    return div;
  }
  public static Function operator /(double f, Function g)
  {
    Div div = new();
    div.Add(new Constant(f));
    div.Add(g);
    return div;
  }
  #endregion

  #region ^
  public static Function operator ^(Function f, Function g)
    =>  new Pow(f, g);
  public static Function operator ^(Function f, double g)
    =>  new Pow(f, new Constant(g));
  public static Function operator ^(double f, Function g)
    =>  new Pow(new Constant(f), g);
  #endregion 
  
}