<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<IPageable<FarmerInfo>>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <table>
        <thead>
            <tr>
                <td>
                    Nume
                </td>
                <td>
                    Cod Fiscal
                </td>
                <td></td>
            </tr>
        </thead>
        <tbody>
            <% foreach (var o in Model.Page)
               {
            %>
            <tr>
                <td>
                    <%=o.Name %>
                </td>
                <td>
                    <%=o.FiscalCode %>
                </td>
                <td>
                    <%=Html.ActionLink("deschide", "Open","Farmer", new{o.Id}, null) %>
                </td>
            </tr>
            <%
                } %>
        </tbody>
    </table>
    <%=Html.Pagination() %>
</asp:Content>
