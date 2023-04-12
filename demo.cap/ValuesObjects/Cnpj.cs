namespace demo.cap.ValuesObjects;

public class Cnpj
{
    public const string msgErro = "CNPJ inválido.";
    public const int CpfMaxLength = 14;

    public string Numero { get; private set; }

    public Cnpj(string numero)
    {
        if (!Validar(numero))
            throw new ArgumentException(msgErro);

        numero = ApenasNumeros(numero);

        Numero = numero.PadLeft(14, '0');
    }
    public static bool Validar(string cnpj)
    {
        int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int soma;
        int resto;
        string digito;
        string tempCnpj;
        cnpj = cnpj.Trim();
        cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

        cnpj = ApenasNumeros(cnpj);

        cnpj = cnpj.PadLeft(14, '0');

        if (cnpj.Length > 14) return false;

        tempCnpj = cnpj.Substring(0, 12);
        soma = 0;
        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = resto.ToString();
        tempCnpj = tempCnpj + digito;
        soma = 0;
        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = digito + resto.ToString();
        return cnpj.EndsWith(digito);

    }

    public static string ApenasNumeros(string input)
        => new(input.Where(char.IsDigit).ToArray());
}