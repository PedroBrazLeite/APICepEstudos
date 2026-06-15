
using APICep.Services;

Console.Write("Digite um ou mais CEPs separados por ';': ");
string entrada = Console.ReadLine()!;

string[] inputs = entrada.Split(';', StringSplitOptions.RemoveEmptyEntries);

var cepsValidos = new List<string>();

foreach (var input in inputs)
{
    string? cep = CepService.ValidarCep(input);

    if (cep is null)
        Console.WriteLine($"CEP inválido: '{input.Trim()}' deve ter 8 dígitos numéricos.");
    else
        cepsValidos.Add(cep);
}

if (cepsValidos.Count == 0)
{
    Console.WriteLine("Nenhum CEP válido informado.");
    return;
}

Console.WriteLine($"\nBuscando {cepsValidos.Count} CEP(s)...\n");

try
{
    var resultados = await CepService.BuscarCepsAsync(cepsValidos);

    for (int i = 0; i < cepsValidos.Count; i++)
    {
        var resultado = resultados[i];

        if (resultado is null)
            Console.WriteLine($"CEP {cepsValidos[i]}: não encontrado.");
        else
            Console.WriteLine(resultado);
    }
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"Erro de rede: {ex.Message}");
}
catch (TaskCanceledException)
{
    Console.WriteLine("Tempo limite da requisição atingido.");
}
catch (Exception ex)
{
    Console.WriteLine($"Erro inesperado: {ex.Message}");
}