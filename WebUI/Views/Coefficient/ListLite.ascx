<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<MRGSP.ASMS.Core.Model.Coefficient>>" %>
<h2>Coeficienti</h2>
<table>
<thead><tr><td>Nume</td><td>Formula</td></tr></thead>
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
</tr>
<%
    } %>
    </table>