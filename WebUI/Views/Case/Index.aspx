<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Core.IPageable<MRGSP.ASMS.Core.Model.Dossier>>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
<h2>Dosare</h2>
    <table>
        <thead>
            <tr>
                <td>
                    Nume
                </td>
                <td>
                    cod fiscal
                </td>
                <td>
                    nume administrator
                </td>
                <td>
                </td>
            </tr>
        </thead>
    <tbody>
    <%foreach (var o in Model.Page)
      {
    %><tr>
        <td>
            <%:o.FarmerName %>
        </td>
        <td>
            <%:o.FiscalCode %>
        </td>
        <td>
            <%:o.AdminFirstName %>
        </td>
        <td>
            <%=Html.ActionLink("deschide","open",new{o.Id}) %>
        </td>
    </tr>
    <%
        } %>
        </tbody>
        </table>
    <%=Html.Pagination() %>
</asp:Content>
