<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="StudentRegistration.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table align="center">
                <tr>
                    <td>StudentName</td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" placeholder="Enter Your Name"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Age</td>
                    <td>
                        <asp:TextBox ID="txtAge" runat="server" placeholder="Enter Your Age"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Address</td>
                    <td>
                        <asp:TextBox ID="txtAdd" runat="server" placeholder="Enter Your Address"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Contact Number</td>
                    <td>
                        <asp:TextBox ID="txtNum" runat="server" placeholder="Enter Your Contact Number"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>State</td>
                    <td>
                        <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" placeholder="Select Your State">
                            <asp:ListItem Text="- Select State -" Value="" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>City</td>
                    <td>
                        <asp:DropDownList ID="ddlCity" runat="server" placeholder="Select Your City">
                            <asp:ListItem Text="- Select City -" Value="" />
                        </asp:DropDownList>
                    </td>
                </tr>
                
                <tr>
                    <td><asp:Button ID="btnReg" runat="server" Text="Register" OnClick="btnReg_Click" /></td>
                </tr>
                
            </table>
        </div>
        

    </form>
</body>
</html>
