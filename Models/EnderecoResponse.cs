using System.Text.Json.Serialization;
namespace APICep.Models;

public record EnderecoResponse(
    [property: JsonPropertyName("cep")]        string? Cep,
    [property: JsonPropertyName("logradouro")] string? Logradouro,
    [property: JsonPropertyName("bairro")]     string? Bairro,
    [property: JsonPropertyName("localidade")] string? Localidade,
    [property: JsonPropertyName("uf")]         string? Uf,
    [property: JsonPropertyName("erro")]       bool? Error  
)
{
    public override string ToString() =>
        $"CEP: {Cep} | {Logradouro}, {Bairro} — {Localidade}/{Uf}";
}