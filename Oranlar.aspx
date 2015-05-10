<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Oranlar.aspx.cs" Inherits="Oranlar" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table><tr><td style="width: 700px;">
          <div>  <asp:Label ID="lblBulten" runat="server" Text="Label"></asp:Label>
            <asp:TextBox ID="txtEid" runat="server"></asp:TextBox>
            <asp:Button ID="btnEidAl"
                runat="server" Text="Entry getir" onclick="btnEidAl_Click" />
        </div></td><td style="width: 200px;">
        
        <div>
            Kuponum:<br />
            <asp:Label ID="lblKuponum" runat="server" Text="Boş"></asp:Label>
        </div>
  </td></tr></table>
    <div style="clear: both">
    </div>
</asp:Content>
