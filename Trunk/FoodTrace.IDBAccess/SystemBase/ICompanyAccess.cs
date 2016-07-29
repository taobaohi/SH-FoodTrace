﻿using FoodTrace.Model;
using System.Collections.Generic;

namespace FoodTrace.IDBAccess
{
    public interface ICompanyAccess : IBaseAccess<CompanyModel>
    {
        int GetEntityCount(string name);

        List<CompanyModel> GetPagerCompanyByConditions(string name, int pageIndex, int pageSize);

        /// <summary>
        /// 获取企业机构树
        /// </summary>
        /// <returns></returns>
        List<ZtreeModel> GetCompantTree();
    }
}
