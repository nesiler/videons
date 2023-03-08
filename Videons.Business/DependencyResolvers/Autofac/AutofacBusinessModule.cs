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
        builder.RegisterType<ChannelManager>().As<IChannelService>().InstancePerLifetimeScope();
        builder.RegisterType<EfChannelDal>().As<IChannelDal>();

        builder.RegisterType<VideoManager>().As<IVideoService>().InstancePerLifetimeScope();
        builder.RegisterType<EfVideoDal>().As<IVideoDal>();

        builder.RegisterType<CategoryManager>().As<ICategoryService>();
        builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

        builder.RegisterType<PlaylistManager>().As<IPlaylistService>();
        builder.RegisterType<EfPlaylistDal>().As<IPlaylistDal>();

        builder.RegisterType<UserManager>().As<IUserService>().InstancePerLifetimeScope();
        builder.RegisterType<EfUserDal>().As<IUserDal>();

        builder.RegisterType<AuthManager>().As<IAuthService>();
        builder.RegisterType<JwtHelper>().As<ITokenHelper>();

        builder.RegisterType<EfHistoryDal>().As<IHistoryDal>();
    }
}