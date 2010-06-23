<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<h2>    Banci</h2>
<div id="bcdialog" style="display: none;" title="create bank">
    first content
</div>
<button class="ui-state-default ui-corner-all" id="create">
    <span class="ui-icon ui-icon-circle-plus fl"></span>Adauga</button>
<br class="cbt" />

<div id="banks">

</div>

<script type="text/javascript">

    function getPage(page) {
        $.get('<%=Url.Action("Page") %>', { page: page },
                function(data) {
                    $("#banks").html(data);
                }
            );
            }

            getPage(1);

    $("#bcdialog").dialog({
        resizable: true,
        height: 300,
        width: 650,
        modal: true,
        autoOpen: false,
        buttons: {
            'Anuleaza': function() { $(this).dialog('close'); },
            'Salveaza': function() { $("form", this).submit(); }
        }
    });

    $("#create").click(function() {
        $.get(
        '<%=Url.Action("Create", "Bank") %>',
         function(data) {
             $("#bcdialog").html(data);
             $("#bcdialog form").ajaxForm({
                 beforeSubmit: function() {
                     v = $("#bcdialog form").validate();
                     return v.valid();
                 },
                 success: postSubmitCallback,  // post-submit callback
                 resetForm: true        // reset the form after successful submit
             });
             $("#bcdialog").dialog('open');
         })
    });

    function postSubmitCallback(result) {
        alert('callback');
        if (result == "ok")
            $('#bcdialog').dialog('close');
        else {
            $('#bcdialog').html(result);
            $("#bcdialog form").ajaxForm({
                beforeSubmit: function() {
                    v = $("#bcdialog form").validate();
                    return v.valid();
                },
                success: postSubmitCallback,  // post-submit callback
                resetForm: true        // reset the form after successful submit
            });
        }
        
    }      
        
</script>