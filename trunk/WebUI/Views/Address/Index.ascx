<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<AddressInfo>>"
 %>

   <table>
        <thead>
            <tr>
                <td>
                    Raion
                </td>
                <td>
                    Localitate
                </td>
                <td>
                    Strada
                </td>
                <td>
                    Block
                </td>
                <td>
                    Apartament
                </td>
                <td>
                    Cod postal
                </td>
                            <td>
                Start
            </td>
            <td>
                Sfarsit
            </td>
            <td>
            </td>
            </tr>
        </thead>
        <tbody>
            <%foreach (var o in Model)
              {
            %>
            <tr>
                <td>
                    <%=o.District %>
                </td>
                <td>
                    <%=o.Locality %>
                </td>
                <td>
                    <%=o.Street %>
                </td>
                <td>
                    <%=o.House %>
                </td>
                <td>
                    <%=o.Apartment %>
                </td>
                <td>
                    <%=o.Zip %>
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

