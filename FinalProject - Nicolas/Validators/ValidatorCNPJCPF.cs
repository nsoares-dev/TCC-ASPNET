using System.ComponentModel.DataAnnotations;
using System.Drawing.Text;
using System.Text.RegularExpressions;

namespace FinalProject___Nicolas.Validators;

public class ValidatorCNPJCPF : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var documento = value as string;

        if (string.IsNullOrEmpty(documento))
            return new ValidationResult("O documento é obrigatório");

        documento = documento.Replace(".", "").Replace("-", "").Replace("/", "");

        if (documento.Length == 11)
        {
            if (!Regex.IsMatch(documento, @"^\d{11}$"))
                return new ValidationResult("CPF Inválido");

            return ValidarCPF(documento) ? ValidationResult.Success : new ValidationResult("CPF Inválido.");
        }
        else if (documento.Length == 14)
        {
            if (!Regex.IsMatch(documento, @"^\d{14}$"))
                return new ValidationResult("CNPJ inválido.");

            return ValidarCNPJ(documento) ? ValidationResult.Success : new ValidationResult("CNPJ inválido.");
        }
        return new ValidationResult("Documento deve ser CPF ou CNPJ Válido.");
    }
    private bool ValidarCPF(string cpf)
    {
        var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");

        if (cpf.Length != 11)
            return false;

        for (int j = 0; j < 10; j++)
            if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                return false;

        var tempCpf = cpf.Substring(0, 9);
        var soma = 0;

        for (var i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        var resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        var digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;

        for (var i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();

        return cpf.EndsWith(digito);
    }

    private bool ValidarCNPJ(string cnpj)
    {
        var multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        var multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        cnpj = cnpj.Trim();
        cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

        if (cnpj.Length != 14)
            return false;

        var tempCnpj = cnpj.Substring(0, 12);
        var soma = 0;

        for (var i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

        var resto = (soma % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        var digito = resto.ToString();
        tempCnpj = tempCnpj + digito;
        soma = 0;

        for (var i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

        resto = (soma % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = digito + resto.ToString();

        return cnpj.EndsWith(digito);
    }
}