<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.DisqualifyInput>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<% using(Html.BeginForm()){%>
<%=Html.HiddenFor(o => o.DossierId) %>
<%=Html.EditorFor(o => o.Reason) %>
<% Html.RenderPartial("save"); %>
<%} %>
</asp:Content>
