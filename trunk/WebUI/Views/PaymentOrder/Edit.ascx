<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MRGSP.ASMS.Infra.Dto.PaymentOrderEditInput>" %>

<% using (Html.BeginForm())
   {%>
<%=Html.EditorFor(o => o.Nr) %>
<%=Html.EditorFor(o => o.Date) %>
<%=Html.EditorFor(o => o.State) %>
<%} %>