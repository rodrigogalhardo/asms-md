<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.CaseCreateInput>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
    adauga dosar
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
<% using (Html.BeginForm())
   {%>
<div class="efield">
    <div class="elabel">
        <label>Fermier</label>
    </div>
    <div class="einput">
        <%
       Html.RenderPartial("lookup", new LookupInfo {For = "farmer", Title = "Alege fermier"});%>    
    </div>    
</div>

<div class="efield">
    <div class="elabel">
        <label>Banca</label>
    </div>
    <div class="einput">
        <%
       Html.RenderPartial("lookup", new LookupInfo {For = "bank", Title = "Alege banca"});%> 
    </div>    
</div>

<div class="efield">
    <div class="elabel">
        <label>Camp</label>
    </div>
    <div class="einput">
        <%:Html.TextBox("aazxc")%> 
    </div>    
</div>

<div class="efield">
    <div class="elabel">
        <label>Camp</label>
    </div>
    <div class="einput">
        <%:Html.TextBox("aazzxcxz")%> 
    </div>    
</div>

<div class="efield">
    <div class="elabel">
        <label>Camp</label>
    </div>
    <div class="einput">
        <%:Html.TextBox("aazzzxczxaas")%> 
    </div>    
</div>

<input type="submit" value="submit"/>
<%
   }%>
</asp:Content>
