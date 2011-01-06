<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<LocalityInput>" %>

    <% using (Html.BeginForm())
       {%>
    <%=Html.EditorFor(o => o.Name) %>
    <% } %>
