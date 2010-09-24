<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<MRGSP.ASMS.Core.Model.CoefficientValueInfo>>" %>

<% Html.RenderPartial("calcItems", Model); %>
<h2>
k final = <%=Model.Sum(o => o.Value) %>
</h2>