using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Reqnroll;

namespace KiWhisky.FrutiLogicPlatform.AcceptanceTests.Steps;

[Binding]
public class RegisterProductSteps
{
    private readonly HttpClient _client;
    private HttpResponseMessage _response;
    private object _productPayload;

    public RegisterProductSteps(HttpResponseMessage response, object productPayload)
    {
        _response = response;
        _productPayload = productPayload;
        var factory = new WebApplicationFactory<Program>();
        _client = factory.CreateClient();
    }

    [Given("""
           el producto no existe con el nombre "(.*)"
           """)]
    public void GivenElProductoNoExisteConElNombre(string nombre)
    {
        // Este paso puede limpiar la base de datos o ignorarse si la DB se reinicia entre tests
    }

    [When("envío una solicitud para crear el producto con los siguientes datos:")]
    public async Task WhenEnvioUnaSolicitudParaCrearElProductoConLosSiguientesDatos(Table table)
    {
        var row = table.Rows[0];

        _productPayload = new
        {
            nombre = row["Nombre"],
            precio = decimal.Parse(row["Precio"]),
            stock = int.Parse(row["Stock"])
        };

        _response = await _client.PostAsJsonAsync("/api/v1/productos", _productPayload);
    }

    [Then(@"la respuesta debe tener código 201")]
    public void ThenLaRespuestaDebeTenerCodigo()
    {
        _response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Then("""
          el cuerpo de la respuesta debe contener el producto con nombre "(.*)"
          """)]
    public async Task ThenElCuerpoDeLaRespuestaDebeContenerElProductoConNombre(string nombreEsperado)
    {
        var json = await _response.Content.ReadFromJsonAsync<ProductoResponse>();

        json.Should().NotBeNull();
        json.Nombre.Should().Be(nombreEsperado);
    }
    
    // Intern class to deserialize response
    private class ProductoResponse
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}
