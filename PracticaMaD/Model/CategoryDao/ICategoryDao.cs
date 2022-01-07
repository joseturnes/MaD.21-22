﻿using Es.Udc.DotNet.ModelUtil.Dao;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    /// <exception cref="InstanceNotFoundException"></exception>
    public interface ICategoryDao : IGenericDao<Category, Int64>
    {
        Category FindByName(string name);

    }
}
