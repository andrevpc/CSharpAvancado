namespace Financeiro.Countries;
using Process;
using Factory;
public class ArgentinaDismissalProcess : DismissalProcess
{
    public override string Title => "Despido de Empleados";
 
    public override void Apply(DismissalArgs args)
    {
        args.Company.Money -= 3 * args.Employe.Wage;
    }
}
 
public class ArgentinaWagePaymentProcess : WagePaymentProcess
{
    public override string Title => "Pago de salario";
 
    public override void Apply(WagePaymentArgs args)
    {
        args.Company.Money -= 1.65m * args.Employe.Wage + 700;
    }
}
 
public class ArgentinaContractProcess : ContractProcess
{
    public override string Title => "Emplear";
 
    public override void Apply(ContractProcessArgs args)
    {
        args.Company.Money -= 0.4m * args.Employe.Wage;
        Console.WriteLine($"{args.Employe.Name} fue empleado");
    }
}

public class ArgentinaProcessFactory : IProcessFactory
{
    public ContractProcess CreateContractProcess()
    {
        throw new NotImplementedException();
    }

    public DismissalProcess CreateDismissalProcess()
        => new ArgentinaDismissalProcess();
 
    public WagePaymentProcess CreateWagePaymentProcess()
        => new ArgentinaWagePaymentProcess();
}