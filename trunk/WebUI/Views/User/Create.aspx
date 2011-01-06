<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.UserCreateInput>" %>

<% using (Html.BeginForm())
   {%>
<%=Html.EditorFor(o => o.Name) %>
<%=Html.EditorFor(o => o.Password) %>
<%=Html.EditorFor(o => o.ConfirmPassword) %>
<%=Html.EditorFor(o => o.Roles) %>
<% } %>
