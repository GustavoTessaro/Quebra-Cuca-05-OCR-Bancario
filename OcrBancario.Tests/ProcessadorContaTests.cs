using OcrBancario;

namespace OcrBancario.Tests;

[TestClass]
public sealed class ProcessadorContaTests
{
    [TestMethod]
    [DataRow("123456789")]
    [DataRow("908172635")]
    [DataRow("000000000")]
    [DataRow("888888888")]
    public void ProcessaContasValidas(string contaEsperada)
    {
        var processador = new ProcessadorConta();
        var linhas = DadosOcr.CriarLinhasConta(contaEsperada);

        var resultado = processador.Processar(linhas[0], linhas[1], linhas[2]);

        Assert.AreEqual(contaEsperada, resultado);
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(2)]
    public void RejeitaLinhaComTamanhoDiferenteDeVinteESete(int indiceDaLinha)
    {
        var processador = new ProcessadorConta();
        var linhas = DadosOcr.CriarLinhasConta("123456789");
        linhas[indiceDaLinha] = " ";

        Assert.Throws<ArgumentException>(() =>
            processador.Processar(linhas[0], linhas[1], linhas[2]));
    }
}