<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Indicator>>" %>

<table>
<% foreach (var o in Model)
   {
%>
<tr>
    <td>
        <%:o.Name %>
    </td>
    <td>
        <%:o.Formula %>
    </td>
    <td>
       <%:"i"+o.Id %>
    </td>
</tr>
<%
    } %>
    </table>