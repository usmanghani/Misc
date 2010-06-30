<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuranSearch.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="System.Web.Extensions, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">.highlight{background:yellow}</style>
    <title>Quran Search</title>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <br />
        <strong><span style="font-size: 24pt; color: #0033ff">Quran Search<br />
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <br />
                    <span style="font-size: 16pt; color: black">Search For:
                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
        <asp:RadioButtonList ID="optOperator" runat="server" Height="21px" Width="107px" RepeatDirection="Horizontal" RepeatLayout="Flow" style="font-size: 12pt; color: black">
            <asp:ListItem Selected="True">AND</asp:ListItem>
            <asp:ListItem>OR</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="Button1_Click" /><br />
                    </span>
                </ContentTemplate>
            </asp:UpdatePanel>
        </span></strong><span style="font-size: 16pt; color: black"></span>&nbsp;<br />
        <span style="color: #0033ff"><span style="font-size: 24pt"><strong>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <span style="font-size: 16pt; color: black">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                            <ProgressTemplate>
                                <span style="font-size: 12pt"><span style="color: #ffffff; background-color: #cc0033">
                                    Searching...</span> </span>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
        <asp:Label ID="lblHits" runat="server" Width="127px" style="font-size: 12pt"></asp:Label>
                        <br />
                        <br />
                        Results<br />
        <asp:ListBox ID="lstResults" runat="server" AutoPostBack="True" Height="225px" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged"
            Width="326px"></asp:ListBox><br />
                        &nbsp;</span>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Command" />
                </Triggers>
            </asp:UpdatePanel>
        </strong></span>
        </span><strong><span style="font-size: 16pt"></span></strong> <br />
        <br />
        <strong><span style="font-size: 16pt">
        </span></strong>
        <br />
        <br />
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
            <ProgressTemplate>
                <span style="background-color: #cc0033"><strong>
                <span style="color: #ffffff;">Loading...</span>
                </strong></span>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <br />
        <div style="text-align: left">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="Label1" runat="server" Style="color: red"></asp:Label><br />
                    &nbsp;
                    <div style="width: 100%; height: 100%; text-align: center;" runat = "server" id = "divResults" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lstResults" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
