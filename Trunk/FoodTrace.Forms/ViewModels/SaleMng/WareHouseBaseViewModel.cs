﻿using Caliburn.Micro;
using FoodTrace.Forms.Models;
using FoodTrace.Forms.Views;
using FoodTrace.IService;
using FoodTrace.Model;
using FoodTrace.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FoodTrace.Forms.ViewModels
{
    [Export(typeof(WareHouseBaseViewModel))]
    public class WareHouseBaseViewModel : Screen, IMainScreenTabItem
    {
        private IWareHouseBaseService WareHouseBaseService = new WareHouseBaseService();

        private PagerModel _pagerModel;

        public PagerModel PagerModel
        {
            get
            {
                return _pagerModel;
            }
            set
            {
                _pagerModel = value;
                NotifyOfPropertyChange(() => PagerModel);
            }
        }

        private BindableCollection<WareHouseBaseModel> _models;

        public BindableCollection<WareHouseBaseModel> ModelCollection
        {

            get
            {
                return _models;
            }
            set
            {
                _models = value;
                NotifyOfPropertyChange(() => ModelCollection);
            }
        }

        public WareHouseBaseViewModel()
        {
            DisplayName = "PlansBaseViewModel";
            TabItemIndex = 0;
        }

        private void LoadData(string key,int pageIndex=1,int pageSize=10)
        {
            Task.Factory.StartNew(() =>
            {
                var list = WareHouseBaseService.GetPagerWareHouseBase(key, 1, 10);
                var total = WareHouseBaseService.GetWareHouseBaseCount(key);
                //var data = iBreedBaseService.GetBreedBaseListPaging(key, pageIndex, pageSize);

                var pageCount = total / pageSize == 0 ? 1 : (total / pageSize) + (total % pageSize == 0 ? 0 : 1);
                ModelCollection = new BindableCollection<WareHouseBaseModel>(list);
                //ModelCollection = new BindableCollection<BreedBaseDto>(data.rows);
                PagerModel = new PagerModel { TotalCount = total, PageCount = pageCount, DetailMsg = string.Format("[共{0}页/共{1}条]", pageCount, total) };
                ModelCollection = new BindableCollection<WareHouseBaseModel>(list);
            });
        }

        public void LoadUserControl(WareHouseBaseView view)
        {
            LoadData(string.Empty);
        }

        public int TabItemIndex { get; set; }

        public void Add()
        {

            var vm = IoC.Get<WareHouseBaseEditViewModel>();
            vm.Mode = Models.EditMode.CREATE;
            vm.Model = new WareHouseBaseModel {
                IsLocked = true,
                IsShow = true
            };
            var result = IoC.Get<IWindowManager>().ShowDialog(vm, null, new Dictionary<string, object>{
                {"Title", "添加地块" },{"ResizeMode", System.Windows.ResizeMode.NoResize},
                        {"Width", 450},
                        {"Height", 600} });
            if (result ?? false)
            {
                LoadData(string.Empty);
            }
        }

        public void GridMouseDoubleClick(DataGrid dg, MouseButtonEventArgs args)
        {
            var vm = IoC.Get<WareHouseBaseEditViewModel>();
            vm.Model = (WareHouseBaseModel)dg.SelectedItem;
            vm.Mode = Models.EditMode.UPDATE;
            var result = IoC.Get<IWindowManager>().ShowDialog(vm, null, new Dictionary<string, object>{
                {"Title", "编辑地块" },{"ResizeMode", System.Windows.ResizeMode.NoResize},
                        {"Width", 450},
                        {"Height", 600} });
            if (result ?? false)
            {
                LoadData(string.Empty);
            }

        }

        public void EditRow(WareHouseBaseModel model)
        {
            var vm = IoC.Get<WareHouseBaseEditViewModel>();
            vm.Model = model;
            vm.Mode = Models.EditMode.UPDATE;
            var result = IoC.Get<IWindowManager>().ShowDialog(vm, null, new Dictionary<string, object>{
                {"Title", "编辑" },{"ResizeMode", System.Windows.ResizeMode.NoResize},
                        {"Width", 450},
                        {"Height", 600} });
            if (result ?? false)
            {
                LoadData(string.Empty);
            }
        }

        public void DeleteRow(WareHouseBaseModel model)
        {
            var result = MessageBox.Show("是否删除", "提示", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                //var model = (WareHouseBaseModel)dg.SelectedItem;
                var message = WareHouseBaseService.DeleteSingleEntity(model.WareHouseID);
                if (message.Status == MessageStatus.Success)
                {
                    LoadData(string.Empty);
                }
            }
        }

        public void DeleteData(DataGrid dg)
        {
            var result = MessageBox.Show("是否删除", "提示", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var model = (WareHouseBaseModel)dg.SelectedItem;
                var message = WareHouseBaseService.DeleteSingleEntity(model.WareHouseID);
                if (message.Status == MessageStatus.Success)
                {
                    LoadData(string.Empty);
                }
            }
        }

        public void FilterData(TextBox tb)
        {
            LoadData(tb.Text);
        }
    }
}
