using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class noc : System.Web.UI.Page
{
    public string sPlace_Map = "";
    public string _id = "";
    public string _User = "";

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
                            _id = Request.QueryString[item].ToString();
                            break;

                        case "_User":
                            _User = Request.QueryString[item].ToString();
                            break;

                    }
                }

                CarregaMapa(_id);
                lblNomeUsuario.Text = _User;
            }

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// CarregaMapa
    /// </summary>
    /// <param name="sIdUser"></param>
    public void CarregaMapa(string sIdUser)
    {
        try
        {

            string ssql = "";

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
                    ssql = @"select	*
                            from	BCC_GEO_GATUsuario a (nolock),
		                            BCC_GEO_GATStatusUsuario b (nolock)
                            where	a.CodStatusUser = b.CodStatusUser and a.idcodusuario = " + _id;

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    string slat = "";
                    string slog = "";
                    string sMontaMapa = "";
                    string sPrimeiraVar = "";
                    sPlace_Map = "";
                    while (dr.Read())
                    {
                        if (dr["latitude"].ToString() == "")
                        {
                            slat = "0";
                        }
                        else
                        {
                            slat = dr["latitude"].ToString();

                        }

                        if (dr["longitude"].ToString() == "")
                        {
                            slog = "0";
                        }
                        else
                        {
                            slog = dr["longitude"].ToString();
                        }


                        sPrimeiraVar = "var uluru1 = { lat: " + slat.ToString().Replace(",", ".") + ", lng: " + slog.ToString().Replace(",", ".") + " };";
                        sPlace_Map = sPlace_Map + "var uluru1 = { lat: " + slat.ToString().Replace(",", ".") + ", lng: " + slog.ToString().Replace(",", ".") + " };";

                        sPlace_Map = sPlace_Map + "var marker = new google.maps.Marker({ position: uluru1, map: map1, title: 'NOME: " + dr["nome"].ToString() + " - ENDEREÇO: " + dr["Endereco"].ToString() + " STATUS: " + dr["status"].ToString()  + "' });";//
                       
                    }

                    sMontaMapa = sMontaMapa + sPrimeiraVar;
                    sMontaMapa = sMontaMapa + " var map1 = new google.maps.Map(document.getElementById('mapa'), { zoom: 15, center: uluru1 }); ";

                    sMontaMapa = sMontaMapa + sPlace_Map;

                    sPlace_Map = sMontaMapa;


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