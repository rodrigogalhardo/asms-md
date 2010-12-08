<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IPageable<FieldsetDisplay>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Seturi de campuri</h2>
    <%=Html.MakePopupForm("Create") %>
    <%=Html.MakePopupForm("Edit", new[]{"Id"}) %>
    <%=Html.Confirm("Sunteti sigur ?") %>
    <%=Html.PopupFormActionLink("Create", "Creaza", htmlAttributes:new{@class="abtn"}) %>
    <table>
        <thead>
            <tr>
                <td style="width: 100%;">
                    Nume
                </td>
                <td>
                    An
                </td>
                <td>
                    Stare
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
        <tbody>
            <%foreach (var o in Model.Page)
              {%>
            <tr>
                <td>
                    <%:o.Name %>
                </td>
                <td>
                    <%:o.Year %>
                </td>
                <td>
                    <%:o.State %>
                </td>
                <td>
                    <%:Html.ActionLink("deschide","open",new{id = o.Id}) %>
                </td>
                <td>
                    <%:Html.ActionLink("vizualizeaza","view",new{id = o.Id}) %>
                </td>
                <% Html.RenderPartial("ed", o.Id); %>
            </tr>
            <%} %>
        </tbody>
    </table>
    <%=Html.Pagination() %>
</asp:Content>
