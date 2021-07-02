using Bogus;
using FluentAssertions;
using GabeSample.Application.Repository;
using GabeSample.Application.Services;
using GabeSample.Domain;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GabeSample.ApplicationTests
{
	[ExcludeFromCodeCoverage]
	public class ProductServiceTests
	{
		private readonly ConcurrentDictionary<string, Domain.Product> _fakeProducts;
		private readonly ILogger<ProductService> _logger;
		private readonly IProductRepository _productRepository;
		private readonly ProductService _productService;

		private int skuIndex = 0;
		public string NextSku
		{
			get
			{
				skuIndex += 1;
				return $"ITM{skuIndex}";
			}
		}

		/// <summary>
		/// Common setup for all Product Service Tests
		/// </summary>
		public ProductServiceTests()
		{
			Faker<Product> faker = new Faker<Product>()
				.RuleFor(o => o.Sku, f => NextSku)
				.RuleFor(o => o.Description, f => f.Lorem.Sentences(1))
				.RuleFor(o => o.Name, f => f.Commerce.ProductName()
			);

			_fakeProducts = new ConcurrentDictionary<string, Product>(
				faker.Generate(3)
				.ToDictionary(keySelector => keySelector.Sku));

			_productRepository = Substitute.For<IProductRepository>();
			_productRepository.Products.Returns(_fakeProducts);

			_logger = Substitute.For<ILogger<ProductService>>();

			_productService = new(_logger, _productRepository);

		}

		[Fact]
		public void ProductService_Ctor_Inject_Null_Logger_Throws_Exception()
		{
			// Arrange
			ILogger<ProductService> logger = null;
			Action productServiceCtor = () => new ProductService(logger, _productRepository);
			// Act
			// Assert
			productServiceCtor.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void ProductService_Ctor_Inject_Null_Repository_Throws_Exception()
		{
			// Arrange
			IProductRepository productRepository = null;
			Action productServiceCtor = () => new ProductService(_logger, productRepository);
			// Act
			// Assert
			productServiceCtor.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public async Task ProductService_GetAllAsync_Calls_Repository_Products_Success()
		{
			// Arrange
			// Act
			_ = await _productService.GetAllAsync();
			// Assert
			_ = _productRepository.Received(1).Products;
		}

		[Fact]
		public async Task ProductService_GetAllAsync_Returns_Expected_Count_Success()
		{
			// Arrange
			// Act
			IEnumerable<Product> products = await _productService.GetAllAsync();
			// Assert
			Assert.Equal(_fakeProducts.Count, products.Count());
		}

		[Fact]
		public async Task ProductService_GetAsync_Calls_Repository_Products_Success()
		{
			// Arrange
			Product fakeProduct = _fakeProducts.Skip(1).First().Value;
			// Act
			_ = await _productService.GetAsync(fakeProduct.Sku);
			// Assert
			_ = _productRepository.Received(1).Products;
		}

		[Theory]
		[InlineData("ITM1")]
		[InlineData("ITM2")]
		[InlineData("ITM3")]
		public async Task ProductService_GetAsync_Returns_Expected_Values_Success(string sku)
		{
			// Arrange
			Product fakeProduct = _fakeProducts[sku];
			// Act
			Product product = await _productService.GetAsync(sku);

			// Assert
			// Add all property accessors for code coverage rather than just compare object hash
			Assert.Equal(fakeProduct.Sku, product.Sku);
			Assert.Equal(fakeProduct.Name, product.Name);
			Assert.Equal(fakeProduct.Description, product.Description);
		}

		[Fact]
		public async Task ProductService_GetAsync_Invalid_Sku_Returns_Null_Success()
		{
			// Arrange
			// Act
			Product product = await _productService.GetAsync("INVALIDSKU");
			// Assert
			Assert.Null(product);
		}

	}
}
