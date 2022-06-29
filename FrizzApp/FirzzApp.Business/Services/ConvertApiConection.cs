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
    public class ConvertApiConection
    {
        private HttpClient _client;

        public ConvertApiConection()
        {
            _client = new HttpClient();
        }

        public async Task<QuotationDto> GetDollarAsync()
        {
            _client.BaseAddress = new Uri("https://dollar-conversor.herokuapp.com");

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            QuotationDto dollar = null;


            HttpResponseMessage response = await _client.GetAsync("https://dollar-conversor.herokuapp.com/cotizacion/usd-ar/actual");


            if (response.IsSuccessStatusCode)
            {
                var quotation = await response.Content.ReadAsStringAsync();
                dollar.Price = Convert.ToInt32(quotation); ;
            }
           
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return dollar;
        }
    }
}
