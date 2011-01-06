<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.UserEditInput>" %>
<% using (Html.BeginForm()){%>
<%=Html.EditorFor(o => o.Roles) %>
<% } %>
