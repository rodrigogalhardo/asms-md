<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Coefficient>>" %>

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
        <form action="<%:Url.Action("Delete",new{coefficientId=o.Id, fieldsetId = o.FieldsetId}) %>" method="post">
        <input type="submit" value="sterge" />
        </form>
    </td>
</tr>
<%
    } %>
    </table>