<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Omu.Awesome.Mvc.Helpers.LookupInfo>" %>
<%
    var o = Model.Prop;
    var sel = Omu.Awesome.Mvc.Settings.Lookup.SelectedRowCssClass;
%>
<script type="text/javascript">
    function grow<%=o %>Click() {
        $(".<%=sel %>", $(this).parent()).removeClass('<%=sel %>').unbind('click').click(grow<%=o %>Click);
        $(this).addClass('<%=sel %>').click(function(){ choose<%=o%>Click(); });
    }

    function choose<%=o%>Click() {        
        var v = $("#<%=o%>list table .<%=sel %>").attr("data-value");
        if (v) {
            $('#<%=o%>').val(v);
            load<%=o %>Display();            
        }            
        $("#select<%=o %>window").dialog('close');
    }

    function load<%=o %>Display() {
        var id = $('#<%=o %>').val();
        if(id)
        $.get('<%=Url.Action("Get", Model.Controller) %>', {id:id},function(data){ $("#display<%=o %>").val(data);});
    }

    $(function () { 
        load<%=o %>Display();
        $(".<%=o %>ie8").remove();

        $("#select<%=o %>window").addClass("<%=o %>ie8");
        $("#select<%=o %>window").dialog({  
            show: "fade",
			hide: "fade",  
            resizable: true,
            height: <%=Model.Height %>,
            width: <%=Model.Width %>,
            title: '<%:Model.Title %>',
            modal: true,           
            buttons: {
                '<%=Model.ChooseText %>': function () {
                    choose<%=o%>Click($(this));
                },
                '<%=Model.CancelText %>': function () { $(this).dialog('close'); }
            },            
            autoOpen: false,
            close: function(event, ui) { $("#select<%=o %>window").find('*').remove(); }
        });

        var lockopen<%=o %> = null;
        $("#open<%=o %>").click(function () {           
            if(lockopen<%=o %> != null) return;
            lockopen<%=o %> = true;
            $.get('<%=Url.Action("Index", Model.Controller) %>',
                {prop: '<%=o %>'},
                function (d) { $("#select<%=o %>window").html(d).dialog('open'); lockopen<%=o %> = null; });
        });
        
        <%if(Model.ClearButton){%>
        $("#clear<%=o %>").click(function(){
            $("#<%=o %>").val("");
            $("#display<%=o %>").val("");
        });   
        <%} %>
    });  
</script>
<div id='select<%=o %>window'></div>
<input type="hidden" id="<%=o %>" name="<%=o %>" value="<%=Model.Value %>" />
<input type="text" id="display<%=o %>" disabled="disabled" class="lookupTextbox" />
<a class="lookupButton" id="open<%=o %>" href="#"></a>
<%if (Model.ClearButton)
  {%>
<a class="clearLookupButton" id="clear<%=o %>" href="#"></a>
<%} %>