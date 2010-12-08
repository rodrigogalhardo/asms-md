<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MRGSP.ASMS.Core.Model.Fpi>" %>
<% if(Model.State == FpiState.Contest) {%>
<%=Html.Confirm("Daca treceti la etapa acordurilor, recalcule si autorizarea dosarelor nu vor mai putea fi efectuate, sunteti sigur ?", "confirmAgreement") %>
<form method="post" action="<%=Url.Action("GoAgreement",new{Model.Id})%>">
<input type="submit" value="spre acorduri" class="confirmAgreement" />
</form>
<%}%>

<% if (Model.State == FpiState.Contest || Model.State == FpiState.Agreement){%>
<%=Html.Confirm("Daca inchideti luna recalcule, autorizarea dosarelor si acorduri aditionale pe aceasta luna nu vor mai putea fi efectuate, sunteti sigur ?", "confirmSeal") %>
<form method="post" action="<%=Url.Action("Seal",new{Model.Id})%>">
<input type="submit" value="inchide" class="confirmSeal" />
</form>
<%} %>
