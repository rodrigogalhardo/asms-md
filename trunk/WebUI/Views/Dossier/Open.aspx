<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Core.Model.DossierInfo>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<%@ Import Namespace="MRGSP.ASMS.WebUI.Controllers" %>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <h2>
        <%:Model.Name %></h2>
    <%=Html.Confirm("Sunteti siguri ca doriti sa autorizati spre plata acest dosar ?") %>
    <p>
        <%=Html.ActionLink("<- spre clasament","Index","Rank",new{Model.FpiId},new{@class="abtn"}) %>
    </p>
    <p>
        <%=Html.ActionLink("vizualizeaza valorile dosarului", "values", new{Model.Id}, new{@class = "abtn"}) %>
    </p>
    <%if (Model.Disqualified)
      {%>
    acest dosar este discalificat
    <%}
      else
      {%>
    <%
        if (Model.StateId == DossierStates.Registered)
        {%>
    <p>
        <%=Html.ActionLink("indeplineste campurile", "Index", "FillFields", new {Model.Id}, new{@class = "abtn"})%>
    </p>
    <%
        }
        if (Model.StateId == DossierStates.Winner || Model.StateId == DossierStates.HasCoefficients)
        {
    %>
    <%=Html.MakePopupForm<DossierController>(o => o.ChangeAmountPayed(0), height:200) %>
    <p>
        <%=Html.PopupFormActionLink<DossierController>(o => o.ChangeAmountPayed(Model.Id), "schimba suma spre plata", new{@class="abtn"}) %>
    </p>
    <%
        }
        if (Model.StateId == DossierStates.Winner)
        {
    %>
    <form action="<%=Url.Action("Authorize", new{Model.Id}) %>" method="post">
    <input type="submit" value="Autorizeaza spre plata" class="confirm" />
    </form>
    <%
        }
        if (Model.StateId == DossierStates.Authorized)
        {
    %>
    <%=Html.Action("index","contract", new{dossierId = Model.Id}) %>
    <%
        }
    %>
    <%if (Model.StateId != DossierStates.Authorized)
      {%>
    <%=Html.MakePopupForm<DossierController>(o => o.Disqualify(0), height:200) %>
    <p>
        <%=Html.PopupFormActionLink<DossierController>(o => o.Disqualify(Model.Id), "discalifica acest dosar",new{@class = "abtn"})%>
    </p>
    <%}%>
    <%
        }%>
</asp:Content>
