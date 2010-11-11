<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Int32>" %>
<%@ Import Namespace="MRGSP.ASMS.WebUI.Controllers" %>
<%=Html.MakePopupForm<ContractController>(o => o.Edit(0), height:400) %>
<%=Html.PopupFormActionLink<ContractController>(o => o.Edit(Model), "Editeaza contract", new{@class = "abtn"}) %>
<a class='abtn' target='blank' href='<%=Url.Content("~/Repor.aspx?report=contract&id="+Model) %>'>Vizualizeaza contractul</a>