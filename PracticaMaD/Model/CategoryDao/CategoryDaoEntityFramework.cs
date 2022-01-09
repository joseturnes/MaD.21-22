using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    public class CategoryDaoEntityFramework :
        GenericDaoEntityFramework<Category, Int64>, ICategoryDao
    {

        public CategoryDaoEntityFramework()
        {
        }

        /// <exception cref="InstanceNotFoundException"/>
        public Category FindByName(string name)
        {
            Category category = null;
            DbSet<Category> categories = Context.Set<Category>();

            var result =
                (from a in categories
                 where a.categoryName == name
                 select a);

            category = result.FirstOrDefault();

            if (category == null)
                throw new InstanceNotFoundException(name,
                    typeof(Category).FullName);

            return category;
        }
    }
}
