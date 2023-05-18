namespace Financeiro.Countries;
using Process;
using Factory;

public class USADismissalProcess : DismissalProcess
{
    public override string Title => "Employe dismiss";

    public override void Apply(DismissalArgs args)
    {
        args.Company.Money -= 4 * args.Employe.Wage;
    }
}

public class USAWagePaymentProcess : WagePaymentProcess
{
    public override string Title => "Payday";

    public override void Apply(WagePaymentArgs args)
    {
        args.Company.Money -= 2m * args.Employe.Wage;
    }
}

public class USAProcessFactory : IProcessFactory
{
    public ContractProcess CreateContractProcess()
        => new BrazilContractProcess();

    public DismissalProcess CreateDismissalProcess()
        => new USADismissalProcess();

    public WagePaymentProcess CreateWagePaymentProcess()
        => new USAWagePaymentProcess();
}
