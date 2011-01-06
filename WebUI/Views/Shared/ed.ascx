<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<int>" %>
<td>
    <%=Html.PopupFormActionLink("Edit","Editeaza", new object[]{Model}) %>
</td>
<td>
    <form method="post" action="<%=Url.Action("Delete") %>" class="fconfirm" >
    <input type="hidden" name="id" value="<%=Model %>" />
    <input type="submit" value="X" class="confirm" />
    </form>
</td>
