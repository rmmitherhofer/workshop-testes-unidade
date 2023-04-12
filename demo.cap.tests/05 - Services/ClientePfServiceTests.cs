using demo.cap.Interfaces.HttpServices;
using demo.cap.Interfaces.Repositories;
using demo.cap.Models;
using demo.cap.Services;
using demo.cap.tests.DadosHumanos;
using demo.cap.tests.Services.Fixture;
using FluentAssertions;
using Moq;
using Xunit.Abstractions;

namespace demo.cap.tests.Services;

[Collection(nameof(ClientePfServiceCollection))]
public class ClientePfServiceTests : Tests
{
    private readonly ClientePfServiceTestsFixture _fixture;

    public ClientePfServiceTests(ITestOutputHelper outputHelper, ClientePfServiceTestsFixture fixture) : base(outputHelper) => _fixture = fixture;

    [Fact(DisplayName = "Incluir novo cliente pessoa fisica, deve executar com sucesso")]
    [Trait("Service", "Cliente PF")]
    public async Task QuandoAdicionarNovoCliente_DadoUmServicoDeClientePFValido_EntaoDeveExecutarComSucesso()
    {
        // Arrange
        var httpService = new Mock<IClienteHttpService>();
        var repository = new Mock<IClientePfRepository>();

        var service = new Mock<ClientePfService>(httpService.Object, repository.Object);

        httpService.Setup(r => r.ObterPF(It.IsAny<string>()))
            .Returns(Task.FromResult(It.IsAny<ClientePessoaFisica>()));

        repository.Setup(r => r.Adicionar(It.IsAny<ClientePessoaFisica>()))
            .Returns(Task.FromResult(true));

        var cliente = BogusClientePf.GerarClienteValido();

        // Act
        await service.Object.Adicionar(cliente);

        // Assert
        httpService.Verify(x => x.ObterPF(It.IsAny<string>()), Times.Once);
        repository.Verify(x => x.Adicionar(It.IsAny<ClientePessoaFisica>()), Times.Once);
    }

    [Fact(DisplayName = "Incluir novo cliente pessoa fisica, deve executar com sucesso")]
    [Trait("Service", "Cliente PF")]
    public async Task DadoUmServicoDeClientePF_QuandoAdicionarNovoCliente_EntaoDeveExecutarComSucesso()
    {
        // Arrange
        _fixture.SetupServiceAutoMocker();
        _fixture.SetupAdicionarComSucesso();
        var cliente = BogusClientePf.GerarClienteValido();

        // Act
        await _fixture.Service.Adicionar(cliente);

        // Assert
        _fixture.Mocker.GetMock<IClienteHttpService>().Verify(x => x.ObterPF(It.IsAny<string>()), Times.Once);
        _fixture.Mocker.GetMock<IClientePfRepository>().Verify(x => x.Adicionar(It.IsAny<ClientePessoaFisica>()), Times.Once);
    }

    [Fact(DisplayName = "Incluir cliente pessoa fisica inválido, deve retornar validações")]
    [Trait("Service", "Cliente PF")]
    public async Task DadoUmServicoDeClientePF_QuandoAdicionarClienteInvalido_EntaoDeveRetornarValidacoes()
    {
        // Arrange
        _fixture.SetupServiceAutoMocker();

        var cliente = BogusClientePf.GerarClienteInvalido();

        // Act
        await _fixture.Service.Adicionar(cliente);

        // Assert
        _fixture.Mocker.GetMock<IClienteHttpService>().Verify(x => x.ObterPF(It.IsAny<string>()), Times.Never);
        _fixture.Mocker.GetMock<IClientePfRepository>().Verify(x => x.Adicionar(It.IsAny<ClientePessoaFisica>()), Times.Never);
    }

    [Fact(DisplayName = "Incluir cliente pessoa fisica existente, deve retornar exceção de cliente ja cadastrado")]
    [Trait("Service", "Cliente PF")]
    public async Task DadoUmServicoDeClientePF_QuandoAdicionarClienteExistente_EntaoDeveRetornarExcecaoClienteJaCadastrado()
    {
        // Arrange
        _fixture.SetupServiceAutoMocker();
        _fixture.SetupAdicionarExistente();
        var cliente = BogusClientePf.GerarClienteValido();

        // Act
        var exception = await Assert.ThrowsAsync< ArgumentException >(async () =>  await _fixture.Service.Adicionar(cliente));

        // Assert
        exception.Should().NotBeNull();
        exception.Message.Should().NotBeNullOrEmpty();
        exception.Message.Should().Be("Cliente já cadastrado.");
        _fixture.Mocker.GetMock<IClienteHttpService>().Verify(x => x.ObterPF(It.IsAny<string>()), Times.Once);
        _fixture.Mocker.GetMock<IClientePfRepository>().Verify(x => x.Adicionar(It.IsAny<ClientePessoaFisica>()), Times.Never);
    }

    [Fact(DisplayName = "Incluir cliente pessoa fisica, deve retornar exceção de serviço HTTP indisponivel")]
    [Trait("Service", "Cliente PF")]
    public async Task DadoUmServicoDeClientePF_QuandoAdicionarCliente_EntaoDeveRetornarExcecaoServicoHttpIndisponivel()
    {
        // Arrange
        _fixture.SetupServiceAutoMocker();
        _fixture.SetupAdicionarServicoHttpIndisponivel();
        var cliente = BogusClientePf.GerarClienteValido();

        // Act
        var exception = await Assert.ThrowsAsync<HttpRequestException>(async () => await _fixture.Service.Adicionar(cliente));

        // Assert
        exception.Should().NotBeNull();
        exception.Message.Should().NotBeNullOrEmpty();
        exception.Message.Should().Be("Test - Falha no request Http.");
        _fixture.Mocker.GetMock<IClienteHttpService>().Verify(x => x.ObterPF(It.IsAny<string>()), Times.Once);
        _fixture.Mocker.GetMock<IClientePfRepository>().Verify(x => x.Adicionar(It.IsAny<ClientePessoaFisica>()), Times.Never);
    }

    [Fact(DisplayName = "Incluir novo cliente pessoa fisica, deve retornar exceção devido commit false")]
    [Trait("Service", "Cliente PF")]
    public async Task DadoUmServicoDeClientePF_QuandoAdicionarCliente_EntaoDeveRetornarExcecaoCommitFalse()
    {
        // Arrange
        _fixture.SetupServiceAutoMocker();
        _fixture.SetupAdicionarFalhaCommit();
        var cliente = BogusClientePf.GerarClienteValido();

        // Act
        var exception = await Assert.ThrowsAsync<ApplicationException>(async () => await _fixture.Service.Adicionar(cliente));

        // Assert
        exception.Should().NotBeNull();
        exception.Message.Should().NotBeNullOrEmpty();
        exception.Message.Should().Be($"Falha ao adicionar cliente {cliente.NomeCompleto()}.");
        _fixture.Mocker.GetMock<IClienteHttpService>().Verify(x => x.ObterPF(It.IsAny<string>()), Times.Once);
        _fixture.Mocker.GetMock<IClientePfRepository>().Verify(x => x.Adicionar(It.IsAny<ClientePessoaFisica>()), Times.Once);
    }

    [Fact(DisplayName = "Incluir novo cliente pessoa fisica, deve retornar exceção devido falha no entity")]
    [Trait("Service", "Cliente PF")]
    public async Task DadoUmServicoDeClientePF_QuandoAdicionarCliente_EntaoDeveRetornarExcecaoFalhaEntity()
    {
        // Arrange
        _fixture.SetupServiceAutoMocker();
        _fixture.SetupAdicionarFalhaEntity();
        var cliente = BogusClientePf.GerarClienteValido();

        // Act
        var exception = await Assert.ThrowsAsync<Exception>(async () => await _fixture.Service.Adicionar(cliente));

        // Assert
        exception.Should().NotBeNull();
        exception.Message.Should().NotBeNullOrEmpty();
        exception.Message.Should().Be("Test - Falha no repositório.");
        _fixture.Mocker.GetMock<IClienteHttpService>().Verify(x => x.ObterPF(It.IsAny<string>()), Times.Once);
        _fixture.Mocker.GetMock<IClientePfRepository>().Verify(x => x.Adicionar(It.IsAny<ClientePessoaFisica>()), Times.Once);

        Print(exception);
    }
}
