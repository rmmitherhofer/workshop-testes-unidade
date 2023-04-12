using demo.cap.Models;

namespace demo.cap.tests.Basico;
public class AssertingObjectTypesTests
{
    [Fact]
    public void ClientePessoaFisica_Criar_DeveRetornarTipoClientePessoaFisica()
    {
        //Arrange & Act
        var cliente = new ClientePessoaFisica(
            "Pedrinho",
            "Silva",
            "pedrinho.silva@live.com",
            new DateTime(1990, 7, 23),
            10000,
            "356.712.080-84");

        //Assert
        Assert.IsType<ClientePessoaFisica>(cliente);
    }

    [Fact]
    public void ClientePessoaFisica_Criar_DeveRetornarTipoDerivadoCliente()
    {
        //Arrange & Act
        var cliente = new ClientePessoaFisica(
            "Pedrinho",
            "Silva",
            "pedrinho.silva@live.com",
            new DateTime(1990, 7, 23),
            10000,
            "356.712.080-84");

        //Assert
        Assert.IsAssignableFrom<Cliente>(cliente);
    }
}
