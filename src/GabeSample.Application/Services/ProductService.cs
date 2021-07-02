#nullable enable

using GabeSample.Application.Repository;
using GabeSample.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GabeSample.Application.Services
{
	/// <summary>
	/// Product Service.  Simplified example of Service/Repository Pattern.
	/// </summary>
	public class ProductService : IProductService
	{
		private readonly ILogger<ProductService> _logger;
		private readonly IProductRepository _productRepository;

		/// <summary>
		/// Product Service
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="productRepository"></param>
		public ProductService(ILogger<ProductService> logger, IProductRepository productRepository)
		{
			_ = logger ?? throw new ArgumentNullException(nameof(logger));
			_ = productRepository ?? throw new ArgumentNullException(nameof(productRepository));

			_logger = logger;
			_productRepository = productRepository;
		}

		/// <summary>
		/// Get all Products.  Returns empty if no Products found.
		/// </summary>
		/// <returns></returns>
		public ValueTask<IEnumerable<Product?>> GetAllAsync()
		{
			IEnumerable<Product?> products = _productRepository
				.Products
				.Select(kvpSkuProduct => kvpSkuProduct.Value);

			_logger.LogDebug($"Returned {products.Count()} Products.");

			return ValueTask.FromResult(products);
		}

		/// <summary>
		/// Get Product by Sku.  Returns null if no Product found.
		/// </summary>
		/// <param name="sku"></param>
		/// <returns></returns>
		public ValueTask<Product?> GetAsync(string sku)
		{

			_productRepository
				.Products
				.TryGetValue(sku, out Product? product);

			//Simplified example, but you can add business logic here.

			string productResult = (product == null)
				? "NOT FOUND"
				: "FOUND";

			_logger.LogDebug($"Product {sku} {productResult}");

			return ValueTask.FromResult(product);
		}
	}
}
