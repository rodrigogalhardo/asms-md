<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="toolbox fl">
    <label for="search-bank-name">
        Nume</label>
    <input type="text" id="search-bank-name" />

    <label for="search-bank-code">
        Cod</label>
    <input type="text" id="search-bank-code" />

    <button class=" abtn" id="banksearch">
        <span class="ui-icon ui-icon-search fl"></span>Cauta</button>

    <button class=" abtn" id="bankcreate">
        <span class="ui-icon ui-icon-circle-plus fl"></span>Adauga</button>
</div>
<br class="cbt" />
<div id="banklist">
</div>

<div id="create-bank-dialog" title="adauga banca">
</div>
<script type="text/javascript">
    $(function () {
        $("#create-bank-dialog").dialog({
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
        
        getBankPage(1);

        $('#search-bank-name').keyup(function (e) {
            if (e.keyCode == 13) {
                getBankPage(1);
            }
        });

        $('#search-bank-code').keyup(function (e) {
            if (e.keyCode == 13) {
                getBankPage(1);
            }
        });

        $("#banksearch").click(function () {
            getBankPage(1);
        });

        $("#bankcreate").click(function () {
            $.get(
        '<%=Url.Action("Create", "Bank") %>',
        function (data) {
            $("#create-bank-dialog").html(data);
            registerCreateForm();
            $("#create-bank-dialog").dialog('open');
            setFocusOnForm("#create-bank-dialog");
        })
        });
    }

    function getBankPage(page) {
        var name = $("#search-bank-name").val();
        var code = $("#search-bank-code").val();
        if (name == null) name = null;
        if (code == null) code = null;

        $.post('<%=Url.Action("Page") %>', { page: page, name: name, code: code },
                function (data) {
                    $("#banklist").html(data);
                }
            );
    }

    function registerCreateForm() {
        $("#create-bank-dialog form").ajaxForm({
            beforeSubmit: function () {
                v = $("#create-bank-dialog form").validate();
                return v.valid();
            },
            success: onSaveResponse,  // post-submit callback
            resetForm: true        // reset the form after successful submit
        });
    }

    function onSaveResponse(result) {
        if (result == "ok") {
            $('#create-bank-dialog').dialog('close');
            getBankPage(1);
        }
        else {
            $('#create-bank-dialog').html(result);
            registerCreateForm();
            setFocusOnForm("#create-bank-dialog");
            
        }
    }

   
    init();
    dob();
        
</script>
