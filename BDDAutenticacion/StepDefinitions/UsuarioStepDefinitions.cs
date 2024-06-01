using System.Net;
using RestSharp;
using Newtonsoft.Json;
using BarberiAppAutenticacion.Models;
using Serilog;

namespace BDDAutenticacion.StepDefinitions
{
    [Binding]
    public class UsuarioStepDefinitions
    {
        private RestClient _client;
        private RestRequest _request;
        private RestResponse _response;
        private string _token;


        [Given(@"que la API está disponible")]
        public void GivenQueLaAPIEstaDisponible()
        {
            _client = new RestClient("https://localhost:7025");
        }

        [Given(@"tengo un token válido")]
        public void GivenTengoUnTokenValido()
        {
            var authClient = new RestClient("https://localhost:7025");
            var authRequest = new RestRequest("/api/token", Method.Post);

            var body = new
            {
                
                usuarioID = 0,
                email = "string",
                alias = "adminPlat",
                contraseña = "1234",
                rolId = 2

            };

            // Serializar el cuerpo como JSON
            authRequest.AddJsonBody(body);

            var authResponse = authClient.Execute(authRequest);

            if (authResponse.StatusCode == HttpStatusCode.OK)
            {
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(authResponse.Content);
                _token = tokenResponse.AccessToken; 
            }
            else
            {
                throw new Exception("Failed to obtain token");
            }
        }

        [When(@"hago una solicitud GET a ""([^""]*)""")]
        public void WhenHagoUnaSolicitudGETA(string resource)
        {
            _request = new RestRequest(resource, Method.Get);
            _request.AddHeader("Authorization", $"Bearer {_token}");
            _response = _client.Execute(_request);

            // Log request and response details for debugging
            Log.Information("Request URL: {Url}", _client.BuildUri(_request));
            Log.Information("Request Headers: {Headers}", string.Join(", ", _request.Parameters));
            Log.Information("Response Status Code: {StatusCode}", _response.StatusCode);
            Log.Information("Response Content: {Content}", _response.Content);
        }

        [Then(@"el código de respuesta debe ser (.*)")]
        public void ThenElCodigoDeRespuestaDebeSer(int expectedStatusCode)
        {
            Assert.Equal(expectedStatusCode, (int)_response.StatusCode);
        }

        [Then(@"la respuesta debe contener una lista de usuarios")]
        public void ThenLaRespuestaDebeContenerUnaListaDeUsuarios()
        {
            var users = JsonConvert.DeserializeObject<List<Usuario>>(_response.Content);
            Assert.NotEmpty(users);
        }


        [Then(@"la respuesta debe contener el correo ""([^""]*)""")]
        public void ThenLaRespuestaDebeContenerElCorreo(string correo)
        {
            var user = JsonConvert.DeserializeObject<Usuario>(_response.Content);
            Assert.Equal(correo, user.Email);
        }

        [Then(@"la respuesta debe contener el alias ""([^""]*)""")]
        public void ThenLaRespuestaDebeContenerElAlias(string alias)
        {
            var user = JsonConvert.DeserializeObject<Usuario>(_response.Content);
            Assert.Equal(alias, user.Alias);
        }

        [Then(@"la respuesta debe contener la contraseña ""([^""]*)""")]
        public void ThenLaRespuestaDebeContenerLaContrasena(string contraseña)
        {
            var user = JsonConvert.DeserializeObject<Usuario>(_response.Content);
            Assert.Equal(contraseña, user.Contraseña);
        }

        [Then(@"la la respuesta debe contener el rol_id (.*)")]
        public void ThenLaLaRespuestaDebeContenerElRol_Id(int rolId)
        {
            var user = JsonConvert.DeserializeObject<Usuario>(_response.Content);
            Assert.Equal(rolId, user.RolId);
        }


    }

    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
