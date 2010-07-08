<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.CaseCreateInput>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<%@ Import Namespace="MvcContrib.UI.InputBuilder.Views" %>
<%@ Import Namespace="xVal.Html" %>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
    adauga dosar
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <% using (Html.BeginForm())
       {%>
    <div class="efield">
        <div class="elabel">
            <label>
                Fermier *:</label>
        </div>
        <div class="einput">
            <%
                Html.RenderPartial("lookup", new LookupInfo { For = "farmer", Title = "Alege fermier", Display = Model.DisplayFarmer });%>
        </div>
        <%:Html.ValidationMessageFor(o => o.FarmerId) %>
    </div>
    <div class="efield">
        <div class="elabel">
            <label>
                Banca *:</label>
        </div>
        <div class="einput">
            <%
                Html.RenderPartial("lookup", new LookupInfo { For = "bank", Title = "Alege banca", Display = Model.DisplayBank });%>
        </div>
        <%:Html.ValidationMessageFor(o => o.BankId) %>
    </div>
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
    <%=Html.Input(o => o.ActivityType) %>
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
     <div class="efield">
        <div class="elabel">
            Cine a perfectat business planul:
        </div>
        <div class="einput">
            <%=Html.DropDownList("ConsultantId",Model.ConsultantId as IEnumerable<SelectListItem>) %>
        </div>
    </div>
    <%=Html.Input(o => o.County) %>
    <div class="esubmit">
        <input class="abtn" type="submit" value="submit" />
    </div>
    <%
        }%>
    <%=Html.ClientSideValidation<CaseCreateInput>() %>
</asp:Content>
