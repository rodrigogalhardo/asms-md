<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<MRGSP.ASMS.Core.Model.Measure>>" %>

<table>
        <thead>
            <tr>
                <td>
                    Nume
                </td>
                <td>
                    Descriere
                </td>
            </tr>
        </thead>
        <tbody>
            <% foreach (var o in Model)
               { %>
            <tr>
                <td>
                    <%:o.Name %>
                </td>
                <td>
                    <%:o.Description %>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>