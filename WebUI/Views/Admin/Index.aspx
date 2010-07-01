<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Index</h2>
    <input type="text" id="azaz" class="mydate" />
    <% Html.RenderPartial("lookup", new LookupInfo { For = "farmer", Choose = false, Title = "administreaza fermerii" }); %>
    <% Html.RenderPartial("lookup", new LookupInfo { For = "bank", Choose = false, Title = "administreaza bancile" }); %>
</asp:Content>
