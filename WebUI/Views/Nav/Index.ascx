<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<ul id="menu">
    <li>
        <%= Html.ActionLink("Acasa", "Index", "Home")%></li>
    <li>
        <%= Html.ActionLink("Dosare", "Index", "Dossier")%></li>
    <li>
        <%=Html.ActionLink("Adauga Dosar","Create", "Dossier") %></li>
    <li>
        <%=Html.ActionLink("Administrare Subventii", "Index", "Admin") %></li>
    <li>
        <%=Html.ActionLink("Registrul Fermierilor", "Index", "FarmerRegister") %></li>
    <li>
        <%=Html.ActionLink("Securitate", "Index", "User") %></li>
</ul>
