<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IPageable<Measure>>" %>
<%@ Import Namespace="MRGSP.ASMS.WebUI.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Masuri</h2>

        <%=Html.MakePopupForm("Create", height:300) %>
        <%=Html.PopupFormActionLink("Create", "Creaza", htmlAttributes: new { @class="abtn" })%>
        <%=Html.MakePopupForm("Edit", new []{"id"}, height:300) %>
        <%=Html.Confirm("sunteti sigur ?") %>
    <table>
        <thead>
            <tr>
                <td>
                    nume
                </td>
                <td>
                    descriere
                </td>
                <td>
                    fara concurs
                </td><td></td><td></td>
            </tr>
        </thead>
        <tbody>
        <% foreach (var o in Model.Page)
           {%>
        <tr>
            <td>
                <%:o.Name %>
            </td>
            <td>
                <%:o.Description %>
            </td>
            <td>
                <%:o.NoContest ? "da" : "nu" %>
            </td>
            <%=Html.Partial("ed",o.Id) %>
        </tr>
        <%
            }%>
            </tbody>
    </table>
    <%=Html.Pagination() %>
</asp:Content>
