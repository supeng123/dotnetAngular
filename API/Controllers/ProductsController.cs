using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specification;
using API.Dtos;
using AutoMapper;
using API.Errors;
using API.Helpers;

namespace API.Controllers
{
    public class ProductsController : BaseApiController {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _productRep;
        private readonly IGenericRepository<ProductBrand> _brandRep;
        private readonly IGenericRepository<ProductType> _typeRep;

        public ProductsController(
            IMapper mapper,
            IGenericRepository<Product> productRep,
             IGenericRepository<ProductBrand> brandRep,
             IGenericRepository<ProductType> typeRep)
        {
            this._mapper = mapper;
            this._productRep = productRep;
            this._brandRep = brandRep;
            this._typeRep = typeRep;
        }
 
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductsSpecParams productParams) {
            var spec = new ProductsWIthTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductWithFiltersForCountSpecification(productParams);
            var totalItems = await _productRep.CountAsync(countSpec);
            var products =  await _productRep.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));

            // return products.Select(product => _mapper.Map<Product, ProductToReturnDto>(product)).ToList();

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id) {
            var spec = new ProductsWIthTypesAndBrandsSpecification(id);
            var product =  await _productRep.GetEntityWithSpec(spec);

            if (product == null) return NotFound(new ApiResponse(404));
            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands() {
            return Ok(await _brandRep.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes() {
            return Ok(await _typeRep.ListAllAsync());
        }
    }
}