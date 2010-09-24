<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<FieldInput>" %>

    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>

        <%=Html.EditorFor(o => o.Name) %>
        <%=Html.EditorFor(o => o.Description) %> 
    <% } %>
    <%=Html.ClientSideValidation<FieldInput>() %>

