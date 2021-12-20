using lab.LocalCosmosDbApp.Helpers;
using lab.LocalCosmosDbApp.Managers;
using lab.LocalCosmosDbApp.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;

namespace lab.LocalCosmosDbApp.Attributes
{
    public class InitAuthorizeAttribute : ActionFilterAttribute
    {
        #region Global Variable Declaration
        private readonly ILogger<InitAuthorizeAttribute> _logger;
        #endregion

        #region Constructor
        public InitAuthorizeAttribute(ILogger<InitAuthorizeAttribute> logger)
        {
            _logger = logger;
        }
        #endregion

        #region Actions

        public override async void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (filterContext.HttpContext.User.Identity != null && filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    string currentUserName = filterContext.HttpContext.User.Identity.Name;
                    string currentUserRoleName = filterContext.HttpContext.User.FindFirst(ClaimTypes.Role).Value;

                    if (!string.IsNullOrEmpty(currentUserName) && !string.IsNullOrEmpty(currentUserRoleName))
                    {
                        var areaRoute = filterContext.RouteData.Values["area"];
                        string? areaName = areaRoute != null ? areaRoute.ToString() : string.Empty;

                        var controllerRoute = filterContext.RouteData.Values["controller"];
                        string? controllerName = controllerRoute != null ? controllerRoute.ToString() : string.Empty;

                        var actionRoute = filterContext.RouteData.Values["action"];
                        string? actionName = actionRoute != null ? actionRoute.ToString() : string.Empty;

                        UserRolePermissionControllerWise(filterContext, currentUserRoleName, controllerName, actionName);

                        UserRolePermissionActionWise(filterContext, controllerName, actionName);

                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(LogMessageHelper.FormateMessageForException(ex, "OnActionExecuting"));
                FilterContextResult(filterContext);
            }

        }

        private void UserRolePermissionControllerWise(ActionExecutingContext filterContext, string currentUserRoleName, string? controllerName, string? actionName)
        {
            if (currentUserRoleName.Contains(AppConstants.AppUserRole.Admin))
            {
                if (!string.IsNullOrEmpty(controllerName))
                {
                    if (AppConstants.AdminUserControllers.Exists(c => c.Equals(controllerName)))
                    {
                        //UserRolePermissionActionWise(filterContext, controllerName, actionName);
                    }
                    else
                    {
                        FilterContextResult(filterContext);
                    }
                }
                else
                {
                    FilterContextResult(filterContext);
                }
            }
            else if (currentUserRoleName.Contains(AppConstants.AppUserRole.Member))
            {
                if (!string.IsNullOrEmpty(controllerName))
                {
                    if (AppConstants.MemberUserControllers.Exists(c => c.Equals(controllerName)))
                    {
                        //UserRolePermissionActionWise(filterContext, controllerName, actionName);
                    }
                    else
                    {
                        FilterContextResult(filterContext);
                    }
                }
                else
                {
                    FilterContextResult(filterContext);
                }
            }
            else if (currentUserRoleName.Contains(AppConstants.AppUserRole.Reader))
            {
                if (!string.IsNullOrEmpty(controllerName))
                {
                    if (AppConstants.ReaderUserControllers.Exists(c => c.Equals(controllerName)))
                    {
                        //UserRolePermissionActionWise(filterContext, controllerName, actionName);
                    }
                    else
                    {
                        FilterContextResult(filterContext);
                    }
                }
                else
                {
                    FilterContextResult(filterContext);
                }
            }
            else
            {
                FilterContextResult(filterContext);
            }
        }

        private void UserRolePermissionActionWise(ActionExecutingContext filterContext, string? controllerName, string? actionName)
        {
            if (!string.IsNullOrEmpty(controllerName) && !string.IsNullOrEmpty(actionName))
            {
                string currentUserAddPermission = filterContext.HttpContext.User.FindFirst("AddPermission").Value;
                string currentUserEditPermission = filterContext.HttpContext.User.FindFirst("EditPermission").Value;
                string currentUserDeletePermission = filterContext.HttpContext.User.FindFirst("DeletePermission").Value;
                string currentUserDetailPermission = filterContext.HttpContext.User.FindFirst("DetailPermission").Value;
                string currentUserIndexPermission = filterContext.HttpContext.User.FindFirst("IndexPermission").Value;

                if (actionName.Contains("Add"))
                {
                    if (!Convert.ToBoolean(currentUserAddPermission))
                    {
                        FilterContextResult(filterContext);
                    }
                }

                if (actionName.Contains("Import"))
                {
                    if (!Convert.ToBoolean(currentUserAddPermission))
                    {
                        FilterContextResult(filterContext);
                    }
                }

                if (actionName.Contains("Export"))
                {
                    if (!Convert.ToBoolean(currentUserAddPermission))
                    {
                        FilterContextResult(filterContext);
                    }
                }

                if (actionName.Contains("Setting"))
                {
                    if (!Convert.ToBoolean(currentUserAddPermission))
                    {
                        FilterContextResult(filterContext);
                    }
                }

                if (actionName.Contains("Edit"))
                {
                    if (!Convert.ToBoolean(currentUserEditPermission))
                    {
                        FilterContextResult(filterContext);
                    }
                }

                if (actionName.Contains("Delete"))
                {
                    if (!Convert.ToBoolean(currentUserDeletePermission))
                    {
                        FilterContextResult(filterContext);
                    }
                }

                if (actionName.Contains("Detail"))
                {
                    if (!Convert.ToBoolean(currentUserDetailPermission))
                    {
                        FilterContextResult(filterContext);
                    }
                }

                if (actionName.Contains("Index"))
                {
                    if (!Convert.ToBoolean(currentUserIndexPermission))
                    {
                        FilterContextResult(filterContext);
                    }
                }
            }
            else
            {
                FilterContextResult(filterContext);
            }
        }

        private void FilterContextResult(ActionExecutingContext filterContext)
        {
            if (!IsAjaxRequest(filterContext.HttpContext.Request))
            {
                filterContext.Result = RequestResult();
            }
            else
            {
                filterContext.Result = RequestResultAjax();
            }
        }

        private bool IsAjaxRequest(HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.Headers != null)
            {
                if (request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return true;
                }
            }
            return false;
        }

        private RedirectResult RequestResult()
        {
            return new RedirectResult("/Account/UnAuthenticated");
        }

        private JsonResult RequestResultAjax()
        {
            var json = new { success = false, message = MessageHelper.MessageTypeDanger, messagetype = MessageHelper.UnAuthenticated };

            return new JsonResult(json);
        }

        #endregion
    }
}
