<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.OrganizationInput>"MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <% using (Html.BeginForm())
       {%>
    <%=Html.EditorFor(o => o.Name) %>
    <%=Html.EditorFor(o => o.FiscalCode) %>
    <%=Html.EditorFor(o => o.OrganizationFormId) %>
    <%=Html.EditorFor(o => o.RegDate) %>
    <%=Html.EditorFor(o => o.RegNr) %>
    <%=Html.EditorFor(o => o.ActivityType) %>
    <% Html.RenderPartial("save"); %>
    <%} %>
</asp:Content>
