using System;
using System.Net;
using System.Collections.Generic;
using RestSharp;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using BarberiAppAutenticacion.Models;

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
            _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKV1RTZXJ2aWNlQWNjZXNzVG9rZW4iLCJqdGkiOiI3OTVkZmQ0Yi1kYTk2LTQ2NjUtYWIxNi04NjEwNzg4NGUyODkiLCJpYXQiOiIxLzA2LzIwMjQgNzoxNzo1MCBhLsKgbS4iLCJFbWFpbCI6ImNsaWVudGUwMUB5b3BtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJhZG1pblBsYXQiLCJSb2xJZCI6IjIiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiIyIiwiZXhwIjoxNzE3MjI2ODcwLCJpc3MiOiJKV1RBdXRoZW50aWNhdGlvblNlcnZlciIsImF1ZCI6IkpXVFNlcnZpY2VQb3N0bWFuQ2xpZW50In0.mWpRUGsSdiFeQhCaiW5tYUn-hBtK-RaUPb8ZzFvGwTU";
        }

        [When(@"hago una solicitud GET a ""([^""]*)""")]
        public void WhenHagoUnaSolicitudGETA(string resource)
        {
            _request = new RestRequest(resource, Method.Get);
            _request.AddHeader("Authorization", $"Bearer {_token}");
            _response = _client.Execute(_request);
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
    }
}
