﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Destiny.Core.Flow.AspNetCore.Api;
using Destiny.Core.Flow.AspNetCore.Ui;
using Destiny.Core.Flow.Dtos;
using Destiny.Core.Flow.Dtos.Menu;
using Destiny.Core.Flow.Dtos.MenuFunction;
using Destiny.Core.Flow.Filter;
using Destiny.Core.Flow.IServices.IMenu;
using Destiny.Core.Flow.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Destiny.Core.Flow.API.Controllers.Menu
{
    [Description("菜单管理")]
    [Authorize]
    public class MenuController : ApiControllerBase
    {
        private readonly IMenuServices _menuServices;
        private readonly IMenuFunctionServices _menuFunctionServices;

        public MenuController(IMenuServices menuServices, IMenuFunctionServices menuFunctionServices)
        {
            _menuServices = menuServices;
            _menuFunctionServices = menuFunctionServices;
        }


        ///// <summary>
        ///// 根据角色Id获取树形菜单信息
        ///// </summary>
        ///// <param name="roleid">角色Id</param>
        ///// <returns></returns>
        //[HttpGet]
        //[Description("获取树形菜单信息")]
        //public async Task<TreeModel<MenuOutDto>> GetTreeAsync(Guid roleid)
        //{
        //    var result = await _menuServices.GetMenuAsync(roleid);
        //    return result.ToTreeModel();
        //}
        /// <summary>
        /// 获取表格菜单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("获取表格菜单信息")]
        public async Task<TreeModel<MenuTableOutDto>> GetTableAsync()
        {
            var result = await _menuServices.GetMenuTableAsync();
            return new TreeModel<MenuTableOutDto>()
            {
                ItemList = result.ItemList,
                Message = result.Message,
                Success = result.Success
            };
        }
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("添加菜单")]
        public async Task<AjaxResult> AddMenuAsync([FromBody] MenuInputDto dto)
        {
            if (dto.Id == Guid.Empty)
            {
                return (await _menuServices.CreateAsync(dto)).ToAjaxResult();
            }
            return (await _menuServices.UpdateAsync(dto)).ToAjaxResult();
        }
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Description("修改菜单")]
        public async Task<AjaxResult> UpdateMenuAsync([FromBody] MenuInputDto dto)
        {
            return (await _menuServices.UpdateAsync(dto)).ToAjaxResult();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Description("删除菜单")]
        public async Task<AjaxResult> DeleteAsync(Guid? id)
        {
            return (await _menuServices.DeleteAsync(id.Value)).ToAjaxResult();
        }
        [HttpGet]
        [Description("获取一个菜单")]
        public async Task<AjaxResult> LoadFormMenuAsync(Guid Id)
        {
            return (await _menuServices.LoadFormMenuAsync(Id)).ToAjaxResult();
        }
        /// <summary>
        /// 登录成功之后获取菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("根据登录账号获取菜单")]
        [NoAuthorityVerification]
        public async Task<AjaxResult> GetMenuAsync()
        {
            return (await _menuServices.GetMenuAsync()).ToAjaxResult();
        }

        /// <summary>
        /// 登录成功之后获取用户菜单树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("登录成功之后获取用户菜单树")]
        [NoAuthorityVerification]
        public async Task<AjaxResult> GetUserMenuTreeAsync()
        {
            return (await _menuServices.GetUserMenuTreeAsync()).ToAjaxResult();
        }
        /// <summary>
        /// 异步得到菜单树数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("异步得到菜单树数据")]

        public async Task<AjaxResult> GetMenuTreeAsync(Guid? roleId)
        {
            return (await _menuServices.GetMenuTreeAsync(roleId)).ToAjaxResult();
        }
        /// <summary>
        /// 异步得到菜单下的按钮
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("异步得到菜单下的按钮")]
        [NoAuthorityVerification]

        public async Task<AjaxResult> GetMenuChildrenButtonAsync(Guid? menuId)
        {
            return (await _menuServices.GetMenuChildrenButton(menuId.Value)).ToAjaxResult();
        }
        /// <summary>
        /// 获取登录用户权限菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("获取登录用户权限菜单")]
        [NoAuthorityVerification]

        public async Task<AjaxResult> GetMenuListAsync()
        {
            return (await _menuServices.GetMenuListAsync()).ToAjaxResult();
        }
        /// <summary>
        /// 异步到菜单功能集合
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("异步到菜单功能集合")]

        public async Task<PageList<MenuFunctionOutPageListDto>> GetMenuFunctionListAsync(Guid id)
        {
            return (await _menuFunctionServices.GetMenuFunctionListAsync(id)).ToPageList();
        }
    }
}