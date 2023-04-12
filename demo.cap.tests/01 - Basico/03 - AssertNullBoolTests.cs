using demo.cap.Models;

namespace demo.cap.tests.Basico;

public class AssertNullBoolTests
{
    [Fact]
    public void Cliente_Nome_NaoDeveSerNuloOuVazio()
    {
        //Arrange & Act
        var cliente = new ClientePessoaFisica(
            "Pedrinho",
            "Silva",
            "pedrinho.silva@live.com",
            new DateTime(1990, 7, 23),
            2.99m,
            "356.712.080-84");

        //Assert
        Assert.False(string.IsNullOrEmpty(cliente.Nome));
    }

    [Fact]
    public void Cliente_Nome_NaoDeveTerApelido()
    {
        //Arrange & Act
        var cliente = new ClientePessoaFisica(
            "Pedrinho",
            "Silva",
            "pedrinho.silva@live.com",
            new DateTime(1990, 7, 23),
            2.99m,
            "356.712.080-84");

        //Assert
        Assert.Null(cliente.Apelido);

        //Assert Bool\
        Assert.True(string.IsNullOrEmpty(cliente.Apelido));
        Assert.False(cliente.Apelido?.Length > 0);
    }
}
