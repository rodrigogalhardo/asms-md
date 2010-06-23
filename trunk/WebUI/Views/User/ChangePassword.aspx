<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.ChangePasswordInput>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ChangePassword
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Schimba parola</h2>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        <fieldset>            
                <%= Html.HiddenFor(model => model.Id) %>
            <div class="efield">
            <div class="elabel">
                <%= Html.LabelFor(model => model.Password) %>
            </div>
            <div class="einput">
                <%= Html.PasswordFor(model => model.Password) %>
                <%= Html.ValidationMessageFor(model => model.Password) %>
            </div>
            </div>
            <div class="efield">
            <div class="elabel">
                <%= Html.LabelFor(model => model.ConfirmPassword) %>
            </div>
            <div class="einput">
                <%= Html.PasswordFor(model => model.ConfirmPassword) %>
                <%= Html.ValidationMessageFor(model => model.ConfirmPassword) %>
            </div>            
            </div>
            <% Html.RenderPartial("submit"); %>
        </fieldset>

    <% } %>

    <% Html.RenderPartial("back"); %>

</asp:Content>

