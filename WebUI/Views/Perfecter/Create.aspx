<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<PerfecterInput>" %>

    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>

        <%=Html.EditorFor(o => o.Name) %>

    <% } %>
    <%=Html.ClientSideValidation<PerfecterInput>() %>


