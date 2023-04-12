namespace demo.cap.tests.Basico;

public class AssertStringsTests
{
    [Fact]
    public void StringsTools_UniNomes_RetornarNomeCompleto()
    {
        //Arrange
        var sut = new StringTools();

        //Act
        var nomeCompleto = sut.Unir("Pedrinho", "Silva");

        //Assert
        Assert.Equal("Pedrinho Silva", nomeCompleto);
    }

    [Fact]
    public void StringsTools_UniNomes_DeveIgnorarCase()
    {
        //Arrange
        var sut = new StringTools();

        //Act
        var nomeCompleto = sut.Unir("Pedrinho", "Silva");

        //Assert
        Assert.Equal("PEDRINHO SILVA", nomeCompleto, true);
    }

    [Fact]
    public void StringsTools_UniNomes_DeveConterTrecho()
    {
        //Arrange
        var sut = new StringTools();

        //Act
        var nomeCompleto = sut.Unir("Pedrinho", "Silva");

        //Assert
        Assert.Contains("nato ", nomeCompleto);
    }

    [Fact]
    public void StringsTools_UniNomes_DeveComecarCom()
    {
        //Arrange
        var sut = new StringTools();

        //Act
        var nomeCompleto = sut.Unir("Pedrinho", "Silva");

        //Assert
        Assert.StartsWith("Ren", nomeCompleto);
    }

    [Fact]
    public void StringsTools_UniNomes_DeveAcabarCom()
    {
        //Arrange
        var sut = new StringTools();

        //Act
        var nomeCompleto = sut.Unir("Pedrinho", "Silva");

        //Assert
        Assert.EndsWith("fer", nomeCompleto);
    }

    [Fact]
    public void StringsTools_UniNomes_ValidarExpressaoRegular()
    {
        //Arrange
        var sut = new StringTools();

        //Act
        var nomeCompleto = sut.Unir("Pedrinho", "Silva");

        //Assert
        Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", nomeCompleto);
    }
}
