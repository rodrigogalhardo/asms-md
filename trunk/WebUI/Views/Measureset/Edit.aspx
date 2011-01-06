<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MeasuresetEditInput>" %>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>
        <%=Html.EditorFor(o => o.Name) %>
    <% } %>
    