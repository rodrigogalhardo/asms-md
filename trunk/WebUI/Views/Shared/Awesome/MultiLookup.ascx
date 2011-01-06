<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Omu.Awesome.Mvc.Helpers.LookupInfo>" %>
<%
    var o = Model.Prefix + Model.Prop.Replace('.', '_').Replace('[','_').Replace(']','_');
%>
<script type="text/javascript">
    function lch<%=o%>() {       
        $("#<%=o %>").empty();
        $.each($("#<%=o%>se li").map(function() { return $(this).attr("data-value"); }).get(), function(){   
            $("#<%=o %>").append($("<input type='hidden' name='<%=o %>' \>").attr("value",this)); 
        });
        lld<%=o %>();                               
        $("#lp<%=o %>").dialog('close');
    }

    function lld<%=o %>() {
        var ids = $("#<%=o%> input").map(function() { return $(this).attr("value"); }).get();
        $("#ld<%=o %>").html('');        
        if(ids.length != 0) $.post('<%=Url.Action("GetMultiple", Model.Controller) %>', $.param({ ids: ids }, true),
        function(d){ 
            $.each(d, function(){$("#ld<%=o %>").append('<li>'+this.Text+'</li>')});
        });
    }

    $(function () {         
        lld<%=o %>();
        $(".<%=o %>ie8").remove();
        $("#lp<%=o %>").addClass("<%=o %>ie8");
        $("#lp<%=o %>").dialog({ 
            resizable: true,
            <% if(Model.Fullscreen)
               {%>
               height: $(window).height()-50, width: $(window).width()-50,
               <%
               }else{%>
            height: <%=Model.Height %>,
            width: <%=Model.Width %>,<%}%>
            title: '<%:Model.Title %>',
            modal: true,           
            buttons: {
                '<%=Model.ChooseText %>': function () {
                    lch<%=o%>($(this));
                },
                '<%=Model.CancelText %>': function () { $(this).dialog('close'); }
            },            
            autoOpen: false,
            close: function(event, ui) { $("#lp<%=o %>").find('*').remove(); }
        });
        <% if(Model.Fullscreen)
           {%>
           $(window).bind("resize", function (e) { 
           $("#lp<%=o %>").dialog("option" , {height: $(window).height()-50, width: $(window).width()-50}).trigger('dialogresize');
           });
           <%
           }%>

        var lck<%=o %> = null;
        $("#lpo<%=o %>").click(function () {           
            if(lck<%=o %> != null) return;
            lck<%=o %> = true;            
            $.get('<%=Url.Action("Mindex", Model.Controller) %>',
                {prop: '<%=o %>'},
                function (d) { 
                $("#lp<%=o %>").html(d).dialog('open'); lck<%=o %> = null;
                });
        });
        
        <%if(Model.ClearButton){%>
        $("#lc<%=o %>").click(function(){
            $("#<%=o %>,#ld<%=o %>").empty();
        });   
        <%} %>
    });  
</script>
<ul id="ld<%=o %>" <%=Model.HtmlAttributes %> ></ul>
<a class="lookupButton" id="lpo<%=o %>" href="#"></a>
<%if (Model.ClearButton)
  {%>
<a class="clearLookupButton" id="lc<%=o %>" href="#"></a>
<%} %>
<div id='lp<%=o %>'></div>
<div id="<%=o %>">

<% if(Model.Value != null && Model.Value is IEnumerable) foreach (var oo in Model.Value as IEnumerable)
{
  %>
 <input type="hidden" name="<%=Model.Prop %>" value="<%=oo %>" /> 
  <%
} %>
</div>
