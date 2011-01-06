<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<Field>>" %>
<% foreach (var o in Model)
   {
%>
<tr id='o<%=o.Id %>'>
    <td>
        <%:o.Name %>
    </td>
    <%=Html.Partial("ed", o.Id) %>
</tr>
<%
   } %>
