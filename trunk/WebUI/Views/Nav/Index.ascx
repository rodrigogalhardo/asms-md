<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<ul id="menu">
    <li>
        <%= Html.ActionLink("Acasa", "Index", "Home")%></li>
    <li>
        <%= Html.ActionLink("Despre", "About", "Home")%></li>
        <li>
        <%= Html.ActionLink("Utilizatori", "Index", "User")%></li>
        
        <li><%= Html.ActionLink("Banci", "Index", "Bank")%></li>
        <li><%=Html.ActionLink("Adauga Dosar","Create", "Case") %></li>
</ul>
