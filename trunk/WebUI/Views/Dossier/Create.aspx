<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.DossierCreateInput>"
    MasterPageFile="~/Views/Shared/Site.Master" %>
<%@ Import Namespace="MRGSP.ASMS.WebUI.Helpers" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
    adauga dosar
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <% using (Html.BeginForm())
       {%>
    <%=Html.EditorFor(o => o.FarmerVersionId) %>
    <%=Html.EditorFor(o => o.MeasureId) %>
    <%=Html.EditorFor(o => o.AdminFirstName) %>
    <%=Html.Example("ex: Vasile")%>
    <%=Html.EditorFor(o => o.AdminLastName) %>
    <%=Html.Example("ex: Popescu")%>
    <%=Html.EditorFor(o => o.RepresentativeFirstName) %>
    <%=Html.EditorFor(o => o.RepresentativeLastName) %>
    <%=Html.EditorFor(o => o.FriendPhone) %>
    <%=Html.EditorFor(o => o.ProTraining) %>
    <%=Html.EditorFor(o => o.Speciality) %>
    <%=Html.Example("Conform dimplomei")%>
    <%=Html.EditorFor(o => o.DiplomaIssuer) %>
    <%=Html.EditorFor(o => o.HasContract) %>
    <%=Html.EditorFor(o => o.ContractNumber) %>
    <%=Html.EditorFor(o => o.ContractDate) %>
    <%=Html.EditorFor(o => o.ServiceProvider) %>
    <%=Html.EditorFor(o => o.AmountRequested) %>
    <%=Html.EditorFor(o => o.PerfecterId) %>
    <% Html.RenderPartial("save"); %>
    <%
        }%>
</asp:Content>
