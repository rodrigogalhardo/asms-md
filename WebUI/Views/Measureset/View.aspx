<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Measureset>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <%
        Html.RenderPartial("back"); %>
    <h2>
        <%:Model.Name %></h2>
    <% Html.RenderAction("AssignedListLite", new { Model.Id }); %>
</asp:Content>
