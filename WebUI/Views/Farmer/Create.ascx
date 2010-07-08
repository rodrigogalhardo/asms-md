<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FarmerCreateInput>" %>
<%@ Import Namespace="MvcContrib.UI.InputBuilder.Views" %>
<%@ Import Namespace="xVal.Html" %>
<% using (Html.BeginForm())
   {%>
<div class="short">
    <%=Html.Input(o => o.Name) %>
    <%=Html.Input(o => o.Code) %>
    <%=Html.Input(o => o.DateReg) %>
    <%=Html.Input(o => o.NrReg) %>
    <div class="efield">
        <div class="elabel">
            Forma de organizare
        </div>
        <div class="einput">
            <%= Html.DropDownList("CompanyTypeId", Model.CompanyTypeId as IEnumerable<SelectListItem>) %>
        </div>
        <%= Html.ValidationMessageFor(model => model.Code) %>
    </div>
    <input type="submit" class="hidden" />
</div>
<% } %>
<%=Html.ClientSideValidation<FarmerCreateInput>() %>