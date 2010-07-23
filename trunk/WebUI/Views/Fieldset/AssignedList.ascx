<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Field>>" %>
<h2>
Campuri asignate
</h2>
<table>
    <%
        foreach (var o in Model)
        {%>
    <tr>
        <td>
            <%:o.Name %>
        </td>
        <td>
            <%:o.Description %>
        </td>
        <td>
            <%:"c" +o.Id %>
        </td>
    </tr>
    <%} %>
</table>
