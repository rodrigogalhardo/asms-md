<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.OrganizationInput>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <% using (Html.BeginForm())
       {%>
       <%=Html.Input(o => o.Name) %>
       <%=Html.Input(o => o.FiscalCode) %>
       <% Html.RenderPartial("dropdown", 
              new DropDownInput {Name = "OrganizationFormId", Value = Model.OrganizationFormId, Label = "Forma organizatorica"}); %>
       <%=Html.Input(o => o.RegDate) %>
       <%=Html.Input(o => o.RegNr) %>
       <%=Html.Input(o => o.ActivityType) %>
       <% Html.RenderPartial("save"); %>
    <%} %>
</asp:Content>
