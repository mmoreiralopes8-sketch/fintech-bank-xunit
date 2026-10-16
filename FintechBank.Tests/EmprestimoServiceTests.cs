using FintechBank.App;

namespace FintechBank.Tests;

public class EmprestimoServiceTests
{
    private readonly EmprestimoService _service = new();

    [Theory]
    [InlineData(10000.00, 10, 800, 1050.00)] // Score alto: 5% de juros -> 10.500 / 10
    [InlineData(10000.00, 10, 600, 1120.00)] // Score medio: 12% de juros -> 11.200 / 10
    [InlineData(10000.00, 10, 400, 1200.00)] // Score baixo: 20% de juros -> 12.000 / 10
    public void CalcularValorParcela_DeveAplicarJurosPorScore(decimal valorTotal, int parcelas, int score, decimal esperado)
    {
        var resultado = _service.CalcularValorParcela(valorTotal, parcelas, score);

        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(5000.00, 1500.00, true)]  // Parcela = 30% da renda -> Aprovado
    [InlineData(5000.00, 1600.00, false)] // Parcela > 30% da renda -> Recusado
    public void AprovarEmprestimo_DeveRespeitarMargemConsignavel(decimal rendaMensal, decimal valorParcela, bool esperado)
    {
        var resultado = _service.AprovarEmprestimo(rendaMensal, valorParcela);

        Assert.Equal(esperado, resultado);
    }
}
