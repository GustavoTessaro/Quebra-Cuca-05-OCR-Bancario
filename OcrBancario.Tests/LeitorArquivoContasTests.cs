using OcrBancario;

namespace OcrBancario.Tests;

[TestClass]
public sealed class LeitorArquivoContasTests
{
    [TestMethod]
    public void LeUmaContaComSeparadorFinalExplicito()
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
    public void LeUltimaContaSemSeparadorFisico()
    {
        var linhas = DadosOcr.CriarLinhasConta("987654321").AsEnumerable();
        var caminho = DadosOcr.CriarArquivoTemporario(linhas);

        try
        {
            var resultado = new LeitorArquivoContas().Ler(caminho);

            CollectionAssert.AreEqual(new[] { "987654321" }, resultado.ToArray());
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
    [DataRow(1)]
    [DataRow(2)]
    public void RejeitaRegistroComApenasUmaOuDuasLinhas(int quantidadeLinhas)
    {
        var caminho = DadosOcr.CriarArquivoTemporario(
            DadosOcr.CriarLinhasConta("123456789").Take(quantidadeLinhas));

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
        var linhas = new List<string>();
        linhas.AddRange(DadosOcr.CriarLinhasConta("123456789"));
        linhas.Add("separador inválido");
        linhas.AddRange(DadosOcr.CriarLinhasConta("987654321"));
        linhas.Add(string.Empty);
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