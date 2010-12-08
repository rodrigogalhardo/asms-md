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
        <li>
            <%=Html.ActionLink("ordinele de plata pentru contracte si acorduri aditionale", "index", "capo",null, new{@class="abtn"})%>
        </li>
        <%} %>
    </ul>
    <h2>
        Rapoarte</h2>
    <%=Html.MakePopupForm<ReportController>(o => o.DossiersByDistrict(), successFunction: "dossiersByDistrict")%>
    <%=Html.MakePopupForm<ReportController>(o => o.CrossDistrictMeasure(0), successFunction: "crossDistrictMeasure", width: 500, height: 200)%>
    <%=Html.MakePopupForm<ReportController>(o => o.CrossDistrictMeasureAmountPayed(0), successFunction: "crossDistrictMeasureAmountPayed", width: 500, height: 200)%>
    <%=Html.Report("dossiersByDistrict", "District", "Year")%>
    <%=Html.Report("crossDistrictMeasure","MeasuresetId","Date")%>
    <%=Html.Report("crossDistrictMeasureAmountPayed", "MeasuresetId", "Date")%>
    <%=Html.Report("operInfo", "MeasuresetId") %>
    <ul class="bl">
        <li>
            <%=Html.PopupFormActionLink<ReportController>(o => o.DossiersByDistrict(), "Raport dosare dupa raion", new{@class = "abtn"}) %></li>
        <li><a href='javascript:operInfo(({MeasuresetId: <%=ViewData["msid"] %>}))' class="abtn">
            informatie operativa</a></li>
        <li>
            <%=Html.PopupFormActionLink<ReportController>(o => o.CrossDistrictMeasure((int) ViewData["msid"]), "număr de dosare depuse la sediul AIPA din fiecare raion pentru fiecare măsură în parte", new { @class = "abtn" })%></li>
        <li>
            <%=Html.PopupFormActionLink<ReportController>(o => o.CrossDistrictMeasureAmountPayed((int) ViewData["msid"]), "sumele totale achitate de către AIPA solicitanților din fiecare raion pentru fiecare măsură în parte", new { @class = "abtn" })%></li>
    </ul>
</asp:Content>
