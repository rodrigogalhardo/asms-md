<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Administrare</h2>
    <fieldset>
        <p>
            <%=Html.ActionLink("Seturi de campuri", "Index", "Fieldset") %>
        </p>
        <p>
            <%=Html.ActionLink("Seturi de masuri", "Index", "Measureset") %>
        </p>
    </fieldset>
    <fieldset>
        <legend>Multimi</legend>
        <p>
            <%=Html.ActionLink("Campuri", "Index","Field") %>
        </p>
        <p>
            <%=Html.ActionLink("Masuri", "Index","Measure") %>
        </p>
        <p>
            <%=Html.ActionLink("Cine perfecteaza planul de afaceri", "Index", "Perfecter") %>
        </p>
    </fieldset>
</asp:Content>
