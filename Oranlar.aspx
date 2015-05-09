<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Oranlar.aspx.cs" Inherits="Oranlar" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="float: left">
        <div style="width: 600px;">
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </div>
        <div style="width: 200px;">
            <div style="display:block">
                Kuponum:<br />
                <asp:Label ID="lblKuponum" runat="server" Text="Boş"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
