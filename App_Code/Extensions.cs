using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using System.Text;

namespace ExtensionMethods
{
    /// <summary>
    /// Summary description for Extensions
    /// </summary>
    public static class Extensions
    {
        public static string Right(this string value, int length)
        {
            if (String.IsNullOrEmpty(value)) return string.Empty;

            return value.Length <= length ? value : value.Substring(value.Length - length);
        }
        public static string Left(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength
                   ? value
                   : value.Substring(0, maxLength)
                   );
        }

        public static string vericek(this string value, string StrBas, string StrSon)
        {
            try
            {
                int IntBas = value.IndexOf(StrBas) + StrBas.Length;
                int IntSon = value.IndexOf(StrSon, IntBas + 1);
                return value.Substring(IntBas, IntSon - IntBas);
            }
            catch
            {
                return "";
            }

        }

        public static Boolean IsNumeric(String s)
        {
            float f;

            return float.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out f);
            //float.TryParse(s, out output);
        }



        public static Boolean IsNull(String t)
        {
            bool rv;

            if (t == null)
            { rv = true; }
            else { rv = false; }

            return rv;
        }
        public static  string HTMLTableString(this DataTable dt, string id, string css)
        {

            String RVl = "";
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<table id=\"" + id + "\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\"  class=\"" + css + "\"  ><thead><tr>");
                foreach (DataColumn c in dt.Columns)
                {
                    sb.AppendFormat("<th>{0}</th>", c.ColumnName);
                }
                sb.AppendLine("</tr></thead><tbody>");

                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("<tr>"); foreach (object o in dr.ItemArray)
                    {
                        sb.AppendFormat("<td>{0}</td>", o.ToString());
                        //System.Web.HttpUtility.HtmlEncode());
                    } sb.AppendLine("</tr>");
                } sb.AppendLine("</tbody></table>");
                RVl = sb.ToString();
            }
            catch (Exception ex)
            {
                RVl = "HATA @ConvertDataTable2HTMLString: " + ex;
            }
            return RVl;
        }

    }
}