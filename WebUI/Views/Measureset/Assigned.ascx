<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Measure>>" %>
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
            <form action="<%:Url.Action("Unassign") %>" method="post">
            <%:Html.Hidden("MeasureId", o.Id) %>
            <%:Html.Hidden("MeasuresetId", ViewData["msi"])%>
            <input type="submit" value="sterge" />
            </form>
        </td>
    </tr>
    <%} %>
</table>

