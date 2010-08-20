<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DropDownInput>" %>
<div class="efield">
    <div class="elabel">
        <%=Model.Label %>:
    </div>
    <div class="einput">
        <%=Html.DropDownList(Model.Name, Model.Value as IEnumerable<SelectListItem>)%>
    </div>
</div>
