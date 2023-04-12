using demo.cap.Enums;
using demo.cap.Models;

namespace demo.cap.tests.Basico;

public class AssertingRangesTests
{
    [Theory]
    [InlineData(2000)]
    [InlineData(3000)]
    [InlineData(5000)]
    [InlineData(15000)]
    public void Cliente_ValorPatrimonio_GrupoDeInvestimentoDevemRespeitarOsTargetsDePatrimonio(decimal valorpatrimonio)
    {
        //Arrange & Act
        var cliente = new ClientePessoaFisica(
            "Pedrinho",
            "Silva",
            "pedrinho.silva@live.com",
            new DateTime(1990, 7, 23),
            valorpatrimonio,
            "356.712.080-84");

        //Assert
        if (cliente.Grupo == GrupoInvestidor.Fusquinha)
            Assert.InRange(cliente.ValorPatrimonio, 1001, 2000);

        if (cliente.Grupo == GrupoInvestidor.Celta)
            Assert.InRange(cliente.ValorPatrimonio, 2001, 3000);

        if (cliente.Grupo == GrupoInvestidor.Civic)
            Assert.InRange(cliente.ValorPatrimonio, 3001, 4000);

        if (cliente.Grupo == GrupoInvestidor.Ferrari)
            Assert.InRange(cliente.ValorPatrimonio, 4001, 5000);

        if (cliente.Grupo == GrupoInvestidor.RollsRoyce)
            Assert.InRange(cliente.ValorPatrimonio, 5001, decimal.MaxValue);

        Assert.NotInRange(cliente.ValorPatrimonio, decimal.MinValue, 1000);
    }
}