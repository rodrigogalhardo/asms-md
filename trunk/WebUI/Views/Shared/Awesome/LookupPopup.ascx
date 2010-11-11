<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    var o = ViewData["prop"].ToString();
%>

<form id="<%="searchForm"+o %>" action="<%=Url.Action("LookupList") %>" method="post">
<%
    Html.RenderAction("SearchForm"); %>
</form>

<div id="<%=o %>list">
</div>
<script type="text/javascript">
$('#<%="searchForm"+o %> input').keypress(function(e){ if(e.which == 13){ e.preventDefault(); $('#<%="searchForm"+o %>').submit(); } })
$('#<%="searchForm"+o %>').ajaxForm({
    success: function (d) {
        $('#<%=o %>list').html(d);
        $("#<%=o %>list table tbody tr").click(grow<%=o %>Click);                       
        $('#<%="searchForm"+o %> input:first').focus();
    }
});
$('#<%="searchForm"+o %>').submit();
</script>
