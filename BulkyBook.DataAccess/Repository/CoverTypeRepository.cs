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
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public CoverTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void update(CoverType coverType)
        {
            var FromDb = _db.coverTypes.FirstOrDefault(x => x.Id == coverType.Id);
            if(FromDb != null)
            {
                FromDb.Name = coverType.Name;
            }
        }
    }
}
