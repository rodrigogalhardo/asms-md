<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%var o = ViewData["prop"].ToString();
  const string ai = "ui-icon-circle-plus";
  const string ri = "ui-icon-circle-arrow-n";
  %>
<form id="lsf<%=o %>" action="<%=Url.Action("Search") %>" method="post">
<% Html.RenderAction("SearchForm"); %>
</form>
<div id='<%=o %>lsh'>
<% Html.RenderAction("header"); %>
</div>
<div id="<%=o %>ls" class="lookupList"></div>
<div id='<%=o %>seh'>
<% Html.RenderAction("header"); %>
</div>
<div id="<%=o %>se" class="lookupSelected"></div>
<script type="text/javascript">
    $('#<%=o %>ls a').live('click', function () { add<%=o %>($(this).closest('.ae-lookup-item')); });
    $('#<%=o %>se a').live('click', function () { rem<%=o %>($(this).closest('.ae-lookup-item')); });
    $("#lp<%=o %>").bind( "dialogresize", lay<%=o %>);
    function lay<%=o %>() {
        var av = $("#lp<%=o %>").height() - $('#lsf<%=o %>').height() - $('#<%=o %>lsh').height() - $('#<%=o %>seh').height() -1;
        $('#<%=o %>ls').css('height', (av * 0.5)+'px');
        $('#<%=o %>se').css('height', (av * 0.5)+'px');
    }
    $('#lsf<%=o %> input').keypress(function (e) { if (e.which == 13) { e.preventDefault();$('#lsf<%=o %>').submit(); }});
    $('#lsf<%=o %>').submit(function (e) {
        e.preventDefault();
        $('#lsf<%=o %>').ajaxSubmit({
            data: { selected: $('#<%=o %>se li').map(function () { return $(this).attr("data-value"); }).get() },
            success: function (d) {
                $('#<%=o %>ls').html(d);
                $('#<%=o %>ls li').draggable({ cancel: "a", revert: "invalid", helper: "clone", cursor: "move" });
                $('#<%=o %>ls li .ae-lookup-mbtn').prepend("<a href='#' title='+' class='ui-icon <%=ai %>'>+</a>");
            }
        });
    }); 
        $('#<%=o %>se').droppable({
        accept: "#<%=o %>ls li",
        activeClass: "ui-state-highlight",
        drop: function (e, ui) { add<%=o %>(ui.draggable);}
    });
    $('#<%=o %>ls').droppable({
        accept: "#<%=o %>se li",
        activeClass: "ui-state-highlight",
        drop: function (e, ui) { rem<%=o %>(ui.draggable); }
    });
    function add<%=o %>(o) {
        o.prependTo($('#<%=o %>se ul')).find('a.<%=ai %>').removeClass('<%=ai %>').addClass('<%=ri %>');
        $(document).trigger('awesome');
    }
    function rem<%=o %>(o) {
        o.prependTo($('#<%=o %>ls ul')).find('a.<%=ri %>').removeClass('<%=ri %>').addClass('<%=ai %>');
        $(document).trigger('awesome');
    }
    $.post('<%=Url.Action("selected") %>', $.param({ ids: $("#<%=o%> input").map(function () { return $(this).attr("value"); }).get() }, true), 
    function (d) {
        $('#<%=o %>se').html(d);        
        $('#<%=o %>se li .ae-lookup-mbtn').prepend("<a href='#' title='^' class='ui-icon <%=ri %>'>^</a>");
        $('#lsf<%=o %>').submit();
        $('#lsf<%=o %> input:first').focus();
        $('#<%=o %>se li').draggable({ cancel: "a", revert: "invalid", helper: "clone", cursor: "move" });
        lay<%=o %>();
    });
</script>
