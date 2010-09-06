<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<MRGSP.ASMS.Core.Model.Dossier>>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<script>
    $(function () {
        $(".winner").addClass("ui-state-highlight");
        $(".loser").addClass("ui-widget-content");
    });
</script>
<h2><%:ViewData["msg"] %></h2>
    <table>
        <thead>
            <tr>
              
                <td>
                    Suma solicitata
                </td>
                <td>
                    Valoare
                </td>
            </tr>
        </thead>
        <tbody>
            <%foreach (var dossier in Model)
              {
            %><tr class="<%= dossier.StateId == 3 ? "loser" : "winner" %>">                
                <td>
                    <%:dossier.AmountRequested %>
                </td>
                <td>
                    <%:dossier.Value %>
                </td>
            </tr>
            <%
                } %>
        </tbody>
    </table>
</asp:Content>
