<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IPageable<Field>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Campuri</h2>
        <%=Html.ActionLink("Creaza", "Create") %>
    <table>
        <thead>
            <tr>
                <td>
                    nume
                </td>
                <td>
                    descriere
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
        </tr>
        <%
            }%>
    </table>
    <%=Html.Pagination() %>
</asp:Content>
