<%@ Page Language="C#" MasterPageFile="Template.Master" Inherits="System.Web.Mvc.ViewPage<DateTime?>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Data" runat="server">
    <%= Html.TextBox("", (Model.HasValue ? Model.Value.ToShortDateString() : string.Empty))%>
        <script type="text/javascript">
            $(function () {
                $("#<%=ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty)%>").datepicker({
                    changeMonth: true,
                    changeYear: true
                });
            });
	</script>
</asp:Content>