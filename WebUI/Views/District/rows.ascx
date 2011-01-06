<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<District>>" %>
<% foreach (var o in Model)
   {
%>
<tr id='o<%=o.Id %>'>
    <td>
        <%:o.Name %>
    </td>
    <td>
        <%:o.Code %>
    </td>
    <%=Html.Partial("ed",o.Id) %>
</tr>
<%
   } %>
