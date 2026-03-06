using System;

namespace BancoX
{
    /// <summary>
    /// Classe que representa uma Conta Poupança sem taxa e com rendimento
    /// </summary>
    public class ContaPoupanca : ContaBancaria
    {
        private decimal _taxaRendimento;

        // Propriedade com encapsulamento
        public decimal TaxaRendimento
        {
            get { return _taxaRendimento; }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("Taxa de rendimento deve estar entre 0 e 100%.");
                _taxaRendimento = value;
            }
        }

        // Construtor
        public ContaPoupanca(string numeroConta, string titular, string cpf, string senha, decimal saldoInicial = 0, decimal taxaRendimento = 0.5m)
            : base(numeroConta, titular, cpf, senha, saldoInicial)
        {
            TaxaRendimento = taxaRendimento;
        }

        // Implementação do método abstrato Sacar (sem taxa)
        public override void Sacar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor do saque deve ser positivo.");

            // Estrutura condicional para validar saldo
            if (valor > _saldo)
            {
                throw new InvalidOperationException(
                    $"Saldo insuficiente! Saldo disponível: R$ {_saldo:F2}"
                );
            }

            _saldo -= valor;
            Console.WriteLine($"Saque de R$ {valor:F2} realizado com sucesso!");
            Console.WriteLine("Sem cobrança de taxa.");
        }

        // Método específico para aplicar rendimento
        public void AplicarRendimento()
        {
            if (_saldo <= 0)
            {
                Console.WriteLine("Não há saldo para aplicar rendimento.");
                return;
            }
            decimal rendimento = _saldo * (TaxaRendimento / 100);
            _saldo += rendimento;
            Console.WriteLine($"Rendimento de {TaxaRendimento}% aplicado!");
            Console.WriteLine($"Valor do rendimento: R$ {rendimento:F2}");
            Console.WriteLine($"Novo saldo: R$ {_saldo:F2}");
        }

        // Sobrescrita do método ExibirDados
        public override void ExibirDados()
        {
            base.ExibirDados(); // Chama o método da classe pai
            Console.WriteLine($"Tipo de Conta: Conta Poupança");
            Console.WriteLine($"Taxa de Rendimento: {TaxaRendimento}% ao mês");
            Console.WriteLine("====================================\n");
        }
    }
}

