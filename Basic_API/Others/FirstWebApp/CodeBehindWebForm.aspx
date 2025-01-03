<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CodeBehindWebForm.aspx.cs" Inherits="FirstWebApp.CodeBehindWebForm" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #TextArea1 {
            width: 575px;
            height: 144px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button Text="hit me button" runat="server" ID="hitMeButton" OnClick="hitMeButton_Click" />
        </div>
    </form>

    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" />
</body>
</html>
