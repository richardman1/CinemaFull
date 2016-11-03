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

namespace IVH7_Cinema.WebUI.Website.Infrastructure
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
            if (System.Threading.Thread.CurrentThread.CurrentCulture.ToString().Equals("nl-NL"))
            {
             kernel.Bind<IPrinter>().To<PDFPrinter>();
             kernel.Bind<IMailer>().To<Mailer>();
            }
            else if (System.Threading.Thread.CurrentThread.CurrentCulture.ToString().Equals("en-GB"))
            {
                kernel.Bind<IPrinter>().To<PDFPrinterEN>();
                kernel.Bind<IMailer>().To<MailerEN>();
            }
            else
            {
                kernel.Bind<IPrinter>().To<PDFPrinter>();
                kernel.Bind<IMailer>().To<Mailer>();
            }
        }
    }
}