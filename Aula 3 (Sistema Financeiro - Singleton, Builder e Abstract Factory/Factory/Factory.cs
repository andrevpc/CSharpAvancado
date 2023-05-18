namespace Financeiro.Factory;
using Process;
public interface IProcessFactory
{
  WagePaymentProcess CreateWagePaymentProcess();
  DismissalProcess CreateDismissalProcess();
  ContractProcess CreateContractProcess();
}