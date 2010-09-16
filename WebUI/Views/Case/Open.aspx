<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Core.Model.DossierInfo>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <h2>
        <%:Model.Name %></h2>
    <%if (Model.Disqualified)
      {%>
    acest dosar este discalificat
    <%}
      else
      {%>
    <%
        if (Model.StateId == (int)DossierStates.Registered)
        {%>
    <%=Html.ActionLink("indeplineste campurile", "Index", "FillFields", new {Model.Id}, null)%>
    <%
        }%>
    <p>
        <%=Html.ActionLink("discalifica acest dosar", "Disqualify", new {dossierId = Model.Id})%>
    </p>
    <%
        }%>
</asp:Content>
