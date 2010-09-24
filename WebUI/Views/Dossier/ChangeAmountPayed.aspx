<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<ChangeAmountPayedInput>" %>

<% using (Html.BeginForm())
   {%>
<%=Html.HiddenFor(o => o.Id) %>
<%=Html.EditorFor(o => o.Amount) %>
<%} %>
