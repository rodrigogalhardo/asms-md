<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>

<asp:Content ID="errorTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Error
</asp:Content>
<asp:Content ID="errorContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Oops! Ceva rau s-a intamplat !
    </h2>
    <hr />
    <img width="500" src="<%= Url.Content("~/Content/error-lolcat-problemz.jpg")%>" class="fl"
        style="margin: 10px;" />
    <div style="padding: 10px;">
        <p>
            Ne cerem scuze, dar a aparut o eroare neastepta pe saitul nostru.
        </p>
        <p>
            <b>Este vina noastra.</b></p>
        <p>
            Detalii despre aceasta eroare a fost inregistrata si noi stim depsre ea.
        </p>
        <p>
            Da, noi ne uitam la fiecare eroare. Chiar incercam si sa fixam cate una.
        </p>
        Nu este strict necesar dar in caz ca doriti sa ne mai trimiteti informatie aditionala
        despre aceasta eroare, trimite-ti un e-mail pe v.plamadeala@mrgsp.md
    </div>
</asp:Content>
