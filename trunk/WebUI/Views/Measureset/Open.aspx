<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Core.Model.Measureset>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <h2>
        setul de masuri
        <%:Model.Name %>
    </h2>
    <% Html.RenderPartial("back"); %>
    <% if (States.Registered.IsEqual(Model.StateId))
       {%>
    <%:Html.ActionLink("alege masuri", "ManageMeasures", new { measuresetId = Model.Id })%>
    <form method="post" action="<%:Url.Action("Activate") %>">
    <input type="hidden" name="id" value="<%:Model.Id %>" />
    <% Html.RenderPartial("save"); %>
    </form>
    <%}
       else if (States.Active.IsEqual(Model.StateId))
       {%>
    <form method="post" action="<%:Url.Action("deactivate") %>">
    <input type="hidden" name="id" value="<%:Model.Id %>" />
    <input type="submit" value="deactiveaza" />
    </form>
    <%} else{%><p>acest set de masuri este inactiv</p><%}%>
</asp:Content>
