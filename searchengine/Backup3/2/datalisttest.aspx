<%@ Page Language="C#" AutoEventWireup="true" CodeFile="datalisttest.aspx.cs" Inherits="datalisttest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>DataList Test</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;</div>
    	<asp:DataList ID="DataList1" runat="server" BackColor="White" 
		BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
		GridLines="Both" Height="130px" RepeatLayout="Flow" Width="238px">
			<footerstyle backcolor="#99CCCC" forecolor="#003399" />
			<itemstyle backcolor="White" forecolor="#003399" />
			<selecteditemstyle backcolor="#009999" font-bold="True" forecolor="#CCFF99" />
			<headerstyle backcolor="#003399" font-bold="True" forecolor="#CCCCFF" />
			<itemtemplate>
				<asp:Label ID="Label1" runat="server" Text="<%# Container.DataItem %>"></asp:Label>
			</itemtemplate>
		</asp:DataList>
		<asp:Repeater ID="Repeater1" runat="server">
		    <ItemTemplate>
		        <%# Container.DataItem %><br />
		    </ItemTemplate>
		</asp:Repeater>
    <asp:GridView ID="GridView1" runat="server" BackColor="White" 
	BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
	GridLines="None" onpageindexchanging="GridView1_PageIndexChanging" 
	onselectedindexchanged="GridView1_SelectedIndexChanged" 
	AutoGenerateColumns="False" onrowcommand="GridView1_RowCommand">
		<footerstyle backcolor="#99CCCC" forecolor="#003399" />
		<rowstyle backcolor="White" forecolor="#003399" />
		<columns>
			<asp:buttonfield CommandName="Show" Text="Show">
			</asp:buttonfield>
			<asp:templatefield HeaderText="Something"><ItemTemplate><%# Container.DataItem %>
			</ItemTemplate></asp:templatefield>
		</columns>
		<pagerstyle backcolor="#99CCCC" forecolor="#003399" horizontalalign="Left" />
		<selectedrowstyle backcolor="#009999" font-bold="True" forecolor="#CCFF99" />
		<headerstyle backcolor="#003399" font-bold="True" forecolor="#CCCCFF" 
		height="0px" />
	</asp:GridView>
	<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </form>
</body>
</html>
