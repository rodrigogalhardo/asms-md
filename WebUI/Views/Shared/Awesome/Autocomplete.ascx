<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AutocompleteInfo>" %>
<%
    var o = Model.Prop;
%>
<input type="text" id="<%=o %>" name="<%=o %>" value="<%=Model.Value %>" <%=Model.HtmlAttributes %> />
<%if(Model.GeneratePropId){ %><input type="hidden" id="<%=Model.PropId %>" name="<%=Model.PropId %>" value="<%=Model.PropIdValue %>" /><%} %>
<script type="text/javascript">
       $(function () {
           $("#<%=o %>").autocomplete({
               delay: <%=Model.Delay %>,
               minLength: <%=Model.MinLength %>,
               source: function (request, response) {               
                   $.ajax({
                       url: '<%=Url.Action("Search", Model.Controller) %>', type: "POST", dataType: "json",
                       data: { searchText: request.term, maxResults: <%=Model.MaxResults %> <%=Model.ParentId == null? "" : ", parent: $('#"+Model.ParentId+"').val()" %> },
                       success: function (d) { response($.map(d, function (o) { return { label: o.Text, value: o.Text, id: o.Id }}));}
                   });
               }
           }); 

            $("#<%=o %>").bind("autocompleteselect", function(e, ui) {
                $('#<%=Model.PropId %>').val(ui.item ? ui.item.id : null).trigger('change');
                $('#<%=o %>').trigger('change');
           });
                  
            $("#<%=o %>").keyup(function(e){if (e.keyCode != '13') $("#<%=Model.PropId %>").val(null).trigger('change');});                 
        });
</script>
