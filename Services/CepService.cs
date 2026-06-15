using System.Text.Json;
using APICep.Models;

namespace APICep.Services;

public static class CepService
{
    private static readonly HttpClient _client = new();
   
    public static string? ValidarCep(string input)
    {
        string cep = input.Replace("-", "").Trim();

        if (cep.Length != 8 || !cep.All(char.IsDigit))
            return null;

        return cep;
    }
    
    public static async Task<EnderecoResponse?> BuscarCepAsync(string cep)
    {
        string url = $"https://viacep.com.br/ws/{cep}/json/";

        string json = await _client.GetStringAsync(url);

        var endereco = JsonSerializer.Deserialize<EnderecoResponse>(json);

        if (endereco?.Error == true)
            return null;

        return endereco;
    }
    
    public static async Task<EnderecoResponse?[]> BuscarCepsAsync(IEnumerable<string> ceps)
    {
        var tarefas = ceps.Select(BuscarCepAsync);
        return await Task.WhenAll(tarefas);
    }
}