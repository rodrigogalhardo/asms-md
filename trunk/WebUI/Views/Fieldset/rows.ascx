<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<FieldsetDisplay>>" %>
<% foreach (var o in Model)
   {
%>
<tr id='o<%=o.Id %>'>
    <td>
        <%:o.Name %>
    </td>
    <td>
        <%:o.Year %>
    </td>
    <td>
        <%:o.State %>
    </td>
    <td>
        <%:Html.ActionLink("deschide","open",new{id = o.Id}) %>
    </td>
    <td>
        <%:Html.ActionLink("vizualizeaza","view",new{id = o.Id}) %>
    </td>
    <% Html.RenderPartial("ed", o.Id); %>
</tr>
<%
   } %>
