﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        ISP_Call SP_Call { get; }
        ICoverTypeRepository coverType { get; }
        IProductRepository product { get; }
        ICompanyRepository company { get; }
        IApplicationUserRepository applicationUser { get; }
        IOrderDetailsRepository orderDetailsRepository { get; }
        IOrderHeaderRepository orderHeaderRepository { get; }
        IShoppingCartRepository shoppingCartRepository { get; }
        void Save();
    }
}
