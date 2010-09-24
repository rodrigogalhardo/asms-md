<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.UserCreateInput>" %>

<% using (Html.BeginForm())
   {%>
<%=Html.EditorFor(o => o.Name) %>
<%=Html.EditorFor(o => o.Password) %>
<%=Html.EditorFor(o => o.ConfirmPassword) %>
<div class="efield">
    <div class="elabel">
        <label>
            Roluri</label>
    </div>
    <div class="einput" style="float: left;">
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
<%= Html.ValidationSummary(true) %>
<%=Html.ValidationMessage("roles") %>
<% } %>
<%=Html.ClientSideValidation<UserCreateInput>() %>
