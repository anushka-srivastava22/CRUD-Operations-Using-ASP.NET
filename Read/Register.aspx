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
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowEditing="GridView1_RowEditing">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-Width="50" ReadOnly="true" />
                    <asp:BoundField DataField="name" HeaderText="Name" ItemStyle-Width="150" />
                    <asp:BoundField DataField="age" HeaderText="Age" ReadOnly="true" ItemStyle-Width="50" DataFormatString="{0:N0}" />
                    <asp:BoundField DataField="state" HeaderText="State" ItemStyle-Width="50" />
                    <asp:BoundField DataField="city" HeaderText="City" ItemStyle-Width="50" />
                    <asp:BoundField DataField="address" HeaderText="Address" ItemStyle-Width="150" />
                    <asp:BoundField DataField="number" HeaderText="Number" ItemStyle-Width="100" />
                    <asp:HyperLinkField Text="View" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/View.aspx?id={0}" />
                    <asp:CommandField ShowEditButton="True" ButtonType="Link" EditText="Edit" />
                </Columns>
            </asp:GridView>
        </div>

    </form>
</body>
</html>
