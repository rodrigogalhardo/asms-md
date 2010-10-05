<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MRGSP.ASMS.WebUI.Helpers.ConfirmInfo>" %>
<script type="text/javascript">
        var currentForm;
        $(function () {
            $("#dialog-confirm").dialog({
                resizable: false,
                height: 220,
                width: 400,
                modal: true,
                autoOpen: false,
                buttons: {
                    'Confirma': function () {
                        $(this).dialog('close');
                        currentForm.submit();
                    },
                    'Anuleaza': function () {
                        $(this).dialog('close');
                    }
                }
            });
            $(".<%=Model.CssClass %>").click(function () {
                currentForm = $(this).closest('form');
                $("#dialog-confirm").dialog('open');
                return false;
            });
        });
</script>
<div id="dialog-confirm" title="Dialog de confirmare">
    <%=Model.Message %>
</div>
