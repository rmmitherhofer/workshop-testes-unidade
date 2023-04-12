using demo.cap.Interfaces.HttpServices;
using demo.cap.Interfaces.Repositories;
using demo.cap.Models;
using demo.cap.Services;
using demo.cap.tests.DadosHumanos;
using Moq;

namespace demo.cap.tests.Services.Fixture;

[CollectionDefinition(nameof(ClientePfServiceCollection))]
public class ClientePfServiceCollection : ICollectionFixture<ClientePfServiceTestsFixture> { }
public class ClientePfServiceTestsFixture : ServiceTestsFixure
{
    public ClientePfService Service { get; private set; }

    public void SetupServiceAutoMocker()
    {
        Mocker = new();
        Service = Mocker.CreateInstance<ClientePfService>();
    }

    public void SetupServiceMoq()
    {
        var httpService = new Mock<IClienteHttpService>();
        var repository = new Mock<IClientePfRepository>();

        Service = new ClientePfService(httpService.Object, repository.Object);
    }

    public void SetupAdicionarComSucesso()
    {
        SetupHttpServiceObterPFInexistente();

        SetupRepositoryAdicionarValido();
    }

    public void SetupAdicionarExistente() => SetupHttpServiceObterPFValido();

    public void SetupAdicionarServicoHttpIndisponivel() => SetupHttpServiceObterPFExcecao();

    public void SetupAdicionarFalhaCommit()
    {
        SetupHttpServiceObterPFInexistente();

        SetupRepositoryAdicionarInvalido();
    }

    public void SetupAdicionarFalhaEntity()
    {
        SetupHttpServiceObterPFInexistente();

        SetupRepositoryAdicionarExcecao();
    }

    #region HttpService
    private void SetupHttpServiceObterPFValido()
        => Mocker.GetMock<IClienteHttpService>()
            .Setup(r => r.ObterPF(It.IsAny<string>()))
            .Returns(Task.FromResult(BogusClientePf.GerarClienteValido()));

    private void SetupHttpServiceObterPFInexistente()
        => Mocker.GetMock<IClienteHttpService>()
            .Setup(r => r.ObterPF(It.IsAny<string>()))
            .Returns(Task.FromResult(It.IsAny<ClientePessoaFisica>()));

    private void SetupHttpServiceObterPFExcecao()
        => Mocker.GetMock<IClienteHttpService>()
            .Setup(r => r.ObterPF(It.IsAny<string>()))
            .ThrowsAsync(new HttpRequestException("Test - Falha no request Http."));
    #endregion

    #region Repository
    private void SetupRepositoryAdicionarValido()
        => Mocker.GetMock<IClientePfRepository>()
            .Setup(r => r.Adicionar(It.IsAny<ClientePessoaFisica>()))
            .Returns(Task.FromResult(true));

    private void SetupRepositoryAdicionarInvalido()
        => Mocker.GetMock<IClientePfRepository>()
            .Setup(r => r.Adicionar(It.IsAny<ClientePessoaFisica>()))
            .Returns(Task.FromResult(false));

    private void SetupRepositoryAdicionarExcecao()
        => Mocker.GetMock<IClientePfRepository>()
            .Setup(r => r.Adicionar(It.IsAny<ClientePessoaFisica>()))
            .ThrowsAsync(new Exception("Test - Falha no repositório."));
    #endregion
}
