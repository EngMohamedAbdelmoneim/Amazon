﻿using Amazon.Core.Entities;
using Amazon.Services.ProductService.Dto;
using Amazon.Services.Utilities;
using Amazone.Infrastructure.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Amazon.Services.ProductService
{
	public class ProductService : IProductService
	{

		private readonly IProductRepository _productRepo;
		private readonly IMapper _mapper;

		public ProductService(IMapper mapper, IProductRepository productRepo)
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

                await HandleProductImages(productDto.ImagesFiles, mappedProduct);

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

		public async Task<ProductToReturnDto> GetProductByIdAsync(int id)
		{
			var product = await _productRepo.GetByIdAsync(id);
			var mappedProduct = _mapper.Map<ProductToReturnDto>(product);
			return mappedProduct;
		}

		public async Task<IReadOnlyList<ProductToReturnDto>> GetProductsByBrandIdAsync(int id)
		{
			var products = await _productRepo.SearchByBrandAsync(id);
			var mappedProdcts = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);

			return mappedProdcts;
		}

		public async Task<IReadOnlyList<ProductToReturnDto>> GetProductsByBrandNameAsync(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return await GetAllProductsAsync();
			}
			var products = await _productRepo.SearchByBrandNameAsync(name);
			var mappedProdcts = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);

			return mappedProdcts;
		}

		public async Task<IReadOnlyList<ProductToReturnDto>> GetProductsByCategoryIdAndNameAsync(string name, int? id)
		{
		
			if(id == 0 || id == null)
			{
				if (string.IsNullOrWhiteSpace(name)) 
				{
					return await GetAllProductsAsync();
				}
				return await GetProductsByNameAsync(name);
			}


			if (id.HasValue && id != 0)
			{
				if (string.IsNullOrWhiteSpace(name))
				{
					return await GetProductsByCategoryIdAsync(id.Value);
				}
			}
			var products = await _productRepo.SearchByCategoryAndProductNameAsync(name,id);
			var mappedProdcts = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
			return mappedProdcts;
		}

		public async Task<IReadOnlyList<ProductToReturnDto>> GetProductsByCategoryIdAsync(int id)
		{

			var products = await _productRepo.SearchByCategoryAsync(id);
			var mappedProdcts = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);

			return mappedProdcts;
		}

		public async Task<IReadOnlyList<ProductToReturnDto>> GetProductsByCategoryNameAsync(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return await GetAllProductsAsync();
			}
			var products = await _productRepo.SearchByCategoryNameAsync(name);
			var mappedProdcts = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);

			return mappedProdcts;
		}

		public async Task<IReadOnlyList<ProductToReturnDto>> GetProductsByNameAsync(string name)
		{
			
			if (string.IsNullOrWhiteSpace(name))
			{
				return await GetAllProductsAsync();
			}
			var products = await _productRepo.SearchByProductNameAsync(name);
			var mappedProdcts = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);

			return mappedProdcts;
		}

		public async Task<IReadOnlyList<ProductToReturnDto>> SearchByStringAsync(string str)
		{
			if (string.IsNullOrWhiteSpace(str))
			{
				return await GetAllProductsAsync();
			}
			var products = await _productRepo.SearchByStringAsync(str);
			var mappedProdcts = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);

			return mappedProdcts;
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

			// Deleting additional images
            if (productDto.ImagesFiles != null && productDto.ImagesFiles.Count > 0)
            {
                foreach (var image in existintProduct.Images.ToList())
                {
					DocumentSettings.DeleteFile("productImages", image.ImagePath);
					existintProduct.Images.Remove(image);
                }

                await HandleProductImages(productDto.ImagesFiles, existintProduct);
            }

			_mapper.Map(productDto, existintProduct);

			await _productRepo.Update(existintProduct);
			return _mapper.Map<ProductToReturnDto>(existintProduct);
		}

		// Helper method to add other product images
        private async Task HandleProductImages(ICollection<IFormFile> imagesFiles, Product product)
        {
            if (imagesFiles != null && imagesFiles.Count() > 0)
            {
                foreach (var image in imagesFiles)
                {
                    var productImage = new ProductImages
                    {
                        ProductId = product.Id,
                        ImagePath = await DocumentSettings.UploadFile(image, "productImages")
                    };
                    product.Images.Add(productImage);
                }
            }
        }
    }
}
