<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.UserEditInput>" %>
        <% using (Html.BeginForm())
           {%>
        <%= Html.ValidationSummary(true) %>
        <%=Html.ValidationMessage("roles") %>
        <div class="efield">
            <div class="elabel">
                <label>
                    Roluri</label>
            </div>
            <div class="einput" style="float:left;">
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
    <% } %>
