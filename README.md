# FintechBank

## Visão geral

**FintechBank** é uma aplicação .NET 10 que concentra regras de negócio de um banco digital: cobrança de taxa de manutenção de conta, validação de saque com cheque especial, cálculo de parcelas de empréstimo com juros por score de crédito e aprovação de empréstimo pela margem consignável. As regras são validadas por testes unitários parametrizados com xUnit.

Atividade da disciplina **Garantia da Qualidade de Software** (Lista 25), desenvolvida em grupo.

## Estrutura

```
FintechBank.slnx
├── FintechBank.App/      # código de produção (ContaCorrenteService, EmprestimoService)
└── FintechBank.Tests/    # testes parametrizados (ContaCorrenteServiceTests, EmprestimoServiceTests)
```

## Requisitos técnicos

- [.NET SDK 10](https://dotnet.microsoft.com/download) (`net10.0`)
- xUnit para testes unitários
- Git

## Regras de negócio

### `ContaCorrenteService`

| Método | Regra |
|--------|-------|
| `CalcularTaxaManutencao(decimal saldo, bool clienteEspecial)` | Saldo ≥ R$ 5.000,00 **ou** cliente especial: `0.00` (isento). Saldo de R$ 1.000,00 a R$ 4.999,99: `12.50`. Saldo abaixo de R$ 1.000,00: `25.00` |
| `PodeRealizarSaque(decimal saldo, decimal limiteChequeEspecial, decimal valorSaque)` | `true` somente se `valorSaque` ≤ `saldo + limiteChequeEspecial` |

### `EmprestimoService`

| Método | Regra |
|--------|-------|
| `CalcularValorParcela(decimal valorTotal, int parcelas, int scoreCredito)` | Score ≥ 700: juros totais de 5% (`× 1.05`). Score de 500 a 699: 12% (`× 1.12`). Score < 500: 20% (`× 1.20`). Retorna o valor com juros dividido pelo número de parcelas |
| `AprovarEmprestimo(decimal rendaMensal, decimal valorParcela)` | Aprovado (`true`) somente se `valorParcela` ≤ 30% da `rendaMensal` |

## Passo a passo: clonar e compilar

```bash
git clone https://github.com/mmoreiralopes8-sketch/fintech-bank-xunit.git
cd fintech-bank-xunit
dotnet build
dotnet run --project FintechBank.App
```

## Como executar os testes unitários

Na raiz do repositório:

```bash
dotnet test
```

## Testes parametrizados (`[Theory]` e `[InlineData]`)

Em vez de escrever um teste para cada cenário, cada método de teste é marcado com `[Theory]` e recebe várias linhas `[InlineData(...)]`. Cada linha vira uma execução independente do mesmo teste, com os parâmetros indicados (entradas e resultado esperado).

### `ContaCorrenteServiceTests`

| Teste | Cenários |
|-------|----------|
| Taxa de manutenção | `(6000, false, 0)` saldo alto isento, `(500, true, 0)` cliente especial isento, `(2500, false, 12.50)` saldo intermediário, `(300, false, 25.00)` saldo baixo |
| Validação de saque | `(1000, 500, 1200, true)` dentro do limite total, `(1000, 500, 1600, false)` excede saldo + limite |

### `EmprestimoServiceTests`

| Teste | Cenários |
|-------|----------|
| Parcela com juros por score | `(10000, 10, 800, 1050)` 5%, `(10000, 10, 600, 1120)` 12%, `(10000, 10, 400, 1200)` 20% |
| Aprovação por margem consignável | `(5000, 1500, true)` parcela = 30% da renda, `(5000, 1600, false)` parcela > 30% |

São 4 métodos `[Theory]` que geram 11 execuções, cobrindo todas as faixas de cada regra.

## Licença

Distribuído sob a licença MIT. Veja o arquivo [LICENSE](LICENSE).
