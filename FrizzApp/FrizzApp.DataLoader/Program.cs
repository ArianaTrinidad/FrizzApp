using Bogus;
using FirzzApp.Business.Dtos.RequestDto;
using FrizzApp.DataLoader.Common;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace FrizzApp.DataLoader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000")
            };

            DataLoaderHelper.ShowMainMenu();

            var option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    CallCreateProductEndpoint(client);
                    DataLoaderHelper.Reset();
                    break;
                default:
                    DataLoaderHelper.Reset();
                    break;
            }
        }

        private static void CallCreateProductEndpoint(HttpClient client)
        {
            Console.WriteLine("Qué cantidad de productos querés crear?");
            var quantity = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= quantity; i++)
            {
                var fake = CreateFakeObject();

                HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(fake),
                                                            Encoding.UTF8,
                                                            "application/json");

                var response = client.PostAsync(client.BaseAddress + "api/product", contentPost).Result;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Error :_)");
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"{i} - {response.Content.ReadAsStringAsync().Result} ");
                    Console.ResetColor();
                }

            }
        }

        private static CreateProductDto CreateFakeObject()
        {
            var random = new Random();

            var fakeObject = new Faker<CreateProductDto>()
                .RuleFor(x => x.Nombre, f => f.Commerce.ProductName())
                .RuleFor(x => x.Descripcion, f => f.Commerce.ProductMaterial())
                .RuleFor(x => x.Nota, f => f.Lorem.Sentence(15))
                .RuleFor(x => x.Presentacion, f => f.Lorem.Word())
                .RuleFor(x => x.ImagenUrl, f => f.Image.PicsumUrl())
                .RuleFor(x => x.Precio, f => random.Next(0, 700))
                .RuleFor(x => x.EsPromo, f => false)
                .Generate(1)
                .FirstOrDefault();

            return fakeObject;
        }
    }
}
