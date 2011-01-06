<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Omu.Awesome.Mvc.Helpers.LookupInfo>" %>
<%
    var o = Model.Prefix + Model.Prop.Replace('.', '_').Replace('[','_').Replace(']','_');
    var sel = Omu.Awesome.Mvc.Settings.Lookup.SelectedRowCssClass;
%>
<script type="text/javascript">
    function lgc<%=o %>() {
        $('li',$(this).parent()).removeClass('<%=sel %>').unbind('click').click(lgc<%=o %>);        
        $(this).addClass('<%=sel %>').click(lch<%=o%>);
    }

    function lch<%=o%>() {
        $('#<%=o %>').val('');
        $('#<%=o%>').val($("#<%=o%>ls .<%=sel %>").attr("data-value"));      
        lld<%=o %>();                       
        $("#lp<%=o%>").dialog('close');
    }

    function lld<%=o %>() {
        $("#ld<%=o%>").val('');
        var id = $('#<%=o %>').val();
        if(id)
        $.get('<%=Url.Action("Get", Model.Controller) %>', {id:id},function(d){ $("#ld<%=o%>").val(d);});
    }

    $(function () {
        $("#ld<%=o%>").addClass("lookupTextbox");
        lld<%=o %>();
        $(".<%=o %>ie8").remove();

        $("#lp<%=o%>").addClass("<%=o %>ie8");
        $("#lp<%=o%>").dialog({ 
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
            close: function(event, ui) { $("#lp<%=o%>").find('*').remove(); }
        });

         <% if(Model.Fullscreen)
           {%>
           $(window).bind("resize", function (e) { 
           $("#lp<%=o %>").dialog("option" , {height: $(window).height()-50, width: $(window).width()-50}).trigger('dialogresize');
           });
           <%
           }%>

        var lck<%=o%> = null;
        $("#lpo<%=o%>").click(function () {           
            if(lck<%=o%> != null) return;
            lck<%=o%> = true;
            $.get('<%=Url.Action("Index", Model.Controller) %>',
                {prop: '<%=o %>'},
                function (d) { $("#lp<%=o%>").html(d).dialog('open'); lck<%=o%> = null; });
        });
        
        <%if(Model.ClearButton){%>
        $("#lc<%=o%>").click(function(){
            $("#<%=o %>").val("");
            $("#ld<%=o%>").val("");
        });   
        <%} %>
    });  
</script>
<div id='lp<%=o%>'>
</div>
<input type="hidden" id="<%=o %>" name="<%=Model.Prop %>" value="<%=Model.Value %>" />
<input type="text" id="ld<%=o%>" disabled="disabled" <%=Model.HtmlAttributes %> />
<a class="lookupButton" id="lpo<%=o%>" href="#"></a>
<%if (Model.ClearButton)
  {%>
<a class="clearLookupButton" id="lc<%=o%>" href="#"></a>
<%} %>