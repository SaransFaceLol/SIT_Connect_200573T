using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIT_Connect_200573T
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        string MYDBConnectionString =
        System.Configuration.ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {

        }

        protected void Change(object sender, EventArgs e)
        {
            string sql = "UPDATE [Account] SET [PasswordHash] = @PasswordHash, [PasswordSalt] = @PasswordSalt WHERE Email = @USERID";
            using (SqlConnection con = new SqlConnection(MYDBConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        //string pwd = get value from your Textbox
                        string pwd = tb_password.Text.ToString().Trim(); ;
                        //Generate random "salt"
                        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                        byte[] saltByte = new byte[8];
                        //Fills array of bytes with a cryptographically strong sequence of random values.
                        rng.GetBytes(saltByte);
                        salt = Convert.ToBase64String(saltByte);
                        SHA512Managed hashing = new SHA512Managed();
                        string pwdWithSalt = pwd + salt;
                        byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                        byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                        finalHash = Convert.ToBase64String(hashWithSalt);
                        RijndaelManaged cipher = new RijndaelManaged();
                        cipher.GenerateKey();

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                        cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                        cmd.Parameters.AddWithValue("@USERID",Session["LoggedIn"].ToString());
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        Response.Redirect("HomePage.aspx", false);

                    }

                }
            }

            
        }
        protected void btn_checkPassword_Click(object sender, EventArgs e)
        {
            // implement codes for the button event
            // Extract data from textbox
            int scores = CheckPassword(tb_password.Text);
            string status = "";
            switch (scores)
            {
                case 1:
                    status = "Password needs to be at least 12 characters long";
                    break;
                case 2:
                    status = "Password lacks a upper case character,digit and a special character ";
                    break;
                case 3:
                    status = "Password lacks a digit and a special character";
                    break;
                case 4:
                    status = "Password lacks a special character";
                    break;
                case 5:
                    status = "Password consists of a combination of digits,upper case,lower case characters and a special character and is 12 characters long";
                    break;
                default:
                    break;
            }
            lbl_pwdchecker.Text = "Status : " + status;
            if (scores < 4)
            {
                lbl_pwdchecker.ForeColor = Color.Red;
                return;
            }
            lbl_pwdchecker.ForeColor = Color.Green;
        }

        private int CheckPassword(string password)
        {
            int score = 0;

            if (password.Length < 12)
            {
                return 1;
            }
            else
            {
                score = 1;
            }
            if (Regex.IsMatch(password, "[a-z]"))
            {
                score++;
            }

            if (Regex.IsMatch(password, "[A-Z]"))
            {
                score++;
            }

            if (Regex.IsMatch(password, "[!@#$%^&*]"))
            {
                score++;
            }

            if (Regex.IsMatch(password, "[0-9]"))
            {
                score++;
            }

            return score;
        }


    }
}