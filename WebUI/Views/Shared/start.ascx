<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%
    var errmsg = (string)ViewData["errmsg"];
%>
<script type="text/javascript">
        $(function () {
            setFocusOnFirst();
            applyjqcolors();

            <%if(errmsg != null){%>
             $("#errmsg").dialog({
                resizable: false,
                height: 220,
                width: 400,
                modal: true,
                buttons: {
                    ' OK ': function () {
                        $(this).dialog('close');                        
                    }
                }
            });
            <%} %>
        });

 function styleup() {
            $("select,fieldset,input:text,.ae-multi-lookup").addClass("ui-corner-all");
            $("input:text,.ae-multi-lookup").addClass('ui-widget-content');
            $("input[type=submit]").addClass("abtn");
            $("table thead").addClass("ui-state-default");

            $(".ae-lookup-list li:even, .ae-lookup-list li:odd").removeClass('ui-widget-content ui-state-highlight');
            $(".ae-lookup-list li:even").addClass("ui-widget-content"); 
            $(".ae-lookup-list li:odd").addClass("ui-state-highlight");

            $("table tbody tr:even, tbody tr:odd").removeClass("ui-widget-content ui-state-highlight");
            $("table tbody tr:even").addClass("ui-widget-content");
            $("table tbody tr:odd").addClass("ui-state-highlight");            
            $("table tbody tr a").addClass("abtn");            
            
            $('.ui-state-highlight a').css('color', $('.ui-state-default').css('color'));
            mybutton(".abtn");
            
            $(".field-validation-error").addClass('ui-state-error ui-corner-all');
            $(".input-validation-error").addClass('ui-state-error');            
        }

        function applyjqcolors() {
            $.fx.speeds._default = 300;            
            $(window).bind("resize", function (e) { $("#main-container").css("min-height", ($(window).height() - 120) + "px"); });
            $("#main-container").css("min-height", ($(window).height() - 120) + "px").addClass("ui-widget-content");

            styleup();
            $("body").ajaxComplete(styleup);
            $(document).bind("awesome", styleup);
        }

        function setFocusOnFirst() {
            $("input:text:visible:first").focus();
        }

        function setFocusOnForm(id) {
            $(id + " form input:text:visible:first").focus();
        }       
</script>
<div id="errmsg" class="hidden">
    <%=ViewData["errmsg"] %>
</div>
<noscript>
    <div id="nos">
        acest sait lucreaza mai bine cu javascript permis
    </div>
</noscript>
