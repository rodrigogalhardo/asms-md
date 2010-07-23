<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FieldInputz>>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <h2>
        indepliniti forma</h2>
<%--          <script>
              $(document).ready(function () {
                  $("#commentForm").validate();
              });
  </script>
--%>

        <form id="commentForm" method="post" action="<%=Url.Action("Index") %>" >
        <input type="hidden" value="<%=ViewData["dossierId"] %>" name="dossierId" id="dossierId"/>
    <% foreach (var o in Model)
       {
    %>
    

    <div class="efield">
        <div class="elabel">
            <label for="c<%=o.Id%>">
                <%:o.Name%></label>:
        </div>
        <div class="einput">
            <input id="c<%=o.Id %>" name="c<%=o.Id %>" type="text" value="<%:o.Value != 0 ? o.Value.ToString() : string.Empty %>" class="required <%= o.HasError ? "input-validation-error": string.Empty  %>" />
        </div>
        <span class="field-validation-error">
        <%:o.ErrorMessage %>
        </span>
        <div class="example">
            <%:o.Description%>
        </div>
    </div>
    <%
        }%>
        <% Html.RenderPartial("save"); %>
        </form>
</asp:Content>
