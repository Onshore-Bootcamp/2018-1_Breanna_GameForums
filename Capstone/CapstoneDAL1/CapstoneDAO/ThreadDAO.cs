using CapstoneDAL.Logging;
using CapstoneDAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Capstone
{
    public class ThreadDAO
    {
        public ThreadDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;

        public List<ThreadDO> ViewAllThreads()
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            SqlDataReader reader = null;
            List<ThreadDO> threadList = new List<ThreadDO>();

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("THREAD_VIEW_ALL", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;

                connectionToSql.Open();
                reader = storedProcedure.ExecuteReader();

                while (reader.Read())
                {
                    ThreadDO threadDO = new ThreadDO();

                    threadDO.ThreadId = (int)reader["ThreadID"];
                    threadDO.Title = reader["Title"] as string;
                    threadDO.Information = reader["Information"] as string;
                    threadList.Add(threadDO);
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
            return threadList;
        }

        public ThreadDO ViewThreadByID (int Id)
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
                storedProcedure.Parameters.AddWithValue("@ThreadID", Id);

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

        public void AddThread(ThreadDO form)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("THREAD_ADD", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("@Title", form.Title);
                storedProcedure.Parameters.AddWithValue("@Information", form.Information);

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

        public void UpdateThread(ThreadDO form)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("THREAD_UPDATE", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("@ThreadID", form.ThreadId);
                storedProcedure.Parameters.AddWithValue("@Title", form.Title);
                storedProcedure.Parameters.AddWithValue("@Information", form.Information);

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

        public void DeleteThread (int Id)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("THREAD_DELETE", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("@ThreadID", Id);

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