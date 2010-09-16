<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<FarmerInfo>>" %>
<table>
    <thead>
        <tr>
            <td>
                Nume
            </td>
            <td>
                Cod fiscal
            </td>
        </tr>
    </thead>
    <tbody>
    <% foreach (var o in Model)
       {
    %>
    <tr class="grow" value="<%:o.FarmerVersionId %>">
        <td>
            <%:o.Name %>
        </td>
        <td>
            <%:o.FiscalCode %>
        </td>
    </tr>
    <%
        } %>
        </tbody>
</table>

<script type="text/javascript">
    $("#farmerversionlist table .grow").click(growfarmerversionClick);
</script>