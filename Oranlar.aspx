<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Oranlar.aspx.cs" Inherits="Oranlar" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>


        function Debe() {

            var src = $('.lblBulten');
            src.html('<img src="imgages/loading.gif"/>');
            // $('#tbMEMO').val('Json.aspx?I=6&T=' + JT + '&M=' + b + '&TP=' + T);
            try {
                $.ajax({
                    url: 'Json.aspx',
                    dataType: 'json',
                    cache: false,
                    success: function(JSON) {
                        var W = ''
                        $.each(JSON, function(i, val) {

                            W += ('<div id="MBOX">  ' + val.baslik + '/@'
                    + val.yazar + '  ' + val.DebeOran + '</div>');

                        });

                        src.empty(); src.html(W);
                    }, error: function(xhr, ajaxOptions, thrownError) { src.append(xhr.status + thrownError); }
                });
            }
            catch (ex) {
                src.empty().prepend('HATA ()' + ex.message);

            }
            finally { }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <script type="text/javascript">
            $(function() {

                Debe();
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
            <td style="width: 200px;">
                <div>
                    Kuponum:<br />
                    <asp:Label ID="lblKuponum" runat="server" Text="Boş"></asp:Label>
                </div>
            </td>
        </tr>
    </table>
    <div style="clear: both">
    </div>
</asp:Content>
