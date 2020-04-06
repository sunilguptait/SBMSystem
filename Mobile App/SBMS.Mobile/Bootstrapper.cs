using SBMS.Mobile.Services;
using SBMS.Mobile.ViewModels;
using SBMS.Mobile.ViewModels.User;
using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using SBMS.Mobile.Services.User;
using SBMS.Mobile.Services.Caching;
using SBMS.Mobile.Services.Common;
using SBMS.Mobile.Services.Student;
using SBMS.Mobile.ViewModels.Student;
using SBMS.Mobile.ViewModels.Orders;
using SBMS.Mobile.Services.Order;
using SBMS.Mobile.ViewModels.BookSeller;

namespace SBMS.Mobile
{
    public static class Bootstrapper
    {
        public static IPageService PageService { get { return App.Container.Resolve<IPageService>(); } }

        public static void Initialize()
        {
            if (App.Container == null)
            {
                var builder = new ContainerBuilder();
                //builder.RegisterType<PageService>().As<IPageService>().SingleInstance();
                builder.RegisterType<PageService>().As<IPageService>();
                builder.RegisterType<UserService>().As<IUserService>();
                builder.RegisterType<LocalCacheService>().As<ICacheService>();
                builder.RegisterType<CommonService>().As<ICommonService>();
                builder.RegisterType<StudentService>().As<IStudentService>();
                builder.RegisterType<OrderService>().As<IOrderService>();

                //
                builder.RegisterType<HomePageViewModel>().AsSelf();
                builder.RegisterType<LoginViewModel>().AsSelf();
                builder.RegisterType<RegisterViewModel>().AsSelf();
          
                builder.RegisterType<ChangePasswordViewModel>().AsSelf();
                builder.RegisterType<ProfileViewModel>().AsSelf();
                builder.RegisterType<StudentViewModel>().AsSelf();
                builder.RegisterType<BuyBookViewModel>().AsSelf();
                builder.RegisterType<AddEditStudentViewModel>().AsSelf();
                builder.RegisterType<ViewOrderViewModel>().AsSelf();
                builder.RegisterType<OrdersViewModel>().AsSelf();
                builder.RegisterType<BookSellerHomePageViewModel>().AsSelf();
                builder.RegisterType<ParentsRegistrationViewModel>().AsSelf();
                builder.RegisterType<ParentsProfileViewModel>().AsSelf();
                

                App.Container = builder.Build();

            }
        }
    }
}
