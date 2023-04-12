namespace demo.cap.tests.Basico;

public class CalculadoraTests
{
    [Fact]
    public void Test()
    {
        //Arrange

        //Act

        //Assert
    }

    [Fact]
    public void Calculadora_Somar_RetornarValorSoma()
    {
        //Arrange
        var calculadora = new Calculadora();

        //Act
        var resultado = calculadora.Somar(2, 2);

        //Assert
        Assert.Equal(4, resultado);
    }

    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(2, 2, 4)]
    [InlineData(3, 3, 6)]
    [InlineData(4, 4, 8)]
    [InlineData(5, 5, 10)]
    [InlineData(6, 6, 12)]
    [InlineData(7, 7, 14)]
    [InlineData(8, 8, 16)]
    [InlineData(9, 9, 18)]
    public void Calculadora_Somar_RetornarValoresSomaCorretos(double v1, double v2, double total)
    {
        //Arrange
        var calculadora = new Calculadora();

        //Act
        var resultado = calculadora.Somar(v1, v2);

        //Assert
        Assert.Equal(total, resultado);
    }
}
