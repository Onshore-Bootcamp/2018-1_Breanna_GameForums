using CapstoneDAL.Logging;
using CapstoneDAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace CapstoneDAL.CapstoneDAO
{
    public class RoleDAO
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;

        public RoleDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<RoleDO> ViewAllRoles ()
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            SqlDataReader reader = null;
            List<RoleDO> roleList = new List<RoleDO>();

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("ROLE_VIEW_ALL", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;

                connectionToSql.Open();
                reader = storedProcedure.ExecuteReader();

                while (reader.Read())
                {
                    RoleDO roleDO = new RoleDO();

                    roleDO.RoleId = (int)reader["RoleID"];
                    roleDO.Name = reader["Name"] as string;
                    roleList.Add(roleDO);
                }
            }
            catch (SqlException ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
                throw ex;
            }
            catch (Exception baseEx)
            {
                Logger.Log("Fatal", baseEx.TargetSite.ToString(), baseEx.Message, baseEx.StackTrace);
                throw baseEx;
            }
            finally
            {
                if (connectionToSql != null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }
            }
            return roleList;
        }

        public RoleDO ViewRoleById (int Id)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            SqlDataReader reader = null;
            RoleDO roleDO = new RoleDO();

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("ROLE_VIEW_BY_ID", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                storedProcedure.Parameters.AddWithValue("@RoleID", Id);

                connectionToSql.Open();
                reader = storedProcedure.ExecuteReader();

                while (reader.Read())
                {
                    roleDO.RoleId = (int)reader["RoleID"];
                    roleDO.Name = reader["Name"] as string;
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Fatal", ex.TargetSite.ToString(), ex.Message, ex.StackTrace);
                throw ex;
            }
            return roleDO;
        }

        public void AddRole (RoleDO form)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("ROLE_ADD", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;

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

        public void UpdateRole (RoleDO form)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("ROLE_UPDATE", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("@RoleID", form.RoleId);
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
                connectionToSql.Close();
                connectionToSql.Dispose();
            }
        }

        public void DeleteRole (int Id)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("ROLE_DELETE", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("@RoleID", Id);

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

        public bool RoleExists(string name)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            SqlDataReader reader = null;
            bool roleExists = false;
            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("ROLE_EXISTS", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("@Name", name);

                connectionToSql.Open();
                reader = storedProcedure.ExecuteReader();

                if (reader.Read())
                {
                    roleExists = (bool)reader["RoleExists"];
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
            return roleExists;
        }
    }
}
