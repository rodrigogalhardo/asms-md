<%@ Page Language="C#" MasterPageFile="Template.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="MRGSP.ASMS.WebUI.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Data" runat="server">
    <%= Html.Lookup(ViewData.TemplateInfo.GetFullHtmlFieldName(""), value:ViewData.TemplateInfo.FormattedModelValue) %>
</asp:Content>