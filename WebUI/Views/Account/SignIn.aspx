<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Login.Master" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.SignInInput>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SignIn
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="loginbox" class="ui-state-default ui-corner-all">
        <% using (Html.BeginForm())
           {%>
        <%= Html.ValidationSummary(true) %>
        <%=Html.EditorFor(o => o.Name) %>
        <%=Html.EditorFor(o => o.Password) %>
        <div class="efield">
            <div class="elabel">&nbsp;
            </div>
            <div class="einput">
                <input type="submit" value="Autentificare" /></div>
        </div>
        <% } %>
    </div>
    <%=Html.ClientSideValidation<SignInInput>() %>
</asp:Content>
