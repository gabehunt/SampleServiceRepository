#nullable enable

using GabeSample.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;

namespace GabeSample.Application.Repository
{
	/// <summary>
	/// Product Repository.
	/// </summary>
	public class ProductRepository : IProductRepository
	{
		private readonly ILogger<ProductRepository>? _logger = null;

		private ConcurrentDictionary<string, Product>? _products = null;

		/// <summary>
		/// Products
		/// </summary>
		public ConcurrentDictionary<string, Product> Products
		{
			get
			{
				if (_products == null)
				{
					//Simplified example, but normally get data from source.
					_products = new();

					_logger.LogInformation($"Initialized {nameof(Products)}.");
				}
				return _products;
			}
		}

		/// <summary>
		/// Product Repository
		/// </summary>
		/// <param name="logger"></param>
		public ProductRepository(ILogger<ProductRepository> logger)
		{
			_ = logger ?? throw new ArgumentNullException(nameof(logger));

			_logger = logger;
		}


	}
}
