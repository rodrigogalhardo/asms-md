<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<MRGSP.ASMS.Core.Model.Phone>>" %>
<table>
    <thead>
        <tr>
            <td>
                Numar
            </td>
            <td>
                Tip
            </td>
            <td>
                Start
            </td>
            <td>
                Sfarsit
            </td>
            <td></td>
        </tr>
    </thead>
    <tbody>
        <%foreach (var o in Model)
          {
        %>
        <tr>
            <td>
                <%=o.Number %>
            </td>
            <td>
                <%=o.Type %>
            </td>
                        <td>
                <%=o.StartDate.Display() %>
            </td>
            <td>
                <%=o.EndDate.Display() %>
            </td>
            <td>
                <%if(!o.EndDate.HasValue){%>
                <form method="post" action="<%=Url.Action("Deactivate")%>">
                <input type="hidden" name="id" value="<%=o.Id%>" />
                <input type="hidden" name="farmerId" value="<%=o.FarmerId%>" />
                <input type="submit" value="X" class="confirm" />
                </form>
                <%} %>
            </td>
        </tr>
        <%
            } %>
        
    </tbody>
</table>
