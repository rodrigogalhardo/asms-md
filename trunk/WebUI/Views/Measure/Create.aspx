<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MeasureInput>" %>

    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>
    <%=Html.EditorFor(o => o.Name) %>
    <%=Html.EditorFor(o => o.Description) %>
    <%=Html.EditorFor(o => o.NoContest) %>
    <% } %>
