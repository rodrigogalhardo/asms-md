<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MRGSP.ASMS.Infra.Dto.CrossDistrictMeasureInput>" %>

<% using(Html.BeginForm()){%>
<%=Html.HiddenFor(o => o.MeasuresetId) %>
<%=Html.EditorFor(o => o.Date) %>
<%} %>