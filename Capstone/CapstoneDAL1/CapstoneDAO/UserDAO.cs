using CapstoneDAL.Logging;
using CapstoneDAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace CapstoneDAL
{
    public class UserDAO
    {
        public UserDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;

        public List<UserDO> ViewAllUsers()
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            SqlDataReader reader = null;
            List<UserDO> userList = new List<UserDO>();

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("USER_VIEW_ALL", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;

                connectionToSql.Open();
                reader = storedProcedure.ExecuteReader();

                while (reader.Read())
                {
                    UserDO userDO = new UserDO();

                    //if (Session["RoleID"] == 1 || Session["RoleID"] == 2)
                    //{
                    userDO.UserId = (int)reader["UserID"];
                    userDO.RoleId = reader["RoleID"] == DBNull.Value ? 0 : (int)reader["RoleID"];
                    userDO.Email = reader["Email"] as string;
                    //userDO.Password = reader["Password"] as string;
                    //}
                    userDO.Name = reader["Name"].ToString();
                    userDO.Username = reader["Username"].ToString();
                    userList.Add(userDO);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
                throw;
            }
            finally
            {
                if (connectionToSql != null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }
            }
            return userList;
        }

        public UserDO ViewUserByID(int id)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            SqlDataReader reader = null;
            UserDO userDO = new UserDO();

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("USER_VIEW_BY_ID", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                storedProcedure.Parameters.AddWithValue("@UserID", id);

                connectionToSql.Open();
                reader = storedProcedure.ExecuteReader();

                while (reader.Read())
                {
                    userDO.UserId = (int)reader["UserID"];
                    userDO.RoleId = (int)reader["RoleID"];
                    userDO.Username = reader["Username"] as string;
                    userDO.Name = reader["Name"] as string;
                    userDO.Email = reader["Email"] as string;
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
                throw ex;
            }
            return userDO;

        }

        public void AddUser(UserDO form)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("USER_ADD", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("@RoleID", form.RoleId);
                storedProcedure.Parameters.AddWithValue("@Username", form.Username);
                storedProcedure.Parameters.AddWithValue("@Password", form.Password);
                storedProcedure.Parameters.AddWithValue("@Email", form.Email);
                storedProcedure.Parameters.AddWithValue("@Name", form.Name);

                connectionToSql.Open();
                storedProcedure.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    ex.Data["uniqueUsername"] = "Username already in use.";
                }
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

        public void UpdateUserInformation(UserDO form)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("USER_UPDATE", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("@UserID", form.UserId);
                storedProcedure.Parameters.AddWithValue("@RoleID", form.RoleId);
                storedProcedure.Parameters.AddWithValue("@Username", form.Username);
                storedProcedure.Parameters.AddWithValue("@Email", form.Email);
                storedProcedure.Parameters.AddWithValue("@Name", form.Name);

                connectionToSql.Open();
                storedProcedure.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    ex.Data["uniqueUsername"] = "Username already in use.";
                }
                else if (ex.Number == 547)
                {
                    ex.Data["invalidRoleId"] = "Invalid role ID.";
                }
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
                throw ex;
            }
            finally
            {
                connectionToSql.Close();
                connectionToSql.Dispose();
            }
        }

        public void DeleteUser(int id)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("USER_DELETE", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("@UserID", id);

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

        public bool UsernameExists(string username)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            SqlDataReader reader = null;
            bool usernameExists = false;
            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("USER_EXISTS", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("@Username", username);

                connectionToSql.Open();
                reader = storedProcedure.ExecuteReader();

                if (reader.Read())
                {
                    usernameExists = (bool)reader["UsernameExists"];
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
                throw;
            }
            finally
            {
                if (connectionToSql != null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }
            }
            return usernameExists;
        }
    }
}
