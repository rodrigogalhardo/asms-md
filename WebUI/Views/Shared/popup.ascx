<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MRGSP.ASMS.WebUI.Helpers.PopupInfo>" %>
<%@ Import Namespace="MRGSP.ASMS.WebUI.Helpers" %>
<%
    var o = Model.Div;
%>
<script type="text/javascript">
        $(function () {

            $("#<%=o %>").ajaxError(function(e, xhr, settings, exception) {$(this).html(xhr.responseText);}); 

            $("#<%=o %>").dialog({
                width: <%=Model.Width %>,
                height: <%=Model.Height %>,
                title: '<%=Model.Title %>',
                modal: true,
                resizable: true,
                autoOpen: false,
                close: function () { $("#<%=o %>").html(""); },
                buttons: {
                    '<%=Model.CancelText %>': function () { $(this).dialog('close'); }, //end function for Cancel button
                    '<%=Model.SaveText %>': function () { $("#<%=o %> form").submit(); } //end function for Ok button
                }//buttons
            }); //dialog
        }); //docready

        function call<%=o %>(<%=JsTools.MakeParameters(Model.Parameters) %>) { 
            $.get('<%=Url.Action(Model.Action, Model.Controller) %>',
            <%=JsTools.JsonParam(Model.Parameters) %>
            update<%=o %>
            );   //get
        } //doit

        function OnSuccess<%=o %>(result) {
            if (result == 'ok') {
                $("#<%=o %>").dialog('close');
                <%if(Model.RefreshOnSuccess){%>
                    location.reload(true);
                <%} %>
            }
            else {
                update<%=o %>(result);
            }
        }

        function update<%=Model.Div %>(data) {
            $("#<%=o %>").html(data);
            $("#<%=o %> form").ajaxForm({
                beforeSubmit: function () { return $("#<%=o %> form").validate().valid(); },
                success: OnSuccess<%=Model.Div %>
            }); //ajaxForm
            $("#<%=o %>").dialog('open');
            $("#<%=o %> form input:visible:first").focus();
        }
</script>
<div id="<%=Model.Div %>" class="short">
    dialogul</div>
