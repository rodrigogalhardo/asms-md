<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DossiersByDistrictInput>" %>

<% using(Html.BeginForm()){%>
<%=Html.EditorFor(o => o.District) %>
<%=Html.EditorFor(o => o.Year) %>
<%} %>