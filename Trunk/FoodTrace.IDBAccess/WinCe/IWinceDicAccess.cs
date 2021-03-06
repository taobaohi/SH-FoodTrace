﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodTrace.Model;
using FoodTrace.Model.WinCeModel;

namespace FoodTrace.IDBAccess
{
    public interface IWinceDicAccess
    {

        /// <summary>
        /// wince用户登录返回用户中文吗
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        string WinceUserLogin(string username, string pwd);
        /// <summary>
        /// 养殖防疫窗口下拉
        /// </summary>
        /// <returns></returns>
        List<DicDto> GetDrugDicList();

        /// <summary>
        /// 保存养殖防疫数据
        /// </summary>
        /// <param name="model"></param>
        void SaveBredDrugData(BredDrug model);

        /// <summary>
        /// 获取养殖健康
        /// </summary>
        /// <returns></returns>
        List<DicDto> GetBreedHealthDicList();

        /// <summary>
        /// 保存健康状况
        /// </summary>
        /// <param name="model"></param>
        void SaveBreedHealthData(BreedHealth model);

        /// <summary>
        /// 养殖用料
        /// </summary>
        /// <returns></returns>
        List<DicDto> GetBreedMaterialDic();
        /// <summary>
        /// 保存用料数据
        /// </summary>
        /// <param name="model"></param>
        void SaveBreedMaterialData(BreedMaterial model);
    }
}
