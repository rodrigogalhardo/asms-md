<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<int>" %>
<%@ Import Namespace="MRGSP.ASMS.WebUI.Controllers" %>
<%=Html.MakePopupForm<ContractController>(o => o.Create(0), height:400) %>
<%=Html.PopupFormActionLink<ContractController>(o => o.Create(Model), "Creaza contract" ,new{@class="abtn"}) %>