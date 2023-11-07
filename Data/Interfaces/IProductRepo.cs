using VillageMaker.ProductService.Domain.Models;

namespace VillageMaker.ProductService.Data.Interfaces;

public interface IProductRepo
{
    bool SaveChanges();
    
    // Maker Commands
    IEnumerable<Maker> GetAllMakers();
    void CreateMaker(Maker maker);
    bool MakerExists(int makerId);
    
    // Product Commands
    IEnumerable<Product> GetProductsForMaker(int makerId);
    Product GetProductForMaker(int makerId, int productId);
    void CreateProduct(int makerId, Product product);
}