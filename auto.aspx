<%@ Page Language="VB" AutoEventWireup="false" CodeFile="auto.aspx.vb" Inherits="auto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv ="refresh" content ="60" />
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 71px;
        }
        .auto-style3 {
            width: 71px;
            height: 60px;
        }
        .auto-style4 {
            height: 60px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p class="MsoNormal">
                Employee Work Scheduling System <o:p></o:p>
            </p>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style3">Time</td>
                    <td class="auto-style4">&nbsp;<asp:TextBox ID="timestamp" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
        <br />
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
