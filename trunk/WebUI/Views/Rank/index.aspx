<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Core.Model.Fpi>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <h2>
        <%: Model.Amount %></h2>
    <%=Html.ActionLink("schimba suma","ChangeAmount",new{fpiId = Model.Id}, new{@class = "fgb"}) %>
    <hr />
    <% Html.RenderPartial("confirm"); %>
    <form method="post" action='<%=Url.Action("recalculate", new{FpiId = Model.Id}) %>'>
    <input type="submit" value="recalcul" class="confirm" />
    </form>
    <br />
    <h3>
        autorizat spre plata</h3>
    <% Html.RenderAction("authorized", new { fpiId = Model.Id });%>
    <h3>
        castigatori</h3>
    <% Html.RenderAction("winners", new { fpiId = Model.Id });%>
    <h3>
        rest</h3>
    <% Html.RenderAction("losers", new { fpiId = Model.Id });%>
    <h3>
        discalificati</h3>
    <% Html.RenderAction("disqualified", new { fpiId = Model.Id });%>
</asp:Content>
