<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    var o = ViewData["prop"].ToString();
%>
<form id="lsf<%=o %>" action="<%=Url.Action("search") %>" method="post">
<% Html.RenderAction("SearchForm"); %>
</form>
<div id='<%=o %>lsh'>
<% Html.RenderAction("header"); %>
</div>
<div id="<%=o%>ls" class="lookupList">
</div>
<script type="text/javascript">
function lay<%=o %>() {
    var av = $("#lp<%=o %>").height() - $('#lsf<%=o %>').height() - $('#<%=o %>lsh').height();
    $('#<%=o %>ls').css('height', av+'px');     
} 
$("#lp<%=o %>").bind( "dialogresize", lay<%=o %>);
$('#lsf<%=o %> input').keypress(function(e){ if(e.which == 13){ e.preventDefault(); $('#lsf<%=o %>').submit(); } })
$('#lsf<%=o %>').ajaxForm({
    success: function (d) {
        lay<%=o %>();
        $('#<%=o%>ls').html(d);
        $("#<%=o%>ls li").click(lgc<%=o %>);        
    }
});
$('#lsf<%=o %>').submit();
$('#lsf<%=o %> input:first').focus();
</script>
