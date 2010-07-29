<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Field>>" %>
<h2>
Campuri asignate
</h2>
<table>
<thead><tr><td>Nume</td><td>Descriere</td><td>Cod</td></tr></thead>
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
