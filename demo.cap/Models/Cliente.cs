using demo.cap.Enums;
using demo.cap.ValuesObjects;
using FluentValidation;
using FluentValidation.Results;

namespace demo.cap.Models;
public abstract class Cliente
{
    public ValidationResult ValidationResult { get; protected set; }
    public Guid Id { get; private set; }
    public string Nome { get; protected set; }
    public string Apelido { get; protected set; }
    public decimal ValorPatrimonio { get; protected set; }
    public GrupoInvestidor? Grupo { get; private set; }
    public IList<string> Habilidades { get; private set; }
    public DateTime DataNascimento { get; protected set; }
    public DateTime DataCadastro { get; private set; }
    public string Email { get; protected set; }
    public bool Ativo { get; protected set; }

    protected Cliente()
    {
        Id = Guid.NewGuid();
        Ativo = false;
        DataCadastro = DateTime.Now;
    }

    private static readonly Dictionary<decimal, GrupoInvestidor> LimitesInvestidores = new()
    {
        { 2000, GrupoInvestidor.Fusquinha },
        { 3000, GrupoInvestidor.Celta },
        { 4000, GrupoInvestidor.Civic },
        { 5000, GrupoInvestidor.Ferrari },
        { decimal.MaxValue, GrupoInvestidor.RollsRoyce }
    };

    protected void DefinirGrupoInvestidor()
    {
        foreach (var limite in LimitesInvestidores.Keys)
        {
            if (ValorPatrimonio > 1000 && ValorPatrimonio <= limite)
            {
                Grupo = LimitesInvestidores[limite];                
                break;
            }
        }
        DefinirHabilidades();
    }
    private void DefinirHabilidades()
    {
        Habilidades = new List<string>() { "C3" };

        switch (Grupo)
        {
            case GrupoInvestidor.Fusquinha:
                Habilidades.Add("C2");
                break;
            case GrupoInvestidor.Celta:
                Habilidades.Add("C2");
                Habilidades.Add("C1");
                break;
            case GrupoInvestidor.Civic:
                Habilidades.Add("C2");
                Habilidades.Add("C1");
                Habilidades.Add("B1");
                break;
            case GrupoInvestidor.Ferrari:
                Habilidades.Add("C2");
                Habilidades.Add("C1");
                Habilidades.Add("B1");
                Habilidades.Add("B2");
                break;
            case GrupoInvestidor.RollsRoyce:
                Habilidades.Add("C2");
                Habilidades.Add("C1");
                Habilidades.Add("B1");
                Habilidades.Add("B2");
                Habilidades.Add("A1");
                Habilidades.Add("A2");
                break;
        }
    }
    public bool EhEspecial() => DataCadastro < DateTime.Now.AddYears(-3) && Ativo;
    public void Inativar() => Ativo = false;
    public void Ativar() => Ativo = true;

    protected virtual bool Validate() => throw new NotImplementedException();

    public class ClienteValidation<T> : AbstractValidator<T> where T : Cliente
    {
        public readonly string NomeEmptyMsg = "Informe o nome";
        public readonly string EmailEmptyMsg = "Informe um e-mail";
        public readonly string DataNascimentoEmptyMsg = "Informe sua data de nascimento";

        public ClienteValidation()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage(NomeEmptyMsg);

            RuleFor(x => x.DataNascimento)
                .NotNull().WithMessage(DataNascimentoEmptyMsg);

            RuleFor(p => p.Email)
                .Custom((email, context) =>
                {
                    if (string.IsNullOrEmpty(email)) context.AddFailure(EmailEmptyMsg);
                    else
                        try { _ = new Email(email); } catch (ArgumentNullException ex) { context.AddFailure(ex.Message); }
                });
        }
    }
}