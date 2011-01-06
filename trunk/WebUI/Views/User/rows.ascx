<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<User>>" %>
<%@ Import Namespace="MRGSP.ASMS.WebUI.Controllers" %>
<% foreach (var o in Model)
   {
%>
<tr id='o<%=o.Id %>'>
    <td>
        <%=o.Name %>
    </td>
    <td>
        <%=Html.PopupFormActionLink<UserController>(v => v.ChangePassword(o.Id), "Schimba parola") %>
        </td><td>
        <%=Html.PopupFormActionLink<UserController>(v => v.Edit(o.Id), "Editeaza") %>
    </td>
</tr>
<%
   } %>
