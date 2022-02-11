<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="SIT_Connect_200573T.HomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="topnav">
  <a class="active" href="#home">Home</a>
  <a href="ChangePassword.aspx">Change Password</a>

</div>
            <fieldset>
                <legend>HomePage</legend>
                <br />
                <asp:Label ID="lbl_userid" runat="server"></asp:Label>
                <br />
                <br />
                <asp:Label ID="lblMessage" runat="server" EnableViewState="false" />
                <br />
                <br />
                <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="LogoutMe" Visible="false" />

            </fieldset>
        </div>
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
