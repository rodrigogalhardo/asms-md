<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IndicatorInput>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Indicatori</h2>
        <p>
        <%:Html.ActionLink("Inapoi","Open","Fieldset",new{id = Model.FieldsetId}, null) %></p>
    <% Html.RenderAction("List", new { Model.FieldsetId }); %>
    <form action="<%:Url.Action("Index") %>" method="post">
    <%=Html.HiddenFor(o => o.FieldsetId) %>
    <%=Html.Input(o => o.Name) %>
    <%=Html.Input(o => o.Formula) %>
    <% Html.RenderPartial("save"); %>
    </form>
    <% Html.RenderAction("AssignedList", "Fieldset",new{Model.FieldsetId}); %>
</asp:Content>
