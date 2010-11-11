<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Omu.Awesome.Mvc.Helpers.AjaxDropdownInfo>" %>
<%
    var o = Model.Prop;
%>
<input type="hidden" name="<%=o %>" id="<%=o %>" value="<%=Model.Value %>"/>
<select id='<%=o %>dropdown'></select>

<script type="text/javascript">
    $(function () {        
        $.post('<%=Url.Action("GetItems", Model.Controller) %>', { key: $('#<%=o %>').val() },
        function (d) {
            $("#<%=o %>dropdown").empty();
            $.each(d, function (i, j) {
                var sel = "";
                if (j.Selected == true) sel = "selected = 'selected'";
                $("#<%=o %>dropdown").append("<option " + sel + " value=\"" + j.Value + "\">" + j.Text + "</option>");
            });
        });
        $("#<%=o %>dropdown").change(function () { $('#<%=o %>').val($('#<%=o %>dropdown').val()); });
    });
</script>
