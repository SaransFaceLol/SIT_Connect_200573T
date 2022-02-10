<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Verification.aspx.cs" Inherits="SIT_Connect_200573T.Verification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   <div>
    
            <p>Verification Code:<asp:TextBox ID="tb_verificationcode" runat="server" Height="25px" Width="100px" /></p>
           <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="Verify" Height="25px" Width="75px"></asp:Button>
    
      <asp:Label ID="lblMsg" runat="server" Text="Error message here(lblMessage)" Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>
