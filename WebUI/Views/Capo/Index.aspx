<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.CapoViewModel>" MasterPageFile="~/Views/Shared/Site.Master" %>

<%@ Import Namespace="MRGSP.ASMS.WebUI.Controllers" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <h2>
        ordinele de plata pentru contracte si acorduri aditionale
    </h2>
    <fieldset>
        <% Html.RenderPartial("SearchForm", Model.SearchForm); %>    
    </fieldset>
    <script type="text/javascript">
        $(function () {
            $("#fsearch").ajaxForm({
                success: function (data) {
                    $("#rd").html(data);
                }
            });
        });
        function upor(o){
            $.get('<%=Url.Action("ItemByPaymentOrder") %>', { id: o.Id }, function (d) {               
                $('#capo' + o.Id).before(d).remove();
            });
        }
        function ucr(o) {
            $.get('<%=Url.Action("ItemByContract") %>', { id: o.Id }, function (d) {
                $('#c' + o.Id).before(d).remove();
            });
        }
        function uar(o) {
            $.get('<%=Url.Action("ItemByAgreement") %>', { id: o.Id }, function (d) {
                $('#a' + o.Id).before(d).remove();
            });
        }
    </script>

    <%=Html.MakePopupForm<PaymentOrderController>(a => a.CreateForAgreement(0,0), successFunction:"uar") %>
    <%=Html.MakePopupForm<PaymentOrderController>(a => a.CreateForContract(0), successFunction: "ucr")%>
    <%=Html.MakePopupForm<PaymentOrderController>(a => a.Edit(0), successFunction:"upor") %>    

    <div id="rd">
    </div>
    
</asp:Content>
