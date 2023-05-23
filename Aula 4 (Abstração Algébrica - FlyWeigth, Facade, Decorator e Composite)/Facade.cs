using Functions;

public static class FunctionUtil
{
  #region linear
  private static Linear linear = null;
  public static Function x
  {
    get
    {
      if (linear is null)
        linear = new Linear();

      return linear;
    }
  }
  #endregion

  #region euler
  private static Constant euler = null;
  public static Function e
  {
    get
    {
      if (euler is null)
        euler = new Constant(Math.E);

      return euler;
    }
  }
  #endregion
  
  #region pi
  private static Constant p = null;
  public static Function pi
  {
    get
    {
      if (p is null)
        p = new Constant(Math.PI);

      return p;
    }
  }
  #endregion
  
  #region sen
  public static Function sin(Function f)
    => new Sin(f);
  public static Function sin(double f)
    => new Sin(new Constant(f));
  #endregion

  #region cos
  public static Function cos(Function f)
    => new Cos(f);
  public static Function cos(double f)
    => new Cos(new Constant(f));
  #endregion

  #region pow
  public static Function pow(Function f, Function g)
    => new Pow(f, g);
  public static Function pow(Function f, double g)
    => new Pow(f, new Constant(g));
  public static Function pow(double f, Function g)
    => new Pow(new Constant(f), g);
  #endregion

  #region ln
  public static Function ln(Function f)
    => new Ln(f);
  public static Function ln(double f)
    => new Ln(new Constant(f));
  #endregion

  #region log
  public static Function log(Function f, Function g)
    => new Log(f, g);
  public static Function log(Function f, double g)
    => new Log(f, new Constant(g));
  public static Function log(double f, Function g)
    => new Log(new Constant(f), g);
  #endregion

}