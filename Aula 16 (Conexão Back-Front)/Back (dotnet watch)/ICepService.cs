public interface ICepService
{
    Task<CepData> Get(string cep);
}