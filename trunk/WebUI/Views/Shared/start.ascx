<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%
    var errmsg = (string) ViewData["errmsg"];
     %>
<script type="text/javascript">
        $(function () {
            setFocusOnFirst();
            dob();
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

        function dob() {
        $("thead").addClass("ui-state-default");
            <%if (ViewData["painttables"] == null)
              {%>
            $("tbody tr:even").addClass("ui-widget-content");
            $("tbody tr:odd").addClass("ui-state-highlight");
            <%
              }%>
            $("input[type=submit]").addClass("abtn");
            $("tbody a").addClass("fgb");
            $("a:contains('napoi')").addClass("fgb");
            $("a:contains('reaza')").addClass("fgb");
            $(".pagination a").addClass("fgb");
            $(".pagination span").addClass("ui-state-highlight");

            $(".formz").addClass("ui-state-highlight");
            $(".formhead").addClass("ui-state-default");

            $("td form").addClass("fr");
        
            $(".fgb").addClass("abtn");
            $(".abtn").hover(
	            function () {
	                $(this).addClass("ui-state-hover");
	            },
	            function () {
	                $(this).removeClass("ui-state-hover");
	            }).bind({

	                'mousedown mouseup': function () {
	                    $(this).toggleClass('ui-state-active');
	                }

	            }).addClass("ui-state-default").addClass("ui-corner-all")
                .bind('mouseleave', function(){$(this).removeClass('ui-state-active')});
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
    <div id="nos" >
    acest sait lucreaza mai bine cu javascript permis
    </div>
</noscript>
