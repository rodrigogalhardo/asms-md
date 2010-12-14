<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MRGSP.ASMS.Core.Model.Capo>" %>
<%@ Import Namespace="MRGSP.ASMS.WebUI.Controllers" %>
<%
    var o = Model; %>
<tr id="<%= o.PoId.HasValue? "capo"+o.PoId : o.AgreementId.HasValue ? "a"+o.AgreementId : "c" + o.ContractNr %>">
    <td>
        <%=o.AgreementNr == null ? "contract" : "acord" %>
    </td>
    <td>
        <%=o.Name %>
    </td>
    <td>
        <%=o.FiscalCode %>
    </td>
    <td>
        <%=o.ContractNr %>
    </td>
    <td>
        <%=o.ContractDate.Display() %>
    </td>
    <td>
        <%=o.AgreementNr %>
    </td>
    <td>
        <%=o.AgreementDate.Display() %>
    </td>
    <td>
        <%=o.Amount.Display() %>
    </td>
    <td>
        <%=o.PoNr %>
    </td>
    <td>
        <%=o.PoDate.Display() %>
    </td>
    <td>
        <%=o.PoState.Display()%>
    </td>
    <td>
        <%=o.PoNr == null ? o.AgreementNr == null ? Html.PopupFormActionLink<PaymentOrderController>(a => a.CreateForContract(o.ContractNr),"+")
                    : Html.PopupFormActionLink<PaymentOrderController>(a => a.CreateForAgreement(o.ContractNr, o.AgreementId.Value), "+")
                    : Html.PopupFormActionLink<PaymentOrderController>(a => a.Edit(o.PoId),"e")%>
    </td>
</tr>
