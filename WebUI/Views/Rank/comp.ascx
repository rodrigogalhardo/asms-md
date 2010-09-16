<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<MRGSP.ASMS.Core.Model.Competitor>>" %>
<table>
    <thead>
        <tr>
            <td>
                nume
            </td>
            <td>
                spre plata
            </td>
            <td>
                k final
            </td>
            <td>
                stare
            </td>
        </tr>
    </thead>
    <tbody>
        <%foreach (var o in Model)
          {
        %><tr>
            <td>
                <%:o.Name %>
            </td>
            <td>
                <%:o.AmountPayed %>
            </td>
            <td>
                <%:o.Value %>
            </td>
            <td>
                <%:o.StateId %>
            </td>
        </tr>
        <%
            } %>
    </tbody>
</table>
