<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Oranlar.aspx.cs" Inherits="Oranlar" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(function() {

            //        $(".kuponekle").live("click", function() {
            //                alert();
            ////                var eid = this.attr("id");
            ////                alert(eid);
            //            })

            //add(''' + val.eid + ''',1,''' + val.baslik + ''',''' + val.DebeOran + ''');
       

        });
    </script>

    <table>
        <tr>
            <td style="width: 700px;">
                <div>
                    <asp:Label ID="lblBulten" runat="server" CssClass="lblBulten" Text="Label"></asp:Label>
                    <hr />
                    entry id - adres :
                    <asp:TextBox ID="txtEid" runat="server"></asp:TextBox>
                    <asp:Button ID="btnEidAl" runat="server" Text="Entry getir" OnClick="btnEidAl_Click" />
                </div>
            </td>
            <td style="width: 200px; vertical-align:top">
                <div>
                    Kuponum:<br />
                    <asp:Label ID="lblKuponum" CssClass="lblKuponum" runat="server" ></asp:Label>
                </div>
            </td>
        </tr>
    </table>
    <div style="clear: both">
    </div>
</asp:Content>
