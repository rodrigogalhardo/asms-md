<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Administrare</h2>
        <p>
        <%=Html.ActionLink("Utilizatori", "Index","User") %>
    </p>
    <p>
        <%=Html.ActionLink("Campuri", "Index","Field") %>
    </p>
    <p>
        <%=Html.ActionLink("Masuri", "Index","Measure") %>
    </p>
    <p>
        <%=Html.ActionLink("Seturi de campuri", "Index", "Fieldset") %>
    </p>
    <p>
        <%=Html.ActionLink("Seturi de masuri", "Index", "Measureset") %>
    </p>
    <p>
        <%=Html.ActionLink("Raioane", "Index", "District") %>
    </p>
    <p>
        <%=Html.ActionLink("Cine perfecteaza planul de afaceri", "Index", "Perfecter") %>
    </p>
    <p>
        <%=Html.ActionLink("Calcularea coeficientilor", "Index", "Calculations") %>
    </p>
</asp:Content>
