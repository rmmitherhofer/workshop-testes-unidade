using demo.cap.ValuesObjects;
using FluentValidation;
using FluentValidation.Results;

namespace demo.cap.Models;

public class ClientePessoaJuridica : Cliente
{
    public string Cnpj { get; private set; }    

    public ClientePessoaJuridica(string nome, string email, DateTime dataNascimento, decimal valorPatrimonio, string cnpj)
    {
        Nome = nome;
        Cnpj = cnpj;
        Email = email;
        DataNascimento = dataNascimento;
        ValorPatrimonio = valorPatrimonio;

        DefinirGrupoInvestidor();
    }

    public void Atualizar(string email, decimal valorPatrimonio)
    {
        Email = email;
        ValorPatrimonio = valorPatrimonio;
    }

    public bool IsValid() => Validate();
    public bool IsInvalid() => !Validate();
    protected override bool Validate()
    {
        ValidationResult = new ClientePessoaJuridicaValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public class ClientePessoaJuridicaValidation : ClienteValidation<ClientePessoaJuridica>
    {
        public const string SobrenomeEmptyMsg = "Informe o sobrenome";
        public const string CnpjEmptyMsg = "Informe o CNPJ";
        public const string ValorAbaixoDoPermitidoMsg = "Valor patrimonial deve ser superior a R$1.000 (mil reais)";

        public ClientePessoaJuridicaValidation()
        {
            RuleFor(e => e.ValorPatrimonio)
                .GreaterThan(1000).WithMessage(ValorAbaixoDoPermitidoMsg);

            RuleFor(e => e.Cnpj)
                .Custom((cnpj, context) =>
                {
                    if (string.IsNullOrEmpty(cnpj))
                    {
                        context.AddFailure(CnpjEmptyMsg);
                    }
                    else
                    {
                        try
                        {
                            _ = new Cnpj(cnpj).Numero;
                        }
                        catch (ArgumentException ex)
                        {
                            context.AddFailure(ex.Message);
                        }
                    }
                });
        }
    }
}
