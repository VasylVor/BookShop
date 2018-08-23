using DomainBookShop.Abstract;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DomainBookShop.Concrete;
namespace WebUi.Infastructure
{
    public class NinjectDependecyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependecyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBinding();
        }

        private void AddBinding()
        {
            _kernel.Bind<IBookRepository>().To<BookDbRepository>();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}