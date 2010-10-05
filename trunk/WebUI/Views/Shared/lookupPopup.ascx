<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    var o = ViewContext.RouteData.Values["controller"].ToString().RemoveSuffix("Lookup");
%>

<form id="<%="searchForm"+o %>" action="<%=Url.Action("LookupList") %>" method="post">
<%
    Html.RenderPartial("searchForm"); %>
</form>

<div id="<%=o %>list">
</div>
<script type="text/javascript">
    $('#<%="searchForm"+o %>').ajaxForm({
        success: function (d) {
            $('#<%=o %>list').html(d);
            $("#<%=o %>list table .grow").click(grow<%=o %>Click);
            dob();
            $('#<%="searchForm"+o %> input:first').focus();
        }
    });
    $('#<%="searchForm"+o %>').submit();
</script>
