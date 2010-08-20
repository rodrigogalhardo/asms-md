<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Core.Model.FarmerInfo>"
    MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <% Html.RenderAction("Info", new { Model.Id }); %>
    <p>
    <%=Html.ActionLink("Informatii de contact", "Index", "ContactInfo", new{farmerId = Model.Id}, null) %>
    </p>
    <br />
    <%if (Model.FType == FarmerType.LandOwner)
      {%>
    <% Html.RenderAction("DisplayLandOwner", new { farmerId = Model.Id }); %>
    <%}
      else
      {%>
    <% Html.RenderAction("DisplayOrganization", new { farmerId = Model.Id }); %>
    <%
        }%>
</asp:Content>
