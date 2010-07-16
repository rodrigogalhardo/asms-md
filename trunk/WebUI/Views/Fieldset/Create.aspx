<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FieldsetInput>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Create</h2>
    <form action="<%=Url.Action("Create") %>" method="post">
    <%=Html.Input(o => o.Name) %>
    <%=Html.Input(o => o.EndDate) %>
    <% Html.RenderPartial("save"); %>
    </form>
    <%=Html.ClientSideValidation<FieldsetInput>() %>
    <% Html.RenderPartial("back"); %>
</asp:Content>
