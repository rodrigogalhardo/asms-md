<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Core.Model.Dossier>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <%=Html.ActionLink("inapoi", "open", new{Model.Id}, new{@class="abtn"}) %>
    <h2>
        valorile campurilor
    </h2>
    <% Html.RenderAction("fieldValues", new { Model.Id }); %>
    <h2>
        valorile indicatorilor
    </h2>
    <% Html.RenderAction("indicatorValues", new { Model.Id }); %>
    <h2>
        valorile coeficientilor
    </h2>
    <% Html.RenderAction("coefficientValues", new { Model.Id  }); %>
</asp:Content>
