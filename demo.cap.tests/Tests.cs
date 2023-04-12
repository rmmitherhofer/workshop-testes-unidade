using System.ComponentModel.DataAnnotations;
using Xunit.Abstractions;

namespace demo.cap.tests;
public abstract class Tests
{
    public readonly ITestOutputHelper _outputHelper;
    protected Tests(ITestOutputHelper outputHelper) => _outputHelper = outputHelper;

    public void Print(FluentValidation.Results.ValidationResult validationResult)
    {
        if (validationResult.Errors.Count == 0)
        {
            _outputHelper.WriteLine("Não foram encontradas notificações nesta validação");
            return;
        }

        _outputHelper.WriteLine($"Foram encontradas {validationResult.Errors.Count} notificações nesta validação: " + Environment.NewLine);

        string detalhe = string.Empty;
        foreach (var erro in validationResult.Errors)
            detalhe += erro.ErrorMessage + Environment.NewLine;

        _outputHelper.WriteLine("são elas: " + Environment.NewLine + detalhe);
    }
    public void Print(IEnumerable<ValidationResult> validationResult)
    {
        if (!validationResult.Any())
        {
            _outputHelper.WriteLine("Não foram encontradas notificações nesta validação");
            return;
        }

        _outputHelper.WriteLine($"Foram encontradas {validationResult.Count()} notificações nesta validação: " + Environment.NewLine);

        string detalhe = string.Empty;
        foreach (var erro in validationResult)
            detalhe += erro.ErrorMessage + Environment.NewLine;

        _outputHelper.WriteLine("são elas: " + Environment.NewLine + detalhe);
    }
    public void Print(string messageError)
    {
        if (string.IsNullOrEmpty(messageError))
        {
            _outputHelper.WriteLine("Não foram encontradas notificações nesta validação");
            return;
        }

        _outputHelper.WriteLine("Foram encontradas 1 notificações nesta validação: " + Environment.NewLine);

        _outputHelper.WriteLine("são elas: " + Environment.NewLine + messageError);
    }

    public void Print<TException>(TException exception) where TException : Exception
    {
        if (exception is null)
        {
            _outputHelper.WriteLine("Não foram encontradas exceções nesta validação");
            return;
        }

        _outputHelper.WriteLine("Foram encontradas 1 exceção nesta validação: " + Environment.NewLine);

        _outputHelper.WriteLine("são elas: " + Environment.NewLine + $"{typeof(TException).Name} - {exception.Message}");
    }
}
