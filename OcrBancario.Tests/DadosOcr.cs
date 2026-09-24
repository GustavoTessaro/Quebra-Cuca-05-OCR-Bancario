namespace OcrBancario.Tests;

internal static class DadosOcr
{
    private static readonly IReadOnlyDictionary<char, string[]> Padroes =
        new Dictionary<char, string[]>
        {
            ['0'] = [" _ ", "| |", "|_|"],
            ['1'] = ["   ", "  |", "  |"],
            ['2'] = [" _ ", " _|", "|_ "],
            ['3'] = [" _ ", " _|", " _|"],
            ['4'] = ["   ", "|_|", "  |"],
            ['5'] = [" _ ", "|_ ", " _|"],
            ['6'] = [" _ ", "|_ ", "|_|"],
            ['7'] = [" _ ", "  |", "  |"],
            ['8'] = [" _ ", "|_|", "|_|"],
            ['9'] = [" _ ", "|_|", " _|"]
        };

    public static string[] ObterPadrao(char digito)
    {
        return Padroes[digito];
    }

    public static string[] CriarLinhasConta(string conta)
    {
        var linhas = new[] { string.Empty, string.Empty, string.Empty };

        foreach (var digito in conta)
        {
            var padrao = ObterPadrao(digito);

            for (var linha = 0; linha < 3; linha++)
            {
                linhas[linha] += padrao[linha];
            }
        }

        return linhas;
    }

    public static string CriarArquivoTemporario(params string[] contas)
    {
        var caminho = Path.Combine(Path.GetTempPath(), $"OcrBancario.Tests-{Guid.NewGuid():N}.txt");
        var linhas = new List<string>();

        foreach (var conta in contas)
        {
            linhas.AddRange(CriarLinhasConta(conta));
            linhas.Add(string.Empty);
        }

        File.WriteAllLines(caminho, linhas);
        return caminho;
    }

    public static string CriarArquivoTemporario(IEnumerable<string> linhas)
    {
        var caminho = Path.Combine(Path.GetTempPath(), $"OcrBancario.Tests-{Guid.NewGuid():N}.txt");
        File.WriteAllLines(caminho, linhas);
        return caminho;
    }

    public static void ExcluirArquivo(string caminho)
    {
        if (File.Exists(caminho))
        {
            File.Delete(caminho);
        }
    }
}