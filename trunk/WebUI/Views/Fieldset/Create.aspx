﻿<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<FieldsetInput>" %>

    <form action="<%=Url.Action("Create") %>" method="post">
    <%=Html.EditorFor(o => o.Name) %>
    <%=Html.EditorFor(o => o.Year) %>
    </form>
    <%=Html.ClientSideValidation<FieldsetInput>() %>
