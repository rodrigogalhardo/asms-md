<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AutocompleteInfo>" %>
<%
    var o = Model.Prefix + Model.Prop.Replace('.', '_').Replace('[', '_').Replace(']', '_');
    var k = Model.Prefix + Model.PropId.Replace('.', '_').Replace('[','_').Replace(']','_');
    var p = Model.ParentId != null ? Model.ParentId.Replace('.', '_').Replace('[', '_').Replace(']', '_') : null;
%>
<input type="text" id="<%=o %>" name="<%=Model.Prop %>" value="<%=Model.Value %>"
    <%=Model.HtmlAttributes %> />
<%if (Model.GeneratePropId)
  { %><input type="hidden" id="<%=k %>" name="<%=Model.PropId %>"
    value="<%=Model.PropIdValue %>" /><%} %>
<script type="text/javascript">
       $(function () {
           $("#<%=o %>").autocomplete({
               delay: <%=Model.Delay %>,
               minLength: <%=Model.MinLength %>,
               source: function (request, response) {               
                   $.ajax({
                       url: '<%=Url.Action("Search", Model.Controller) %>', type: "POST", dataType: "json",
                       data: { searchText: request.term, maxResults: <%=Model.MaxResults %> <%=p == null? "" : ", parent: $('#"+p+"').val()" %> },
                       success: function (d) { response($.map(d, function (o) { return { label: o.Text, value: o.Text, id: o.Id }}));}
                   });
               }
           }); 

            $("#<%=o %>").bind("autocompleteselect", function(e, ui) {
                $('#<%=k %>').val(ui.item ? ui.item.id : null).trigger('change');
                $('#<%=o %>').trigger('change');
           });
                  
            $("#<%=o %>").keyup(function(e){if (e.keyCode != '13') $("#<%=k %>").val(null).trigger('change');});                 
        });
</script>
