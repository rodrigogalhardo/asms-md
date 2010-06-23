<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IPageable<MRGSP.ASMS.Core.Model.User>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Utilizatori</h2>
        <script type="text/javascript">
        $(function() {
            $("#create").click(function() { window.location = '<%=Url.Action("Create") %>'; });
        });
    </script>
    
    <button class="ui-state-default ui-corner-all" id="create">
        <span class="ui-icon ui-icon-circle-plus fl"></span>Adauga</button>
    <br class="cbt" />
    <% foreach (var item in Model.Page)
       { %>
    <p>
        <%=item.Name %>
        <%=Html.ActionLink("Change pass", "ChangePassword", new {id = item.Id}) %>
        <%=Html.ActionLink("Edit", "Edit", new {id = item.Id}) %>
        </p>
    <% } %>
    <%=Html.Pagination() %>
</asp:Content>
