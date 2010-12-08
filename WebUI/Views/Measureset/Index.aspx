<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IPageable<MeasuresetDisplay>>" %>

<%@ Import Namespace="MRGSP.ASMS.WebUI.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Seturi de masuri</h2>
    <%=Html.MakePopupForm<MeasuresetController>(o => o.Create()) %>
    <%=Html.MakePopupForm<MeasuresetController>(o => o.Edit(0)) %>
    <%=Html.PopupFormActionLink<MeasuresetController>(o => o.Create(), "Creaza", new{@class="abtn"}) %>
    <%=Html.Confirm("Sunteti sigur ?") %>
    <table>
        <thead>
            <tr>
                <td class="w1">
                    nume
                </td>
                <td>
                    an
                </td>
                <td>
                    stare
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </thead>
        <% foreach (var o in Model.Page)
           {%>
        <tr>
            <td>
                <%:o.Name %>
            </td>
            <td>
                <%:o.Year %>
            </td>
            <td>
                <%:o.State%>
            </td>
            <td>
                <%:Html.ActionLink("deschide", "open", new{o.Id}, null)%>
            </td>
            <td>
                <%:Html.ActionLink("vizualizeaza", "view", new{o.Id}, null)%>
            </td>
            <% Html.RenderPartial("ed", o.Id); %>
        </tr>
        <%
            }%>
    </table>
    <%=Html.Pagination() %>
</asp:Content>
