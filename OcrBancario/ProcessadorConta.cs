using System.Text;

namespace OcrBancario;

public sealed class ProcessadorConta
{
    private readonly ReconhecedorDigito reconhecedorDigito = new();

    public string Processar(string primeiraLinha, string segundaLinha, string terceiraLinha)
    {
        ArgumentNullException.ThrowIfNull(primeiraLinha);
        ArgumentNullException.ThrowIfNull(segundaLinha);
        ArgumentNullException.ThrowIfNull(terceiraLinha);

        ValidarLargura(primeiraLinha);
        ValidarLargura(segundaLinha);
        ValidarLargura(terceiraLinha);

        var conta = new StringBuilder(9);

        for (var posicao = 0; posicao < 9; posicao++)
        {
            var inicio = posicao * 3;
            var digito = reconhecedorDigito.Reconhecer(
                primeiraLinha.Substring(inicio, 3),
                segundaLinha.Substring(inicio, 3),
                terceiraLinha.Substring(inicio, 3));

            conta.Append(digito);
        }

        return conta.ToString();
    }

    private static void ValidarLargura(string linha)
    {
        if (linha.Length != 27)
        {
            throw new ArgumentException("Cada linha da conta deve ter exatamente 27 caracteres.");
        }
    }
}