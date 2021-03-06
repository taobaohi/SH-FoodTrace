﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodTrace.IService;
using FoodTrace.IService.BreedMng;
using FoodTrace.Model;
using FoodTrace.Model.BaseDto;
using FoodTrace.Model.DtoModel;

namespace FoodTrace.WebSite.Controllers.BreedManage
{
    public class BreedHomeController : BaseController
    {
        private readonly IBreedHomeService _homeService;
        private readonly IBreedAreaService _areaService;
        private readonly IBreedVarietyService _varietyService;

        public BreedHomeController(IBreedHomeService homeService,IBreedAreaService areaService,
            IBreedVarietyService varietyService)
        {
            _homeService = homeService;
            _areaService = areaService;
            _varietyService = varietyService;
        }
        public ActionResult Index()
        {
            var breedarea = _areaService.GetAreaGridList(1, 1000);
            var varietyList = _varietyService.GetVarietyList();

            ViewBag.BreedArea = new SelectList(breedarea.rows, "AreaID", "AreaName");
            ViewBag.VarietyList = new SelectList(varietyList, "VarietyName", "VarietyName");
            return View();
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pSize"></param>
        /// <returns></returns>
        public JsonResult GetList(int page, int rows)
        {
            var data = new GridList<BreedHomeDto>();
            try
            {
                data=_homeService.GetBreedHomeGridList(page, rows);
            }
            catch (Exception)
            {
               
            }

            return Json(data);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult SaveData(BreedHomeModel model)
        {
            var result = new ResultJson();
            try
            {
                var msg = new MessageModel();
                if (model.HomeID == 0)
                {
                    msg = _homeService.InsertSingleBreedHome(model);
                }
                else
                {
                    msg = _homeService.UpdateSingleBreedHome(model);
                }

                if (msg.Status == MessageStatus.Success)
                {
                    result.IsSuccess = true;
                }
            }
            catch (Exception)
            {
                
            }

            return Json(result);
        }


        /// <summary>
        /// 根据Id获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetBreedHomeById(int id)
        {
            var result = new ResultJson();
            try
            {
                result.Data =_homeService.GetBreedHomeDtoById(id);
                result.IsSuccess = true;

            }
            catch (Exception)
            {
                
            }

            return Json(result);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public JsonResult DeleteByIds(string ids)
        {
            var result = new ResultJson();
            try
            {
                var msg = _homeService.DeleteByIds(ids);
                if (msg.Status == MessageStatus.Success)
                {
                    result.IsSuccess = true;
                }
            }
            catch (Exception)
            {
              
            }

            return Json(result);
        }
	}
}