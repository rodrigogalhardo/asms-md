<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<MRGSP.ASMS.Core.Model.OrganizationDisplay>>" %>
<%= Html.ActionLink("Editeaza","EditOrganization",new{Model.First().FarmerId}, new{@class="fgb"}) %>
<table>
    <thead>
        <tr>
            <td>
                Nume
            </td>
            <td>
                Forma
            </td>
            <td>
                Cod fiscal
            </td>
            <td>
                Data inregistrarii
            </td>
            <td>
                Nr de inregistrare
            </td>
            <td>
                start
            </td>
            <td>
                sfarsit
            </td>
        </tr>
    </thead>
    <tbody>
        <%foreach (var o in Model)
          {
        %>
        <tr>
            <td>
                <%=o.Name %>
            </td>
            <td>
                <%=o.OrganizationForm %>
            </td>
            <td>
                <%=o.FiscalCode %>
            </td>
            <td>
                <%=o.RegDate.Display() %>
            </td>
            <td>
                <%=o.RegNr %>
            </td>
            <td>
                <%=o.StartDate.Display() %>
            </td>
            <td>
                <%=o.EndDate.Display()%>
            </td>
        </tr>
        <%
            } %>
    </tbody>
</table>
