<div align="center">

# 🏦 OCR Bancário

### Reconhecimento de contas bancárias representadas por caracteres ASCII

Desafio de lógica desenvolvido durante o curso da **Academia do Programador**.

<br>

![C#](https://img.shields.io/badge/C%23-Programming-512BD4?style=for-the-badge&logo=csharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![MSTest](https://img.shields.io/badge/Tests-MSTest-25A162?style=for-the-badge&logo=dotnet&logoColor=white)

</div>

---

## 📋 Sobre o desafio

O objetivo do **OCR Bancário** é interpretar números de contas bancárias representados por caracteres ASCII.

Cada conta possui **9 dígitos**, e cada dígito é desenhado utilizando apenas:

```text
_   |   espaço
```

Cada dígito ocupa um bloco de **3 × 3 caracteres**.

Por exemplo, o número `0` é representado desta forma:

```text
 _ 
| |
|_|
```

Como uma conta possui 9 dígitos, cada uma das três linhas utilizadas para representá-la possui exatamente:

```text
9 dígitos × 3 caracteres = 27 caracteres
```

---

## 🔎 Como funciona

A aplicação lê o arquivo `contas-ocr.txt` e processa cada conta encontrada.

O fluxo da aplicação pode ser resumido da seguinte forma:

```text
┌──────────────────────┐
│    contas-ocr.txt    │
└──────────┬───────────┘
           │
           ▼
┌──────────────────────┐
│ LeitorArquivoContas  │
└──────────┬───────────┘
           │
           ▼
┌──────────────────────┐
│   ProcessadorConta   │
└──────────┬───────────┘
           │
           ▼
┌──────────────────────┐
│ ReconhecedorDigito   │
└──────────┬───────────┘
           │
           ▼
┌──────────────────────┐
│   Número da conta    │
└──────────────────────┘
```

### 1️⃣ Leitura do arquivo

O `LeitorArquivoContas` lê os registros presentes no arquivo e preserva os espaços da representação ASCII.

### 2️⃣ Divisão da conta

Cada linha da conta possui **27 caracteres** e é dividida em nove trechos de três caracteres:

```text
┌───┬───┬───┬───┬───┬───┬───┬───┬───┐
│ 1 │ 2 │ 3 │ 4 │ 5 │ 6 │ 7 │ 8 │ 9 │
└───┴───┴───┴───┴───┴───┴───┴───┴───┘
  3   3   3   3   3   3   3   3   3
 caracteres por bloco
```

As três partes correspondentes formam o bloco **3 × 3** de um dígito.

### 3️⃣ Reconhecimento

O `ReconhecedorDigito` compara cada bloco com os padrões conhecidos dos dígitos de `0` a `9`.

Por exemplo:

```text
 _ 
 _|  ──────►  2
|_ 
```

### 4️⃣ Formação da conta

Depois que os nove blocos são reconhecidos, os dígitos são concatenados:

```text
1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9
                    │
                    ▼
               123456789
```

---

## 🧩 Exemplo

Uma conta presente no arquivo pode ser representada desta maneira:

```text
    _  _     _  _  _  _  _ 
  | _| _||_||_ |_   ||_||_|
  ||_  _|  | _||_|  ||_| _|
```

O programa divide a representação em nove blocos e identifica:

```text
┌───┬───┬───┬───┬───┬───┬───┬───┬───┐
│ 1 │ 2 │ 3 │ 4 │ 5 │ 6 │ 7 │ 8 │ 9 │
└───┴───┴───┴───┴───┴───┴───┴───┴───┘
```

Produzindo:

```text
Conta identificada: 123456789
```

---

## 📁 Estrutura do projeto

```text
Quebra-Cuca-05-OCR-Bancario/
│
├── 📂 OcrBancario/
│   ├── Program.cs
│   ├── LeitorArquivoContas.cs
│   ├── ProcessadorConta.cs
│   ├── ReconhecedorDigito.cs
│   └── OcrBancario.csproj
│
├── 📂 OcrBancario.Tests/
│   ├── ReconhecedorDigitoTests.cs
│   ├── ProcessadorContaTests.cs
│   ├── LeitorArquivoContasTests.cs
│   └── OcrBancario.Tests.csproj
│
├── contas-ocr.txt
├── OcrBancario.sln
└── README.md
```

### Responsabilidade de cada componente

| Componente | Responsabilidade |
|---|---|
| `Program.cs` | Inicia a aplicação e exibe as contas identificadas |
| `LeitorArquivoContas.cs` | Lê e separa os registros do arquivo |
| `ProcessadorConta.cs` | Divide uma conta em nove blocos 3 × 3 |
| `ReconhecedorDigito.cs` | Identifica os dígitos de `0` a `9` |
| `OcrBancario.Tests` | Contém os testes automatizados |
| `contas-ocr.txt` | Arquivo utilizado como entrada da aplicação |

---

## ▶️ Como executar

### Pré-requisito

É necessário possuir o **.NET 10 SDK** instalado.

Para conferir a versão:

```bash
dotnet --version
```

### 1. Compilar

Na raiz do repositório:

```bash
dotnet build OcrBancario.sln
```

### 2. Executar

```bash
dotnet run --project OcrBancario
```

> [!IMPORTANT]
> O arquivo `contas-ocr.txt` deve permanecer na raiz do repositório, pois é utilizado como arquivo de entrada pela aplicação.

---

## 💻 Exemplo de saída

Utilizando o arquivo de contas do desafio, a aplicação identifica:

```text
Conta identificada: 123456789
Conta identificada: 987654321
Conta identificada: 102938475
Conta identificada: 564738291
Conta identificada: 314159265
Conta identificada: 271828182
Conta identificada: 456789012
Conta identificada: 908172635
Conta identificada: 135792468
Conta identificada: 246813579
```

A ordem das contas é preservada durante todo o processamento.

---

## 🧪 Testes automatizados

O projeto possui testes automatizados utilizando **MSTest**.

Para executar:

```bash
dotnet test OcrBancario.sln
```

Os testes verificam:

- reconhecimento individual dos dígitos de `0` a `9`;
- padrões ASCII inválidos;
- processamento de contas completas;
- contas com dígitos repetidos;
- linhas com tamanhos inválidos;
- leitura de uma ou várias contas;
- preservação da ordem dos registros;
- registros incompletos;
- separadores inválidos;
- limite máximo de contas;
- último registro com ou sem separador vazio explícito.

---

## ⚠️ Validações

A aplicação valida situações como:

| Situação | Comportamento |
|---|---|
| Linha diferente de 27 caracteres | Entrada rejeitada |
| Bloco diferente de 3 × 3 | Entrada rejeitada |
| Padrão de dígito desconhecido | Entrada rejeitada |
| Registro incompleto | Entrada rejeitada |
| Separador intermediário inválido | Entrada rejeitada |
| Mais de 500 contas | Entrada rejeitada |
| Último registro sem linha vazia final | Aceito |

---

## 🛠️ Tecnologias

<div align="center">

| Tecnologia | Utilização |
|:---:|---|
| **C#** | Linguagem principal |
| **.NET 10** | Plataforma da aplicação |
| **MSTest** | Testes automatizados |
| **Git** | Controle de versão |

</div>

---

## 📌 Restrições do desafio

O reconhecimento dos números é realizado pela própria aplicação através da comparação dos padrões ASCII.

> [!NOTE]
> Nenhuma biblioteca externa de OCR é utilizada.

A aplicação é capaz de processar até **500 contas em uma única execução**, mantendo a ordem original dos registros.

---

<div align="center">

### 🏦 OCR Bancário

**Academia do Programador**

Desenvolvido por Gustavo Tessaro como exercício de lógica e manipulação de strings em C#.

</div>