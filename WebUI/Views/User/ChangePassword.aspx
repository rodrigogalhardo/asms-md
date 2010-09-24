<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<ChangePasswordInput>" %>

    <% using (Html.BeginForm())
       {%>
    <%= Html.ValidationSummary(true) %>
    <%= Html.HiddenFor(model => model.Id) %>
        <%=Html.EditorFor(o => o.Password) %>
        <%=Html.EditorFor(o => o.ConfirmPassword) %>        
    <% } %>
    <%=Html.ClientSideValidation<ChangePasswordInput>() %>
