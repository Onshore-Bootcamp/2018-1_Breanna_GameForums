using Capstone.Custom;
using Capstone.Mapping;
using Capstone.Models;
using CapstoneDAL.CapstoneDAO;
using CapstoneBLL;
using CapstoneDAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using CapstoneBLL.Models;

namespace Capstone.Controllers
{
    public class PostController : Controller
    {
        private PostDAO _dataAccess;
        ThreadDAO _threadDataAccess;

        public PostController()
        {
            string connection = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            _dataAccess = new PostDAO(connection);
            _threadDataAccess = new ThreadDAO(connection);
        }

        //Gets a list of all posts
        [HttpGet]
        public ActionResult Index()
        {
            ActionResult response = null;
            List<PostDO> postList = null;
            List<PostPO> displayList = null;
            PostViewModel postVM = new PostViewModel();

            try
            {
                //sets postList to the ViewAllPosts method in UserDAO then maps and displays it
                postList = _dataAccess.ViewAllPosts();
                displayList = PostMapper.ListDOToPO(postList);
                postVM.Posts = displayList;
                response = View(postVM);
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
            }
            return response;
        }

        //Views posts with a specific thread ID
        [HttpGet]
        public ActionResult ViewPostsByThreadId(int threadId)
        {
            List<PostDO> postList = null;
            List<PostPO> displayList = null;
            PostViewModel postVM = new PostViewModel();
            postVM.ThreadId = threadId;
            ActionResult response = null;
            try
            {
                postList = _dataAccess.ViewPostsByThreadId(threadId);
                displayList = PostMapper.ListDOToPO(postList);
                //Uses a post view model which holds the list of posts and the thread ID
                postVM.Posts = displayList;
                response = View(postVM);
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
            }
            return response;
        }

        //Views posts by currently logged in user's ID
        [HttpGet]
        public ActionResult ViewPostsByUserId(int userId)
        {
            List<PostDO> postList = null;
            List<PostPO> displayList = null;
            PostViewModel postVM = new PostViewModel();
            ActionResult response = null;

            try
            {
                postList = _dataAccess.ViewPostsByUserId(userId);
                displayList = PostMapper.ListDOToPO(postList);
                postVM.Posts = displayList;
                //Sets the response to the view which contains the post view model's info
                response = View(postVM);
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
            }
            return response;
        }

        [HttpGet]
        public ActionResult AddPost(int threadId)
        {
            int sessionId = 0;
            if (Session["UserID"] != null)
            {
                int.TryParse(Session["UserID"].ToString(), out sessionId);
            }
            //pulls currently logged in user's ID and sends it to the add post view
            PostPO newPost = new PostPO();
            newPost.UserId = sessionId;
            newPost.ThreadId = threadId;

            return View(newPost);
        }

        [HttpPost]
        public ActionResult AddPost(PostPO form)
        {
            ActionResult response = null;
            if (ModelState.IsValid)
            {
                try
                {
                    PostDO dataObject = PostMapper.PostPOToDO(form);
                    _dataAccess.AddPost(dataObject);
                    response = RedirectToAction("ViewPostsByThreadId", "Post", new { ThreadId = form.ThreadId });

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
        public ActionResult UpdatePost(int postId)
        {
            ActionResult response = null;
            try
            {
                PostDO dataObject = _dataAccess.ViewPostById(postId);
                PostPO displayObject = PostMapper.PostDOToPO(dataObject);
                response = View(displayObject);

            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
            }
            return response;
        }

        [HttpPost]
        public ActionResult UpdatePost(PostPO form)
        {
            ActionResult response = null;
            int userId = 0;
            int userRole = 0;

            if (Session["RoleID"] != null)
            {
                //get the role ID and user ID from session
                int.TryParse(Session["UserID"].ToString(), out userId);
                int.TryParse(Session["RoleID"].ToString(), out userRole);
            }

            if (userId == form.UserId || userRole == 1 || userRole == 2)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        PostDO dataObject = PostMapper.PostPOToDO(form);
                        _dataAccess.UpdatePost(dataObject);
                        response = RedirectToAction("ViewPostsByThreadId", "Post", new { ThreadId = form.ThreadId });
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
                    }
                }
                else
                {
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
        public ActionResult DeletePost(int postId, int threadId)
        {
            ActionResult response = null;
            try
            {
                //sends the post ID to the delete post method 
                _dataAccess.DeletePost(postId);
                response = RedirectToAction("ViewPostsByThreadId", "Post", new { threadId });
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
            }
            return response;
        }
    }
}