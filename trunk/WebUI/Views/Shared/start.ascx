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
            $("hr").addClass("ui-state-default");
            $("select").addClass("ui-corner-all");
            $("input:text").addClass("ui-corner-all");
            $("input[type=submit]").addClass("abtn");
            $("thead").addClass("ui-state-default");
            $("tbody tr:even").addClass("ui-widget-content");
            $("tbody tr:odd").addClass("ui-state-highlight");
            $("tbody a").addClass("abtn");
            $('.ui-state-highlight a').css('color', $('.ui-state-default').css('color'));
            mybutton(".abtn");
            $(".field-validation-error").addClass('ui-state-error ui-corner-all');
            $(".input-validation-error").addClass('ui-state-error');
        }

        function applyjqcolors() {
            $.fx.speeds._default = 300;
            $("pre").addClass("ui-state-highlight");
            $(window).bind("resize", function (e) { $("#main-container").css("min-height", ($(window).height() - 120) + "px"); });
            $("#main-container").css("min-height", ($(window).height() - 120) + "px");
            $("#main-container").addClass("ui-widget-content");

            styleup();

            $("body").ajaxComplete(function () {
                styleup();
            });
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
