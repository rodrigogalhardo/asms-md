<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Core.Model.Measureset>"
    MasterPageFile="~/Views/Shared/Site.Master" %>
<%@ Import Namespace="MRGSP.ASMS.WebUI.Controllers" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <h2>
        setul de masuri
        <%:Model.Name %>
    </h2>
    <%=Html.Confirm("Sunteti sigur ca doriti sa efectuati aceasta actiune ?") %>
    <% Html.RenderPartial("back"); %>
    
    
    <% if (States.Registered.IsEqual(Model.StateId))
       {%>
     
    <%:Html.ActionLink("alege masuri", "ManageMeasures", new { measuresetId = Model.Id }, new{@class="abtn"})%>
 
    <form method="post" action="<%:Url.Action("Activate") %>">
    <input type="hidden" name="id" value="<%:Model.Id %>" class="confirm" />
        <input type="submit" value="Salveaza" />
    </form>
   <br />
    <%}
       else if (States.Active.IsEqual(Model.StateId))
       {%>
   
        <%=Html.ActionLink("planul financiar","Index","Fpi",new{measuresetId = Model.Id}, new{@class="abtn"} ) %>
 
    <form method="post" action="<%:Url.Action("deactivate") %>">
    <input type="hidden" name="id" value="<%:Model.Id %>" />
    <input type="submit" value="dezactiveaza" class="confirm"/>
    </form>
    <%}
       else
       {%>
        acest set de masuri este inactiv
    <%}%>
    <script type="text/javascript">
        function openReport(o) {
            window.open('<%=Url.Content(@"~\Repor.aspx") %>' + '?report=crossDistrictMeasure&measuresetId=' + o.MeasuresetId + '&date='+o.Date, 'newtaborsomething');
        }
    </script>
    <br />
    <%=Html.MakePopupForm<ReportController>(o => o.CrossDistrictMeasure(0), successFunction:"openReport", width:500, height:200) %>
    <%=Html.PopupFormActionLink<ReportController>(o => o.CrossDistrictMeasure(Model.Id),"raport raioane x masuri", new{@class="abtn"}) %>

</asp:Content>
