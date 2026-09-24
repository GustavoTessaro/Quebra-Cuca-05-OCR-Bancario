namespace OcrBancario;

public sealed class LeitorArquivoContas
{
    private readonly ProcessadorConta processadorConta = new();

    public IReadOnlyList<string> Ler(string caminho)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(caminho);

        if (!File.Exists(caminho))
        {
            throw new FileNotFoundException("O arquivo de entrada não foi encontrado.", caminho);
        }

        var linhas = File.ReadAllLines(caminho);

        if (linhas.Length == 0 || (linhas.Length % 4 != 0 && linhas.Length % 4 != 3))
        {
            throw new FormatException("O arquivo deve conter registros completos de quatro linhas.");
        }

        var quantidadeContas = (linhas.Length + 3) / 4;

        if (quantidadeContas > 500)
        {
            throw new FormatException("O arquivo não pode conter mais de 500 contas.");
        }

        var contas = new List<string>(quantidadeContas);

        for (var indice = 0; indice < quantidadeContas; indice++)
        {
            var inicio = indice * 4;

            if (inicio + 3 < linhas.Length && linhas[inicio + 3].Length != 0)
            {
                throw new FormatException($"A linha separadora do registro {indice + 1} deve ser vazia.");
            }

            contas.Add(processadorConta.Processar(
                linhas[inicio],
                linhas[inicio + 1],
                linhas[inicio + 2]));
        }

        return contas;
    }
}