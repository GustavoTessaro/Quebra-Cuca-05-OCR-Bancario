using OcrBancario;

var reconhecedor = new ReconhecedorDigito();

Console.WriteLine($"0: {reconhecedor.Reconhecer(" _ ", "| |", "|_|")}");
Console.WriteLine($"1: {reconhecedor.Reconhecer("   ", "  |", "  |")}");
Console.WriteLine($"8: {reconhecedor.Reconhecer(" _ ", "|_|", "|_|")}");
