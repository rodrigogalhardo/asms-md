<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FieldsInput>" %>
<table>
    <%
        foreach (Field o in Model.Fields)
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
            <%:Html.Hidden("FieldId", o.Id) %>
            <%:Html.Hidden("FieldsetId", Model.FieldsetId)%>
            <input type="submit" value="adauga" />
            </form>
        </td>
    </tr>
    <%} %>
</table>