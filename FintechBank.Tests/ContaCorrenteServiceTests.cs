using FintechBank.App;

namespace FintechBank.Tests;

public class ContaCorrenteServiceTests
{
    private readonly ContaCorrenteService _service = new();

    [Theory]
    [InlineData(6000.00, false, 0.00)]  // Saldo alto -> Isento
    [InlineData(500.00, true, 0.00)]    // Cliente Especial -> Isento
    [InlineData(2500.00, false, 12.50)] // Saldo intermediario -> R$ 12,50
    [InlineData(300.00, false, 25.00)]  // Saldo baixo -> R$ 25,00
    public void CalcularTaxaManutencao_DeveRetornarTaxaEsperada(decimal saldo, bool clienteEspecial, decimal esperado)
    {
        var resultado = _service.CalcularTaxaManutencao(saldo, clienteEspecial);

        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(1000.00, 500.00, 1200.00, true)]  // Dentro do limite total
    [InlineData(1000.00, 500.00, 1600.00, false)] // Excede saldo + limite
    public void PodeRealizarSaque_DeveValidarSaldoMaisLimite(decimal saldo, decimal limite, decimal valorSaque, bool esperado)
    {
        var resultado = _service.PodeRealizarSaque(saldo, limite, valorSaque);

        Assert.Equal(esperado, resultado);
    }
}
