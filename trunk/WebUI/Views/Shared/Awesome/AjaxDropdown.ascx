<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Omu.Awesome.Mvc.Helpers.AjaxDropdownInfo>" %>
<%
    var o = Model.Prefix + Model.Prop.Replace('.', '_').Replace('[', '_').Replace(']', '_');
    var p = Model.ParentId != null ? Model.ParentId.Replace('.', '_').Replace('[', '_').Replace(']', '_') : null;
%>
<input type="hidden" name="<%=Model.Prop %>" id="<%=o %>" value="<%=Model.Value %>"/>
<select id='<%=o %>dropdown' <%=Model.HtmlAttributes %> ></select>

<script type="text/javascript">
    $(function () {        
        loadAjaxDropdown<%=o %>();
        $("#<%=o %>dropdown").change(function () { $('#<%=o %>').val($('#<%=o %>dropdown').val()).trigger('change'); });
        <% if(p != null){%>
        $('#<%=p %>').change(function(){loadAjaxDropdown<%=o %>(true);});
        <%} %>
    });

    function loadAjaxDropdown<%=o %>(c){
    if(c)$('#<%=o %>').val(null);
    $.post('<%=Url.Action("GetItems", Model.Controller) %>', { key: $('#<%=o %>').val() <%=p == null? "" : ", parent: $('#"+p+"').val()" %> },
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
