<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IPageable<Perfecter>>" %>

<%@ Import Namespace="MRGSP.ASMS.WebUI.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Campuri</h2>
    <%=Html.MakePopup<PerfecterController>(o => o.Create(),height:200) %>
    <%=Html.PopupActionLink<PerfecterController>(o => o.Create(), "Creaza") %>
    <table>
        <thead>
            <tr>
                <td>
                    nume
                </td>
            </tr>
        </thead>
        <% foreach (var o in Model.Page)
           {%>
        <tr>
            <td>
                <%:o.Name %>
            </td>
        </tr>
        <%
            }%>
    </table>
    <%=Html.Pagination() %>
</asp:Content>
