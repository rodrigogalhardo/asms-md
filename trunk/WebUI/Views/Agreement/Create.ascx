<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AgreementInput>" %>

<% using(Html.BeginForm()){%>
<%=Html.EditorFor(o => o.Amount) %>
<%=Html.EditorFor(o => o.Date) %>
<%=Html.HiddenFor(o => o.ContractId) %>

<%} %>