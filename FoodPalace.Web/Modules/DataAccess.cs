using FoodPalace.Infrastructure;
using FoodPalace.Infrastructure.Entities;
using FoodPalace.Infrastructure.Intaface;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FoodPalace.Web.Modules
{
    public class DataAccess : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<DataContext>().InRequestScope();
            Bind<IDataRepository>().To<EntityRepository>().InRequestScope();
            Bind<IProductRepository>().To<EFProductRepository>().InRequestScope();
            //Bind<IAccountQueries>().To<AccountQueries>();

        }
    }
}