using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Ninject;
using System.Configuration;
using System.Data.Entity;

namespace Es.Udc.DotNet.PracticaMaD.HTTP.Util.IoC
{
    internal class IoCManagerNinject : IIoCManager
    {
        private static IKernel kernel;
        private static NinjectSettings settings;

        public void Configure()
        {
            settings = new NinjectSettings() { LoadExtensions = true };
            kernel = new StandardKernel(settings);

            /* UserProfileDao */
            kernel.Bind<IUserProfileDao>().
                To<UserProfileDaoEntityFramework>();

            /* UserService */
            kernel.Bind<IUserService>().
                To<UserService>();

            /* ImageUploadDao */
            kernel.Bind<IImageUploadDao>().
                To<ImageUploadDaoEntityFramework>();

            /*ImageUploadService*/
            kernel.Bind<IImageUploadService>().
                To<ImageUploadService>();

            /* ITagDao */
            kernel.Bind<ITagDao>().
                To<TagDaoEntityFramework>();

            /* ITagDao */
            kernel.Bind<ICommentDao>().
                To<CommentDaoEntityFramework>();

            /* ITagService */
            kernel.Bind<ICommentService>().
                To<CommentService>();

            /* ITagService */
            kernel.Bind<ITagService>().
                To<TagService>();

            /* ICategoryDao */
            kernel.Bind<ICategoryDao>().
                To<CategoryDaoEntityFramework>();



            /* DbContext */
            string connectionString =
                ConfigurationManager.ConnectionStrings["PhotogramEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);
        }

        public T Resolve<T>()
        {
            return kernel.Get<T>();
        }
    }
}