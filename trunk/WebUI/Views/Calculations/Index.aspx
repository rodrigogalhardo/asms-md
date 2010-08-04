<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<h2>Calcularea coeficientilor</h2>
    <form method="post" action='<%=Url.Action("Calculate") %>'>
    <input type="submit" value="calculeaza coeficientii pe luna trecuta " />
    </form>
</asp:Content>
