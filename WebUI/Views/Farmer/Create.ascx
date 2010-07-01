<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FarmerCreateInput>" %>
<%@ Import Namespace="xVal.Html" %>
<% using (Html.BeginForm())
   {%>
<div class="efield">
    <div class="elabel">
        <%= Html.LabelFor(model => model.Name) %>
    </div>
    <div class="einput">
        <%= Html.TextBoxFor(model => model.Name) %>        
    </div>
    <%= Html.ValidationMessageFor(model => model.Name) %>
</div>
<div class="efield">
    <div class="elabel">
        <%= Html.LabelFor(model => model.Code) %>
    </div>
    <div class="einput">
        <%= Html.TextBoxFor(model => model.Code) %>        
    </div>
    <%= Html.ValidationMessageFor(model => model.Code) %>
</div>
<div class="efield">
    <div class="elabel">
        <%= Html.LabelFor(model => model.DateReg) %>
    </div>
    <div class="einput">
        <%= Html.TextBoxFor(model => model.DateReg, new Dictionary<string, object>{{"class", "mydate"}}) %>        
    </div>
    <%= Html.ValidationMessageFor(model => model.DateReg) %>
</div>
<div class="efield">
    <div class="elabel">
        <%= Html.LabelFor(model => model.NrReg) %>
    </div>
    <div class="einput">
        <%= Html.TextBoxFor(model => model.NrReg) %>
        <%= Html.ValidationMessageFor(model => model.NrReg) %>
    </div>
</div>
<input type="submit" class="hidden" />
<% } %>
<%=Html.ClientSideValidation<FarmerCreateInput>() %>