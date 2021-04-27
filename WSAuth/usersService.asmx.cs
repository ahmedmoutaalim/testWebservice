using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WSAuth
{
    /// <summary>
    /// Summary description for usersService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class usersService : System.Web.Services.WebService
    {

           [WebMethod]
            public List<user> listuser( )
            {
                List<user> users = new List<user>();

                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("SP_Getuser", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                  
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                    user user  = new user();

                    user.id = Convert.ToInt32(rdr["id"]);
                    user.username = rdr["username"].ToString();
                    user.firstname = rdr["firstName"].ToString();
                    user.lastname = rdr["lastname"].ToString();
                    user.creationdate = DateTime.Parse(rdr["creationDate"].ToString()) ;

                    users.Add(user);
                }
                }

                return users;
            }




        [WebMethod]
        public user createuser ( string username, string password, string firstname, string lastname)
        {
            user user = new user();

            user.username = username;
            user.password = password;
            user.firstname = firstname;
            user.lastname = lastname;
            user.creationdate = DateTime.Now;

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SP_insertuser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();
                cmd.Parameters.AddWithValue("@username", user.username);
                cmd.Parameters.AddWithValue("@password ", user.password);
                cmd.Parameters.AddWithValue("@firstName ", user.firstname);
                cmd.Parameters.AddWithValue("@lastname ", user.lastname);
                cmd.Parameters.AddWithValue("@creationDate ", user.creationdate);



                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }

            return user;
        }



        [WebMethod]
        public user loginuser(string username, string password)
        {
            user user = new user();

            user.username = username;
            user.password = password;
            

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SP_getlogin", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();
                cmd.Parameters.AddWithValue("@username", user.username);
                cmd.Parameters.AddWithValue("@password ", user.password);
             


                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    user.id = Convert.ToInt32(rdr["id"]);
                    user.username = rdr["username"].ToString();
                    user.firstname = rdr["firstName"].ToString();
                    user.lastname = rdr["lastname"].ToString();
                    



                }
                con.Close();

            }

            return user;
        }



    }
}
