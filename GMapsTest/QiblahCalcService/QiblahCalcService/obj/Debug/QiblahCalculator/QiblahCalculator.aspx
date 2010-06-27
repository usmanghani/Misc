<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QiblahCalculator.aspx.cs"
    Inherits="QiblahCalculator.QiblahCalculator" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Qiblah Calculator</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>
            Qiblah Calculator</h1>
        <table>
            <tr>
                <td>
                    Address:
                </td>
                <td>
                    <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:LinkButton ID="lnkGo" runat="server" CommandName="Calculate" 
                        PostBackUrl="~/QiblahCalculator.aspx">Go</asp:LinkButton>
                </td>
            </tr>
        </table>
        <br />
        <b>Note:</b> Address can be in any form that works with Google Maps. You can just input "city country" or "city state" for US.
        <br />
        In countries where Google Maps supports full addresses, like US, Canada, Australia, complete addresses will work also.
        <br />
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdateProgress ID="progress" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="10">
            <ProgressTemplate>
                <asp:Label ID="lblProgress" runat="server" Text="Calculating..."></asp:Label>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <b><asp:Label ID="lblResult" runat="server" Text="[PlaceHolder]" Visible="false"></asp:Label></b>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="lnkGo" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
