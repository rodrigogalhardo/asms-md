<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

    <script type="text/javascript">
        var currentForm;
        $(function() {
            $("#dialog-confirm").dialog({
                resizable: false,
                height: 220,
                width: 400,
                modal: true,
                autoOpen: false,
                buttons: {
                    'Sterge': function() {
                        $(this).dialog('close');
                        currentForm.submit();
                    },
                    'Anuleaza': function() {
                        $(this).dialog('close');
                    }
                }
            });
            $(".delete").click(function() {
                currentForm = $(this).closest('form');
                $("#dialog-confirm").dialog('open');
                return false;
            });            
        });
    </script>

    <div id="dialog-confirm" title="Confirmati stergerea ?">
        Acest element va fi sters si va fi imposibil de recuperat. Sunteti sigur ?        
    </div>