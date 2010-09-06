<%@ Page Language="C#" MasterPageFile="Template.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Data" runat="server">
<% %>
    <%= Html.TextBox("", Model) %>
        <script type="text/javascript">
            $(function () {
                $("#<%=ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty)%>").datepicker();
            });
	</script>
</asp:Content>