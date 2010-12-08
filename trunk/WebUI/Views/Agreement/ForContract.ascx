<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<MRGSP.ASMS.Core.Model.Agreement>>" %>
<%@ Import Namespace="MRGSP.ASMS.WebUI.Controllers" %>
<%=Html.MakePopupForm<AgreementController>(o => o.Create(0)) %>
<%=Html.MakePopupForm<AgreementController>(o => o.Edit(0)) %>
<%=Html.PopupFormActionLink<AgreementController>(o => o.Create((int)ViewData["contractId"]), "Adauga acord aditional",new{@class= "abtn"})%>

<%=Html.Report("agreement","Id") %>
<table>
    <thead>
        <tr>
            <td>
                Nr
            </td><td class="w1">
                Suma adaugata
            </td>
            <td></td>
            <td></td>
        </tr>
    </thead>
    <tbody>
        <% foreach (var o in Model)
           {
        %>
        <tr>
            <td>
                <%:o.Nr %>
            </td>
            <td>
                <%:o.Amount %>
            </td> 
            <td>
            <a href="javascript:agreement({Id: '<%=o.Id %>'});" > Raport</a>
            </td>
            <td>
                <%=Html.PopupFormActionLink<AgreementController>(a => a.Edit(o.Id)) %>
            </td>
        </tr>
        <%
            }%>
    </tbody>
</table>
