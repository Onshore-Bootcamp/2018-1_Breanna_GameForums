using System.Collections.Generic;
using System.Web.Mvc;
using Capstone.Mapping;
using Capstone.Models;
using CapstoneDAL.Models;
using CapstoneDAL;
using System;
using System.Configuration;
using Capstone.Custom;
using System.Data.SqlClient;

namespace Capstone.Controllers
{
    public class UserController : Controller
    {
        private UserDAO _dataAccess;

        public UserController()
        {
            string connection = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            _dataAccess = new UserDAO(connection);
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<UserDO> userList = null;
            List<UserPO> displayList = null;
            try
            {
                if (Session["Username"] != null)
                {
                    //displays a list of all users
                    userList = _dataAccess.ViewAllUsers();
                    displayList = UserMapper.ListDOToPO(userList);
                }
                else
                {
                    //Writes an error message to the user if not logged in
                    TempData["message"] = "You must be logged in to view that page";
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
            }
            return View(displayList);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(UserPO form)
        {
            ActionResult response = null;
            if (ModelState.IsValid)
            {
                try
                {
                    //checks the user inputted username against the database to see if it already exists
                    string username = form.Username;
                    bool usernameExists = _dataAccess.UsernameExists(username);

                    if (!usernameExists)
                    {
                        //if the username does not exist, continues with add user procedure
                        UserDO dataObject = UserMapper.UserPOToDO(form);
                        _dataAccess.AddUser(dataObject);
                        response = RedirectToAction("Index", "User");
                    }
                    else
                    {
                        //if username does exist already, shows error
                        ModelState.AddModelError("Username", "Username already in use, please try a different username");
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
        public ActionResult UpdateUser(int userId)
        {
            ActionResult response = null;
            try
            {
                UserDO dataObject = _dataAccess.ViewUserByID(userId);
                UserPO displayObject = UserMapper.UserDOToPO(dataObject);
                response = View(displayObject);
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
            }
            return response;
        }

        [HttpPost]
        public ActionResult UpdateUser(UserPO form)
        {
            ActionResult response = null;
            if (ModelState.IsValid)
            {
                try
                {
                    UserDO dataObject = UserMapper.UserPOToDO(form);
                    _dataAccess.UpdateUserInformation(dataObject);
                    response = RedirectToAction("Index", "User");
                }
                catch (SqlException ex)
                {
                    //uses custom Sql error to show that a username already exists
                    if (ex.Data.Contains("uniqueUsername"))
                    {
                        ModelState.AddModelError("Username", ex.Data["uniqueUsername"].ToString());
                    }
                    //uses custom Sql error to show that the inputted role ID is not valid
                    else if (ex.Data.Contains("invalidRoleId"))
                    {
                        ModelState.AddModelError("RoleId", ex.Data["invalidRoleId"].ToString());
                    }
                    Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
                    response = View();
                }
            }
            else
            {
                response = View(form);
            }
            return response;
        }

        [HttpGet]
        public ActionResult DeleteUser(int userId)
        {
            ActionResult response = null;
            try
            {
                //deletes user based on user ID
                _dataAccess.DeleteUser(userId);
                response = RedirectToAction("Index", "User");
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
            }
            return response;
        }
    }
}