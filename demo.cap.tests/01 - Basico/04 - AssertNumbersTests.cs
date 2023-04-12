namespace demo.cap.tests.Basico;

public class AssertNumbersTests
{
    [Fact]
    public void Calculadora_Somar_DeverSerIgual()
    {
        //Arrange
        var calculadora = new Calculadora();

        //Act
        var result = calculadora.Somar(1, 2);

        //Assert
        Assert.Equal(3, result);
    }

    [Fact]
    public void Calculadora_Somar_NaoDeverSerIgual()
    {
        //Arrange
        var calculadora = new Calculadora();

        //Act
        var result = calculadora.Somar(1.13123123123, 2.13123123123);

        //Assert
        Assert.Equal(3.3, result, 1);
    }
}
