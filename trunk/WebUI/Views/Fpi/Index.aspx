<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<MRGSP.ASMS.Core.Model.Fpi>>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <%
        var measures = ViewData["measures"] as IEnumerable<Measure>;
        var measuresetId = (int)ViewData["measuresetId"];
    %>
    <table>
        <thead>
            <tr>
                <td>
                    Luna
                </td>
                <% foreach (var measure in measures)
                   {
                %><td>
                    <%:measure.Name %>
                </td>
                <%
                    } %></tr>
        </thead>
        <tbody>
            <%for (var i = 1; i <= 12; i++)
              {
            %>
            <tr>
                <td>
                    <%=i %>
                </td>
                <%
                    foreach (var measure in measures)
                    {
                        Response.Write("<td>");
                        var x = Model.Where(fpi => fpi.MeasureId == measure.Id && fpi.Month == i).SingleOrDefault();
                        if (x != null)
                        {
                %>
                <%=x.Amount %>
                <%=Html.ActionLink("Clasament","Rank",new{fpiId = x.Id}) %>
                <%
}
                        else
                        {
                %>
                <%=Html.ActionLink("+", "Create", new{ measuresetId, measureId = measure.Id, month = i}) %>
                <%
                    }
                        Response.Write("</td>");
                    }%>
            </tr>
            <%
                } %>
        </tbody>
    </table>
</asp:Content>
