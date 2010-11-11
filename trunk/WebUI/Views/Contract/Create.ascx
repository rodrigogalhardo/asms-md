<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ContractInput>" %>
<%using (Html.BeginForm())
  {%>
<%=Html.EditorFor(o => o.Date) %>
<%=Html.EditorFor(o => o.Account) %>
<%=Html.EditorFor(o => o.BankName) %>
<%=Html.EditorFor(o => o.BankCode) %>
<%=Html.HiddenFor(o => o.DossierId) %>
<%=Html.EditorFor(o => o.SupportNr) %>
<%} %>
