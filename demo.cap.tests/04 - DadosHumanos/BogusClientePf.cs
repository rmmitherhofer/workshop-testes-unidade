using Bogus;
using Bogus.Extensions.Brazil;
using demo.cap.Models;

namespace demo.cap.tests.DadosHumanos;
public static class BogusClientePf
{
    public static IEnumerable<ClientePessoaFisica> GerarClientesValidos(int quant) => GerarCliente(quant);

    public static ClientePessoaFisica GerarClienteValido() => GerarCliente(quant: 1).FirstOrDefault();

    private static IEnumerable<ClientePessoaFisica> GerarCliente(int quant)
    {
        if (quant == 0) return new List<ClientePessoaFisica>();

        var clientes =  new Faker<ClientePessoaFisica>("pt_BR")
            .CustomInstantiator(c => new ClientePessoaFisica(
                nome: c.Person.FirstName,
                sobrenome: c.Person.LastName,
                email: c.Person.Email,
                dataNascimento: c.Person.DateOfBirth,
                valorPatrimonio: c.Finance.Amount(),
                cpf: c.Person.Cpf()
            ))
            .Generate(quant);

        clientes.ForEach(x => x.Ativar());

        return clientes;
    }

    public static IEnumerable<ClientePessoaFisica> GerarClientesInvalidos(int quant) => GerarClienteInvalido(quant);

    public static ClientePessoaFisica GerarClienteInvalido() => GerarClienteInvalido(quant: 1).FirstOrDefault();

    private static IEnumerable<ClientePessoaFisica> GerarClienteInvalido(int quant)
    {
        if (quant == 0) return new List<ClientePessoaFisica>();

        return new Faker<ClientePessoaFisica>("pt_BR")
            .CustomInstantiator(_ => new ClientePessoaFisica(
                nome: null,
                sobrenome: null,
                email: null,
                dataNascimento: DateTime.MinValue,
                valorPatrimonio: 0,
                cpf: null
            ))
            .Generate(quant);
    }
}
