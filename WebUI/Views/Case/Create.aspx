<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.DossierCreateInput>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
    adauga dosar
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <% using (Html.BeginForm())
       {%>
       <div class="efield">
        <div class="elabel">
            Masura:
        </div>
        <div class="einput">
            <%=Html.DropDownList("MeasureId", Model.MeasureId as IEnumerable<SelectListItem>)%>
        </div>
    </div>
    <%=Html.Input(o => o.FarmerName) %>
    <div class="efield">
        <div class="elabel">
            Forma organizatorico-juridica:
        </div>
        <div class="einput">
            <%=Html.DropDownList("CompanyTypeId",Model.CompanyTypeId as IEnumerable<SelectListItem>) %>
        </div>
    </div>
    <%=Html.Input(o => o.DateReg) %>
    <%=Html.Input(o => o.NrReg) %>
    <%=Html.Input(o => o.ActivityType) %>
    <%=Html.Input(o => o.FiscalCode) %>
    <div class="efield">
        <div class="elabel">
            Zona geografica:
        </div>
        <div class="einput">
            <%=Html.DropDownList("AreaId",Model.AreaId as IEnumerable<SelectListItem>) %>
        </div>
    </div>
    <div class="efield">
        <div class="elabel">
            Raion:
        </div>
        <div class="einput">
            <%=Html.DropDownList("DistrictId",Model.DistrictId as IEnumerable<SelectListItem>) %>
        </div>
    </div>
    <%=Html.Input(o => o.County) %>
    <%=Html.Input(o => o.BankName) %>
    <%=Html.Input(o => o.BankCode) %>
    <%=Html.Input(o => o.SettlementAccount) %>
    <%=Html.Input(o => o.AdminFirstName) %>
    <%=Html.Input(o => o.AdminLastName) %>
    <%=Html.Input(o => o.RepresentativeFirstName) %>
    <%=Html.Input(o => o.RepresentativeLastName) %>
    <%=Html.Input(o => o.Phone) %>
    <%=Html.Input(o => o.Fax) %>
    <%=Html.Input(o => o.Mobile) %>
    <%=Html.Input(o => o.FriendPhone) %>
    <%=Html.Input(o => o.Email) %>
    <%=Html.Input(o => o.ProTraining) %>
    <%=Html.Input(o => o.Speciality) %>
    <%=Html.Input(o => o.DiplomaIssuer) %>
    <%=Html.Input(o => o.HasContract) %>
    <%=Html.Input(o => o.ContractNumber) %>
    <%=Html.Input(o => o.ContractDate) %>
    <%=Html.Input(o => o.ServiceProvider) %>
    <%=Html.Input(o => o.AmountRequested) %>
    <div class="efield">
        <div class="elabel">
            Cine a perfectat business planul:
        </div>
        <div class="einput">
            <%=Html.DropDownList("PerfecterId",Model.PerfecterId as IEnumerable<SelectListItem>) %>
        </div>
    </div>
    <% Html.RenderPartial("save"); %>
    <%
        }%>
    <%=Html.ClientSideValidation<DossierCreateInput>() %>
</asp:Content>
