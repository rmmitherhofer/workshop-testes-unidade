namespace demo.cap.ValuesObjects;
public class Cpf
{
    public const string msgErro = "CPF inválido.";
    public const int CpfMaxLength = 11;

    public string Numero { get; private set; }

    public Cpf(string numero)
    {
        if (!Validar(numero))
            throw new ArgumentException(msgErro);

        numero = ApenasNumeros(numero);

        Numero = numero.PadLeft(11, '0');
    }

    public static bool Validar(string cpf)
    {
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string tempCpf;
        string digito;
        int soma;
        int resto;
        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");

        cpf = ApenasNumeros(cpf);

        cpf = cpf.PadLeft(11, '0');

        if (cpf.Length > 11) return false;

        tempCpf = cpf.Substring(0, 9);
        soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = digito + resto.ToString();
        return cpf.EndsWith(digito);
    }

    public static string ApenasNumeros(string input)
        => new(input.Where(char.IsDigit).ToArray());
}