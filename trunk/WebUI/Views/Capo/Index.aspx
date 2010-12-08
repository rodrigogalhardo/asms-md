<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.CapoViewModel>" MasterPageFile="~/Views/Shared/Site.Master" %>

<%@ Import Namespace="MRGSP.ASMS.WebUI.Controllers" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <h2>
        ordinele de plata pentru contracte si acorduri aditionale
    </h2>

    <fieldset>
        <% Html.RenderPartial("SearchForm", Model.SearchForm); %>    
    </fieldset>

    <%=Html.MakePopupForm<PaymentOrderController>(a => a.CreateForAgreement(0,0)) %>
    <%=Html.MakePopupForm<PaymentOrderController>(a => a.CreateForContract(0)) %>
    <%=Html.MakePopupForm<PaymentOrderController>(a => a.Edit(0)) %>
    <table>
        <thead>
            <tr>
                <td>
                    tip
                </td>
                <td>
                    denumirea
                </td>
                <td>
                    cod fiscal
                </td>
                <td>
                    nr contract
                </td>
                <td>
                    data contract
                </td>
                <td>
                    nr acord
                </td>
                <td>
                    data acord
                </td>
                <td>
                    suma
                </td>
                <td>
                    nr ordin
                </td>
                <td>
                    data ordin
                </td>
                <td>
                    stare ordin
                </td>
                <td>
                </td>
            </tr>
        </thead>
        <tbody>
            <% foreach (var o in Model.List)
               {
            %>
            <tr>
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
                    : Html.PopupFormActionLink<PaymentOrderController>(a => a.CreateForAgreement(o.ContractNr, o.AgreementId.Value))
                    : Html.PopupFormActionLink<PaymentOrderController>(a => a.Edit(o.PoId),"e")%>
                </td>
            </tr>
            <%
               } %>
        </tbody>
    </table>
</asp:Content>
