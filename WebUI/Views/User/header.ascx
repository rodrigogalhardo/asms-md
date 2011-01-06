<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="MRGSP.ASMS.WebUI.Controllers" %>
<h2>
    Utilizatori</h2>
<%=Html.MakePopupForm<UserController>(o => o.ChangePassword(0)) %>