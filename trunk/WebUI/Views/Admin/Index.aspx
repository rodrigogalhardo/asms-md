<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Administrare</h2>
        <p>
    <%=Html.ActionLink("Campuri", "Index","Fields") %>
    </p>
    <p>
    <%=Html.ActionLink("Seturi de campuri", "Index", "Fieldset") %>
    </p>
</asp:Content>
