namespace OcrBancario;

public sealed class ReconhecedorDigito
{
    private static readonly Dictionary<string, char> Padroes = new()
    {
        [CriarChave(" _ ", "| |", "|_|")] = '0',
        [CriarChave("   ", "  |", "  |")] = '1',
        [CriarChave(" _ ", " _|", "|_ ")] = '2',
        [CriarChave(" _ ", " _|", " _|")] = '3',
        [CriarChave("   ", "|_|", "  |")] = '4',
        [CriarChave(" _ ", "|_ ", " _|")] = '5',
        [CriarChave(" _ ", "|_ ", "|_|")] = '6',
        [CriarChave(" _ ", "  |", "  |")] = '7',
        [CriarChave(" _ ", "|_|", "|_|")] = '8',
        [CriarChave(" _ ", "|_|", " _|")] = '9'
    };

    public char Reconhecer(string primeiraLinha, string segundaLinha, string terceiraLinha)
    {
        ArgumentNullException.ThrowIfNull(primeiraLinha);
        ArgumentNullException.ThrowIfNull(segundaLinha);
        ArgumentNullException.ThrowIfNull(terceiraLinha);

        ValidarLargura(primeiraLinha);
        ValidarLargura(segundaLinha);
        ValidarLargura(terceiraLinha);

        var chave = CriarChave(primeiraLinha, segundaLinha, terceiraLinha);

        if (Padroes.TryGetValue(chave, out var digito))
        {
            return digito;
        }

        throw new ArgumentException("O bloco 3x3 não corresponde a um dígito conhecido.");
    }

    private static string CriarChave(string primeiraLinha, string segundaLinha, string terceiraLinha)
    {
        return $"{primeiraLinha}\n{segundaLinha}\n{terceiraLinha}";
    }

    private static void ValidarLargura(string linha)
    {
        if (linha.Length != 3)
        {
            throw new ArgumentException("Cada linha do bloco deve ter exatamente 3 caracteres.");
        }
    }
}