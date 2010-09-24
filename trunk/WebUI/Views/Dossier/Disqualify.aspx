<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.DisqualifyInput>" %>

<% using(Html.BeginForm()){%>
<%=Html.HiddenFor(o => o.DossierId) %>
<%=Html.EditorFor(o => o.Reason) %>
<%} %>

<%=Html.ClientSideValidation<DisqualifyInput>() %>