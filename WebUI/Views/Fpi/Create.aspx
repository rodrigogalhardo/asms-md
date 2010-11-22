<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.FpiInput>" %>
    <% using (Html.BeginForm())
       {%>
    <%=Html.HiddenFor(o => o.MeasureId) %>
    <%=Html.HiddenFor(o => o.MeasuresetId) %>
    <%=Html.HiddenFor(o => o.Month) %>
    <%=Html.EditorFor(o => o.Amount) %>
    <%=Html.EditorFor(o => o.Amountm) %>
    <%} %>

