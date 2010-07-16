<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Fieldset>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <h2>
        <%:Model.Name %></h2>

    <%:Html.ActionLink("Inapoi", "Open", new {id=Model.Id } ) %><br />


    <h2>
        Campuri asignate</h2>
    <% Html.RenderAction("Assigned", new { id = Model.Id }); %>
    <h2>
        Campuri disponibile</h2>
    <% Html.RenderAction("Unassigned", new { id = Model.Id }); %>
</asp:Content>
