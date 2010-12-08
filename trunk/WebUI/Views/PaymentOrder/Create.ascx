<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MRGSP.ASMS.Infra.Dto.PaymentOrderCreateInput>" %>
<form method="post" action='<%=Url.Action("Create") %>'>
<%=Html.HiddenFor(o => o.AgreementId) %>
<%=Html.HiddenFor(o => o.ContractId) %>
<%=Html.EditorFor(o => o.Nr) %>
<%=Html.EditorFor(o => o.Date) %>
</form>
