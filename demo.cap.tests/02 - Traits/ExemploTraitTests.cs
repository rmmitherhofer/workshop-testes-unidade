using demo.cap.Models;

namespace demo.cap.tests.Basico.Traits;
public class ExemploTraitTests
{
    [Fact]
    public void QuandoNovo_DadoUmCliente_EntaoDeveEstarValido()
    {
        //Arrange

        //Act

        //Assert
    }

    [Fact(DisplayName = "Novo cliente pessoa física, deve estar válido")]
    [Trait("Entidade", "Cliente pessoa física")]
    public void DadoUmCliente_QuandoNovo_EntaoDeveEstarValido()
    {
        //Arrange

        //Act

        //Assert
    }
}
