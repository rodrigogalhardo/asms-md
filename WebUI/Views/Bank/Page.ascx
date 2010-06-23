<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IPageable<Bank>>" %>
<% foreach (var bank in Model.Page)
   {
%>
<p>
    <%=bank.Name %></p>
<%
    } %>
<div class='pagination'>
    <%
        for (var i = 0; i < Model.PageCount; i++)
        {
            if (Model.PageIndex != i + 1)
            {%>
    <a href='javascript:getPage(<%=i+1%>)' class='ui-state-default'>
        <%=i + 1%></a>
    <%
            }
            else
            {%>
    <span class='ui-state-highlight current'>
        <%=i + 1%></span>
    <%
            }
        }
%>
</div>
<button click="javascript:getPage(<%=Model.PageIndex %>)"> refresh</button>
<input type="hidden" value="<%=Model.PageIndex %>" class="pageIndex" />
