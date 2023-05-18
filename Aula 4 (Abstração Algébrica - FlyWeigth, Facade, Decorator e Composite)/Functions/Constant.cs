namespace Functions;

public class Constant : Function
{
  private double valor;

  public Constant(double valor)
    => this.valor = valor;

  protected override double get(double x) => this.valor;
  
  public override Function Derive()
    => new Constant(0);
  
  public override string ToString() => this.valor.ToString();
}