<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.UserCreateInput>" %>
<%@ Import Namespace="xVal.Html" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Adauga utilizator</h2>
    <% using (Html.BeginForm())
       {%>
    <%= Html.ValidationSummary(true) %>
    <%=Html.ValidationMessage("roles") %>
    <fieldset>
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
        <div class="efield">
            <div class="elabel">
                <%= Html.LabelFor(model => model.ConfirmPassword) %>
            </div>
            <div class="einput">
                <%= Html.PasswordFor(model => model.ConfirmPassword) %>
                <%= Html.ValidationMessageFor(model => model.ConfirmPassword) %>
            </div>
        </div>
        <div class="efield">
            <div class="elabel">
                <label>
                    Roluri</label>
            </div>
            <div class="einput">
                <% foreach (var role in Model.Roles as IEnumerable<SelectListItem>)
                   {
                %>
                <p>
                    <input <%if(role.Selected) Response.Write("checked='checked'");%> id='role<%=role.Value %>'
                        type="checkbox" name='roles' value='<%=role.Value %>' />
                    <label for="<%="role"+role.Value %>">
                        <%=role.Text %></label>
                </p>
                <%
                    } %>
            </div>
        </div>
        <br class="cbt" />
        <div class="esubmit">
            <input type="submit" value="Salveaza" />
        </div>
    </fieldset>
    <% } %>
    <div>
        <%= Html.ActionLink("Inapoi la lista", "Index") %>
    </div>
    <%=Html.ClientSideValidation<UserCreateInput>() %>
</asp:Content>
