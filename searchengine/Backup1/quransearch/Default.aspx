<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="System.Web.Extensions, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <span style="font-size: 16pt"><strong>Search for:</strong></span>&nbsp;
        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="Button1_Click" /><br />
        <br />
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:DataList ID="DataList1" runat="server" CellPadding="4" ForeColor="#333333">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EFF3FB" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderTemplate>
                            Search Results
                        </HeaderTemplate>
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <ItemTemplate>
                            &nbsp;<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        </ItemTemplate>
                    </asp:DataList><br />
                    &nbsp;
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
