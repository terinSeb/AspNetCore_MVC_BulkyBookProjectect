using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void update(Product product)
        {
            var ObjFromDb = _db.Products.FirstOrDefault(x => x.Id == product.Id);
            if(ObjFromDb != null)
            {
                if(product.ImageUrl != null)
                {
                    ObjFromDb.ImageUrl = product.ImageUrl;
                }
                ObjFromDb.ISBN = product.ISBN;
                ObjFromDb.ListPrice = product.ListPrice;
                ObjFromDb.Price = product.Price;
                ObjFromDb.Price50 = product.Price50;
                ObjFromDb.Price100 = product.Price100;
                ObjFromDb.Title = product.Title;
                ObjFromDb.Description = product.Description;
                ObjFromDb.CoverTypeId = product.CoverTypeId;
                ObjFromDb.CategoryId = product.CategoryId;
                ObjFromDb.Author = product.Author;
            }
        }
    }
}
