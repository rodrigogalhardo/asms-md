<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<DistrictInput>" %>

    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>

        <%=Html.EditorFor(o => o.Name) %>
        <%=Html.EditorFor(o => o.Code) %>    
    <% } %>