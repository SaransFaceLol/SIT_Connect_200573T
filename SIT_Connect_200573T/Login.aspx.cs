using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;

namespace SIT_Connect_200573T
{
    public partial class Login : System.Web.UI.Page
    {
        string MYDBConnectionString =
       System.Configuration.ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
        public string action = null;
        static string randomotp;
        

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginMe(object sender, EventArgs e)
        {
            string pwd = HttpUtility.HtmlEncode(tb_password.Text.ToString().Trim());
            string userid = HttpUtility.HtmlEncode(tb_userid.Text.ToString().Trim());
            SHA512Managed hashing = new SHA512Managed();
            string dbHash = getDBHash(userid);
            string dbSalt = getDBSalt(userid);


           // if (ValidateCaptcha())

                try
                {
                    if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                    {
                        string pwdWithSalt = pwd + dbSalt;
                        byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                        string userHash = Convert.ToBase64String(hashWithSalt);

                        if (userHash.Equals(dbHash))
                        {
                            Session["LoggedIn"] = tb_userid.Text;
                            string guid = Guid.NewGuid().ToString();
                            Session["AuthToken"] = guid;
                            Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                            Response.Redirect("HomePage.aspx", false);

                            Random random = new Random();
                            randomotp = random.Next(000000, 999999).ToString();
                            OTPgenerator(userid, randomotp);
                            SendVerificationCode(randomotp);
                            Response.Redirect("Verification.aspx", false);





                    }
                    else
                        {
                            lblMessage.Text = "Email or password is invalid. Please try again";
                            Response.Redirect("Login.aspx", false);
                            action = "Failed to log in";
                            
                        }

                        

                }
                    else
                    {
                        lblMessage.Text = "Wrong username or password";
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally { }
        }

        protected string getDBHash(string userid)
        {
            string h = null;
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "select PasswordHash FROM Account WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["PasswordHash"] != null)
                        {
                            if (reader["PasswordHash"] != DBNull.Value)
                            {
                                h = reader["PasswordHash"].ToString();
                            }
                        }
                    }

                }
            }
            catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return h;
        }

        protected string getDBSalt(string userid)
        {
            string s = null;
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "select PasswordSalt FROM ACCOUNT WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PasswordSalt"] != null)
                        {
                            if (reader["PasswordSalt"] != DBNull.Value)
                            {
                                s = reader["PasswordSalt"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return s;
        }


        public class MyObject
        {
            public string success { get; set; }
            public List<string> ErrorMessage { get; set; }
        }

        public bool ValidateCaptcha()
        {
            bool result = true;
            string captchaResponse = Request.Form["g-recaptcha-response"];
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create
        ("https://www.google.com/recaptcha/api/siteverify?secret=6Lf_bWUeAAAAACxFiNPUQda_A9gg8q3mUY5mG5eK &response" + captchaResponse);

            try
            {
                using (WebResponse wResponse = req.GetResponse())
                {
                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        string jsonResponse = readStream.ReadToEnd();
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        MyObject jsonObject = js.Deserialize<MyObject>(jsonResponse);
                        result = Convert.ToBoolean(jsonObject.success);

                    }
                }
                return result;
            }
            catch (WebException ex)
            {
                throw ex;
            }

        }

        protected string OTPgenerator(string userid, string randomNumber)
        {
            string verificationcode = null;
            SqlConnection con = new SqlConnection(MYDBConnectionString);
            string sql = "UPDATE ACCOUNT SET VerificationCode = @VerificationCode WHERE EMAIL = @USERID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@VerificationCode", randomNumber);
            cmd.Parameters.AddWithValue("@USERID", userid);

            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["VerificationCode"] != null)
                        {
                            verificationcode = reader["VerificationCode"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { con.Close(); }
            return verificationcode;
        }
        protected string SendVerificationCode(string verificationcode)
        {
            string address = "SITConnect <nypasassignment@gmail.com>";
            string str = null;
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("nypasassignment@gmail.com", "MYDBCONNECTION"),
                EnableSsl = true
            };
            var message = new MailMessage
            {
                Subject = "Application Security Verification Code",
                Body = "Your verification is " + verificationcode + "\n Please input this to login."
            };
            message.To.Add(tb_userid.Text.ToString());
            message.From = new MailAddress(address);
            try
            {
                smtpClient.Send(message);
                return str;
            }
            catch
            {
                throw;
            }
        }

    }

}