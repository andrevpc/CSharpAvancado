public class CpfService
{
    private int getVerificationDigits(Cpf cpf)
    {
        var str = cpf.Value;

        int sum = 0;
        for (int i = 0; i < 8; i++)
        {
            int digit = str[i] - '0';
            sum += (i + 2) * digit;
        }
        int verifier1 = sum % 11;

        for (int i = 0; i < 8; i++)
        {
            int digit = str[i] - '0';
            sum -= digit;
        }
        sum += 9 * verifier1;
        int verifier2 = sum % 11;

        return 10 * verifier1 + verifier2;
    }

    public void Validate(Cpf cpf)
    {
        if (cpf is null)
            throw new ArgumentNullException("cpf");

        int verifier = getVerificationDigits(cpf);
        bool isValid = verifier == cpf.VerificationDigit;

        cpf.Verified = true;
        cpf.Validated = isValid;
    }

    public Cpf Generate(int region = -1)
    {
        Cpf cpf = new Cpf();

        if (region == -1)
            region = Random.Shared.Next(10);

        cpf.FiscalRegionDigit = region;
        cpf.RandomDigits = Random.Shared.Next(100_000_000);
        cpf.VerificationDigit = getVerificationDigits(cpf);

        cpf.Verified = true;
        cpf.Validated = true;
        return cpf;
    }
}