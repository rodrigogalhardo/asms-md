<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<MRGSP.ASMS.Core.Model.Competitor>>" %>
<table>
    <thead>
        <tr>
            <td>
                nume
            </td>
            <td class="ent">
                spre plata
            </td>
            <td class="ent">
                k final
            </td>
            <td class="ent">
                stare
            </td>
            <td class="ent">
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
                <%:o.AmountPayed.Display() %>
            </td>
            <td>
                <%:o.Value.Display() %>
            </td>
            <td>
                <%:o.StateId %>
            </td>
            <td>
                <%=Html.ActionLink("deschide","open", "dossier", new{o.Id}, null) %>
            </td>
        </tr>
        <%
            } %>
    </tbody>
</table>
