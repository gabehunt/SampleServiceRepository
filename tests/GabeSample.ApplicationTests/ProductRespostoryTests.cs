using GabeSample.Application.Repository;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace GabeSample.ApplicationTests
{
	[ExcludeFromCodeCoverage]
	public class ProductRespostoryTests
	{
		private readonly IProductRepository _productRepository;

		public ProductRespostoryTests()
		{
			ILogger<ProductRepository> _logger = Substitute.For<ILogger<ProductRepository>>();
			_productRepository = new ProductRepository(_logger);
		}

		[Fact]
		public void ProductRepository_NotNull_Success()
		{
			// Arrange
			// Act
			// Assert
			Assert.NotNull(_productRepository);
		}

		[Fact]
		public void ProductRepository_Products_NotNull_Success()
		{
			// Arrange
			// Act
			// Assert
			Assert.NotNull(_productRepository.Products);
		}

		[Fact]
		public void ProductRepository_Products_Empty_Success()
		{
			// Arrange
			// Act
			// Assert
			Assert.Empty(_productRepository.Products);
		}
	}
}
