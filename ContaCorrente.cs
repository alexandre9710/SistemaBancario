using System;
namespace BancoX
{
    /// <summary>
    /// Classe que representa uma Conta Corrente com taxa de saque
    /// </summary>
    public class ContaCorrente : ContaBancaria
    {
        private decimal _taxaSaque;
        // Propriedade com encapsulamento
        public decimal TaxaSaque
        {
            get { return _taxaSaque; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Taxa de saque não pode ser negativa.");
                _taxaSaque = value;
            }
        }
        // Construtor
        public ContaCorrente(string numeroConta, string titular, decimal saldoInicial = 0, decimal taxaSaque = 5.00m)
            : base(numeroConta, titular, saldoInicial)
        {
            TaxaSaque = taxaSaque;
        }
        // Implementação do método abstrato Sacar
        public override void Sacar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor do saque deve ser positivo.");
            decimal valorTotal = valor + TaxaSaque;
            // Estrutura condicional para validar saldo
            if (valorTotal > _saldo)
            {
                throw new InvalidOperationException(
                    $"Saldo insuficiente! Saldo disponível: R$ {_saldo:F2} | " +
                    $"Necessário: R$ {valorTotal:F2} (R$ {valor:F2} + taxa R$ {TaxaSaque:F2})"
                );
            }
            _saldo -= valorTotal;
            Console.WriteLine($"Saque de R$ {valor:F2} realizado com sucesso!");
            Console.WriteLine($"Taxa de saque: R$ {TaxaSaque:F2}");
            Console.WriteLine($"Total debitado: R$ {valorTotal:F2}");
        }
        // Sobrescrita do método ExibirDados
        public override void ExibirDados()
        {
            base.ExibirDados(); // Chama o método da classe pai
            Console.WriteLine($"Tipo de Conta: Conta Corrente");
            Console.WriteLine($"Taxa de Saque: R$ {TaxaSaque:F2}");
            Console.WriteLine("====================================\n");
        }
    }
}
