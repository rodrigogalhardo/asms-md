<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<MRGSP.ASMS.Core.Model.LandOwnerInfo>>" %>
<%= Html.ActionLink("Editeaza","EditLandOwner",new{Model.First().FarmerId}, new{@class="abtn"}) %>
<table>
    <thead>
        <tr>
            <td>
                Nume
            </td>
            <td>
                Prenume
            </td>
            <td>
                Patronimic
            </td>
            <td>
                Cod fiscal
            </td>
            <td>
                Date nasterii
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
        <% foreach (var o in Model)
           {
        %><tr>
            <td>
                <%=o.FirstName %>
            </td>
            <td>
                <%=o.LastName %>
            </td>
            <td>
                <%=o.FathersName %>
            </td>
            <td>
                <%=o.FiscalCode %>
            </td>
            <td>
                <%=o.DateOfBirth.Display() %>
            </td>
            <td>
                <%=o.StartDate.Display() %>
            </td>
            <td>
                <%=o.EndDate.Display() %>
            </td>
        </tr>
        <%
            } %>
    </tbody>
</table>
