using demo.cap.Enums;
using demo.cap.Models;
using demo.cap.tests.DadosHumanos;
using FluentAssertions;

namespace demo.cap.tests.Models;
public class ClientePessoaFisicaTests
{
    [Fact(DisplayName = "Novo cliente pessoa física, deve estar válido")]
    [Trait("Entidade", "Cliente pessoa física")]
    public void DadoUmCliente_QuandoNovo_EntaoDeveEstarValido()
    {
        //Arrange
        var cliente = new ClientePessoaFisica(
            "Pedrinho",
            "Silva",
            "pedrinho.silva@live.com",
            new DateTime(1990, 7, 23),
            2.99m,
            "884.338.770-70");

        //Act
        var result = cliente.IsValid();

        //Assert
        Assert.True(result);
        Assert.Empty(cliente.ValidationResult.Errors);
    }

    [Fact(DisplayName = "Novo cliente pessoa física, deve estar válido")]
    [Trait("Entidade", "Cliente pessoa física")]
    public void QuandoNovo_DadoUmCliente_EntaoDeveEstarValido()
    {
        //Arrange
        var cliente = BogusClientePf.GerarClienteValido();

        //Act
        var result = cliente.IsValid();

        //Assert
        result.Should().BeTrue();
        cliente.ValidationResult.Errors.Should().BeEmpty();
    }

    [Theory(DisplayName = "atualizar cliente pessoa física, deve ser retornar grupo investidor")]
    [Trait("Entidade", "Cliente pessoa física")]
    [InlineData(1876.78, GrupoInvestidor.Fusquinha)]
    [InlineData(2971.71, GrupoInvestidor.Celta)]
    [InlineData(3004.71, GrupoInvestidor.Civic)]
    [InlineData(4526.79, GrupoInvestidor.Ferrari)]
    [InlineData(5379.12, GrupoInvestidor.RollsRoyce)]
    public void QuandoAtualizarComPatrimonioDeAteCincoMilReais_DadoUmCliente_EntaoDevePertencerAoGrupoFerrari(decimal valorPatrimonio, GrupoInvestidor grupoEsperado)
    {
        //Arrange
        var cliente = BogusClientePf.GerarClienteValido();

        //Act
        cliente.Atualizar(cliente.Email, valorPatrimonio);

        //Assert      
        cliente.Grupo.Should().Be(grupoEsperado);
    }

    [Fact(DisplayName = "Novo cliente pessoa física, deve apresentar nome completo")]
    [Trait("Entidade", "Cliente pessoa física")]
    public void QuandoNovo_DadoUmCliente_EntaoDevePossuirNomeCompleto()
    {
        //Arrange
        var cliente = BogusClientePf.GerarClienteValido();

        //Act
        var nomeCompleto = cliente.NomeCompleto();

        //Assert
        nomeCompleto.Should().Be($"{cliente.Nome} {cliente.Sobrenome}");
    }

    [Theory(DisplayName = "Novo cliente pessoa física CPF incorreto, deve estar inválido")]
    [Trait("Entidade", "Cliente pessoa física")]
    [InlineData(null)]
    [InlineData("15978419744")]
    [InlineData("00011122233")]
    [InlineData("15231766607213")]
    [InlineData("528781682082")]
    [InlineData("35555868512")]
    [InlineData("36014132822")]
    [InlineData("72186126500")]
    [InlineData("23775274811")]
    public void QuandoCPFIncorreto_DadoUmCliente_EntaoDeveEstarInvalido(string cpf)
    {
        //Arrange
        var cliente = new ClientePessoaFisica(
            "Pedrinho",
            "Silva",
            "pedrinho.silva@live.com",
            new DateTime(1990, 7, 23),
            2.99m,
            cpf);

        //Act
        var result = cliente.IsValid();

        //Assert
        result.Should().BeFalse();
        cliente.ValidationResult.Errors.Should().HaveCount(1);
    }

    [Theory(DisplayName = "Novo cliente pessoa física CPF correto, deve estar válido")]
    [Trait("Entidade", "Cliente pessoa física")]
    [InlineData("039.608.440-00")]
    [InlineData("82515557015")]
    public void QuandoCPFCorreto_DadoUmCliente_EntaoDeveEstarValido(string cpf)
    {
        //Arrange
        var cliente = new ClientePessoaFisica(
            "Pedrinho",
            "Silva",
            "pedrinho.silva@live.com",
            new DateTime(1990, 7, 23),
            2.99m,
            cpf);

        //Act
        var result = cliente.IsValid();

        //Assert
        result.Should().BeTrue();
        cliente.ValidationResult.Errors.Should().BeEmpty();
    }
}
