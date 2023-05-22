using Functions;

public static class FunctionUtil
{
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

  public static Function sin(Function f)
    => new Sin(f);

  public static Function cos(Function f)
    => new Cos(f);
  public static Function pow(Function f, Function g)
    => new Pow(f, g);
  public static Function ln(Function f)
    => new Ln(f);
}