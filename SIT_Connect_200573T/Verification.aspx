<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Verification.aspx.cs" Inherits="SIT_Connect_200573T.Verification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <table style="border:solid;border-width:thin">
        <tr>
            <td><asp:Label ID="lblmobele" runat="server" Text="Enter Mobile Number"></asp:Label></td>
            <td><asp:TextBox ID="txtmobileno" runat="server"></asp:TextBox></td>
        </tr>
            <tr>
                <td></td>
                <td><asp:Button ID="btnSendOTP" runat="server" Text="Send OTP" OnClick="btnSendOTP_Click"></asp:Button></td>

            </tr>
     </table>
      <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
