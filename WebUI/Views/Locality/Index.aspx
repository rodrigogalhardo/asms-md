<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IPageable<Locality>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Localități</h2>
    <%=Html.MakePopupForm("Create") %>
    <%=Html.MakePopupForm("Edit", new[]{"id"}) %>
    <%=Html.Confirm("Sunteti sigur ?") %>
    <%=Html.PopupFormActionLink("Create", "Creaza",htmlAttributes:new{@class= "abtn"}) %>
    <table>
        <thead>
            <tr>
                <td style="width:100%;">
                    Denumire
                </td>
                <td >
                </td>
                <td>
                </td>
            </tr>
        </thead>
        <tbody>
            <% foreach (var o in Model.Page)
               {%>
            <tr>
                <td>
                    <%:o.Name %>
                </td>
                <%=Html.Partial("ed", o.Id) %>
            </tr>
            <%
                }%>
        </tbody>
    </table>
    <%=Html.Pagination() %>
</asp:Content>
