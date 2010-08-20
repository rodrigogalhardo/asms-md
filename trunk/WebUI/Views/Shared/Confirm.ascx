<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

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
            $(".confirm").click(function () {
                currentForm = $(this).closest('form');
                $("#dialog-confirm").dialog('open');
                return false;
            });
        });
    </script>

    <div id="dialog-confirm" title="Sunteti sigur ?">
        Aceasta actiune nu poate fi anulata, Sunteti sigur ca doriti sa efectuati aceasta actiune ?
    </div>