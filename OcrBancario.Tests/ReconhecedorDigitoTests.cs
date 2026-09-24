using OcrBancario;

namespace OcrBancario.Tests;

[TestClass]
public sealed class ReconhecedorDigitoTests
{
    [TestMethod]
    [DataRow('0')]
    [DataRow('1')]
    [DataRow('2')]
    [DataRow('3')]
    [DataRow('4')]
    [DataRow('5')]
    [DataRow('6')]
    [DataRow('7')]
    [DataRow('8')]
    [DataRow('9')]
    public void ReconheceTodosOsDigitos(char digito)
    {
        var reconhecedor = new ReconhecedorDigito();
        var padrao = DadosOcr.ObterPadrao(digito);

        var resultado = reconhecedor.Reconhecer(padrao[0], padrao[1], padrao[2]);

        Assert.AreEqual(digito, resultado);
    }

    [TestMethod]
    public void RejeitaLinhaComTamanhoDiferenteDeTres()
    {
        var reconhecedor = new ReconhecedorDigito();

        Assert.Throws<ArgumentException>(() =>
            reconhecedor.Reconhecer(" _", "| |", "_|"));
    }

    [TestMethod]
    public void RejeitaPadraoDesconhecido()
    {
        var reconhecedor = new ReconhecedorDigito();

        Assert.Throws<ArgumentException>(() =>
            reconhecedor.Reconhecer(" _ ", " _ ", " _ "));
    }
}