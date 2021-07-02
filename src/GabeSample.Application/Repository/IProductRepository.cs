using GabeSample.Domain;
using System.Collections.Concurrent;

namespace GabeSample.Application.Repository
{
	public interface IProductRepository
	{
		ConcurrentDictionary<string, Product> Products { get; }
	}
}