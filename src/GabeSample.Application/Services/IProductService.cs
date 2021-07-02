using GabeSample.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GabeSample.Application.Services
{
	public interface IProductService
	{
		ValueTask<IEnumerable<Product>> GetAllAsync();

		ValueTask<Product> GetAsync(string sku);
	}
}