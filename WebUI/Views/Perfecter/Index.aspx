<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IPageable<Perfecter>>" %>

<%@ Import Namespace="MRGSP.ASMS.WebUI.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        cine perfecteaza business planul</h2>
    <%=Html.MakePopupForm<PerfecterController>(o => o.Create(),height:200) %>
    <%=Html.MakePopupForm<PerfecterController>(o => o.Edit(0),height:200) %>
    <%=Html.Confirm("Sunteti sigur ?") %>
    <%=Html.PopupFormActionLink<PerfecterController>(o => o.Create(), "Creaza", new{@class="abtn"}) %>
    <table>
        <thead>
            <tr>
                <td style="width: 100%;">
                    nume
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </thead>
        <% foreach (var o in Model.Page)
           {%>
        <tr>
            <td>
                <%:o.Name %>
            </td>
            <%=Html.Partial("ed", o.Id) %>
        </tr>
        <%
            }%>
    </table>
    <%=Html.Pagination() %>
</asp:Content>
