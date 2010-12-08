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
                resizable: <%=Model.Resizable.ToString().ToLower() %>,
                autoOpen: false,  
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
                close: function () { $("#<%=o %>").html(""); }
            }); 
        });

        var l<%=o %> = null;
        function call<%=o %>(<%=JsTools.MakeParameters(Model.Parameters) %>) { 
            if(l<%=o %> != null) return;
            l<%=o %> = true;
            $.get('<%=Url.Action(Model.Action, Model.Controller) %>',            
            <%=JsTools.JsonParam(Model.Parameters) %>            
            function(d){
            l<%=o %> = null;
            $("#<%=o %>").html(d).dialog('open');
            });
        }  
</script>
<div id="<%=o %>">
</div>
