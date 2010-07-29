<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ChangePasswordInput>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    schimba parola
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Schimba parola</h2>
    <% using (Html.BeginForm())
       {%>
    <%= Html.ValidationSummary(true) %>
    <fieldset>
        <%= Html.HiddenFor(model => model.Id) %>
        <%=Html.Input(o => o.Password) %>
        <%=Html.Input(o => o.ConfirmPassword) %>        
        <% Html.RenderPartial("submit"); %>
    </fieldset>
    <% } %>
    <% Html.RenderPartial("back"); %>
</asp:Content>
