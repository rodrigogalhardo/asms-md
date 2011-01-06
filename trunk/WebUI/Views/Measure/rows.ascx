<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<Measure>>" %>
<% foreach (var o in Model)
   {
%>
 <tr id='o<%=o.Id %>'>
            <td>
                <%:o.Name %>
            </td>
            <td>
                <%:o.Description %>
            </td>
            <td>
                <%:o.NoContest ? "da" : "nu" %>
            </td>
            <%=Html.Partial("ed",o.Id) %>
        </tr>
<%
   } %>
