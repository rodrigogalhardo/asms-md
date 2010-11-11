<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IPageable<Field>>" %>
<%@ Import Namespace="MRGSP.ASMS.WebUI.Controllers" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Campuri</h2>
        <%=Html.MakePopupForm<FieldController>(o => o.Create(), height:250) %>
        <%=Html.PopupFormActionLink<FieldController>(o => o.Create(), "Creaza") %>
    <table>
        <thead>
            <tr>
                <td>
                    nume
                </td>
                <td>
                    descriere
                </td>
            </tr>
        </thead>
        <% foreach (var o in Model.Page)
           {%>
        <tr>
            <td>
                <%:o.Name %>
            </td>
            <td>
                <%:o.Description %>
            </td>
        </tr>
        <%
            }%>
    </table>
    <%=Html.Pagination() %>
</asp:Content>
