<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeatherApp.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title></title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

</head>
<body>
  <font face="verdana">
  <form id="form1" runat="server">
  <div style="height: 373px">
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <a class="navbar-brand" runat="server" href="WeatherApp/">Weather App</a>
        </div>
     </div>
      <font size="2">&nbsp;<br />
&nbsp;&nbsp;Please enter City, State Abbreviation:<br />
      <font size="3">
      <br />
      <br />
&nbsp;&nbsp;Latest Weather Forecast:</font></div>
  
      <asp:Button ID="Button1" runat="server" style="z-index: 1; left: 293px; top: 100px; position: absolute; height: 30px; width: 166px;" Text="Display Weather" OnClick="Button1_Click" />
&nbsp;
      <font size="3">
      <br />
      <br />
      <br />
      <asp:TextBox ID="TextBox1" runat="server" style="z-index: 1; left: 10px; top: 100px; position: absolute; height: 30px; width: 259px"></asp:TextBox>
      <br />
      <br />
      <br />
      <br />
      <br />
      <asp:Image ID="Image3" runat="server" style="z-index: 1; left: 500px; top: 320px; position: absolute; height: 30px; width: 30px" BorderStyle="None" />
      <br />
      <asp:Label ForeColor="Black" ID="Label1" runat="server" style="z-index: 1; left: 10px; top: 170px; position: absolute; right: 720px; height: 360px;" Text=""></asp:Label>
      <asp:Image ID="Image2" runat="server" style="z-index: 1; left: 500px; top: 250px; position: absolute; height: 30px; width: 30px" BorderStyle="None" />
      <br />
      <br />
  
      <asp:Image ID="Image1" runat="server" style="z-index: 1; left: 500px; top: 190px; position: absolute; height: 30px; width: 30px" BorderStyle="None" />
  
  </div>
  </form>
</body>
</html>

