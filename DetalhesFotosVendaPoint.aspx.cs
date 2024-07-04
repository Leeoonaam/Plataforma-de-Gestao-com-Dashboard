using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class DetalhesFotosVendaPoint : System.Web.UI.Page
{
    public string _id = "";
    public string _User = "";
    string _sdtini = "";
    string _sdtfim = "";

    /// <summary>
    /// Erro
    /// </summary>
    /// <param name="smsg"></param>
    void Erro(string smsg)
    {
        Session["tipo_alerta"] = "error";
        Session["titulo_alerta"] = "Erro";
        Session["mensagem_alerta"] = smsg;
    }


    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (IsPostBack == false)
            {
                //seleciona as variaveis
                foreach (string item in Request.QueryString)
                {
                    //valores
                    switch (item)
                    {
                        case "_id":
                            Session["IdUsuario"] = Request.QueryString[item].ToString();
                            break;
                        case "_User":
                            _User = Request.QueryString[item].ToString();
                            break;
                        case "_sdtini":
                            _sdtini = Request.QueryString[item].ToString();
                            break;
                        case "_sdtfim":
                            _sdtfim = Request.QueryString[item].ToString();
                            break;
                    }
                }
                CarregaTela(Session["IdUsuario"].ToString());
            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// CarregaTela
    /// </summary>
    /// <param name="sIdUser"></param>
    public void CarregaTela(string sIdUser)
    {
        try
        {
            string ssql = "";

            //seleciona as variaveis
            foreach (string item in Request.QueryString)
            {
                //valores
                switch (item)
                {
                    case "_id":
                        _id = Request.QueryString[item].ToString();
                        break;
                    case "_User":
                        _User = Request.QueryString[item].ToString();
                        break;
                    case "_sdtini":
                        _sdtini = Request.QueryString[item].ToString();
                        break;
                    case "_sdtfim":
                        _sdtfim = Request.QueryString[item].ToString();
                        break;

                }
            }

            gergatlink ogat = new gergatlink();

            //Dados
            using (SqlConnection cn = ogat.abre_cn())
            {
                //US LANG
                using (SqlCommand cmd_Lang = new SqlCommand())
                {
                    cmd_Lang.Connection = cn;
                    cmd_Lang.CommandText = "SET LANGUAGE us_english;";
                    cmd_Lang.CommandType = CommandType.Text;

                    cmd_Lang.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand())
                {
                    //monta select
                    ssql = @"select	 
		                            b.Nome,
		                            c.TipoVenda,
		                            a.DataInsercao,
		                            a.Foto_Point,
                                    a.Foto_Point2,
                                    a.Foto_Point3

                            from	TEL_MP_GATVenda a(nolock),
		                            TEL_MP_GATUsuario b(nolock),
		                            TEL_MP_GATTipoVenda c(nolock)

                            where	a.IdCodUsuario = b.IDCodUsuario
                            and		a.IdTipoVenda= c.IdTipoVenda
                            
                            and		a.idcodvenda = " + _id;

                    //ssql = ssql + "and a.DataInsercao between '" + _sdtini + "' and '" + _sdtfim + "'";

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    string sTable = "";

                    while (dr.Read())
                    {
                        byte[] bytes = (byte[])dr["Foto_Point"];
                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                        //verifica se contem valor
                        if (base64String != "AA==")
                        {
                            sTable = sTable + " <br /><table width='100%' class='text-center' cellpadding='0' cellspacing='0'>";
                            sTable = sTable + " <tr>";

                            // NOME OPERADOR
                            sTable = sTable + " <td>";
                            sTable = sTable + " <h3 style='margin-top:10px'><b>Nome Vendedor: </b>" + dr["Nome"].ToString() + "</h3>";
                            sTable = sTable + " </td>";
                            sTable = sTable + " </tr>";

                            // DATA
                            sTable = sTable + " <tr><td>";
                            sTable = sTable + " <h3 style='margin-top:0px'><b> Data: </b>" + dr["DataInsercao"].ToString() + "</h3>";
                            sTable = sTable + " </td></tr>";

                            sTable = sTable + "</table>";
                            sTable = sTable + " <div class='row'>&nbsp;</div>";

                            Image1.ImageUrl = "data:image/png;base64," + base64String;

                            byte[] bytes2 = (byte[])dr["Foto_Point2"];
                            string base64String2 = Convert.ToBase64String(bytes2, 0, bytes2.Length);

                            Image2.ImageUrl = "data:image/png;base64," + base64String2;

                            byte[] bytes3 = (byte[])dr["Foto_Point3"];
                            string base64String3 = Convert.ToBase64String(bytes3, 0, bytes3.Length);

                            Image3.ImageUrl = "data:image/png;base64," + base64String3;
                        }
                    }

                    ltrHistorico.Text = sTable;
                    dr.Close();
                }
            }
        }
        catch (Exception err)
        {
            Erro("Erro" + err.Message);
        }
    }
}