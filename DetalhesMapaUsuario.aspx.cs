using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class DetalhesMapaUsuario : System.Web.UI.Page
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
                            Session["IdUsuarioMapa"] = Request.QueryString[item].ToString();
                            break;
                        case "_User":
                            _User = Request.QueryString[item].ToString();
                            break;
                    }
                }
                CarregaMapa(Session["IdUsuarioMapa"].ToString());
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
                    ssql = @"select	a.Nome,
		                            b.Status,
		                            a.Endereco,
		                            a.Data_Ping_Localizacao,
		                            a.Latitude,
		                            a.Longitude
                            from	TEL_MP_GATUsuario a (nolock),
		                            TEL_MP_GATStatusUsuario b (nolock)
                            where	a.CodStatusUser = b.CodStatusUser
                            and     a.idcodusuario = " + _id;

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    string slat = "";
                    string slog = "";
                    string sMontaMapa = "";
                    string sPrimeiraVar = "";
                    sPlace_Map = "";
                    string sTable = "";
                    
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
                        
                        sTable = sTable + " <table width='100%'  class='text-center' cellpadding='0' cellspacing='0'>";
                        sTable = sTable + " <tr>";

                        // NOME OPERADOR
                        sTable = sTable + " <td>";
                        sTable = sTable + " <h3 style='margin-top:10px'><b>Nome Vendedor: </b>" + dr["Nome"].ToString() + "</h3>";
                        sTable = sTable + " </td>";
                        sTable = sTable + " </tr>";
                        sTable = sTable + " <tr>";

                        // DATA
                        sTable = sTable + " <td>";
                        sTable = sTable + " <h3 style='margin-top:10px'><b> Data: </b>" + dr["Data_Ping_Localizacao"].ToString() + "</h3>";
                        sTable = sTable + " </td>";
                        sTable = sTable + " </tr>";

                        // STATUS
                        sTable = sTable + " <td>";

                        if (dr["Status"].ToString() == "Logado")
                        {
                            sTable = sTable + "<h3 style='margin-top:10px'> <span class='label label-sucess' style='color: White; background: Green;' ><asp:Label>Status: " + dr["Status"].ToString().ToUpper() + "</asp:Label ></span> </h3>";
                        }
                        else if (dr["Status"].ToString() == "Deslogado")
                        {
                            sTable = sTable + "<h3 style='margin-top:10px'> <span class='label label-default' style='color: White; background: Grey;' ><asp:Label>Status: " + dr["Status"].ToString().ToUpper() + "</asp:Label ></span></h3>";
                        }
                        else if (dr["Status"].ToString() == "Check-In")
                        {
                            sTable = sTable + "<h3 style='margin-top:10px'>  <span class='label label-primary' style='color: white; background: blue;' ><asp:Label>Status: " + dr["Status"].ToString().ToUpper() + "</asp:Label ></span></h3>";
                        }
                        else if (dr["Status"].ToString() == "Check-Out")
                        {
                            sTable = sTable + "<h3 style='margin-top:10px'>  <span class='label label-danger'><asp:Label>Status: " + dr["Status"].ToString().ToUpper() + "</asp:Label ></span></h3>";
                        }

                        sTable = sTable + " </td>";
                        sTable = sTable + " </tr>";
                        sTable = sTable + "</table>";
                        sTable = sTable + " <div class='row'>&nbsp;</div>";


                        sPrimeiraVar = "var uluru1 = { lat: " + slat.ToString().Replace(",", ".") + ", lng: " + slog.ToString().Replace(",", ".") + " };";
                        sPlace_Map = sPlace_Map + "var uluru1 = { lat: " + slat.ToString().Replace(",", ".") + ", lng: " + slog.ToString().Replace(",", ".") + " };";
                        sPlace_Map = sPlace_Map + "var marker = new google.maps.Marker({ position: uluru1, map: map1, title: 'NOME: " + dr["Nome"].ToString() + " - ENDEREÇO: " + dr["Endereco"].ToString() + " STATUS: " + dr["Status"].ToString() + "' });";//
                    }

                    sMontaMapa = sMontaMapa + sPrimeiraVar;
                    sMontaMapa = sMontaMapa + " var map1 = new google.maps.Map(document.getElementById('mapa'), { zoom: 18, center: uluru1 }); ";
                    sMontaMapa = sMontaMapa + sPlace_Map;
                    sPlace_Map = sMontaMapa;
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


    /// <summary>
    /// Timer1_Tick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        CarregaMapa(Session["IdUsuarioMapa"].ToString());
    }
}