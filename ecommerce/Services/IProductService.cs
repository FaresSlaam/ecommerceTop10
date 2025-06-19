using ecommerce.Models;
using ecommerce.Repository;
using ecommerce.ViewModels.Product;

namespace ecommerce.Services
{
    public interface IProductService : IRepository<Product>
    {
        public List<Product> GetPageList(int skipstep, int pageSize);
        Task<ProductWithCatNameAndComments> WithCatNameAndComments(int id);
        void Insert(ProductWithListOfCatesViewModel p);
        void Update(ProductWithListOfCatesViewModel p);
        ProductWithListOfCatesViewModel GetViewModel(int id);

        // ✅ تعديل الدالة بحيث ترجع List<Product>
        List<Product> GetProductsByCategoryId(int id);
    }
}
