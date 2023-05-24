using System;

class Program
{
  public static void Main (string[] args)
  {
    var builder = Company.GetBuilder();
 
    builder
        .SetName("Mercado Libre")
        .InArgentina()
        .SetInitialCapital(20_000_000);
     
    builder
        .AddEmploye("Marquitos Guapo", 50_000)
        .AddEmploye("Paulito Pino", 20_000);
     
    Company.New(builder);
     
    // Me rendí, me voy a Brasil
    builder = Company.GetBuilder();
     
    builder
        .SetName("Mercado Livre")
        .InBrazil()
        .SetInitialCapital(1_000_000);
     
    builder
        .AddEmploye("Marcos Bonito", 2_500)
        .AddEmploye("Paulo Pinheiro", 1_000);
     
    Company.New(builder);
     
    Employe employe = new Employe();
    employe.Name = "Xispita";
    employe.Wage = 2_000;
    Company.Current.Contract(employe);
     
    Company.Current.Dismiss("Marcos Bonito");
     
    Company.Current.PayWages();
  }
}