namespace FintechBank.App;

public class EmprestimoService
{
    public decimal CalcularValorParcela(decimal valorTotal, int parcelas, int scoreCredito)
    {
        decimal fatorJuros;

        if (scoreCredito >= 700)
        {
            fatorJuros = 1.05m;
        }
        else if (scoreCredito >= 500)
        {
            fatorJuros = 1.12m;
        }
        else
        {
            fatorJuros = 1.20m;
        }

        decimal valorComJuros = valorTotal * fatorJuros;
        return valorComJuros / parcelas;
    }

    public bool AprovarEmprestimo(decimal rendaMensal, decimal valorParcela)
    {
        return valorParcela <= rendaMensal * 0.30m;
    }
}
