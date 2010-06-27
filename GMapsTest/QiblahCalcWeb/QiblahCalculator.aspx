<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QiblahCalculator.aspx.cs"
    Inherits="QiblahCalculator.QiblahCalculator" %>
<%@ Import Namespace="QiblahCalculator" %>
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
                    <label for="txtAddress">Location/Address:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtAddress" runat="server" AutoPostBack="true"></asp:TextBox>
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
                <asp:Repeater ID="rptResults" runat="server">
                <HeaderTemplate>
                <h2>Results: </h2>
                </HeaderTemplate>
                <ItemTemplate>
                <b>Address: </b><%# DataBinder.Eval(Container.DataItem, "Address") %><%--<asp:Label ID="lblAddress" runat="server" Text="[Address goes here]"></asp:Label>--%>
                <br />
                <b>Accuracy: </b><%# DataBinder.Eval(Container.DataItem, "Accuracy") %><%--<asp:Label ID="lblAccuracy" runat="server" Text="[Accuracy goes here]"></asp:Label>--%>
                <br />
                <b>Longitude: </b><%# DataBinder.Eval(Container.DataItem, "Longitude") %><%--<asp:Label ID="lblLongitude" runat="server" Text="[Longitude goes here]"></asp:Label>--%>
                <br />
                <b>Latitude: </b><%# DataBinder.Eval(Container.DataItem, "Latitude") %><%--<asp:Label ID="lblLatitude" runat="server" Text="[Latitude goes here]"></asp:Label>--%>
                <br />
                <b>Altitude: </b><%# DataBinder.Eval(Container.DataItem, "Altitude") %><%--<asp:Label ID="lblAltitude" runat="server" Text="[Altitude goes here]"></asp:Label>--%>
                <br />
                <b>Qiblah: </b>
                <%# Utils.StringizeQiblah(Utils.CalculateQiblah(
                    (double)DataBinder.Eval(Container.DataItem, "Longitude"),
                                                        (double)DataBinder.Eval(Container.DataItem, "Latitude"))) 
                    %>
                <%--<asp:Label ID="lblQiblah" runat="server" Text="[Qiblah goes here]"></asp:Label>--%>
                </ItemTemplate>
                <SeparatorTemplate>
                <hr />
                <br />
                </SeparatorTemplate>
                </asp:Repeater>
                <asp:GridView ID="gvResults" runat="server" GridLines="None" ShowHeader="false" AllowPaging="true">
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkGo" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
