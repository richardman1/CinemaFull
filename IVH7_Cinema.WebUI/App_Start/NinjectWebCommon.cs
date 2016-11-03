[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(IVH7_Cinema.WebUI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(IVH7_Cinema.WebUI.App_Start.NinjectWebCommon), "Stop")]

namespace IVH7_Cinema.WebUI.App_Start {
    using System;
    using System.Web;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Moq;

    using Ninject;
    using Ninject.Web.Common;

    using IVH7_Cinema.Domain.Abstract;
    using IVH7_Cinema.Domain.Concrete;
    using IVH7_Cinema.Domain.Entities;
    using System.Diagnostics.CodeAnalysis;
    [ExcludeFromCodeCoverage] 
    public static class NinjectWebCommon {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop() {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel() {
            var kernel = new StandardKernel();
            try {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            } catch {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel) {
            System.Web.Mvc.DependencyResolver.SetResolver(new IVH7_Cinema.WebUI.Infrastructure.NinjectDependencyResolver(kernel));

            /*Mock<ICinemaRepository> mock = new Mock<ICinemaRepository>();
            mock.Setup(m => m.Movies).Returns(new List<Movie> {
                new Movie { Title = "Hunger Games", Duration = 240, Genre = "Action"},
                new Movie { Title = "50 Shades of Grey", Duration = 120, Genre = "Romance"}
            });
            kernel.Bind<ICinemaRepository>().ToConstant(mock.Object);*/

        }
    }
}

