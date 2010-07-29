<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Core.Model.Fieldset>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<% Html.RenderPartial("back"); %>
    <% Html.RenderAction("AssignedListLite", new { fieldsetId = Model.Id }); %>
    <% Html.RenderAction("ListLite", "Indicator", new { fieldsetId = Model.Id}); %>
    <% Html.RenderAction("ListLite", "Coefficient", new { fieldsetId = Model.Id}); %>

</asp:Content>
