<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LookupInfo>" %>
<script type="text/javascript">

    function grow<%=Model.For %>Click() {
        $(".selected", $(this).parent()).removeClass('selected').unbind('click').click(grow<%=Model.For %>Click);
        $(this).addClass('selected');

        <% if (Model.Choose)
           {%>
        $(this).click(function () {
            choose<%=Model.For%>Click("#select-<%=Model.For%>-dialog", "#<%=Model.For%>list", '<%:Url.Action("Get", Model.For)%>', '#display<%=Model.For%>', '#<%=Model.For%>Id');
        });
        <%
           }%>
    }

    <%if(Model.Choose)
      {%>
    function choose<%=Model.For%>Click(theDialog, list, url, display, input) {
        var v = $(list + " table .selected").attr("value");
        if (v != null)
            $.post(url, { id: v },
                            function (data) { $(display).val(data); $(input).val(v); });
        $(theDialog).dialog('close');
    }
    <%
      }%>

    $(function () {
        $("#select-<%=Model.For %>-dialog").dialog({
            resizable: true,
            height: 400,
            width: 700,
            modal: true,            

            <%if(Model.Choose)
              {%>
            buttons: {
                'Alege': function () {
                    choose<%=Model.For%>Click("#select-<%=Model.For%>-dialog", "#<%=Model.For%>list", '<%:Url.Action("Get", Model.For)%>', '#display<%=Model.For%>', '#<%=Model.For%>Id');
                },
                'Anuleaza': function () { $(this).dialog('close'); }
            },
            <%
              }%>
            autoOpen: false
        }); // end dialog

        $("#open-<%=Model.For %>").click(function () {
            $("#create-<%=Model.For %>-dialog").remove();

            $.get(
                '<%=Url.Action("index", Model.For) %>',
                function (data) { $("#select-<%=Model.For %>-dialog").html(data).dialog('open'); }
                );
        });
    });
        
</script>
<div id="select-<%=Model.For %>-dialog" title="<%=Model.Title %>" class="popup">
</div>
<%if (Model.Choose)
  { %>
<button id="open-<%=Model.For %>" type="button">
    ...</button>
<input type="text" id="display<%=Model.For %>" disabled="disabled" />
<input type="hidden" id="<%=Model.For %>Id" name="<%=Model.For %>Id" />
<%}
  else
  {%>
<button id="open-<%=Model.For %>">
    <%=Model.Title %></button>
<%
    }%>