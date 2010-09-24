<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ChangeAmountInput>" %>
    <% using (Html.BeginForm())
       {%>
    <%=Html.HiddenFor(o => o.Id) %>
    <%=Html.EditorFor(o => o.Amount) %>
    <%} %>
    

