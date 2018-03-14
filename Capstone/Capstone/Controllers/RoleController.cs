using Capstone.Custom;
using Capstone.Mapping;
using Capstone.Models;
using CapstoneDAL.CapstoneDAO;
using CapstoneDAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace Capstone.Controllers
{
    public class RoleController : Controller
    {
        private RoleDAO _dataAccess;

        public RoleController()
        {
            string connection = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            _dataAccess = new RoleDAO(connection);
        }

        //Displays a list of roles
        [HttpGet]
        public ActionResult Index()
        {
            List<RoleDO> roleList = null;
            List<RolePO> displayList = null;
            try
            {
                roleList = _dataAccess.ViewAllRoles();
                //Sends the role list from the data object to the presentation object to display it
                displayList = RoleMapper.ListDOToPO(roleList);
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
            }
            return View(displayList);
        }

        [HttpGet]
        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRole(RolePO form)
        {
            ActionResult response = null;
            if (ModelState.IsValid)
            {
                try
                {
                    //sets user inputted role name to a variable and checks to see if the name exists in the database
                    string name = form.Name;
                    bool roleExists = _dataAccess.RoleExists(name);

                    if (!roleExists)
                    {
                        RoleDO dataObject = RoleMapper.RolePOToDO(form);
                        _dataAccess.AddRole(dataObject);
                        response = RedirectToAction("Index", "Role");
                    }
                    else
                    {
                        ModelState.AddModelError("Name", "Role name already in use, please choose another.");
                        response = View();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
                }
            }
            else
            {
                response = View(form);
            }
            return response;
        }

        [HttpGet]
        public ActionResult UpdateRole(int Id)
        {
            ActionResult response = null;
            try
            {
                //views the update page for a specific role, using the role ID
                RoleDO dataObject = _dataAccess.ViewRoleById(Id);
                RolePO displayObject = RoleMapper.RoleDOToPO(dataObject);
                response = View(displayObject);

            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
            }
            return response;
        }

        [HttpPost]
        public ActionResult UpdateRole(RolePO form)
        {
            ActionResult response = null;
            if (ModelState.IsValid)
            {
                try
                {
                    //sets the user inputted role name to a variable to be used in the code
                    string name = form.Name;
                    //sends the user inputted variable to RoleExists methods to see if it already exists
                    bool roleExists = _dataAccess.RoleExists(name);

                    if (!roleExists)
                    {
                        //if the role name does not exist, continue with the update procedure
                        RoleDO dataObject = RoleMapper.RolePOToDO(form);
                        _dataAccess.UpdateRole(dataObject);
                        response = RedirectToAction("Index", "Role");
                    }
                    else
                    {
                        //if the role name does exist, show error message
                        ModelState.AddModelError("Name", "Role name already in use, please choose another.");
                        response = View();
                    }

                }
                catch (Exception ex)
                {
                    Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
                }
            }
            else
            {
                response = View(form);
            }
            return response;
        }

        [HttpGet]
        public ActionResult DeleteRole(int id)
        {
            ActionResult response = null;
            try
            {
                //deletes role by the role ID
                _dataAccess.DeleteRole(id);
                response = RedirectToAction("Index", "Role");

            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
            }
            return response;
        }
    }
}