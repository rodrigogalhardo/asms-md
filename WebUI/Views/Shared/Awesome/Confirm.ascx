<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Omu.Awesome.Mvc.Helpers.ConfirmInfo>" %>
<%
    var o = Model.CssClass; %>
<script type="text/javascript">
        var currentForm<%=Model.CssClass %>;
        $(function () {
            $("#dialog-confirm-<%=o %>").dialog({
            show: "drop",
			hide: "fade",
                resizable: false,
                height: <%=Model.Height %>,
                width: <%=Model.Width %>,
                modal: true,
                autoOpen: false,
                buttons: {
                    '<%=Model.YesText %>': function () {
                        $(this).dialog('close');
                        currentForm<%=o %>.submit();
                    },
                    '<%=Model.NoText %>': function () {
                        $(this).dialog('close');
                    }
                }
            });
            $(".<%=o %>").live('click', function () {
                currentForm<%=o %> = $(this).closest('form');
                $("#dialog-confirm-<%=o %>").dialog('open');
                return false;
            });
        });
</script>
<div id="dialog-confirm-<%=o %>" title="<%=Model.Title %>">
    <%=Model.Message %>
</div>
