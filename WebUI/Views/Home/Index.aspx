<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%if (ViewData["msid"] != null)
      {%>
    <p>
        <%=Html.ActionLink("planul financiar","Index","Fpi",new{measuresetId = (int)ViewData["msid"]}, new{@class="fgb"} ) %></p>
    <%} %>
</asp:Content>
