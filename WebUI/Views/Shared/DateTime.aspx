<%@ Page Title="" Language="C#" MasterPageFile="Field.Master" Inherits="System.Web.Mvc.ViewPage<PropertyViewModel<DateTime>>" %>

<%@ Import Namespace="MvcContrib.UI.InputBuilder.Views" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Label" runat="server">
    <label for="<%=Model.Name%>">
        <%=Model.Label%></label></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Input" runat="server">
    <%
        var val = Model.Value == default(DateTime) ? string.Empty : Model.Value.Date.ToShortDateString(); %>

    <%=Html.TextBox(Model.Name, val, new {@id=Model.Name}) %>
    <script type="text/javascript">
        $(function () {
            $("#<%=Model.Name%>").datepicker();
        });
	</script>
</asp:Content>
