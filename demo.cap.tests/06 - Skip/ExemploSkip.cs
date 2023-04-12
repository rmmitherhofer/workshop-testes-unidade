namespace demo.cap.tests.Basico.Skip;
public class ExemploSkip
{
    [Fact(DisplayName = "Novo Cliente 2.0", Skip = "Teste quebrando, revisar depois")]
    [Trait("Skip", "Teste Incompleto")]
    public void Teste_NaoEstaPassando_VersaoIncompleta()
    {
        //Arrange

        //Act

        //Assert
    }
}
