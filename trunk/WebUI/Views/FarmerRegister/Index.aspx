<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <fieldset>
        <p>
            <%=Html.ActionLink("Lista fermierilor", "Index", "Farmer") %>
        </p>
    </fieldset>
    <fieldset>
        <legend>Inregistreaza Fermier</legend>
        <p>
            <%=Html.ActionLink("Intreprindere", "CreateOrganization", "Farmer") %>
        </p>
        <p>
            <%=Html.ActionLink("Properietar de pamant agricol", "CreateLandOwner", "Farmer") %>
        </p>
    </fieldset>
    <fieldset>
        <legend>Multimi</legend>
        <p>
            <%=Html.ActionLink("Raioane", "Index", "District") %>
        </p>
    </fieldset>
</asp:Content>
