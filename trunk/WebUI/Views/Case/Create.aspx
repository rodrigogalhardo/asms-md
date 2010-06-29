<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.CaseCreateInput>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
    adauga dosar
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        function growClick() {
            $(".selected", $(this).parent()).removeClass('selected').unbind('click').click(growClick);
            $(this).addClass('selected');

            $(this).click(function () {
                chooseItem("#select-bank-dialog", "#banklist", '<%:Url.Action("Get","Bank") %>', '#displayBank', '#bankId');
            });
        }

        function chooseItem(theDialog, list, url, display, input) {
            var v = $(list + " table .selected").attr("value");
            if (v != null)
                $.post(url, { id: v },
                            function (data) { $(display).val(data); $(input).val(v); });
            $(theDialog).dialog('close');
        }

        $(function () {            
            $("#select-bank-dialog").dialog({
                resizable: true,
                height: 400,
                width: 700,
                modal: true,
                autoOpen: false,
                buttons: {
                    'Alege': function () {
                        chooseItem("#select-bank-dialog", "#banklist", '<%:Url.Action("Get","Bank") %>', '#displayBank', '#bankId');
//                        var v = $("#banklist table .selected").attr("value");
//                        if (v != null)
//                            $.post('<%:Url.Action("Get","Bank") %>', { id: v },
//                            function (data) { $("#displayBank").val(data.Name + ' | ' + data.Code); $('#bankId').val(v); });
//                        $(this).dialog('close');
                    },
                    'Anuleaza': function () { $(this).dialog('close'); }
                }
            }); // end dialog

            $("#bopen").click(function () {
                $("#bank-create-dialog").remove();

                $.get(
                '<%=Url.Action("Index","Bank") %>',
                function (data) { $("#select-bank-dialog").html(data).dialog('open'); }
                );
            });

        });
        
    </script>
    <div id="select-bank-dialog" title="selectati o banca" class="popup">
    </div>
    <button id="bopen">
        openit</button>
    <input type="text" id="displayBank"   disabled="disabled"/>
    <input type="hidden" id="bankId" name="bankId" />
</asp:Content>
