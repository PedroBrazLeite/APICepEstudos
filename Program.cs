using APICep.Services;

Console.Write("Digite um ou mais CEPs separados por ';': ");

string entrada = Console.ReadLine()!;

string?[] cepsValidos = entrada.Split(';', StringSplitOptions.RemoveEmptyEntries)
    .Select(item =>
    {
        // Extrair para um médodo
        var sanitizado = CepService.SanitizarCep(item);
        
        var valido = CepService.ValidarCep(sanitizado);

        if (!valido)
        {
            Console.WriteLine($"CEP inválido: '{sanitizado}' deve ter 8 dígitos numéricos.");
        }

        return valido ? item : null;
    })
    .Where(item => item != null)
    .ToArray();

if (cepsValidos.Length == 0)
{
    Console.WriteLine("Nenhum CEP válido informado.");
    return;
}

Console.WriteLine($"\nBuscando {cepsValidos.Length} CEP(s)...\n");

try
{
    var resultados = await CepService
        .BuscarCepsAsync(cepsValidos!);

    foreach (var result in resultados)
    {
        if (result.Sucesso)
        {
            Console.WriteLine(result.Resultado);
        }
        else
        {
            Console.WriteLine($"CEP {result.Cep}: não encontrado.");
        }
    }
}
catch (TaskCanceledException)
{
    Console.WriteLine("Tempo limite da requisição atingido.");
}
catch (Exception ex)
{
    Console.WriteLine($"Erro inesperado: {ex.Message}");
}