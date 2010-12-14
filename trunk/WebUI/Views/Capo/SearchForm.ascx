<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CapoSearchInput>" %>
<form id="fsearch" action="<%=Url.Action("search") %>" method="get">
<%=Html.EditorFor(o => o.MeasureId) %>
<%=Html.EditorFor(o => o.StartDate) %>
<%=Html.EditorFor(o => o.EndDate) %>
<%=Html.EditorFor(o => o.PoState) %>
<div class="efield">
    <div class="elabel">
        &nbsp;</div>
    <input type="submit" value="cauta" /></div>
</form>
