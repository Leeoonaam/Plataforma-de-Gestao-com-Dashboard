using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for gergatlink
/// </summary>
public class gergatlink
{

    public gergatlink()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    /// <summary>
    /// abre_cn
    /// </summary>
    /// <returns></returns>
    public SqlConnection abre_cn()
    {
        try
        {
            string sconn = "";

            //Local 
            sconn = @"Data Source=LEONAM\SQLEXPRESS01;Initial Catalog=stt1;Persist Security Info=True;User ID=sa;Password=12345678";

            //Producao
            //sconn = "Data Source=dbsq0007.whservidor.com;Initial Catalog=stt1;Persist Security Info=True;User ID=stt1;Password=@stt007@";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = sconn;
            cn.Open();


            return cn;
        }
        catch (Exception err)
        {
            throw;
            return null;
        }
    }

    /// <summary>
    /// horacheia
    /// </summary>
    /// <param name="segundos"></param>
    /// <returns></returns>
    public string horacheia(int segundos)
    {
        try
        {


            int hor = (int)(segundos / (60 * 60));

            int min = (int)((segundos - (hor * 60 * 60)) / 60);

            int seg = (int)(segundos - (hor * 60 * 60) - (min * 60));


            string sret = hor.ToString("00") + ":" + min.ToString("00") + ":" + seg.ToString("00");

            return sret;

        }
        catch (Exception)
        {

            throw;
            return "";
        }
    }

    /// <summary>
    /// TrataHTMLASCII
    /// </summary>
    /// <param name="stexto"></param>
    /// <returns></returns>
    public string TrataHTMLASCII(string stexto)
    {
        try
        {
            string st = stexto;

            st = st.Replace("&#33;", "!");
            st = st.Replace("&#35;", "#");
            st = st.Replace("&#36;", "$");
            st = st.Replace("&#37;", "%");
            st = st.Replace("&#38;", "&");
            st = st.Replace("&#39;", "'");
            st = st.Replace("&#40;", "(");
            st = st.Replace("&#41;", ")");
            st = st.Replace("&#42;", "*");
            st = st.Replace("&#43;", "+");
            st = st.Replace("&#44;", ",");
            st = st.Replace("&#45;", "-");
            st = st.Replace("&#46;", ".");
            st = st.Replace("&#47;", "/");
            st = st.Replace("&#48;", "0");
            st = st.Replace("&#49;", "1");
            st = st.Replace("&#50;", "2");
            st = st.Replace("&#51;", "3");
            st = st.Replace("&#52;", "4");
            st = st.Replace("&#53;", "5");
            st = st.Replace("&#54;", "6");
            st = st.Replace("&#55;", "7");
            st = st.Replace("&#56;", "8");
            st = st.Replace("&#57;", "9");
            st = st.Replace("&#58;", ":");
            st = st.Replace("&#59;", ";");
            st = st.Replace("&#60;", "<");
            st = st.Replace("&#61;", "=");
            st = st.Replace("&#62;", ">");
            st = st.Replace("&#63;", "?");
            st = st.Replace("&#64;", "@");
            st = st.Replace("&#65;", "A");
            st = st.Replace("&#66;", "B");
            st = st.Replace("&#67;", "C");
            st = st.Replace("&#68;", "D");
            st = st.Replace("&#69;", "E");
            st = st.Replace("&#70;", "F");
            st = st.Replace("&#71;", "G");
            st = st.Replace("&#72;", "H");
            st = st.Replace("&#73;", "I");
            st = st.Replace("&#74;", "J");
            st = st.Replace("&#75;", "K");
            st = st.Replace("&#76;", "L");
            st = st.Replace("&#77;", "M");
            st = st.Replace("&#78;", "N");
            st = st.Replace("&#79;", "O");
            st = st.Replace("&#80;", "P");
            st = st.Replace("&#81;", "Q");
            st = st.Replace("&#82;", "R");
            st = st.Replace("&#83;", "S");
            st = st.Replace("&#84;", "T");
            st = st.Replace("&#85;", "U");
            st = st.Replace("&#86;", "V");
            st = st.Replace("&#87;", "W");
            st = st.Replace("&#88;", "X");
            st = st.Replace("&#89;", "Y");
            st = st.Replace("&#90;", "Z");
            st = st.Replace("&#91;", "[");
            st = st.Replace("&#93;", "]");
            st = st.Replace("&#94;", "^");
            st = st.Replace("&#95;", "_");
            st = st.Replace("&#96;", "`");
            st = st.Replace("&#97;", "a");
            st = st.Replace("&#98;", "b");
            st = st.Replace("&#99;", "c");
            st = st.Replace("&#100;", "d");
            st = st.Replace("&#101;", "e");
            st = st.Replace("&#102;", "f");
            st = st.Replace("&#103;", "g");
            st = st.Replace("&#104;", "h");
            st = st.Replace("&#105;", "i");
            st = st.Replace("&#106;", "j");
            st = st.Replace("&#107;", "k");
            st = st.Replace("&#108;", "l");
            st = st.Replace("&#109;", "m");
            st = st.Replace("&#110;", "n");
            st = st.Replace("&#111;", "o");
            st = st.Replace("&#112;", "p");
            st = st.Replace("&#113;", "q");
            st = st.Replace("&#114;", "r");
            st = st.Replace("&#115;", "s");
            st = st.Replace("&#116;", "t");
            st = st.Replace("&#117;", "u");
            st = st.Replace("&#118;", "v");
            st = st.Replace("&#119;", "w");
            st = st.Replace("&#120;", "x");
            st = st.Replace("&#121;", "y");
            st = st.Replace("&#122;", "z");
            st = st.Replace("&#123;", "{");
            st = st.Replace("&#124;", "|");
            st = st.Replace("&#125;", "}");
            st = st.Replace("&#126;", "~");
            st = st.Replace("&#161;", "¡");
            st = st.Replace("&#162;", "¢");
            st = st.Replace("&#163;", "£");
            st = st.Replace("&#164;", "¤");
            st = st.Replace("&#165;", "¥");
            st = st.Replace("&#166;", "¦");
            st = st.Replace("&#167;", "§");
            st = st.Replace("&#168;", "¨");
            st = st.Replace("&#169;", "©");
            st = st.Replace("&#170;", "ª");
            st = st.Replace("&#171;", "«");
            st = st.Replace("&#172;", "¬");
            st = st.Replace("&#173;", "");
            st = st.Replace("&#174;", "®");
            st = st.Replace("&#175;", "¯");
            st = st.Replace("&#176;", "°");
            st = st.Replace("&#177;", "±");
            st = st.Replace("&#178;", "²");
            st = st.Replace("&#179;", "³");
            st = st.Replace("&#180;", "´");
            st = st.Replace("&#181;", "µ");
            st = st.Replace("&#182;", "¶");
            st = st.Replace("&#183;", "·");
            st = st.Replace("&#184;", "¸");
            st = st.Replace("&#185;", "¹");
            st = st.Replace("&#186;", "º");
            st = st.Replace("&#187;", "»");
            st = st.Replace("&#188;", "¼");
            st = st.Replace("&#189;", "½");
            st = st.Replace("&#190;", "¾");
            st = st.Replace("&#191;", "¿");
            st = st.Replace("&#192;", "À");
            st = st.Replace("&#193;", "Á");
            st = st.Replace("&#194;", "Â");
            st = st.Replace("&#195;", "Ã");
            st = st.Replace("&#196;", "Ä");
            st = st.Replace("&#197;", "Å");
            st = st.Replace("&#198;", "Æ");
            st = st.Replace("&#199;", "Ç");
            st = st.Replace("&#200;", "È");
            st = st.Replace("&#201;", "É");
            st = st.Replace("&#202;", "Ê");
            st = st.Replace("&#203;", "Ë");
            st = st.Replace("&#204;", "Ì");
            st = st.Replace("&#205;", "Í");
            st = st.Replace("&#206;", "Î");
            st = st.Replace("&#207;", "Ï");
            st = st.Replace("&#208;", "Ð");
            st = st.Replace("&#209;", "Ñ");
            st = st.Replace("&#210;", "Ò");
            st = st.Replace("&#211;", "Ó");
            st = st.Replace("&#212;", "Ô");
            st = st.Replace("&#213;", "Õ");
            st = st.Replace("&#214;", "Ö");
            st = st.Replace("&#215;", "×");
            st = st.Replace("&#216;", "Ø");
            st = st.Replace("&#217;", "Ù");
            st = st.Replace("&#218;", "Ú");
            st = st.Replace("&#219;", "Û");
            st = st.Replace("&#220;", "Ü");
            st = st.Replace("&#221;", "Ý");
            st = st.Replace("&#222;", "Þ");
            st = st.Replace("&#223;", "ß");
            st = st.Replace("&#224;", "à");
            st = st.Replace("&#225;", "á");
            st = st.Replace("&#226;", "â");
            st = st.Replace("&#227;", "ã");
            st = st.Replace("&#228;", "ä");
            st = st.Replace("&#229;", "å");
            st = st.Replace("&#230;", "æ");
            st = st.Replace("&#231;", "ç");
            st = st.Replace("&#232;", "è");
            st = st.Replace("&#233;", "é");
            st = st.Replace("&#234;", "ê");
            st = st.Replace("&#235;", "ë");
            st = st.Replace("&#236;", "ì");
            st = st.Replace("&#237;", "í");
            st = st.Replace("&#238;", "î");
            st = st.Replace("&#239;", "ï");
            st = st.Replace("&#240;", "ð");
            st = st.Replace("&#241;", "ñ");
            st = st.Replace("&#242;", "ò");
            st = st.Replace("&#243;", "ó");
            st = st.Replace("&#244;", "ô");
            st = st.Replace("&#245;", "õ");
            st = st.Replace("&#246;", "ö");
            st = st.Replace("&#247;", "÷");
            st = st.Replace("&#248;", "ø");
            st = st.Replace("&#249;", "ù");
            st = st.Replace("&#250;", "ú");
            st = st.Replace("&#251;", "û");
            st = st.Replace("&#252;", "ü");
            st = st.Replace("&#253;", "ý");
            st = st.Replace("&#254;", "þ");
            st = st.Replace("&#255;", "ÿ");
            st = st.Replace("&#256;", "Ā");

            return st;

        }
        catch (Exception)
        {
            throw;
            return "";
        }

    }



}