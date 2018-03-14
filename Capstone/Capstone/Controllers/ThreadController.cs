using Capstone.Custom;
using Capstone.Mapping;
using Capstone.Models;
using CapstoneBLL;
using CapstoneBLL.Models;
using CapstoneDAL.CapstoneDAO;
using CapstoneDAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace Capstone.Controllers
{
    public class ThreadController : Controller
    {
        private ThreadDAO _threadDataAccess;
        private PostDAO _postDataAccess;
        private ThreadBLL _ThreadBLL;

        public ThreadController()
        {
            string connection = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            _threadDataAccess = new ThreadDAO(connection);
            _postDataAccess = new PostDAO(connection);
            _ThreadBLL = new ThreadBLL();
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<ThreadDO> threadList = _threadDataAccess.ViewAllThreads();
            List<ThreadPO> displayList = ThreadMapper.ListDOToPO(threadList);
            ThreadViewModel viewModel = new ThreadViewModel();
            if (Session["Username"] != null)
            {             //views a list of all threads

                viewModel.AverageWords = AverageWordsPerPost();
                viewModel.Threads = displayList;
            }
            else
            {
                TempData["message"] = "Must be logged in to view that page.";
            }
            return View(viewModel);
        }

        [HttpGet]
        public float AverageWordsPerPost()
        {
            List<PostDO> postList = null;
            List<PostPO> displayList = null;
            float averageWords = 0;
            try
            {
                //gets a list of posts to count the words in the content of all posts
                postList = _postDataAccess.ViewAllPosts();
                displayList = PostMapper.ListDOToPO(postList);
                List<PostBO> ListToBofromPo = PostMapper.ListPOToBO(displayList);
                //sets the number of words in the posts to a variable
                averageWords = _ThreadBLL.AverageWordsPerPost(ListToBofromPo);
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
            }
            return averageWords;
        }

        [HttpGet]
        public ActionResult AddThread()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddThread(ThreadPO form)
        {
            ActionResult response = null;
            if (ModelState.IsValid)
            {
                try
                {
                    //maps and adds the user inputted thread information to the database
                    ThreadDO dataObject = ThreadMapper.ThreadPOToDO(form);
                    _threadDataAccess.AddThread(dataObject);
                    //sends user back to the list of threads page
                    response = RedirectToAction("Index", "Thread");
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
        public ActionResult UpdateThread(int threadId)
        {
            ActionResult response = null;
            try
            {
                //shows the update form for the thread that was clicked
                ThreadDO dataObject = _threadDataAccess.ViewThreadByID(threadId);
                ThreadPO displayObject = ThreadMapper.ThreadDOToPO(dataObject);
                response = View(displayObject);
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
            }
            return response;
        }

        [HttpPost]
        public ActionResult UpdateThread(ThreadPO form)
        {
            ActionResult response = null;
            if (ModelState.IsValid)
            {
                try
                {
                    //updates the thread with user inputted information
                    ThreadDO dataObject = ThreadMapper.ThreadPOToDO(form);
                    _threadDataAccess.UpdateThread(dataObject);
                    response = RedirectToAction("Index", "Thread");
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
        public ActionResult DeleteThread(int threadId)
        {
            ActionResult response = null;
            try
            {
                //deletes a thread by the thread's ID
                _threadDataAccess.DeleteThread(threadId);
                response = RedirectToAction("Index", "Thread");
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
            }
            return response;
        }
    }
}