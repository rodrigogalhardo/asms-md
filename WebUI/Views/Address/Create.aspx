<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.AddressInput>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <% Html.RenderAction("Info", "Farmer", new { id = Model.FarmerId }); %>
    <%=Html.ActionLink("Inapoi","Index", "ContactInfo", new{Model.FarmerId}, null) %>
    <% using (Html.BeginForm())
       {%>
    <%=Html.EditorFor(o => o.DistrictId) %>
    <%=Html.EditorFor(o => o.Locality) %>
    <%=Html.EditorFor(o => o.Street) %>
    <%=Html.EditorFor(o => o.House) %>
    <%=Html.EditorFor(o => o.Apartment) %>
    <%=Html.EditorFor(o => o.Zip) %>
    <%=Html.HiddenFor(o => o.FarmerId) %>
    <%
        Html.RenderPartial("save");%>
    <%
        } %>
</asp:Content>
