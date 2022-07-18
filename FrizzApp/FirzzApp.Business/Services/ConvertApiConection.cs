using FirzzApp.Business.Dtos;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Interfaces;
using FrizzApp.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FirzzApp.Business.Services
{
    public class ConvertApiConection
    {
        private HttpClient _client;

        public ConvertApiConection()
        {
            _client = new HttpClient();
        }

        public async Task<decimal> GetDollarAsync()
        {
            _client.BaseAddress = new Uri("https://dollar-conversor.herokuapp.com");

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            decimal dollar = default;
            try
            {
                HttpResponseMessage responseApi = await _client.GetAsync("https://dollar-conversor.herokuapp.com/cotizacion/usd-ar/actual");


                if (responseApi.IsSuccessStatusCode)
                {
                    string quotation = await responseApi.Content.ReadAsStringAsync();
                    ResponseConectionQuotationDto quotationObject = JsonConvert.DeserializeObject<ResponseConectionQuotationDto>(quotation);
                    dollar = quotationObject.Precio;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return dollar;
        }
    }
}
