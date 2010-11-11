<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<object>>" %>
<%@ Import Namespace="System.ComponentModel" %>
<%
    var structure = ViewData["structure"] as LookupListInfo;

    var props = TypeDescriptor.GetProperties(Model.FirstOrDefault());
%>
<table>
    <thead>
        <tr>
            <% foreach (var column in structure.Captions)
               {%>
            <td>
                <%=column %>
            </td>
            <%
                } %>
        </tr>
    </thead>
    <tbody>
        <% foreach (var o in Model)
           {
        %>
        <tr class="lookupListRow" data-value="<%= props[structure.Key].GetValue(o) %>">
            <% foreach (var col in structure.Columns)
               {%>
            <td>
                <%= props[col].GetValue(o)%>
            </td>
            <%
                }%>
        </tr>
        <%
            } %>
    </tbody>
</table>
