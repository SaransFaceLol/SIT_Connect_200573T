using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIT_Connect_200573T
{
    public partial class Verification : System.Web.UI.Page
    {
        string MYDBConnectionString =
        System.Configuration.ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
        public string action = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                {
                    Response.Redirect("Login.aspx", false);
                }
            }
        }
        protected string VerifyOTP(string userid)
        {
            string verificationcode = null;
            SqlConnection con = new SqlConnection(MYDBConnectionString);
            string sql = "SELECT VerificationCode FROM ACCOUNT WHERE EMAIL = @USERID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@USERID", userid);

            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        verificationcode = reader["VerificationCode"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return verificationcode;
        }
        protected void Verify(object sender, EventArgs e)
        {
            if (HttpUtility.HtmlEncode(tb_verificationcode.Text.ToString()) == VerifyOTP(Session["LoggedIn"].ToString()))
            {
                createLog();
                Response.Redirect("HomePage.aspx", false);
            }
            else
            {
                lblMsg.Text = "Verification code is incorrect";
            }
        }
        protected void createLog()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(MYDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Log VALUES (@Log, @userLog, @LogTime)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Log", "Logged in successfully");
                            cmd.Parameters.AddWithValue("@userLog", Session["LoggedIn"].ToString());
                            cmd.Parameters.AddWithValue("@LogTime", DateTime.Now);
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}