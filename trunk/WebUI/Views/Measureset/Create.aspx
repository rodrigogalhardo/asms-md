<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MeasuresetInput>" %>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>
        <%=Html.EditorFor(o => o.Name) %>
        <%=Html.EditorFor(o => o.Year) %>   
    <% } %>
    <%=Html.ClientSideValidation<MeasuresetInput>() %>
    