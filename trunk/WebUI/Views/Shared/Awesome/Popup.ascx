<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Omu.Awesome.Mvc.Helpers.PopupInfo>" %>
<%
    var o = "p" + Model.Prefix + (Model.Action + Model.Controller).ToLower();
%>
<script type="text/javascript">
        $(function () {
            $("#<%=o %>").dialog({
                show: "fade",			    
                width: <%=Model.Width %>,
                height: <%=Model.Height %>,
                title: '<%=Model.Title %>',
                modal: <%=Model.Modal.ToString().ToLower() %>,
                <%=Model.Position %>
                resizable: <%=Model.Resizable.ToString().ToLower() %>,                
                <%if(Model.Buttons.Count > 0)
                {%>
                buttons: {
                <%
                var i = 0;
                    foreach (var button in Model.Buttons)
                    {
                        i++;
                    %>  
                     "<%=button.Key %>" : <%=button.Value %><%=i == Model.Buttons.Count ? "": "," %>
                    <%} %>                    
                },
                <%} %>     
                autoOpen: false,
            }); 
        });

        var l<%=o %> = null;
        function call<%=o %>(<%=JsTools.MakeParameters(Model.Parameters) %>) { 
            if(l<%=o %> != null) return;
            l<%=o %> = true;
            <%if(Model.Content == null)
              {%>
            $.get('<%=Url.Action(Model.Action, Model.Controller) %>',            
            <%=JsTools.JsonParam(Model.Parameters) %>            
            function(d){
            l<%=o %> = null;
            $("#<%=o %>").html(d).dialog('open');
            });
            <%
              }else
              {%>
            $("#<%=o %>").dialog('open');  
            l<%=o %> = null;
              <%}%>            
        }  
</script>
<div id="<%=o %>">
<%=Model.Content %>
</div>
