using Amazon.Core.Entities;
using Amazon.Services.ProductService.Dto;
using Amazon.Services.Utilities;
using Amazone.Infrastructure.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.ProductService
{
	public class ProductService : IProductService
	{

		private readonly IGenericRepository<Product> _productRepo;
		private readonly IMapper _mapper;

		public ProductService(IMapper mapper, IGenericRepository<Product> productRepo)
		{
			_mapper = mapper;
			_productRepo = productRepo;
		}


		public async Task<ProductToReturnDto> AddProduct(ProductDto productDto)
		{
			try
			{
				var mappedProduct = _mapper.Map<Product>(productDto);
				mappedProduct.PictureUrl = await DocumentSettings.UploadFile(productDto.ImageFile, "productImages");

				await _productRepo.Add(mappedProduct);

				var productToReturn = _mapper.Map<ProductToReturnDto>(mappedProduct);
				return productToReturn;
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);

			}	
		}

		public async Task<IReadOnlyList<ProductToReturnDto>> DeleteProduct(int id)
		{
			var product = await _productRepo.GetByIdAsync(id);
			if (product is null)
				return null;

			await _productRepo.Delete(product);
			DocumentSettings.DeleteFile("Uploads", product.PictureUrl);

			var products = await GetAllProductsAsync();
			return products;
		}

		public async Task<IReadOnlyList<ProductToReturnDto>> GetAllProductsAsync()
		{
			var products = await _productRepo.GetAllAsync();
			var mappedProdcts = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);

			return mappedProdcts;
		}

		public async Task<ProductToReturnDto> GetProductByIdAsync(int? id)
		{
			var product = await _productRepo.GetByIdAsync(id);
			var mappedProduct = _mapper.Map<ProductToReturnDto>(product);
			return mappedProduct;
		}

		public async Task<ProductToReturnDto> UpdateProduct(int id, ProductDto productDto)
		{
			var existintProduct = await _productRepo.GetByIdAsync(id);
			if (existintProduct == null)
				throw new Exception("Product Doesn't Exist");

			if (productDto.ImageFile != null)
			{
				DocumentSettings.DeleteFile("Uploads", existintProduct.PictureUrl);
				existintProduct.PictureUrl = await DocumentSettings.UploadFile(productDto.ImageFile, "productImages");
			}

			_mapper.Map(productDto, existintProduct);


			await _productRepo.Update(existintProduct);
			return _mapper.Map<ProductToReturnDto>(existintProduct);
		}
	}
}
