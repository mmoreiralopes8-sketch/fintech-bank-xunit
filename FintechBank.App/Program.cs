using FintechBank.App;

var conta = new ContaCorrenteService();
var emprestimo = new EmprestimoService();

Console.WriteLine($"Taxa de manutencao (saldo R$ 2.500, nao especial): R$ {conta.CalcularTaxaManutencao(2500.00m, false)}");
Console.WriteLine($"Pode sacar R$ 1.200 (saldo 1.000 + limite 500): {conta.PodeRealizarSaque(1000.00m, 500.00m, 1200.00m)}");

var parcela = emprestimo.CalcularValorParcela(10000.00m, 10, 800);
Console.WriteLine($"Parcela (R$ 10.000 em 10x, score 800): R$ {parcela}");
Console.WriteLine($"Aprovado (renda R$ 5.000)? {emprestimo.AprovarEmprestimo(5000.00m, parcela)}");
