using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CurdOperationUsingAngular.Models;

namespace CurdOperationUsingAngular.DataAccessLayer
{
    public class UserDAL
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConn"].ConnectionString);

        //Getting All Users
        public List<UserTable> GetAllUsers()
        {
            List<UserTable> users = new List<UserTable>();

            SqlCommand command = new SqlCommand("sp_getallusers", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();

            connection.Open();
            adapter.Fill(dt);
            connection.Close();

            foreach (DataRow dataRow in dt.Rows)
            {
                users.Add(new UserTable
                {
                    Id = Convert.ToInt32(dataRow["ID"]),
                    Name = dataRow["Name"].ToString(),
                    Email = dataRow["email"].ToString(),
                    Age = Convert.ToInt32(dataRow["Age"]),
                    Mobilenum = Convert.ToDouble(dataRow["Mobilenum"])
                });
            }
            return users;
        }


        //Adding User
        public bool AddUser(UserTable user)
        {
            int i = 0;
            SqlCommand command = new SqlCommand("sp_insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Age", user.Age);
            command.Parameters.AddWithValue("@Mobilenum", user.Mobilenum);

            connection.Open();
            i = command.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        //Getting User By Id
        public UserTable GetUserById(int id)
        {
            SqlCommand command = new SqlCommand("sp_getuserbyid", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);
            DataTable dt = new DataTable();

            adapter.Fill(dt);
            UserTable users = new UserTable();
            if (dt.Rows.Count > 0)
            {
                users.Id = Convert.ToInt32(dt.Rows[0]["ID"]);
                users.Name = dt.Rows[0]["Name"].ToString();
                users.Email = dt.Rows[0]["email"].ToString();
                users.Age = Convert.ToInt32(dt.Rows[0]["Age"]);
                users.Mobilenum = Convert.ToDouble(dt.Rows[0]["Mobilenum"]);
            }
            return users;
        }


        //Updating user data
        public bool UpdateUser(int id, UserTable user)
        {
            int i = 0;
            SqlCommand command = new SqlCommand("sp_updateuser", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Age", user.Age);
            command.Parameters.AddWithValue("@Mobilenum", user.Mobilenum);

            connection.Open();
            i = command.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //Delete User
        public bool DeleteUser(int id)
        {
            int i = 0;
            SqlCommand command = new SqlCommand("sp_deleteuserbyid", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            i = command.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}