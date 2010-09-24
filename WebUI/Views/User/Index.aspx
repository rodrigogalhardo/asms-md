<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IPageable<MRGSP.ASMS.Core.Model.User>>" %>

<%@ Import Namespace="MRGSP.ASMS.WebUI.Controllers" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Utilizatori</h2>
    <%=Html.MakePopup<UserController>(o => o.Create(), height:500) %>
    <%=Html.MakePopup<UserController>(o => o.Edit(0),width:500, height:400) %>
    <%=Html.MakePopup<UserController>(o => o.ChangePassword(0)) %>
    <%=Html.PopupActionLink<UserController>(o => o.Create(), "Creaza") %>
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
                <%=Html.PopupActionLink<UserController>(o => o.ChangePassword(item.Id), "Schimba parola") %>
                <%=Html.PopupActionLink<UserController>(o => o.Edit(item.Id), "Editeaza") %>
            </td>
        </tr>
        <% } %>
    </table>
    <%=Html.Pagination() %>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
