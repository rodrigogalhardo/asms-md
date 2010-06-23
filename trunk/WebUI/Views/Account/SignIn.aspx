<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Login.Master" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.SignInInput>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SignIn
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="loginbox" class="ui-state-default">
        <% using (Html.BeginForm())
           {%>
        <%= Html.ValidationSummary(true) %>
        <div class="efield">
            <div class="elabel">
                <%= Html.LabelFor(model => model.Name) %>
            </div>
            <div class="einput">
                <%= Html.TextBoxFor(model => model.Name) %>
                <%= Html.ValidationMessageFor(model => model.Name) %>
            </div>
        </div>
        <div class="efield">
            <div class="elabel">
                <%= Html.LabelFor(model => model.Password) %>
            </div>
            <div class="einput">
                <%= Html.PasswordFor(model => model.Password) %>
                <%= Html.ValidationMessageFor(model => model.Password) %>
            </div>
        </div>
        <div class="esubmit">
            <input type="submit" value="Sign In" />
        </div>
        <% } %>
    </div>
</asp:Content>
