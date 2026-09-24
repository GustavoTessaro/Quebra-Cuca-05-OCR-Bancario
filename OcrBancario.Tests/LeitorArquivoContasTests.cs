using OcrBancario;

namespace OcrBancario.Tests;

[TestClass]
public sealed class LeitorArquivoContasTests
{
    [TestMethod]
    public void LeUmaConta()
    {
        var caminho = DadosOcr.CriarArquivoTemporario("123456789");

        try
        {
            var resultado = new LeitorArquivoContas().Ler(caminho);

            CollectionAssert.AreEqual(new[] { "123456789" }, resultado.ToArray());
        }
        finally
        {
            DadosOcr.ExcluirArquivo(caminho);
        }
    }

    [TestMethod]
    public void LeMultiplasContasPreservandoAOrdem()
    {
        var caminho = DadosOcr.CriarArquivoTemporario("908172635", "123456789", "888888888");

        try
        {
            var resultado = new LeitorArquivoContas().Ler(caminho);

            CollectionAssert.AreEqual(
                new[] { "908172635", "123456789", "888888888" },
                resultado.ToArray());
        }
        finally
        {
            DadosOcr.ExcluirArquivo(caminho);
        }
    }

    [TestMethod]
    public void RejeitaRegistroIncompleto()
    {
        var caminho = DadosOcr.CriarArquivoTemporario(DadosOcr.CriarLinhasConta("123456789").Take(3));

        try
        {
            Assert.Throws<FormatException>(() => new LeitorArquivoContas().Ler(caminho));
        }
        finally
        {
            DadosOcr.ExcluirArquivo(caminho);
        }
    }

    [TestMethod]
    public void RejeitaLinhaAsciiComTamanhoInvalido()
    {
        var linhas = DadosOcr.CriarLinhasConta("123456789");
        linhas[1] = linhas[1][..26];
        var caminho = DadosOcr.CriarArquivoTemporario(linhas.Append(string.Empty));

        try
        {
            Assert.Throws<ArgumentException>(() => new LeitorArquivoContas().Ler(caminho));
        }
        finally
        {
            DadosOcr.ExcluirArquivo(caminho);
        }
    }

    [TestMethod]
    public void RejeitaSeparadorComConteudo()
    {
        var linhas = DadosOcr.CriarLinhasConta("123456789").Append("separador inválido");
        var caminho = DadosOcr.CriarArquivoTemporario(linhas);

        try
        {
            Assert.Throws<FormatException>(() => new LeitorArquivoContas().Ler(caminho));
        }
        finally
        {
            DadosOcr.ExcluirArquivo(caminho);
        }
    }

    [TestMethod]
    public void RejeitaMaisDeQuinhentasContas()
    {
        var caminho = DadosOcr.CriarArquivoTemporario(Enumerable.Repeat("123456789", 501).ToArray());

        try
        {
            Assert.Throws<FormatException>(() => new LeitorArquivoContas().Ler(caminho));
        }
        finally
        {
            DadosOcr.ExcluirArquivo(caminho);
        }
    }
}