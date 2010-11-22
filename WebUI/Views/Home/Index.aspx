<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="MRGSP.ASMS.WebUI.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ul class="bl">
    <li>
     <%=Html.ActionLink("Adauga Dosar","Create", "Dossier", null, new {@class="abtn"}) %>
     </li>
    <%if (ViewData["msid"] != null)
      {%>
    <li>
        <%=Html.ActionLink("planul financiar","Index","Fpi",new{measuresetId = (int)ViewData["msid"]}, new{@class="abtn"} ) %></li>
    <%} %>
    </ul>

    <%=Html.MakePopupForm<ReportController>(o => o.DossiersByDistrict(), successFunction: "dossiersByDistrict")%>
    <%=Html.PopupFormActionLink<ReportController>(o => o.DossiersByDistrict(),"Raport dosare dupa raion", new{@class = "abtn"}) %>
    <script type="text/javascript">
        function dossiersByDistrict(o) {
            window.open('<%=Url.Content(@"~\Repor.aspx") %>' + '?report=dossiersByDistrict&district=' + o.District + '&year=' + o.Year, 'newtaborsomething');
        }
    </script>


    <script type="text/javascript">
        function crossDistrictMeasure(o) {
                window.open('<%=Url.Content(@"~\Repor.aspx") %>' + '?report=crossDistrictMeasure&measuresetId=' + o.MeasuresetId + '&date=' + o.Date, 'newtaborsomething');
            }
    </script>

    <%=Html.MakePopupForm<ReportController>(o => o.CrossDistrictMeasure(0), successFunction: "crossDistrictMeasure", width: 500, height: 200)%>
    <%=Html.PopupFormActionLink<ReportController>(o => o.CrossDistrictMeasure((int) ViewData["msid"]), "raport raioane x masuri", new { @class = "abtn" })%>

 <script type="text/javascript">
        function crossDistrictMeasureAmountPayed(o) {
                window.open('<%=Url.Content(@"~\Repor.aspx") %>' + '?report=crossDistrictMeasureAmountPayed&measuresetId=' + o.MeasuresetId + '&date=' + o.Date, 'newtaborsomething');
            }
    </script>

    <%=Html.MakePopupForm<ReportController>(o => o.CrossDistrictMeasureAmountPayed(0), successFunction: "crossDistrictMeasureAmountPayed", width: 500, height: 200)%>
    <%=Html.PopupFormActionLink<ReportController>(o => o.CrossDistrictMeasureAmountPayed((int) ViewData["msid"]), "raport raioane x masuri sume", new { @class = "abtn" })%>

</asp:Content>
