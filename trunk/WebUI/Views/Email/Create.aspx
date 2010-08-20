<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.EmailInput>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <% Html.RenderAction("Info", "Farmer", new { id = Model.FarmerId }); %>
    <%=Html.ActionLink("Inapoi", "Index", "ContactInfo", new { Model.FarmerId }, null) %>
    <% using (Html.BeginForm())
       {%>
    <%=Html.Input(o => o.Address) %>
    <%=Html.HiddenFor(o => o.FarmerId) %>
    <% Html.RenderPartial("save"); %>
    <%} %>
</asp:Content>
