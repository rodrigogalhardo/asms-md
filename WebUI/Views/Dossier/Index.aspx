<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<IPageable<MRGSP.ASMS.Core.Model.DossierInfo>>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <h2>
        Dosare</h2>
    <table>
        <thead>
            <tr>
                <td>
                    Nume fermier
                </td>
                <td>
                    Cod fiscal
                </td>
                <td>
                    Data crearii
                </td>
                <td>
                    Masura
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
                    <%=o.Name %>
                </td>
                <td>
                    <%=o.FiscalCode %>
                </td>
                <td>
                    <%=o.CreatedDate %>
                </td>
                <td>
                    <%=o.Measure %>
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
