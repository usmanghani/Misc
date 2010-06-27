<%@ Page Language="C#" AutoEventWireup="true" Inherits="QiblahCalculator.QiblahCalcMobile"
    CodeBehind="QiblahCalcMobile.aspx.cs" %>

<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <mobile:Form ID="Form1" Runat="server">
            <b>Qiblah Calculator</b>
            <br /><br />
            Location/Address: <br />
                    <mobile:TextBox ID="txtAddress" Runat="server">
                    </mobile:TextBox>
                    <mobile:Command ID="cmdGo" Runat="server" CommandName="Calculate" SoftkeyLabel="Go" OnClick="cmdGo_Click">Go</mobile:Command>
                    <br />
                    <b>
                        <mobile:Label ID="lblResult" Runat="server" Text="[PlaceHolder]" Visible="false">
                        </mobile:Label></b>
<br />
        <b>Note: </b> Address can be in any form that works with Google Maps. You can just input "city country" or "city state" for US.
        In countries where Google Maps supports full addresses, like US, Canada, Australia, complete addresses will work also.
    </mobile:Form>
</body>
</html>
