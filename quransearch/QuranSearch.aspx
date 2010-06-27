<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuranSearch.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="System.Web.Extensions, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        .highlight
        {
            background: yellow;
        }
    .newStyle1 {
	background-color: #CCCCCC;
}
.newStyle2 {
	background-color: #CCCCCC;
}
.newStyle3 {
}
    </style>
    <title>Quran Search</title>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <br />
    <strong><span style="font-size: 24pt; color: #0033ff">Quran Search<br />
        <asp:UpdatePanel ID="UpdatePanel3" runat="server"><ContentTemplate><br /><span style="font-size: 16pt; color: black">Search For: <asp:TextBox ID="txtSearch" runat="server" AutoCompleteType="Search"></asp:TextBox><asp:Button
                        ID="btnSearch" runat="server" Text="Search" OnClick="Button1_Click" /><br /></span><strong><span style="font-size: 24pt; color: #0033ff"><span style="font-size: 16pt;
                    color: black"><br /><asp:RadioButtonList ID="optOperator" 
			runat="server" Height="21px"
                        Style="font-size: 12pt; color: black" Width="248px" 
			CssClass="newStyle1"><asp:ListItem Selected="True" Value="AND">Search for all terms</asp:ListItem><asp:ListItem Value="OR">Search for any terms</asp:ListItem></asp:RadioButtonList></span></span></strong></ContentTemplate></asp:UpdatePanel>
    </span></strong>
    <hr />
    <span style="font-size: 16pt; color: black"></span>&nbsp;<span style="color: #0033ff"><span
        style="font-size: 24pt"><strong><asp:UpdatePanel ID="UpdatePanel2" runat="server"
            UpdateMode="Conditional"><ContentTemplate><span 
			style="font-size: 16pt; color: black"><asp:updateprogress ID="UpdateProgress2" 
			runat="server" AssociatedUpdatePanelID="UpdatePanel3"><progresstemplate><span 
					style="font-size: 12pt"><span style="color: #ffffff; background-color: #cc0033">Searching...</span> </span></progresstemplate>
			</asp:updateprogress>
			<asp:Label ID="lblHits" runat="server" Height="16px" Style="font-size: 12pt" 
			Width="368px"></asp:Label>
			<br />
			<br />
			<asp:GridView ID="grdResults" runat="server" AutoGenerateColumns="False" 
			CellPadding="4" ForeColor="#333333" GridLines="None" 
			OnPageIndexChanging="grdResults_PageIndexChanging" 
			OnRowCommand="grdResults_RowCommand">
				<footerstyle backcolor="#507CD1" font-bold="True" forecolor="White" />
				<rowstyle backcolor="#EFF3FB" />
				<columns>
					<asp:buttonfield CommandName="Show" Text="Show">
					</asp:buttonfield>
					<asp:boundfield DataField="Para" HeaderText="Para">
					</asp:boundfield>
					<asp:boundfield DataField="Surah" HeaderText="Sura">
					</asp:boundfield>
					<asp:boundfield DataField="Ayah" HeaderText="Ayah">
					</asp:boundfield>
				</columns>
				<pagerstyle backcolor="#2461BF" forecolor="White" horizontalalign="Center" />
				<selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
				<headerstyle backcolor="#507CD1" font-bold="True" forecolor="White" />
				<editrowstyle backcolor="#2461BF" />
				<alternatingrowstyle backcolor="White" />
			</asp:GridView>
			<br />&nbsp;</span> </ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Command" /><asp:AsyncPostBackTrigger ControlID="grdResults" EventName="PageIndexChanging" /></Triggers></asp:UpdatePanel></strong></span></span><br />
    <hr />
    <div style="text-align: center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:updateprogress 
			ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2"><progresstemplate><span 
				style="background-color: #cc0033"><strong style="text-align: center"><span 
				style="color: #ffffff; text-align: center;">Loading...</span> </strong></span></progresstemplate>
			</asp:updateprogress>
			&nbsp;<div style="width: 100%; height: 100%; text-align: center;" runat="server"
                    id="divResults" /></ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="grdResults" EventName="RowCommand" /></Triggers></asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
