using VillageMaker.ProductService.Data.Interfaces;
using VillageMaker.ProductService.Domain.Models;

namespace VillageMaker.ProductService.Data.Repositories;

public class ProductRepo : IProductRepo
{
    private readonly AppDbContext _context;

    public ProductRepo(AppDbContext context)
    {
        _context = context;
    }
    public bool SaveChanges()
    {
        return (_context.SaveChanges() >= 0);
    }

    public IEnumerable<Maker> GetAllMakers()
    {
        return _context.Makers.ToList();
    }

    public void CreateMaker(Maker maker)
    {
        if (maker == null)
            throw new ArgumentNullException(nameof(maker));

        _context.Makers.Add(maker);
    }

    public bool MakerExists(int makerId)
    {
        return _context.Makers.Any(x => x.Id == makerId);
    }

    public bool ExternalMakerExists(int externalMakerId)
    {
        return _context.Makers.Any(x => x.ExternalId == externalMakerId);
    }

    public IEnumerable<Product> GetProductsForMaker(int makerId)
    {
        return _context.Products
            .Where(x => x.MakerId == makerId)
            .OrderBy(x => x.Category);
    }

    public Product GetProductForMaker(int makerId, int productId)
    {
        return _context.Products.FirstOrDefault(x => x.MakerId == makerId && x.Id == productId);
    }

    public void CreateProduct(int makerId, Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        product.MakerId = makerId;
        _context.Products.Add(product);
    }
}