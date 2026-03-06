using BancoX;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SistemaBancario
{
    class Program
    {
        // Lista para armazenar as contas (estrutura de dados)
        private static List<ContaBancaria> contas = new List<ContaBancaria>();
        private static Random random = new Random();

        static void Main(string[] args)
        {
            

            // Estrutura de repetição principal do sistema
            bool continuar = true;
            while (continuar)
            {
                try
                {
                    continuar = ExibirMenuPrincipal();
                }
                catch (Exception ex)
                {
                    // Tratamento de exceções genéricas
                    Console.WriteLine($"\n❌ Erro inesperado: {ex.Message}");
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }

            Console.WriteLine("\n👋 Obrigado por usar o Sistema Bancário!");
        }

        // Menu principal com estrutura condicional
        static bool ExibirMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║   Banco X                                  ║");
            Console.WriteLine("╚════════════════════════════════════════════╝\n");
            Console.WriteLine("========== MENU PRINCIPAL ==========");
            Console.WriteLine("1 - Criar Nova Conta");
            Console.WriteLine("2 - Listar Todas as Contas");
            Console.WriteLine("3 - Acessar Conta");
            Console.WriteLine("4 - Sair");
            Console.WriteLine("====================================");
            Console.Write("Escolha uma opção: ");

            try
            {
                int opcao = int.Parse(Console.ReadLine());

                // Estrutura condicional switch
                switch (opcao)
                {
                    case 1:
                        CriarNovaConta();
                        break;
                    case 2:
                        ListarContas();
                        break;
                    case 3:
                        AcessarConta();
                        break;
                    case 4:
                        return false; // Sair do sistema
                    default:
                        Console.WriteLine("\n⚠️ Opção inválida! Escolha entre 1 e 4.");
                        Console.ReadKey();
                        break;
                }
            }
            catch (FormatException)
            {
                // Tratamento de exceção para entrada inválida
                Console.WriteLine("\n❌ Erro: Digite apenas números!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Erro: {ex.Message}");
                Console.ReadKey();
            }

            return true;
        }

        // Método para criar nova conta com estrutura condicional
        static void CriarNovaConta()
        {
            Console.Clear();
            Console.WriteLine("========== CRIAR NOVA CONTA ==========");
            Console.WriteLine("Tipos de Conta:");
            Console.WriteLine("1 - Conta Corrente");
            Console.WriteLine("2 - Conta Poupança");
            Console.WriteLine("3 - Conta Empresarial");
            Console.WriteLine("======================================");
            Console.Write("Escolha o tipo de conta: ");

            try
            {
                int tipo = int.Parse(Console.ReadLine());

                Console.Write("Nome do titular: ");
                string titular = Console.ReadLine();

                string cpf;
                while (true)
                {
                    Console.Write("CPF (apenas 11 dígitos numéricos): ");
                    cpf = Console.ReadLine();
                    if (Regex.IsMatch(cpf, @"^\d{11}$"))
                    {
                        break;
                    }
                    Console.WriteLine("\n❌ CPF inválido. Deve conter 11 dígitos numéricos.");
                }

                string senha;
                while (true)
                {
                    Console.Write("Crie uma senha (mínimo 4 caracteres): ");
                    senha = Console.ReadLine();
                    if (senha.Length >= 4)
                    {
                        break;
                    }
                    Console.WriteLine("\n❌ A senha deve ter no mínimo 4 caracteres.");
                }

                string numeroConta = GerarNumeroContaUnico();
                Console.WriteLine($"Número da conta gerado: {numeroConta}");

                Console.Write("Saldo inicial (opcional, Enter para R$ 0,00): ");
                string saldoStr = Console.ReadLine();
                decimal saldoInicial = string.IsNullOrWhiteSpace(saldoStr) ? 0 : decimal.Parse(saldoStr);

                ContaBancaria novaConta = null;

                // Estrutura condicional switch para criar o tipo correto de conta
                switch (tipo)
                {
                    case 1:
                        Console.Write("Taxa de saque (opcional, Enter para R$ 5,00): ");
                        string taxaStr = Console.ReadLine();
                        decimal taxaSaque = string.IsNullOrWhiteSpace(taxaStr) ? 5.00m : decimal.Parse(taxaStr);
                        novaConta = new ContaCorrente(numeroConta, titular, cpf, senha, saldoInicial, taxaSaque);
                        break;

                    case 2:
                        Console.Write("Taxa de rendimento % (opcional, Enter para 0,5%): ");
                        string rendStr = Console.ReadLine();
                        decimal taxaRendimento = string.IsNullOrWhiteSpace(rendStr) ? 0.5m : decimal.Parse(rendStr);
                        novaConta = new ContaPoupanca(numeroConta, titular, cpf, senha, saldoInicial, taxaRendimento);
                        break;

                    case 3:
                        Console.Write("Limite de empréstimo (opcional, Enter para R$ 10.000,00): ");
                        string limiteStr = Console.ReadLine();
                        decimal limiteEmprestimo = string.IsNullOrWhiteSpace(limiteStr) ? 10000m : decimal.Parse(limiteStr);
                        novaConta = new ContaEmpresarial(numeroConta, titular, cpf, senha, saldoInicial, limiteEmprestimo);
                        break;

                    default:
                        Console.WriteLine("\n⚠️ Tipo de conta inválido!");
                        Console.ReadKey();
                        return;
                }

                contas.Add(novaConta);
                Console.WriteLine("\n✅ Conta criada com sucesso!");
                novaConta.ExibirDados();
            }
            catch (FormatException)
            {
                Console.WriteLine("\n❌ Erro: Formato de número inválido!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\n❌ Erro de validação: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Erro ao criar conta: {ex.Message}");
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private static string GerarNumeroContaUnico()
        {
            string novoNumero;
            do
            {
                string parteNumerica = random.Next(10000, 99999).ToString(); // 5 dígitos
                string digitoVerificador = random.Next(0, 9).ToString(); // 1 dígito
                novoNumero = $"{parteNumerica}-{digitoVerificador}";
            } while (contas.Exists(c => c.NumeroConta == novoNumero)); // Garante unicidade
            return novoNumero;
        }

        // Método para listar contas com estrutura de repetição
        static void ListarContas()
        {
            Console.Clear();
            Console.WriteLine("========== LISTA DE CONTAS ==========\n");

            // Estrutura condicional para verificar se há contas
            if (contas.Count == 0)
            {
                Console.WriteLine("Nenhuma conta cadastrada.");
            }
            else
            {
                // Estrutura de repetição foreach
                foreach (var conta in contas)
                {
                    conta.ExibirDados();
                }
            }

            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        // Método para acessar conta específica
        static void AcessarConta()
        {
            Console.Clear();
            Console.WriteLine("========== ACESSAR CONTA ==========");
            string numeroConta;
            while (true)
            {
                Console.Write("Digite o número da conta (XXXXX-X): ");
                numeroConta = Console.ReadLine();
                if (Regex.IsMatch(numeroConta, @"^\d{5}-\d{1}$"))
                {
                    break;
                }
                Console.WriteLine("\n❌ Formato de número de conta inválido. Deve seguir o padrão XXXXX-X (5 dígitos, hífen, 1 dígito).");
            }

            // Estrutura de repetição para buscar conta
            ContaBancaria contaEncontrada = null;
            foreach (var conta in contas)
            {
                if (conta.NumeroConta == numeroConta)
                {
                    contaEncontrada = conta;
                    break;
                }
            }

            // Estrutura condicional para validar se conta foi encontrada
            if (contaEncontrada == null)
            {
                Console.WriteLine("\n❌ Conta não encontrada!");
                Console.ReadKey();
                return;
            }

            Console.Write("Digite a senha: ");
            string senhaDigitada = Console.ReadLine();

            if (contaEncontrada.Senha != senhaDigitada)
            {
                Console.WriteLine("\n❌ Senha incorreta!");
                Console.ReadKey();
                return;
            }

            // Menu da conta com estrutura de repetição
            bool voltarMenu = false;
            while (!voltarMenu)
            {
                try
                {
                    voltarMenu = ExibirMenuConta(contaEncontrada);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Erro: {ex.Message}");
                    Console.ReadKey();
                }
            }
        }

        // Menu de operações da conta
        static bool ExibirMenuConta(ContaBancaria conta)
        {
            Console.Clear();
            conta.ExibirDados();

            Console.WriteLine("========== OPERAÇÕES ==========");
            Console.WriteLine("1 - Depositar");
            Console.WriteLine("2 - Sacar");
            Console.WriteLine("3 - Exibir Dados");

            // Opções específicas por tipo de conta (estrutura condicional)
            if (conta is ContaPoupanca)
            {
                Console.WriteLine("4 - Aplicar Rendimento");
            }
            else if (conta is ContaEmpresarial)
            {
                Console.WriteLine("4 - Solicitar Empréstimo");
                Console.WriteLine("5 - Pagar Empréstimo");
            }

            Console.WriteLine("0 - Voltar ao Menu Principal");
            Console.WriteLine("===============================");
            Console.Write("Escolha uma opção: ");

            try
            {
                int opcao = int.Parse(Console.ReadLine());

                // Estrutura condicional switch
                switch (opcao)
                {
                    case 1:
                        RealizarDeposito(conta);
                        break;
                    case 2:
                        RealizarSaque(conta);
                        break;
                    case 3:
                        conta.ExibirDados();
                        Console.ReadKey();
                        break;
                    case 4:
                        // Polimorfismo: comportamento diferente por tipo
                        if (conta is ContaPoupanca cp)
                        {
                            cp.AplicarRendimento();
                            Console.ReadKey();
                        }
                        else if (conta is ContaEmpresarial ce)
                        {
                            SolicitarEmprestimo(ce);
                        }
                        else
                        {
                            Console.WriteLine("\n⚠️ Opção inválida para este tipo de conta!");
                            Console.ReadKey();
                        }
                        break;

                    case 5:
                        if (conta is ContaEmpresarial ce2)
                        {
                            PagarEmprestimo(ce2);
                        }
                        else
                        {
                            Console.WriteLine("\n⚠️ Opção inválida para este tipo de conta!");
                            Console.ReadKey();
                        }
                        break;

                    case 0:
                        return true; // Voltar ao menu principal

                    default:
                        Console.WriteLine("\n⚠️ Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\n❌ Erro: Digite apenas números!");
                Console.ReadKey();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\n❌ Erro de validação: {ex.Message}");
                Console.ReadKey();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\n❌ Operação inválida: {ex.Message}");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Erro: {ex.Message}");
                Console.ReadKey();
            }

            return false;
        }

        static void RealizarDeposito(ContaBancaria conta)
        {
            Console.Write("\nDigite o valor para depósito: ");
            try
            {
                decimal valor = decimal.Parse(Console.ReadLine());
                conta.Depositar(valor);
            }
            catch (FormatException)
            {
                Console.WriteLine("\n❌ Erro: Valor inválido. Digite um número.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\n❌ Erro: {ex.Message}");
            }
            Console.ReadKey();
        }

        static void RealizarSaque(ContaBancaria conta)
        {
            Console.Write("\nDigite o valor para saque: ");
            try
            {
                decimal valor = decimal.Parse(Console.ReadLine());
                conta.Sacar(valor);
            }
            catch (FormatException)
            {
                Console.WriteLine("\n❌ Erro: Valor inválido. Digite um número.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\n❌ Erro: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\n❌ Erro: {ex.Message}");
            }
            Console.ReadKey();
        }

        static void SolicitarEmprestimo(ContaEmpresarial conta)
        {
            Console.Write("\nDigite o valor do empréstimo: ");
            try
            {
                decimal valor = decimal.Parse(Console.ReadLine());
                conta.SolicitarEmprestimo(valor);
            }
            catch (FormatException)
            {
                Console.WriteLine("\n❌ Erro: Valor inválido. Digite um número.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\n❌ Erro: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\n❌ Erro: {ex.Message}");
            }
            Console.ReadKey();
        }

        static void PagarEmprestimo(ContaEmpresarial conta)
        {
            Console.Write("\nDigite o valor para pagar o empréstimo: ");
            try
            {
                decimal valor = decimal.Parse(Console.ReadLine());
                conta.PagarEmprestimo(valor);
            }
            catch (FormatException)
            {
                Console.WriteLine("\n❌ Erro: Valor inválido. Digite um número.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\n❌ Erro: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\n❌ Erro: {ex.Message}");
            }
            Console.ReadKey();
        }
    }
}

