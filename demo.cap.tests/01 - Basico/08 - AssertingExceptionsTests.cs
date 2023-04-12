using demo.cap.Models;

namespace demo.cap.tests.Basico;

public class AssertingExceptionsTests
{
    [Fact]
    public void Calculadora_Dividir_DeveRetornarErroDivisaoPorZero()
    {
        //Arrange
        var calculadora = new Calculadora();

        //Act & Assert
        Assert.Throws<DivideByZeroException>(() => calculadora.Dividir(10, 0));
    }

    [Fact]
    public void Funcionario_Salario_DeveRetornarErroSalarioInferiorPermitido()
    {
        //Arrange & Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            new ClientePessoaFisica(
                "Pedrinho",
                "Silva",
                "pedrinho.silva@live.com",
                new DateTime(1990, 7, 23),
                -64777.97m,
                "356.712.080-84"));

        Assert.Equal("Valor de patrimonio negativo.", exception.Message);
    }
}
