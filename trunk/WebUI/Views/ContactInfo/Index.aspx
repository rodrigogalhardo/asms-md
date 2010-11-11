<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/Site.Master" %>
<%@ Import Namespace="MRGSP.ASMS.WebUI.Helpers" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<% Html.Confirm("sunteti sigur ?"); %>
    <%
        var id = ViewData["id"]; %>
    <% Html.RenderAction("Info", "Farmer", new { id }); %>
    <p>
        <%=Html.ActionLink("Inapoi", "Open", "Farmer", new{id}, null) %></p>
        <br />
    <%=Html.ActionLink("adauga adresa", "Create", "Address", new { farmerId = id }, new { @class = "abtn" })%>
    <%
       Html.RenderAction("Index", "Address", new {farmerId = id}); %>
    <%=Html.ActionLink("adauga telefon", "Create", "Phone", new { farmerId = id }, new { @class = "abtn" })%>
    <%
       Html.RenderAction("Index", "Phone", new {farmerId = id}); %>
    <%=Html.ActionLink("adauga email", "Create", "Email", new { farmerId = id }, new { @class = "abtn" })%>
    <%
       Html.RenderAction("Index", "Email", new {farmerId = id});%>
</asp:Content>
