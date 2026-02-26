# Sistema de Gerenciamento de Contas Bancárias

Sistema completo desenvolvido em C# utilizando Programação Orientada a Objetos (POO).

## 📁 Estrutura do Projeto

```
SistemaBancario/
├── ContaBancaria.cs          # Classe abstrata base
├── ContaCorrente.cs           # Conta com taxa de saque
├── ContaPoupanca.cs           # Conta com rendimento
├── ContaEmpresarial.cs        # Conta com limite de empréstimo
├── Program.cs                 # Programa principal com menu
├── SistemaBancario.csproj     # Arquivo de projeto
├── DOCUMENTACAO_IMPLEMENTACAO.md  # Documentação técnica
└── README.md                  # Este arquivo
```

## 🚀 Como Executar

### No Visual Studio (Recomendado)

1. **Abrir o projeto:**
   - Abra o Visual Studio
   - Clique em "Abrir um projeto ou solução"
   - Selecione o arquivo `SistemaBancario.csproj`

2. **Executar:**
   - Pressione `F5` ou clique em "Iniciar"
   - O console será aberto com o menu interativo

### Via Linha de Comando

```bash
cd SistemaBancario
dotnet build
dotnet run
```

## 💡 Funcionalidades

### Tipos de Conta

1. **Conta Corrente**
   - Taxa de saque configurável (padrão: R$ 5,00)
   - Saque deduz valor + taxa

2. **Conta Poupança**
   - Sem taxa de saque
   - Rendimento mensal configurável (padrão: 0,5%)

3. **Conta Empresarial**
   - Limite de empréstimo (padrão: R$ 10.000,00)
   - Permite saque além do saldo usando o limite
   - Funções para solicitar e pagar empréstimo

### Operações Disponíveis

- ✅ Criar contas (Corrente, Poupança, Empresarial)
- ✅ Depositar valores
- ✅ Sacar valores (com regras específicas por tipo)
- ✅ Consultar saldo e dados da conta
- ✅ Aplicar rendimento (Poupança)
- ✅ Solicitar empréstimo (Empresarial)
- ✅ Pagar empréstimo (Empresarial)
- ✅ Listar todas as contas

## 🎯 Conceitos de POO Implementados

- **Encapsulamento**: Atributos privados com propriedades públicas
- **Herança**: Classes filhas herdam de ContaBancaria
- **Polimorfismo**: Método Sacar() implementado diferentemente em cada classe
- **Abstração**: Classe abstrata ContaBancaria

## 🛡️ Tratamento de Erros

O sistema possui tratamento completo de exceções:

- Validação de entradas numéricas
- Verificação de saldo suficiente
- Validação de valores positivos
- Mensagens de erro claras e informativas

## 📚 Requisitos

- .NET 6.0 ou superior
- Visual Studio 2019/2022 (ou .NET SDK para linha de comando)

## 👨‍💻 Exemplo de Uso

```
1. Criar uma Conta Corrente
   - Número: 001
   - Titular: João Silva
   - Saldo inicial: R$ 1.000,00
   - Taxa de saque: R$ 5,00

2. Realizar um saque de R$ 100,00
   - Será debitado: R$ 105,00 (R$ 100 + R$ 5 de taxa)
   - Novo saldo: R$ 895,00

3. Realizar um depósito de R$ 500,00
   - Novo saldo: R$ 1.395,00
```

## 📖 Documentação

Para documentação técnica completa, consulte o arquivo `DOCUMENTACAO_IMPLEMENTACAO.md`.

---

**Desenvolvido como projeto educacional de Programação Orientada a Objetos em C#**
