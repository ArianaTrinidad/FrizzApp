using FrizzApp.Data.Entities;
using FrizzApp.Data.Enums;
using FrizzApp.Data.Extensions;
using FrizzApp.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace FrizzApp.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        private readonly IHttpContextAccessor _httpContextAccesor;

        private readonly IConfiguration _configuration;

        public ProductRepository(DataContext context, IHttpContextAccessor httpContext, IConfiguration configuration)
        {
            _context = context;
            _httpContextAccesor = httpContext;
            _configuration = configuration;
        }


        public List<Product> GetAll(int pageNumber, int pageSize, string palabraClave, decimal precioMin, decimal precioMax, int categoriaId)
        {
            int take = pageSize > 0 ? pageSize : 100;
            int skip = pageNumber > 0 ? (pageNumber - 1) * (pageSize > 0 ? pageSize : 100) : 0;


            var partialResult = _context.Products
                .Where(x => x.ProductStatusId != (int)ProductStatusEnum.Deleted);

            if (string.IsNullOrWhiteSpace(palabraClave) == false)
            {
                partialResult = partialResult
                    .Where(x => x.Name.Contains(palabraClave)
                             || x.Description.Contains(palabraClave));
            }

            if (precioMin != default)
            {
                partialResult = partialResult
                    .Where(x => x.Price >= precioMin);
            }

            if (precioMax != default)
            {
                partialResult = partialResult
                    .Where(x => x.Price <= precioMax);
            }

            if (categoriaId != default)
            {
                partialResult = partialResult
                    .Where(x => x.CategoryId != categoriaId);
            }


            var result = partialResult
                .Include(x => x.Category)
                .Skip(skip)
                .Take(take)
                .OrderBy(x => x.Id)
                .ToList();


            return result;
        }

        public Product GetById(int id)
        {
            var result = _context.Products.FirstOrDefault(x => x.Id == id);
            return result;
        }


        public List<Product> GetAllQueryableExtesion(int pageNumber, int pageSize, string palabraClave, decimal precioMin, decimal precioMax, int categoriaId)
        {
            int take = pageSize > 0 ? pageSize : 100;
            int skip = pageNumber > 0 ? (pageNumber - 1) * (pageSize > 0 ? pageSize : 100) : 0;

            return _context.Products
                .Where(x => x.ProductStatusId != (int)ProductStatusEnum.Deleted)
                //.WhereIf( condition: , func: lambda expression )
                .WhereIf(string.IsNullOrWhiteSpace(palabraClave) == false, x => x.Name.Contains(palabraClave) || x.Description.Contains(palabraClave))
                .WhereIf(precioMin != default, x => x.Price >= precioMin)
                .WhereIf(precioMax != default, x => x.Price <= precioMax)
                .WhereIf(categoriaId != default, x => x.CategoryId != categoriaId)
                .Skip(skip)
                .Take(take)
                .OrderBy(x => x.Id)
                .ToList();
        }

        public void Create(Product entity)
        {
            entity.SetCreateAuditFields(_httpContextAccesor.GetUserFromToken());

            _context.Products.Add(entity);
            _context.SaveChanges();
        }


        public void Update(Product entityFromDto)
        {
            var entity = _context.Products.Where(x => x.Id == entityFromDto.Id).FirstOrDefault();

            if (entity == null)
                return;

            if (!string.IsNullOrWhiteSpace(entityFromDto.Name))
            {
                entity.Name = entityFromDto.Name;
            }

            if (!string.IsNullOrWhiteSpace(entityFromDto.Description))
            {
                entity.Description = entityFromDto.Description;
            }

            if (!string.IsNullOrWhiteSpace(entityFromDto.Notes))
            {
                entity.Notes = entityFromDto.Notes;
            }

            if (!string.IsNullOrWhiteSpace(entityFromDto.Presentation))
            {
                entity.Presentation = entityFromDto.Presentation;
            }

            if (!string.IsNullOrWhiteSpace(entityFromDto.ImageUrl))
            {
                entity.ImageUrl = entityFromDto.ImageUrl;
            }

            if (entityFromDto.Price != default)
            {
                entity.Price = entityFromDto.Price;
            }

            if (entityFromDto.IsPromo.HasValue)
            {
                entity.IsPromo = entityFromDto.IsPromo;
            }

            if (entityFromDto.Category != default)
            {
                entity.Category = entityFromDto.Category;
            }

            if (entityFromDto.ProductStatusId != default)
            {
                entity.ProductStatusId = entityFromDto.ProductStatusId;
            }

            entity.SetUpdateAuditFields(_httpContextAccesor.GetUserFromToken());

            _context.Products.Update(entity);
            _context.SaveChanges();
        }


        public bool ChangeStatus(int id)
        {
            var entity = _context.Products.Where(x => x.Id == id).FirstOrDefault();

            if (entity != null)
            {
                entity.SetUpdateAuditFields(_httpContextAccesor.GetUserFromToken());
                entity.ProductStatusId = (int)ProductStatusEnum.WithoutStock;

                _context.Update(entity);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public string Delete(int id)
        {
            var entity = _context.Products.Where(x => x.Id == id).FirstOrDefault();

            if (entity != null)
            {
                entity.SetDeleteAuditFields(_httpContextAccesor.GetUserFromToken());
                entity.ProductStatusId = (int)ProductStatusEnum.Deleted;

                _context.Products.Update(entity);
                _context.SaveChanges();

                return "Entity deleted correctly";
            }
            else
            {
                return "The entity does not exists";
            }
        }

        public void SendMail()
        {
            MailMessage mail = new MailMessage();

            string usermail = _configuration["usuariogmail"];
            string passwordmail = _configuration["passwordgmail"];
            string addressee = _configuration["adminGmail"];
            var deliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            string subject = $"A product was deleted.";
            string body = $"The product information... ";


            mail.From = new MailAddress(usermail);
            mail.To.Add(new MailAddress(addressee));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            string smtpserver = _configuration["hostGmail"];
            int port = int.Parse(_configuration["portGmail"]);
            bool ssl = bool.Parse(_configuration["sslGmail"]);
            bool defaultCreadentials = bool.Parse(_configuration["defaultcredentialsGmail"]);

            SmtpClient smtpClient = new SmtpClient();

            smtpClient.Host = smtpserver;// smpt.gmail.com
            smtpClient.Port = port; //587
            smtpClient.EnableSsl = ssl; //true
            smtpClient.UseDefaultCredentials = defaultCreadentials; //false
            smtpClient.DeliveryMethod = deliveryMethod; //System.Net.Mail.SmtpDeliveryMethod.Network


            NetworkCredential usercredential = new NetworkCredential(usermail, passwordmail);

            smtpClient.UseDefaultCredentials = defaultCreadentials;
            smtpClient.Send(mail);
        }
    }
}
