using Capstone.Mapping;
using Capstone.Models;
using CapstoneDAL.CapstoneDAO;
using CapstoneDAL.Models;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace Capstone.Controllers
{
    public class AccountController : Controller
    {
        private AccountDAO _dataAccess;

        public AccountController()
        {
            string connection = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            _dataAccess = new AccountDAO(connection);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginPO form)
        {
            ActionResult response = null;
            LoginPO login = new LoginPO();

            try
            {
                //Checks ModelState to be sure info inputted matches the current model
                if (ModelState.IsValid)
                {
                    LoginDO dataObject = LoginMapper.LoginPOToDO(form);
                    LoginDO databaseLogin = _dataAccess.ViewUserByUsername(dataObject);

                    //Checks whether the inputted password and username are correct, and handles that properly
                    if (databaseLogin == null || form.Password != databaseLogin.Password)
                    {
                        ModelState.AddModelError("Password", "Username or password incorrect, please try again.");
                        response = View();
                    }
                    else if (form.Password == databaseLogin.Password && form.Username == databaseLogin.Username)
                    {
                        Session["Username"] = databaseLogin.Username;
                        Session["UserID"] = databaseLogin.UserId;
                        Session["RoleID"] = databaseLogin.RoleId;

                        //keeps user logged in for 5 minutes, after they log in they get sent to home page
                        Session.Timeout = 5;
                        response = RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    response = View();
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
            }
            return response;
        }

        [HttpGet]
        public ActionResult Logout()
        {
            //Session.Abandon closes out the current session
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterPO form)
        {
            ActionResult response = null;
            if (ModelState.IsValid)
            {
                try
                {
                    string Username = form.Username;
                    bool UsernameExists = _dataAccess.UsernameExists(Username);

                    //The "!" is "not", "Username does not exist"
                    if (!UsernameExists)
                    {
                        RegisterDO dataObject = RegisterMapper.RegisterPOToDO(form);
                        _dataAccess.RegisterUser(dataObject);
                        //has user login after they have registered
                        response = RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        ModelState.AddModelError("Username", "Username already in use, please try a different username.");
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
    }
}
