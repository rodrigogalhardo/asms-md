<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.LandOwnerInput>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<% using(Html.BeginForm()) {%>
<%=Html.Input(o => o.FirstName) %>
<%=Html.Input(o => o.LastName) %>
<%=Html.Input(o => o.FathersName) %>
<%=Html.Input(o => o.DateOfBirth) %>
<%=Html.Input(o => o.FiscalCode) %>
<% Html.RenderPartial("save"); %>
<%}%>
</asp:Content>
