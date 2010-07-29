<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IndicatorInput>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <%:Html.ActionLink("Inapoi","Open","Fieldset",new{id = Model.FieldsetId}, null) %></p>
        
        <h2>
        Indicatori</h2>
        
    <% Html.RenderAction("List", new { Model.FieldsetId }); %>
    <form action="<%:Url.Action("Index") %>" method="post">
    <%=Html.ValidationSummary(true) %>
    <%=Html.HiddenFor(o => o.FieldsetId) %>
    <%=Html.Input(o => o.Name) %>
    <%=Html.Input(o => o.Formula) %>
    <% Html.RenderPartial("save"); %>
    </form>
    <% Html.RenderAction("AssignedListLite", "Fieldset",new{Model.FieldsetId}); %>
</asp:Content>
