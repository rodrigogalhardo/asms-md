<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Indicator>>" %>
<h2>Indicatori</h2>
<table class='atbl'>
<thead>
<tr><td>Nume</td><td>Formula</td><td>Cod</td></tr>
</thead>
<% foreach (var o in Model)
   {
%>
<tr>
    <td>
        <%:o.Name %>
    </td>
    <td>
        <%:o.Formula %>
    </td>
    <td>
       <%:"i"+o.Id %>
    </td>
</tr>
<%
    } %>
    </table>