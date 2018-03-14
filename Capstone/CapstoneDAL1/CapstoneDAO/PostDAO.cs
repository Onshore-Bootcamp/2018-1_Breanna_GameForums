using CapstoneDAL.Logging;
using CapstoneDAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace CapstoneDAL.CapstoneDAO
{
    public class PostDAO
    {
        public PostDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;

        public List<PostDO> ViewAllPosts()
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            SqlDataReader reader = null;
            List<PostDO> postList = new List<PostDO>();

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("POST_VIEW_ALL", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;

                connectionToSql.Open();
                reader = storedProcedure.ExecuteReader();

                while (reader.Read())
                {
                    PostDO postDO = new PostDO();
                    postDO.PostId = (int)reader["PostID"];
                    postDO.UserId = (int)reader["UserID"];
                    postDO.ThreadId = (int)reader["ThreadID"];
                    postDO.CreationDate = (DateTime)reader["CreationDate"];
                    if (postDO.EditDate == null)
                    {
                        postDO.EditDate = postDO.CreationDate;
                    }
                    else
                    {
                        postDO.EditDate = (DateTime)reader["EditDate"];
                    }
                    postDO.Title = reader["Title"] as string;
                    postDO.Content = reader["Content"] as string;
                    postList.Add(postDO);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
                throw ex;
            }
            finally
            {
                if (connectionToSql != null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }
            }
            return postList;
        }

        public PostDO ViewPostById(int id)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            SqlDataReader reader = null;
            PostDO postDO = new PostDO();

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("POST_VIEW_BY_POST_ID", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                storedProcedure.Parameters.AddWithValue("@PostID", id);

                connectionToSql.Open();
                reader = storedProcedure.ExecuteReader();

                while (reader.Read())
                {
                    postDO.PostId = (int)reader["PostID"];
                    postDO.UserId = (int)reader["UserID"];
                    postDO.ThreadId = (int)reader["ThreadID"];
                    postDO.Title = reader["Title"] as string;
                    postDO.CreationDate = (DateTime)reader["CreationDate"];
                    postDO.EditDate = (DateTime)reader["EditDate"];
                    postDO.Content = reader["Content"] as string;
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
                throw ex;
            }
            return postDO;
        }

        public List<PostDO> ViewPostsByThreadId(int threadId)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            SqlDataReader reader = null;
            List<PostDO> postList = new List<PostDO>();

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("POST_VIEW_BY_THREAD_ID", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                storedProcedure.Parameters.AddWithValue("@ThreadId", threadId);

                connectionToSql.Open();
                reader = storedProcedure.ExecuteReader();

                while (reader.Read())
                {
                    PostDO postDO = new PostDO();
                    postDO.ThreadId = (int)reader["ThreadID"];
                    postDO.PostId = (int)reader["PostID"];
                    postDO.UserId = (int)reader["UserID"];
                    postDO.Username = reader["Username"] as string;
                    postDO.Title = reader["Title"] as string;
                    postDO.CreationDate = (DateTime)reader["CreationDate"];
                    postDO.EditDate = (DateTime)reader["EditDate"];
                    postDO.Content = reader["Content"] as string;
                    postList.Add(postDO);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
                throw ex;
            }
            return postList;
        }

        public List<PostDO> ViewPostsByUserId(int userId)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            SqlDataReader reader = null;
            List<PostDO> postList = new List<PostDO>();

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("POST_VIEW_BY_USER_ID", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                storedProcedure.Parameters.AddWithValue("@UserId", userId);

                connectionToSql.Open();
                reader = storedProcedure.ExecuteReader();

                while (reader.Read())
                {
                    PostDO postDO = new PostDO();
                    postDO.ThreadId = (int)reader["ThreadID"];
                    postDO.PostId = (int)reader["PostID"];
                    postDO.UserId = (int)reader["UserID"];
                    postDO.Username = reader["Username"] as string;
                    postDO.Title = reader["Title"] as string;
                    postDO.CreationDate = (DateTime)reader["CreationDate"];
                    postDO.EditDate = (DateTime)reader["EditDate"];
                    postDO.Content = reader["Content"] as string;
                    postList.Add(postDO);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
                throw ex;
            }
            return postList;
        }

        public ThreadDO ViewThreadByID(int id)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            SqlDataReader reader = null;
            ThreadDO threadDO = new ThreadDO();

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("THREAD_VIEW_BY_THREAD_ID", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                storedProcedure.Parameters.AddWithValue("@ThreadID", id);

                connectionToSql.Open();
                reader = storedProcedure.ExecuteReader();

                while (reader.Read())
                {
                    threadDO.ThreadId = (int)reader["ThreadID"];
                    threadDO.Title = reader["Title"] as string;
                    threadDO.Information = reader["Information"] as string;
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
                throw ex;
            }
            return threadDO;
        }

        public void AddPost(PostDO form)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("POST_ADD", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("@UserID", form.UserId);
                storedProcedure.Parameters.AddWithValue("@ThreadID", form.ThreadId);
                storedProcedure.Parameters.AddWithValue("@CreationDate", DateTime.Now);
                storedProcedure.Parameters.AddWithValue("@EditDate", DateTime.Now);
                storedProcedure.Parameters.AddWithValue("@Title", form.Title);
                storedProcedure.Parameters.AddWithValue("@Content", form.Content);

                connectionToSql.Open();
                storedProcedure.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
                throw ex;
            }
            finally
            {
                if (connectionToSql != null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }
            }
        }

        public void UpdatePost(PostDO form)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("POST_UPDATE", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("@PostID", form.PostId);
                storedProcedure.Parameters.AddWithValue("@ThreadID", form.ThreadId);
                storedProcedure.Parameters.AddWithValue("@EditDate", DateTime.Now);
                storedProcedure.Parameters.AddWithValue("@Title", form.Title);
                storedProcedure.Parameters.AddWithValue("@Content", form.Content);

                connectionToSql.Open();
                storedProcedure.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
                throw ex;
            }
            finally
            {
                connectionToSql.Close();
                connectionToSql.Dispose();
            }
        }

        public void DeletePost(int postId)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("POST_DELETE", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("@PostID", postId);

                connectionToSql.Open();
                storedProcedure.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
                throw ex;
            }
            finally
            {
                connectionToSql.Close();
                connectionToSql.Dispose();
            }
        }
    }
}
