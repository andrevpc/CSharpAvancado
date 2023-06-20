public class Cpf
{
    public int RandomDigits { get; set; }
    public int FiscalRegionDigit { get; set; }
    public int VerificationDigit { get; set; }
    public bool Verified { get; set; } = false;
    public bool Validated { get; set; } = false;

    public string Value
    {
        get => $"{RandomDigits:000.000.00}{FiscalRegionDigit}-{VerificationDigit:00}";
        set
        {
            if (value is null)
                throw new InvalidCastException("Value can't be null.");

            value = value
                .Replace("-", "")
                .Replace(".", "");

            if (value.Length != 11)
                throw new InvalidCastException("Invalid number of digits.");

            RandomDigits = int.Parse(value.Substring(0, 8));
            FiscalRegionDigit = int.Parse(value.Substring(8, 1));
            VerificationDigit = int.Parse(value.Substring(9, 2));
        }
    }

    public string Region
    {
        get => FiscalRegionDigit switch
        {
            1 => "DF, GO, MT, MS e TO",
            2 => "AC, AP, AM, PA, RO e RR",
            3 => "CE, MA e PI",
            4 => "AL, PB, PE e RN",
            5 => "BA e SE",
            6 => "MG",
            7 => "ES e RJ",
            8 => "SP",
            9 => "PR e SC",
            10 => "RS",
            _ => "Região desconhecida"
        };
    }
}
