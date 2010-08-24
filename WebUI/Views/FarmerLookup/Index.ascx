<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="toolbox fl">
    <label for="search-farmer-name">
        Nume</label>
    <input type="text" id="search-farmer-name" />
    <label for="search-farmer-code">
        Cod fiscal</label>
    <input type="text" id="search-farmer-code" />
    <a class="fgb" id="farmersearch">Cauta</a>
</div>
<br class="cbt" />
<div id="farmerlist">
</div>
<script type="text/javascript">
    
    function searchFarmers() {
        var name = $("#search-farmer-name").val();
        var code = $("#search-farmer-code").val();
        if (name == null) name = null;
        if (code == null) code = null;

        $.post('<%=Url.Action("Page") %>', { name: name, code: code },
                function (data) {
                    $("#farmerlist").html(data);
                    dob();
                }
            );
    }

    $('#farmersearch').click(searchFarmers);

    $('#search-farmer-name').keyup(function (e) {
        if (e.keyCode == 13) {
            searchFarmers();
        }
    });

    dob();
    
</script>
