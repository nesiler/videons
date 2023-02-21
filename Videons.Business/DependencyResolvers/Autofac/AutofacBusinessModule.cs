using Autofac;
using Videons.Business.Abstract;
using Videons.Business.Concrete;
using Videons.Core.Utilities.Security.Jwt;
using Videons.DataAccess.Abstract;
using Videons.DataAccess.Concrete.EntityFramework;

namespace Videons.Business.DependencyResolvers.Autofac;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ChannelManager>().As<IChannelService>();
        builder.RegisterType<EfChannelDal>().As<IChannelDal>();

        builder.RegisterType<CategoryManager>().As<ICategoryService>();
        builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

        builder.RegisterType<UserManager>().As<IUserService>();
        builder.RegisterType<EfUserDal>().As<IUserDal>();

        builder.RegisterType<AuthManager>().As<IAuthService>();
        builder.RegisterType<JwtHelper>().As<ITokenHelper>();
    }
}