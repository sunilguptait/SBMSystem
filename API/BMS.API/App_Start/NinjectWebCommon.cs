
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(BMS.API.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(BMS.API.App_Start.NinjectWebCommon), "Stop")]

namespace BMS.API.App_Start
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using BMS.Services.Book;
    using BMS.Services.BookSeller;
    using BMS.Services.Class;
    using BMS.Services.Common;
    using BMS.Services.EmailSender;
    using BMS.Services.Order;
    using BMS.Services.Publisher;
    using BMS.Services.School;
    using BMS.Services.Student;
    using BMS.Services.Users;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Modules;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {


            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();



            RegisterServices(kernel);

            NinjectDependencyResolver ninjectResolver = new NinjectDependencyResolver(kernel);
            DependencyResolver.SetResolver(ninjectResolver); //MVC 
            GlobalConfiguration.Configuration.DependencyResolver = ninjectResolver; //Web API

            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<ICommonService>().To<CommonService>();
            kernel.Bind<IStudentService>().To<StudentService>();
            kernel.Bind<IOrderService>().To<OrderService>();
            kernel.Bind<IBookSellerService>().To<BookSellerService>();
            kernel.Bind<IBookService>().To<BookService>();
            kernel.Bind<IPublisherService>().To<PublisherService>();
            kernel.Bind<IClassService>().To<ClassService>();
            kernel.Bind<ISchoolService>().To<SchoolService>();
            kernel.Bind<IEmailSenderService>().To<EmailSenderService>();
            
            //var modules = new List<INinjectModule>
            //    {
            //        new ServiceModule()
            //    };
            //kernel.Load(modules);
        }
    }
}