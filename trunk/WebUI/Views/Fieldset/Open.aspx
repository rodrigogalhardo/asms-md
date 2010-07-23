<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Core.Model.Fieldset>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <h2>
        <%:Model.Name %></h2>
    <%if (FieldsetStates.Registered.IsEqual(Model.StateId))
      {%>
    <%:Html.ActionLink("alege campuri", "ManageFields", new {id = Model.Id})%>
    <form method="post" action="<%:Url.Action("HasFields") %>">
    <input type="hidden" name="fieldsetId" value="<%:Model.Id %>" />
    <% Html.RenderPartial("save"); %>
    </form>
    <%
        }
      else if (FieldsetStates.HasFields.IsEqual(Model.StateId))
      {
    %>
    <%:Html.ActionLink("indicatori", "Index", "Indicator", new { fieldsetId = Model.Id }, null)%>
    <form method="post" action="<%:Url.Action("HasIndicators") %>">
    <input type="hidden" name="fieldsetId" value="<%:Model.Id %>" />
    <% Html.RenderPartial("save"); %>
    </form>
    <%
        }

      else if (FieldsetStates.HasIndicators.IsEqual(Model.StateId))
      {
    %>
    <%:Html.ActionLink("coeficienti", "Index", "Coefficient", new{fieldsetId = Model.Id}, null) %>
    <form method="post" action="<%:Url.Action("HasCoefficients") %>">
    <input type="hidden" name="fieldsetId" value="<%:Model.Id %>" />
    <% Html.RenderPartial("save"); %>
    </form>
    <%
        }
        
    else if (FieldsetStates.HasCoefficients.IsEqual(Model.StateId))
      {
    %>
    <form method="post" action="<%:Url.Action("Activate") %>">
    <input type="hidden" name="fieldsetId" value="<%:Model.Id %>" />
    <input type="submit" value="Activeaza" />
    </form>
    <%
        }
        else if (FieldsetStates.Active.IsEqual(Model.StateId))
      {
    %>
    <form method="post" action="<%:Url.Action("Deactivate") %>">
    <input type="hidden" name="fieldsetId" value="<%:Model.Id %>" />
    <input type="submit" value="Dezactiveaza" />
    </form><p>
    acest set este activ
    </p>
    <%
        }
        else
        {%><p>acest set este inactiv</p><%}
        %>
    <% Html.RenderPartial("back"); %>
</asp:Content>
