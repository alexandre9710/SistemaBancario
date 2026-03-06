using System;
using System.Text.RegularExpressions;
namespace BancoX
{
    /// <summary>
    /// Classe abstrata que representa uma conta bancária genérica
    /// </summary>
    public abstract class ContaBancaria
    {
        // Atributos privados com encapsulamento
        private string _numeroConta;
        private string _titular;
        protected decimal _saldo; // Protected para permitir acesso nas classes filhas
        // Propriedades com encapsulamento
        public string NumeroConta
        {
            get { return _numeroConta; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Número da conta não pode ser vazio.");

                // Validação do formato: 5 dígitos, hífen, 1 dígito
                if (!Regex.IsMatch(value, @"^\d{5}-\d{1}$"))
                    throw new ArgumentException("Número da conta inválido. Deve seguir o padrão XXXXX-X (5 dígitos, hífen, 1 dígito).");

                _numeroConta = value;
            }
        }
        public string Titular
        {
            get { return _titular; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nome do titular não pode ser vazio.");
                _titular = value;
            }
        }
        public decimal Saldo
        {
            get { return _saldo; }
            protected set { _saldo = value; } // Set protegido
        }
        // Construtor
        public ContaBancaria(string numeroConta, string titular, decimal saldoInicial = 0)
        {
            NumeroConta = numeroConta;
            Titular = titular;
            if (saldoInicial < 0)
                throw new ArgumentException("Saldo inicial não pode ser negativo.");
            _saldo = saldoInicial;
        }
        // Método concreto para depósito
        public virtual void Depositar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor do depósito deve ser positivo.");
            _saldo += valor;
            Console.WriteLine($"Depósito de R$ {valor:F2} realizado com sucesso!");
        }
        // Método abstrato para saque (implementado nas classes filhas)
        public abstract void Sacar(decimal valor);
        // Método virtual para exibir dados (pode ser sobrescrito)
        public virtual void ExibirDados()
        {
            Console.WriteLine("\n========== DADOS DA CONTA ==========");
            Console.WriteLine($"Número da Conta: {NumeroConta}");
            Console.WriteLine($"Titular: {Titular}");
            Console.WriteLine($"Saldo Atual: R$ {Saldo:F2}");
        }
    }
}
