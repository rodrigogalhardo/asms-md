<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IPageable<Bank>>" %>
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
    <% foreach (var bank in Model.Page)
       {
    %>
    <tr class="grow" value="<%:bank.Id %>">
        <td>
            <%:bank.Name %>
        </td>
        <td>
            <%:bank.Code %>
        </td>
    </tr>
    <%
        } %>
</table>

<%:Html.AjaxPagination(Model.PageCount, Model.PageIndex, "getPage") %>

<input type="hidden" value="<%=Model.PageIndex %>" class="pageIndex" />

<script type="text/javascript">
    $(".grow").click(growClick);
</script>
