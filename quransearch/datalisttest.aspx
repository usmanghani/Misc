<%@ Page Language="C#" AutoEventWireup="true" CodeFile="datalisttest.aspx.cs" Inherits="datalisttest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="System.Web.Extensions, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>DataList Test</title>
</head>
<body>
    <form id="form1" runat="server">
    	<asp:ScriptManager ID="ScriptManager1" runat="server">
		</asp:ScriptManager>
		<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"><contenttemplate><asp:GridView 
			ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" 
			BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
			GridLines="None" onpageindexchanging="GridView1_PageIndexChanging" 
			onrowcommand="GridView1_RowCommand" 
			onselectedindexchanged="GridView1_SelectedIndexChanged"><footerstyle 
			backcolor="#99CCCC" forecolor="#003399" /><rowstyle backcolor="White" 
			forecolor="#003399" /><columns><asp:buttonfield CommandName="Show" Text="Show">
				
						
			</asp:buttonfield><asp:templatefield HeaderText="Data Items">
				
						<itemtemplate>
						<asp:Label ID ="lblDataItem" Text="<%# Container.DataItem %>" runat="server">
						</asp:Label>
							<%--<%# Container.DataItem %>--%>
																																														</itemtemplate>
				
						</asp:templatefield>
			</columns>
			<pagerstyle backcolor="#99CCCC" forecolor="#003399" horizontalalign="Left" />
			<selectedrowstyle backcolor="#009999" font-bold="True" forecolor="#CCFF99" />
			<headerstyle backcolor="#003399" font-bold="True" forecolor="#CCCCFF" 
			height="0px" />
			</asp:GridView>
			<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
			<br />
			<br />
			<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
			<br /></contenttemplate><triggers><asp:asyncpostbacktrigger ControlID="GridView1" EventName="PageIndexChanging" /></triggers></asp:UpdatePanel>
    	<p>
			This is going to be a great thing</p>
		<p>
			This is great</p>
    </form>
</body>
</html>
