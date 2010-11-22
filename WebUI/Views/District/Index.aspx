<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IPageable<District>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Raioane</h2>
    <%=Html.MakePopupForm("Create") %>
    <%=Html.MakePopupForm("Edit",new[]{"Id"}) %>
    <%=Html.Confirm("Sunteti sigur ?") %>
    <%=Html.PopupFormActionLink("Create", "Creaza", htmlAttributes:new{@class = "abtn"}) %>
    <table>
        <thead>
            <tr>
                <td style="width: 100%;">
                    nume
                </td>
                <td>
                    abrevierea
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
                <%:o.Code %>
            </td>
            <%=Html.Partial("ed",o.Id) %>
        </tr>
        <%
            }%>
    </table>
    <%=Html.Pagination() %>
</asp:Content>
