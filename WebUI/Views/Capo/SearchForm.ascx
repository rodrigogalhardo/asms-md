<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CapoSearchInput>" %>
    <form action="<%=Url.Action("search") %>" method="get">
    <%=Html.EditorFor(o => o.MeasureId) %>
    <%=Html.EditorFor(o => o.StartDate) %>
    <%=Html.EditorFor(o => o.EndDate) %>
    <%=Html.EditorFor(o => o.PoState) %>
    <input type="submit" value="cauta" />
    </form>

