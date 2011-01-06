<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<MRGSP.ASMS.Core.Model.Measure>>" %>

<table class='atbl'>
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
            <form action="<%:Url.Action("Assign") %>" method="post">
            <%:Html.Hidden("MeasureId", o.Id) %>
            <%:Html.Hidden("MeasuresetId", ViewData["msi"])%>
            <input type="submit" value="adauga" />
            </form>
        </td>
    </tr>
    <%} %>
</table>

