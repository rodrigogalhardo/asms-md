<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IPageable<FieldsetDisplay>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Seturi de campuri</h2>
    <%:Html.ActionLink("Creaza", "Create") %>
    <table>
        <thead>
            <tr>
                <td>
                    Nume
                </td>
                <td>
                    Data sfarsit
                </td>
                <td>
                    Stare
                </td>
            </tr>
        </thead>
        <%foreach (var o in Model.Page)
          {%>
        <tr>
            <td>
                <%:o.Name %>
            </td>
            <td>
                <%:o.EndDate.ToShortDateString() %>
            </td>
            <td>
                <%:o.State %>
            </td>
            <td>
                <%:Html.ActionLink("deschide","open",new{id = o.Id}) %>
            </td>
        </tr>
        <%} %>
    </table>
    <%=Html.Pagination() %>
</asp:Content>
