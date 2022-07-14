using AutoMapper;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Interfaces.IServices;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using FrizzApp.Data.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace FirzzApp.Business.Services
{
    public class ProductStatusService : IProductStatusService
    {
        private readonly IProductStatusRepository _repository;
        private readonly IMapper _mapper;

        public ProductStatusService(IProductStatusRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public List<GetProductStatusResponseDto> GetAll()
        {
            var result = _repository.GetAll();

            var response = _mapper.Map<List<GetProductStatusResponseDto>>(result);

            return response;
        }


        public Result<string> Create(CreateProductStatusDto dto)
        {
            var entity = _mapper.Map<ProductStatus>(dto);

            _repository.Create(entity);

            return Result<string>.Success($"{entity.Name}");
        }


        public Result Delete(int id)
        {
            var result = _repository.Delete(id);

            return result
                ? Result.Success()
                : Result.Fail(default);
        }
    }
}
