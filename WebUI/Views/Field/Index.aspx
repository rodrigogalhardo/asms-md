<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IPageable<Field>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Campuri</h2>
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
                <%:o.Description %>
            </td>
            <%=Html.Partial("ed", o.Id) %>
        </tr>
        <%
            }%>
    </table>
    <%=Html.Pagination() %>
</asp:Content>
