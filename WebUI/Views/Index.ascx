<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="toolbox fl">
    <label for="search-farmer-name">
        Nume</label>
    <input type="text" id="search-farmer-name" />

    <label for="search-farmer-code">
        Cod</label>
    <input type="text" id="search-farmer-code" />

    <button class="abtn" id="farmersearch">
        <span class="ui-icon ui-icon-search fl"></span>Cauta</button>
    <button class="abtn" id="farmercreate">
        <span class="ui-icon ui-icon-circle-plus fl"></span>Adauga</button>
</div>
<br class="cbt" />
<div id="farmerlist">
</div>
<div id="create-farmer-dialog" title="adauga fermier">
</div>
<script type="text/javascript">

    $(function () {
        $("#create-farmer-dialog").dialog({
            resizable: true,
            height: 500,
            width: 650,
            modal: true,
            autoOpen: false,
            buttons: {
                'Salveaza': function () { $("form", this).submit(); },
                'Anuleaza': function () { $(this).dialog('close'); }
            }
        });
    });

    function registerFarmerCreateForm() {
        $(".mydate").datepicker();
        $("#create-farmer-dialog form").ajaxForm({
            beforeSubmit: function () {
                var v = $("#create-farmer-dialog form").validate();
                return v.valid();
            },
            success: onSaveFarmerResponse,
            resetForm: true
        });
    }

    function onSaveFarmerResponse(result) {
        if (result == "ok") {
            $('#create-farmer-dialog').dialog('close');
            getFarmerPage(1);
        }
        else {
            $('#create-farmer-dialog').html(result);
            registerFarmerCreateForm();
            setFocusOnForm("#create-farmer-dialog");
        }
    }

    function getFarmerPage(page) {
        var name = $("#search-farmer-name").val();
        var code = $("#search-farmer-code").val();
        if (name == null) name = null;
        if (code == null) code = null;

        $.post('<%=Url.Action("Page") %>', { page: page, name: name, code: code },
                function (data) {
                    $("#farmerlist").html(data);
                }
            );
    }

    ///on load
    getFarmerPage(1);
    dob();

    $('#search-farmer-name').keyup(function (e) {
        if (e.keyCode == 13) {
            getFarmerPage(1);
        }
    });

    $('#search-farmer-code').keyup(function (e) {
        if (e.keyCode == 13) {
            getFarmerPage(1);
        }
    });

    $("#farmersearch").click(function () {
        getFarmerPage(1);
    });

    $("#farmercreate").click(function () {
        $.get(
        '<%=Url.Action("Create", "Farmer") %>',
        function (data) {
            $("#create-farmer-dialog").html(data);
            registerFarmerCreateForm();
            $("#create-farmer-dialog").dialog('open');
            setFocusOnForm("#create-farmer-dialog");
        })
    });   
        
</script>
