<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SIT_Connect_200573T.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Form</title>
    <script src="https://www.google.com/recaptcha/api.js?render=6Lf_bWUeAAAAAFXa-MJWUizkNajUCm0xp1osUp2A"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="topnav">
    <a href="Registration.aspx">Registration</a>
    <a href="Login.aspx" class="active" >Login</a>

</div>

    <fieldset>
    <legend>Login</legend>
    <p>Email: <asp:TextBox ID="tb_userid" runat="server" Height="25px" Width="137px"/> </p>
    <p>Password: <asp:TextBox ID="tb_password" runat="server" Height="25px" Width="137px"/> </p>
    <p>
        <asp:Button ID="btn_login" runat="server" Text="Login" OnClick="LoginMe" Height="29px" Width="62px"/>
    <br />
    <br />
    <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response"/>


    <asp:Label ID="lblMessage" runat="server" EnableViewState="False" > </asp:Label>
    </p>
    </fieldset>

    </div>
     
    </form>

    <script>
     grecaptcha.ready(function () {
     grecaptcha.execute(' SITE KEY ', { action: 'Login' }).then(function (token) {
     document.getElementById("g-recaptcha-response").value = token;
     });
     });
    </script>
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
