<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IPageable<MRGSP.ASMS.Core.Model.User>>" %>

<%@ Import Namespace="MRGSP.ASMS.WebUI.Controllers" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Utilizatori</h2>
    <%=Html.MakePopupForm<UserController>(o => o.Create(), height:500) %>
    <%=Html.MakePopupForm<UserController>(o => o.Edit(0),width:500, height:400) %>
    <%=Html.MakePopupForm<UserController>(o => o.ChangePassword(0)) %>
    <%=Html.PopupFormActionLink<UserController>(o => o.Create(), "Creaza") %>
    <table>
        <thead>
            <tr>
                <td>
                    Nume
                </td>
                <td>
                </td>
            </tr>
        </thead>
        <% foreach (var item in Model.Page)
           { %>
        <tr>
            <td>
                <%=item.Name %>
            </td>
            <td>
                <%=Html.PopupFormActionLink<UserController>(o => o.ChangePassword(item.Id), "Schimba parola") %>
                <%=Html.PopupFormActionLink<UserController>(o => o.Edit(item.Id), "Editeaza") %>
            </td>
        </tr>
        <% } %>
    </table>
    <%=Html.Pagination() %>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
