using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IVH7_Cinema.Domain.Concrete;
using IVH7_Cinema.Domain.Abstract;
using IVH7_Cinema.Domain.Entities;
using Ninject;
using System.Diagnostics.CodeAnalysis;

namespace IVH7_Cinema.WebUI.Infrastructure
{
    [ExcludeFromCodeCoverage] 
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type
        serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<ICinemaRepository>().To<EFCinemaRepository>();
            kernel.Bind<IMailer>().To<Mailer>();
            kernel.Bind<IPrinter>().To<PDFPrinter>();
        }
    }
}