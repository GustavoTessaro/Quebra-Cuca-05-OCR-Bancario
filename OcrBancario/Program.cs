using OcrBancario;

var processador = new ProcessadorConta();

var primeiraLinha = "   " + " _ " + " _ " + "   " + " _ " + " _ " + " _ " + " _ " + " _ ";
var segundaLinha = "  |" + " _|" + " _|" + "|_|" + "|_ " + "|_ " + "  |" + "|_|" + "|_|";
var terceiraLinha = "  |" + "|_ " + " _|" + "  |" + " _|" + "|_|" + "  |" + "|_|" + " _|";

var conta = processador.Processar(primeiraLinha, segundaLinha, terceiraLinha);

Console.WriteLine($"Conta identificada: {conta}");
