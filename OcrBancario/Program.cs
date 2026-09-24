using OcrBancario;

var leitor = new LeitorArquivoContas();

try
{
	var contas = leitor.Ler("entrada.txt");

	foreach (var conta in contas)
	{
		Console.WriteLine($"Conta identificada: {conta}");
	}
}
catch (FileNotFoundException excecao)
{
	Console.WriteLine($"Erro: {excecao.Message}");
}
catch (FormatException excecao)
{
	Console.WriteLine($"Erro no formato do arquivo: {excecao.Message}");
}
catch (ArgumentException excecao)
{
	Console.WriteLine($"Erro nos dados da conta: {excecao.Message}");
}
