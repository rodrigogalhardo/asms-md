<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Repor.aspx.cs" Inherits="MRGSP.ASMS.WebUI.Repor" %>
<%@ Register Assembly="Stimulsoft.Report.WebFx, Version=2010.3.900.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a"
    Namespace="Stimulsoft.Report.WebFx" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            setsize();
            $(window).bind("resize", setsize);
        });
        function setsize() {
            $(".repor").css("height", ($(window).height()) + "px");
        }
    </script>
    <style type="text/css">
        html, body, div
        {
            width: 100%;
            height: 100%;
            border: 0;
            margin: 0;
            padding: 0;
        }
        .repor
        {
            width: 100%;
            height: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:StiWebViewerFx ID="StiWebViewerFx1" runat="server" CssClass="repor" Width="100%" />
    </div>
    </form>
</body>
</html>
