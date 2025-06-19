using ecommerce.Models;
using ecommerce.Repository;
using ecommerce.ViewModels;
using ecommerce.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productrepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly Context _context;  

        public ProductService(IProductRepository _repository, ICategoryRepository categoryRepository, Context context)
        {
            productrepository = _repository;
            this.categoryRepository = categoryRepository;
            _context = context;
        }

        public List<Product> GetAll(string? include = null)
        {
            return include == null ? productrepository.GetAll() : productrepository.GetAll(include);
        }

        public List<Product> GetPageList(int skipstep, int pageSize)
        {
            return productrepository.GetPageList(skipstep, pageSize);
        }

        public Product Get(int id)
        {
            return productrepository.Get(id);
        }

        public List<Product> Get(Func<Product, bool> where)
        {
            return productrepository.Get(where);
        }

        public void Insert(Product product)
        {
            productrepository.Insert(product);
            productrepository.Save();
        }

        public void Update(Product product)
        {
            productrepository.Update(product);
            productrepository.Save();
        }

        public void Delete(Product product)
        {
            productrepository.Delete(product);
            productrepository.Save();
        }

        public void Save()
        {
            productrepository.Save();
        }

        public async Task<ProductWithCatNameAndComments> WithCatNameAndComments(int id)
        {
            Product p = productrepository.Get(id);
            if (p == null) return null;

            Category c = categoryRepository.Get(p.CategoryId);

            return new ProductWithCatNameAndComments
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Color = p.Color,
                Description = p.Description,
                Price = p.Price,
                Quantity = p.Quantity,
                Rating = p.Rating,
                CateName = c?.Name
            };
        }

        public void Insert(ProductWithListOfCatesViewModel p)
        {
            Product product = new()
            {
                Id = p.Id,
                Name = p.Name,
                Color = p.Color,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Quantity = p.Quantity,
                Rating = p.Rating,
                CategoryId = p.CategoryId
            };

            productrepository.Insert(product);
            productrepository.Save();
        }

        public ProductWithListOfCatesViewModel GetViewModel(int id)
        {
            Product p = productrepository.Get(id);
            if (p == null) return null;

            return new ProductWithListOfCatesViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Color = p.Color,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Quantity = p.Quantity,
                Rating = p.Rating,
                CategoryId = p.CategoryId,
                categories = categoryRepository.GetAll()
            };
        }

        public void Update(ProductWithListOfCatesViewModel p)
        {
            Product product = productrepository.Get(p.Id);
            if (product == null) return;

            product.Name = p.Name;
            product.Color = p.Color;
            product.Description = p.Description;
            product.ImageUrl = p.ImageUrl;
            product.Price = p.Price;
            product.Quantity = p.Quantity;
            product.Rating = p.Rating;
            product.CategoryId = p.CategoryId;

            productrepository.Update(product);
            productrepository.Save();
        }

        public List<Product> GetProductsByCategoryId(int categoryId)
        {
            return _context.Product.Where(p => p.CategoryId == categoryId).ToList();
        }
    }
}
