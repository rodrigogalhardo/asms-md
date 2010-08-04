<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.FpiInput>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <% using (Html.BeginForm())
       {%>
    <%=Html.HiddenFor(o => o.MeasureId) %>
    <%=Html.HiddenFor(o => o.MeasuresetId) %>
    <%=Html.HiddenFor(o => o.Month) %>
    <%=Html.Input(o => o.Amount) %>
    <% Html.RenderPartial("save"); %>
    <%} %>
</asp:Content>
