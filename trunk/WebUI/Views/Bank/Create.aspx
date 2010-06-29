<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MRGSP.ASMS.Infra.Dto.BankCreateInput>" %>
<%@ Import Namespace="xVal.Html" %>    
    <% using (Html.BeginForm())
       {%>
        <div class="efield">
            <div class="elabel">
                <%= Html.LabelFor(model => model.Name) %>
            </div>
            <div class="einput">
                <%= Html.TextBoxFor(model => model.Name) %>
                <%= Html.ValidationMessageFor(model => model.Name) %>
            </div>
        </div>
        <div class="efield">
            <div class="elabel">
                <%= Html.LabelFor(model => model.Code) %>
            </div>
            <div class="einput">
                <%= Html.TextBoxFor(model => model.Code) %>
                <%= Html.ValidationMessageFor(model => model.Code) %>
            </div>
        </div>
        <input type="submit" class="hidden" />
    <% } %>
    
    <%=Html.ClientSideValidation<BankCreateInput>() %>