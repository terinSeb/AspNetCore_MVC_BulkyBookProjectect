using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(Company company)
        {
            var ObjFromDb = _db.Companies.FirstOrDefault(x => x.Id == company.Id);
            if(ObjFromDb != null)
            {
                ObjFromDb.City = company.City;
                ObjFromDb.IsAutherizedCompany = company.IsAutherizedCompany;
                ObjFromDb.Name = company.Name;
                ObjFromDb.PhoneNumber = company.PhoneNumber;
                ObjFromDb.PostCode = company.PostCode;
                ObjFromDb.State = company.State;
                ObjFromDb.StreetAddress = company.StreetAddress;                
            }
        }
    }
}
