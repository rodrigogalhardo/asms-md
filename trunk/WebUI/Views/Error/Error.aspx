<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>

<asp:Content ID="errorTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Error
</asp:Content>
<asp:Content ID="errorContent" ContentPlaceHolderID="MainContent" runat="server">

    <div style="padding: 10px;">
        <p>
            Ne cerem scuze, dar a aparut o eroare neastepta pe saitul nostru.
        </p>
       
        <p>
            Detalii despre aceasta eroare a fost inregistrata si noi stim depsre ea.
        </p>
        
        Nu este strict necesar dar in caz ca doriti sa ne mai trimiteti informatie aditionala
        despre aceasta eroare, trimite-ti un e-mail pe v.plamadeala@mrgsp.md
    </div>
</asp:Content>
