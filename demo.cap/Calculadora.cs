namespace demo.cap;
public class Calculadora
{
    public double Somar(double v1, double v2) => v1 + v2;
    public double Subtrair(double v1, double v2) => v1 - v2;
    public int Dividir(int v1, int v2) => v1 / v2;
    public double Multiplicar(double v1, double v2) => v1 * v2;
}


public class StringTools
{
    public string Unir(string nome, string sobrenome) => $"{nome} {sobrenome}";
}