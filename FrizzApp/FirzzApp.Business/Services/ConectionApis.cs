using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Interfaces;
using FrizzApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FirzzApp.Business.Services
{
    public class ConectionApis 
    { 
        private HttpClient _client;

        public ConectionApis()
        {
            //Necesito el client
            _client = new HttpClient();
        }

        public async Task<DollarDto> GetDollarAsync(string path)
        {
            //No entiendo la firma

            //Hago peticion, si da bien entro al IF, sino devuelvo un null


            DollarDto dollar = null;
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                dollar = default; //await response.Content.ReadAsAsync<DollarDto>();
            }
            return dollar;
        }

        public async Task RunAsync()
        {
            //Conecta a la API
            _client.BaseAddress = new Uri("https://dollar-conversor.herokuapp.com");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                //Tengo el valor
                var dollar = await GetDollarAsync("https://dollar-conversor.herokuapp.com/cotizacion/usd-ar/actual");
            }
            catch (Exception e)
            {
                //Si no tengo el producto bien
                Console.WriteLine(e.Message);
            }
        }
    }
}
