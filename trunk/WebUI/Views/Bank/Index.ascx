<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="bank-create-dialog" title="create bank">
</div>
<div class="toolbox fl">
    <label for="sbname">
        Nume</label>
    <input type="text" id="sbname" />
    <label for="sbcode">
        Cod</label>
    <input type="text" id="sbcode" />
    <button class="ui-state-default ui-corner-all" id="banksearch">
        <span class="ui-icon ui-icon-search fl"></span>Cauta</button>
    <button class="ui-state-default ui-corner-all" id="bankcreate">
        <span class="ui-icon ui-icon-circle-plus fl"></span>Adauga</button>
</div>
<br class="cbt" />
<div id="banklist">
</div>
<script type="text/javascript">
    $(function () {
        $("#bank-create-dialog").dialog({
            resizable: true,
            height: 230,
            width: 650,
            modal: true,
            autoOpen: false,
            buttons: {
                'Salveaza': function () { $("form", this).submit(); },
                'Anuleaza': function () { $(this).dialog('close'); }
            }
        });
    });

    function init() {
        getPage(1);

        $('#sbname').keyup(function (e) {
            if (e.keyCode == 13) {
                getPage(1);
            }
        });

        $('#sbcode').keyup(function (e) {
            if (e.keyCode == 13) {
                getPage(1);
            }
        });


        $("#search").click(function () {
            getPage(1);
        });

        $("#bankcreate").click(function () {
            $.get(
        '<%=Url.Action("Create", "Bank") %>',
        function (data) {
            $("#bank-create-dialog").html(data);
            registerCreateForm();
            $("#bank-create-dialog").dialog('open');
            setFocusOnForm("#bank-create-dialog");
        })
        });
    }

    function getPage(page) {
        var name = $("#sbname").val();
        var code = $("#sbcode").val();
        if (name == null) name = null;
        if (code == null) code = null;

        $.post('<%=Url.Action("Page") %>', { page: page, name: name, code: code },
                function (data) {
                    $("#banklist").html(data);
                }
            );
    }

    function registerCreateForm() {
        $("#bank-create-dialog form").ajaxForm({
            beforeSubmit: function () {
                v = $("#bank-create-dialog form").validate();
                return v.valid();
            },
            success: onSaveResponse,  // post-submit callback
            resetForm: true        // reset the form after successful submit
        });
    }

    function onSaveResponse(result) {
        if (result == "ok") {
            $('#bank-create-dialog').dialog('close');
            getPage(1);
        }
        else {
            $('#bank-create-dialog').html(result);
            registerCreateForm();
            setFocusOnForm("#bank-create-dialog");
        }
    }

    init();
        
</script>
