<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LookupInfo>" %>
<script type="text/javascript">

    function grow<%=Model.For %>Click() {
        $(".selected", $(this).parent()).removeClass('selected').unbind('click').click(grow<%=Model.For %>Click);
        $(this).addClass('selected');

        $(this).click(function () {
            choose<%=Model.For%>Click();
        });
    }

    function choose<%=Model.For%>Click() {
        var v = $("#<%=Model.For%>list table .selected").attr("value");
        if (v)        {
            $('#<%=Model.For%>').val(v);
            load<%=Model.For %>Display();            
        }            
        $("#select-<%=Model.For%>-dialog").dialog('close');
    }

    function load<%=Model.For %>Display() {
        var id = $('#<%=Model.For %>').val();
        if(id)
        $.get('<%=Url.Action("Get", Model.For + "Lookup") %>', {id:id},function(data){ $("#display<%=Model.For %>").val(data);});
    }

    $(function () { 
        load<%=Model.For %>Display();

        $("#select-<%=Model.For %>-dialog").dialog({    
            resizable: true,
            height: <%=Model.Height %>,
            width: <%=Model.Width %>,
            modal: true,           
            buttons: {
                '<%=Model.ChooseText %>': function () {
                    choose<%=Model.For%>Click();
                },
                '<%=Model.CancelText %>': function () { $(this).dialog('close'); }
            },            
            autoOpen: false
        }); // end dialog

        $("#open-<%=Model.For %>").click(function () {           
            $.get('<%=Url.Action("Index", Model.For + "Lookup") %>',
                function (d) { $("#select-<%=Model.For %>-dialog").html(d).dialog('open'); });
        });        
    });        

</script>
<div id="select-<%=Model.For %>-dialog" title="<%=Model.Title %>">
</div>
<%=Html.Hidden(Model.For) %>
<input type="text" id="display<%=Model.For %>" disabled="disabled" />
<a class=" abtn btn fl" id="open-<%=Model.For %>" href="#"><span class="ui-icon ui-icon-newwin">
</span></a>