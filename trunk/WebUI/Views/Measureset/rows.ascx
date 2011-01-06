<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<MeasuresetDisplay>>" %>
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
        <%:o.State%>
    </td>
    <td>
        <%:Html.ActionLink("deschide", "open", new{o.Id}, null)%>
    </td>
    <td>
        <%:Html.ActionLink("vizualizeaza", "view", new{o.Id}, null)%>
    </td>
    <% Html.RenderPartial("ed", o.Id); %>
</tr>
<%
   } %>
