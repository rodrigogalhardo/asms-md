<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ul class="bl">
    <li>
     <%=Html.ActionLink("Adauga Dosar","Create", "Dossier", null, new {@class="abtn"}) %>
     </li>
    <%if (ViewData["msid"] != null)
      {%>
    <li>
        <%=Html.ActionLink("planul financiar","Index","Fpi",new{measuresetId = (int)ViewData["msid"]}, new{@class="abtn"} ) %></li>
    <%} %>
    </ul>


</asp:Content>
