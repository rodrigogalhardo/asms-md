<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<MRGSP.ASMS.Core.Model.Capo>>" %>
<%@ Import Namespace="MRGSP.ASMS.WebUI.Controllers" %>

<table>
        <thead>
            <tr>
                <td>
                    tip
                </td>
                <td>
                    denumirea
                </td>
                <td>
                    cod fiscal
                </td>
                <td>
                    nr contract
                </td>
                <td>
                    data contract
                </td>
                <td>
                    nr acord
                </td>
                <td>
                    data acord
                </td>
                <td>
                    suma
                </td>
                <td>
                    nr ordin
                </td>
                <td>
                    data ordin
                </td>
                <td>
                    stare ordin
                </td>
                <td>
                </td>
            </tr>
        </thead>
        <tbody>
            <% foreach (var o in Model)
               {
            %>
           <% Html.RenderPartial("item", o); %>
            <%
               } %>
        </tbody>
    </table>