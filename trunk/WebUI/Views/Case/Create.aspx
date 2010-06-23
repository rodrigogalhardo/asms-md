<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.CaseCreateInput>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
    adauga dosar
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">

    <script type="text/javascript">
        $(function() {
            $("#bidialog").dialog({
                resizable: true,
                height: 500,
                width: 700,
                modal: true,
                autoOpen: false,
                buttons: { 'Anuleaza': function() { $(this).dialog('close'); } }
            }); // end dialog
        }); //end document ready

        $(function() {
            $("#bopen").click(function() {
            $.get(
                '<%=Url.Action("Index","Bank") %>',
                function(data) { $("#bidialog").html(data).dialog('open'); }
                );
            });
        });
        
    </script>

    <div id="bidialog" title="the dialog">
        first content
    </div>
    <button id="bopen">
        openit</button>
</asp:Content>
