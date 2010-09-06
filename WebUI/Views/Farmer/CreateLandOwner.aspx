<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.LandOwnerInput>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<% using(Html.BeginForm()) {%>
<%=Html.EditorFor(o => o.FirstName) %>
<%=Html.EditorFor(o => o.LastName) %>
<%=Html.EditorFor(o => o.FathersName) %>
<%=Html.EditorFor(o => o.DateOfBirth) %>
<%=Html.EditorFor(o => o.FiscalCode) %>
<% Html.RenderPartial("save"); %>
<%}%>
</asp:Content>
