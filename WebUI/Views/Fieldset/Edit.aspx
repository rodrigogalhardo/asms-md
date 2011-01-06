<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<FieldsetEditInput>" %>

    <form action="<%=Url.Action("Create") %>" method="post">
    <%=Html.EditorFor(o => o.Name) %>
    </form>
