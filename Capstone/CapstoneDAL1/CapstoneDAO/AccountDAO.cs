using CapstoneDAL.Logging;
using CapstoneDAL.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace CapstoneDAL.CapstoneDAO
{
    public class AccountDAO
    {
        public AccountDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;

        public LoginDO ViewUserByUsername(LoginDO form)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            SqlDataReader reader = null;
            LoginDO loginDO = null;
            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("USER_LOGIN", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("@Username", form.Username);

                connectionToSql.Open();
                reader = storedProcedure.ExecuteReader();

                loginDO = new LoginDO();

                if (reader.Read())
                {
                    loginDO.Username = reader["Username"] as string;
                    loginDO.UserId = (int)reader["UserID"];
                    loginDO.RoleId = (int)reader["RoleID"];
                    loginDO.Password = reader["Password"] as string;
                }
                else
                {
                    loginDO = null;
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
            return loginDO;
        }

        public void RegisterUser(RegisterDO form)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("USER_REGISTER", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("@Username", form.Username);
                storedProcedure.Parameters.AddWithValue("@Password", form.Password);
                storedProcedure.Parameters.AddWithValue("@Email", form.Email);
                storedProcedure.Parameters.AddWithValue("@Name", form.Name);

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

        public bool UsernameExists(string Username)
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

                storedProcedure.Parameters.AddWithValue("@Username", Username);

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
