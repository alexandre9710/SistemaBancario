using System;

namespace BancoX
{
    /// <summary>
    /// Classe que representa uma Conta Empresarial com limite de empréstimo
    /// </summary>
    public class ContaEmpresarial : ContaBancaria
    {
        private decimal _limiteEmprestimo;
        private decimal _emprestimoUtilizado;

        // Propriedades com encapsulamento
        public decimal LimiteEmprestimo
        {
            get { return _limiteEmprestimo; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Limite de empréstimo não pode ser negativo.");
                _limiteEmprestimo = value;
            }
        }

        public decimal EmprestimoUtilizado
        {
            get { return _emprestimoUtilizado; }
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Empréstimo utilizado não pode ser negativo.");
                _emprestimoUtilizado = value;
            }
        }

        public decimal LimiteDisponivel
        {
            get { return _limiteEmprestimo - _emprestimoUtilizado; }
        }

        // Construtor
        public ContaEmpresarial(string numeroConta, string titular, decimal saldoInicial = 0, decimal limiteEmprestimo = 10000m)
            : base(numeroConta, titular, saldoInicial)
        {
            LimiteEmprestimo = limiteEmprestimo;
            EmprestimoUtilizado = 0;
        }

        // Implementação do método abstrato Sacar (pode usar limite de empréstimo)
        public override void Sacar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor do saque deve ser positivo.");

            decimal saldoTotal = _saldo + LimiteDisponivel;

            // Estrutura condicional para validar saldo + limite
            if (valor > saldoTotal)
            {
                throw new InvalidOperationException(
                    $"Saldo + limite insuficiente! Disponível: R$ {saldoTotal:F2} " +
                    $"(Saldo: R$ {_saldo:F2} + Limite: R$ {LimiteDisponivel:F2})"
                );
            }

            // Estrutura condicional: usa primeiro o saldo, depois o limite
            if (valor <= _saldo)
            {
                // Saque apenas do saldo
                _saldo -= valor;
                Console.WriteLine($"Saque de R$ {valor:F2} realizado do saldo.");
            }
            else
            {
                // Usa todo o saldo e parte do limite
                decimal valorDoLimite = valor - _saldo;
                _saldo = 0;
                _emprestimoUtilizado += valorDoLimite;

                Console.WriteLine($"Saque de R$ {valor:F2} realizado com sucesso!");
                Console.WriteLine($"Utilizado R$ {valorDoLimite:F2} do limite de empréstimo.");
            }
        }

        // Método específico para solicitar empréstimo
        public void SolicitarEmprestimo(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor do empréstimo deve ser positivo.");

            // Estrutura condicional para validar limite disponível
            if (valor > LimiteDisponivel)
            {
                throw new InvalidOperationException(
                    $"Limite insuficiente! Disponível: R$ {LimiteDisponivel:F2}"
                );
            }

            _emprestimoUtilizado += valor;
            _saldo += valor;

            Console.WriteLine($"Empréstimo de R$ {valor:F2} aprovado e creditado!");
            Console.WriteLine($"Limite restante: R$ {LimiteDisponivel:F2}");
        }

        // Método específico para pagar empréstimo
        public void PagarEmprestimo(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor do pagamento deve ser positivo.");

            // Estrutura condicional para validar saldo
            if (valor > _saldo)
            {
                throw new InvalidOperationException(
                    $"Saldo insuficiente para pagamento! Saldo: R$ {_saldo:F2}"
                );
            }

            // Estrutura condicional: não pode pagar mais que o devido
            if (valor > _emprestimoUtilizado)
            {
                Console.WriteLine($"Valor maior que o empréstimo devido (R$ {_emprestimoUtilizado:F2}).");
                Console.WriteLine($"Pagando apenas R$ {_emprestimoUtilizado:F2}.");
                valor = _emprestimoUtilizado;
            }

            _saldo -= valor;
            _emprestimoUtilizado -= valor;

            Console.WriteLine($"Pagamento de R$ {valor:F2} realizado!");
            Console.WriteLine($"Empréstimo restante: R$ {_emprestimoUtilizado:F2}");
            Console.WriteLine($"Limite disponível: R$ {LimiteDisponivel:F2}");
        }

        // Sobrescrita do método ExibirDados
        public override void ExibirDados()
        {
            base.ExibirDados(); // Chama o método da classe pai
            Console.WriteLine($"Tipo de Conta: Conta Empresarial");
            Console.WriteLine($"Limite de Empréstimo: R$ {LimiteEmprestimo:F2}");
            Console.WriteLine($"Empréstimo Utilizado: R$ {EmprestimoUtilizado:F2}");
            Console.WriteLine($"Limite Disponível: R$ {LimiteDisponivel:F2}");
            Console.WriteLine("====================================\n");
        }
    }
}
