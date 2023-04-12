using demo.cap.Models;

namespace demo.cap.tests.Basico;

public class AssertingCollectionsTests
{
    [Fact]
    public void Cliente_Habilidades_NaoDevePossuirHabilidadesVazias()
    {
        //Arrange & Act
        var cliente = new ClientePessoaFisica(
            "Pedrinho",
            "Silva",
            "pedrinho.silva@live.com",
            new DateTime(1990, 7, 23),
            0,
            "356.712.080-84");

        //Assert
        Assert.All(cliente.Habilidades, habilidade => Assert.False(string.IsNullOrWhiteSpace(habilidade)));
    }

    [Fact]
    public void Cliente_Habilidades_FusquinhaDevePossuirHabilidadeC2()
    {
        //Arrange & Act
        var cliente = new ClientePessoaFisica(
            "Pedrinho",
            "Silva",
            "pedrinho.silva@live.com",
            new DateTime(1990, 7, 23),
            1020,
            "356.712.080-84");

        //Assert
        Assert.Contains("C2", cliente.Habilidades);
    }

    [Fact]
    public void Cliente_Habilidades_FusquinhaNaoDevePossuirHabilidadeA1()
    {
        //Arrange & Act
        var cliente = new ClientePessoaFisica(
            "Pedrinho",
            "Silva",
            "pedrinho.silva@live.com",
            new DateTime(1990, 7, 23),
            1020,
            "356.712.080-84");

        //Assert
        Assert.DoesNotContain("A1", cliente.Habilidades);
    }

    [Fact]
    public void Funcionario_Habilidades_SeniorDevePossuirTodasHabilidades()
    {
        //Arrange & Act
        var cliente = new ClientePessoaFisica(
            "Pedrinho",
            "Silva",
            "pedrinho.silva@live.com",
            new DateTime(1990, 7, 23),
            123987.97m,
            "356.712.080-84");

        var habilidades = new[]
        {
            "C3",
            "C2",
            "C1",
            "B1",
            "B2",
            "A1",
            "A2"
        };

        //Assert
        Assert.Equal(habilidades, cliente.Habilidades);
    }
}
