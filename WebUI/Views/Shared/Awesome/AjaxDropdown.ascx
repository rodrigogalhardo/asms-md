<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Omu.Awesome.Mvc.Helpers.AjaxDropdownInfo>" %>
<%
    var o = Model.Prop;
%>
<input type="hidden" name="<%=o %>" id="<%=o %>" value="<%=Model.Value %>"/>
<select id='<%=o %>dropdown' <%=Model.HtmlAttributes %> ></select>

<script type="text/javascript">
    $(function () {        
        loadAjaxDropdown<%=o %>();
        $("#<%=o %>dropdown").change(function () { $('#<%=o %>').val($('#<%=o %>dropdown').val()).trigger('change'); });
        <% if(Model.ParentId != null){%>
        $('#<%=Model.ParentId %>').change(function(){loadAjaxDropdown<%=o %>(true);});
        <%} %>
    });

    function loadAjaxDropdown<%=o %>(c){
    if(c)$('#<%=o %>').val(null);
    $.post('<%=Url.Action("GetItems", Model.Controller) %>', { key: $('#<%=o %>').val() <%=Model.ParentId == null? "" : ", parent: $('#"+Model.ParentId+"').val()" %> },
        function (d) {
            $("#<%=o %>dropdown").empty();
            $.each(d, function (i, j) {
                var sel = "";
                if (j.Selected == true) sel = "selected = 'selected'";
                $("#<%=o %>dropdown").append("<option " + sel + " value=\"" + j.Value + "\">" + j.Text + "</option>");
            });
            if(c) $("#<%=o %>dropdown").trigger('change');
        });
    }
</script>
