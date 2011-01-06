<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<object>>" %>
<ul class="ae-lookup-list">
    <% 
        if (Model != null)
        {
            foreach (var o in Model)
            {
    %>
        <% Html.RenderPartial("item", o); %>
   
    <%
            }
        }%>
</ul>
