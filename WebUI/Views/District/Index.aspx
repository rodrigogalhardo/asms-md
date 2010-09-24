﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IPageable<District>>" %>
<%@ Import Namespace="MRGSP.ASMS.WebUI.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Raioane</h2>
    <%=Html.MakePopup<DistrictController>(o => o.Create()) %>
    
    <%=Html.PopupActionLink<DistrictController>(o => o.Create(), "Creaza") %>
    
    <table>
        <thead>
            <tr>
                <td>
                    nume
                </td>
                <td>
                    abrevierea
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
                <%:o.Code %>
            </td>
        </tr>
        <%
            }%>
    </table>
    <%=Html.Pagination() %>
</asp:Content>
