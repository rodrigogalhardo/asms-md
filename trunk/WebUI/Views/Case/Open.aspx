<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Core.Model.Dossier>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
<h2> <%:Model.FarmerName %></h2>
<% if(Model.StateId == (int)DossierStates.Registered) {%>
<%=Html.ActionLink("indeplineste campurile", "Index", "FillFields", new{Model.Id}, null) %>
<%}%>
</asp:Content>
