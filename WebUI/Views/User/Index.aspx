<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IPageable<MRGSP.ASMS.Core.Model.User>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Utilizatori</h2>
    <script type="text/javascript">
        $(function () {
            $("#create").click(function () { window.location = '<%=Url.Action("Create") %>'; });
        });
    </script>

    <button class="ui-state-default ui-corner-all" id="create">
        <span class="ui-icon ui-icon-circle-plus fl"></span>Adauga</button>
    <br class="cbt" />
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
    <tr><td>
        <%=item.Name %></td><td>
        <%=Html.ActionLink("Schimba parola", "ChangePassword", new {id = item.Id}) %>
        <%=Html.ActionLink("Editeaza", "Edit", new {id = item.Id}) %>
        </td>
    </tr>
    <% } %>
    </table>
    <%=Html.Pagination() %>
</asp:Content>
