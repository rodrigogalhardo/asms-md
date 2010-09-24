<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<MRGSP.ASMS.Core.Model.CalcItem>>" %>

<table>
    <thead>
        <tr>
            <td>
                Nume
            </td>
            <td>
                Valoare
            </td>
        </tr>
    </thead>
    <tbody>
        <%foreach (var o in Model)
          {%>
        <tr>
            <td>
                <%=o.Name %>
            </td>
            <td class="ent">
                <%=o.Value.Display() %>
            </td>
        </tr>
        <%} %>
    </tbody>
</table>
