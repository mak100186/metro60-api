using Metro60.Core.Data;
using Metro60.Core.Extensions;
using Metro60.Core.Models;

using Microsoft.EntityFrameworkCore;

namespace Metro60.Core.Services;

public interface IProductService
{
    Task<ProductModel> AddProduct(ProductModel model);
    Task<ProductModel> UpdateProduct(int id, ProductModel model);
    Task<List<ProductModel>> GetAll();
    Task<ProductModel?> Get(int id);
}

public class ProductService : IProductService
{
    private readonly MetroDbContext _context;

    public ProductService(MetroDbContext context)
    {
        _context = context;
    }
    public async Task<ProductModel> AddProduct(ProductModel model)
    {
        var product = model.ToDbo();

        await _context.Products.AddAsync(product);

        await _context.SaveChangesAsync();

        return product.ToDto();
    }
    public async Task<ProductModel?> Get(int id)
    {
        var product = await _context.Products.SingleOrDefaultAsync(product => product.Id == id);
        
        return product?.ToDto();
    }
    public async Task<List<ProductModel>> GetAll()
    {
        var products = await _context.Products.ToListAsync();

        return products.Select(dbo => dbo.ToDto()).ToList();
    }
    public async Task<ProductModel> UpdateProduct(int id, ProductModel model)
    {
        var product = await _context.Products.SingleOrDefaultAsync(product => product.Id == id);

        if (product == null)
        {
            throw new ArgumentException("Given Id not found.", nameof(id));
        }

        product.UpdateWith(model);

        await _context.SaveChangesAsync();

        return product.ToDto();
    }
}
