using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {        

        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            SP_Call = new SP_Call(_db);
            coverType = new CoverTypeRepository(_db);
            product = new ProductRepository(_db);
            company = new CompanyRepository(_db);
            applicationUser = new ApplicationUserRepository(_db);
            orderDetailsRepository = new OrderDetailsrRepository(_db);
            orderHeaderRepository = new OrderHeaderRepository(_db);
            shoppingCartRepository = new ShoppingCartRepository(_db);
        }        

        public ICategoryRepository Category { get; private set; }

        public ISP_Call SP_Call { get; private set; }

        public ICoverTypeRepository coverType { get; private set; }

        public IProductRepository product { get; private set; }
        public ICompanyRepository company { get; private set; }
        public IApplicationUserRepository applicationUser { get; private set; }
        public IOrderHeaderRepository orderHeaderRepository { get; private set; }
        public IOrderDetailsRepository orderDetailsRepository { get; private set; }
        public IShoppingCartRepository shoppingCartRepository { get; private set; }
        public void Dispose()
        {
            _db.Dispose();
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
