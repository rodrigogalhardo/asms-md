<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IPageable<Farmer>>" %>
<table>
    <thead class="ui-state-default">
        <tr>
            <th>
                Nume
            </th>
            <th>
                Cod
            </th>
        </tr>
    </thead>
    <% foreach (var o in Model.Page)
       {
    %>
    <tr class="grow" value="<%:o.Id %>">
        <td>
            <%:o.Name %>
        </td>
        <td>
            <%:o.Code %>
        </td>
    </tr>
    <%
        } %>
</table>

<%:Html.AjaxPagination(Model.PageCount, Model.PageIndex, "getFarmerPage") %>

<script type="text/javascript">
    $("#farmerlist table .grow").click(growfarmerClick);
</script>

