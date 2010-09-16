<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ChangeAmountInput>" %>

<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <%=Html.ActionLink("Inapoi","Index","Rank", new{fpiId = Model.Id}, null) %>
    <% using (Html.BeginForm())
       {%>
    <%=Html.HiddenFor(o => o.Id) %>
    <%=Html.EditorFor(o => o.Amount) %>
    <% Html.RenderPartial("save"); %>
    <%} %>
    </asp:Content>
