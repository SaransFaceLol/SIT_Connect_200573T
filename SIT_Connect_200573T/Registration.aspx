<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="SIT_Connect_200573T.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Registration</title>

     <div class="topnav">

<a href="Registration.aspx" class="active">Registration</a>
  <a href="Login.aspx">Login</a>
  <a href="#contact">Contact</a>
  <a href="#about">About</a>
</div>

    
    <script type="text/javascript">
        function validate() {
            var str = document.getElementById('<%=tb_password.ClientID %>').value;

            if (str.length < 12) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password length must be at least 12 Characters";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("too short");
            }
            else if (str.search(/[0-9]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password requires at least 1 number";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_number");
            }
            else if (str.search(/[A-Z]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password requires at least 1 upper case character";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_uppercase");
            }
            else if (str.search(/[a-z]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password requires at least 1 lower case character";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_lowercase");
            }
            else if (str.search(/[!@#$%^&*]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password requires at least 1 special character";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_special");
            }

            document.getElementById("lbl_pwdchecker").innerHTML = "Excellent!"
            document.getElementById("lbl_pwdchecker").style.color = "Blue";
           
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        Registration Account<br />
        <br />
        <br />
        First Name&nbsp;&nbsp;
        <asp:TextBox ID="tb_firstname" runat="server" Width="223px"></asp:TextBox>
        <br />
        <br />
        Last Name&nbsp;&nbsp;
        <asp:TextBox ID="tb_lastname" runat="server" Width="223px"></asp:TextBox>
        <br />
        <br />
        Credit Number&nbsp;&nbsp;
        <asp:TextBox ID="tb_creditnumber" runat="server" Width="223px"></asp:TextBox>
        <br />
        <br />
        Credit Date&nbsp;&nbsp;
        <asp:TextBox ID="tb_creditdate" runat="server" Width="223px"></asp:TextBox>
        <br />
        <br />
        Credit CVV&nbsp;&nbsp;
        <asp:TextBox ID="tb_creditcvv" runat="server" Width="223px"></asp:TextBox>
        <br />
        <br />
        Email&nbsp;&nbsp;
        <asp:TextBox ID="tb_email" runat="server" Width="223px"></asp:TextBox>
        <br />
        <br />
        Password&nbsp;&nbsp;
        <asp:TextBox ID="tb_password" runat="server" Width="223px" TextMode="Password"></asp:TextBox>
        <asp:Label ID="lbl_pwdchecker" runat="server" Text="pwdchecker"></asp:Label>
        <br />
        <br />
        Date Of Birth&nbsp;&nbsp;
        <asp:TextBox ID="tb_dateofbirth" runat="server" Width="223px" type="date"></asp:TextBox>
        <br />
        <br />
        Photo&nbsp;&nbsp;
        <asp:TextBox ID="tb_photo" runat="server" Width="223px"></asp:TextBox>
        <br />
        <br />

        <asp:Button ID="Button1" runat="server" OnClick="btn_checkPassword_Click" Text="Check" Width="98px" />
    &nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Submit" Width="98px" />
        <br />
    </form>
</body>
</html>

<style>
body {
  margin: 0;
  font-family: Arial, Helvetica, sans-serif;
}

.topnav {
  overflow: hidden;
  background-color: #333;
}

.topnav a {
  float: left;
  color: #f2f2f2;
  text-align: center;
  padding: 14px 16px;
  text-decoration: none;
  font-size: 17px;
}

.topnav a:hover {
  background-color: #ddd;
  color: black;
}

.topnav a.active {
  background-color: #04AA6D;
  color: white;
}
</style>
