using System;
using Ninject;
using TM.Database;

namespace TM.API.Ninject
{
    public static class NinjectConfig
    {
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            try
            {
                kernel.Bind<IUnitOfWork>().To<EfUnitOfWork>();
                return kernel;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}