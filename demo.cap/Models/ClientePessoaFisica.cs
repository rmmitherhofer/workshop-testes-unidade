using demo.cap.ValuesObjects;
using FluentValidation;
using FluentValidation.Results;

namespace demo.cap.Models;

public class ClientePessoaFisica : Cliente
{
    public string Cpf { get; private set; }
    public string Sobrenome { get; private set; }
    public ClientePessoaFisica(string nome, string sobrenome, string email, DateTime dataNascimento, decimal valorPatrimonio, string cpf)
    {
        Cpf = cpf;
        Nome = nome;
        Sobrenome = sobrenome;
        Email = email;
        ValorPatrimonio = valorPatrimonio;
        DataNascimento = dataNascimento;

        if (ValorPatrimonio < 0) throw new ArgumentException("Valor de patrimonio negativo.");

        DefinirGrupoInvestidor();
    }

    public string NomeCompleto() => $"{Nome} {Sobrenome}";

    public void Atualizar(string email, decimal valorPatrimonio)
    {
        Email = email;
        ValorPatrimonio = valorPatrimonio;

        DefinirGrupoInvestidor();
    }

    public bool IsValid() => Validate();
    public bool IsInvalid() => !Validate();
    protected override bool Validate()
    {
        ValidationResult = new ClientePessoaFisicaValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public class ClientePessoaFisicaValidation : ClienteValidation<ClientePessoaFisica>
    {
        public const string SobrenomeEmptyMsg = "Informe o sobrenome";
        public const string CpfEmptyMsg = "Informe o CPF";

        public ClientePessoaFisicaValidation()
        {
            RuleFor(p => p.Sobrenome)
                .NotEmpty().WithMessage(SobrenomeEmptyMsg);

            RuleFor(e => e.Cpf)
                .Custom((cpf, context) =>
                {
                    if (string.IsNullOrEmpty(cpf))
                    {
                        context.AddFailure(CpfEmptyMsg);
                    }
                    else
                    {
                        try
                        {
                            _ = new Cpf(cpf).Numero;
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
