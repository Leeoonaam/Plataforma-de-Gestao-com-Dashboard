using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class dashboard : System.Web.UI.Page
{
    //variaveis
    public string _sperfil = "";
    public string _sequipe = "";
    public string _susuario = "";
    public string _sdtini = "";
    public string _sdtfim = "";
    public string _sdtini_min = "";
    public string _sdtfim_min = "";

    public int sTotVendedores = 0;
    public string sPlace_Map = "";

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
    /// Carrega_Lista_Filtro_Perfil
    /// </summary>
    /// <param name="svalor"></param>
    public void Carrega_Lista_Filtro_Perfil(string svalor)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sdml = "";

                    sdml = "select  * from TEL_MP_GATPerfil (nolock) where idpermissao = 7 and  habilitado = 1 ";
                    if (svalor != "") sdml = sdml + " and perfil like '%" + svalor + "%'";
                    sdml = sdml + " order by perfil";

                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbperfil_filtro.Items.Clear();
                    while (dr.Read())
                    {
                        oitem = new ListItem();
                        oitem.Text = dr["perfil"].ToString();
                        oitem.Value = dr["idcodperfil"].ToString();

                        cmbperfil_filtro.Items.Add(oitem);

                    }
                    dr.Close();

                }

            }

        }
        catch (Exception err)
        {
            Erro("ista filtro Perfil:" + err.Message);
        }
    }

    /// <summary>
    /// Carrega_Lista_Filtro_Equipe
    /// </summary>
    /// <param name="svalor"></param>
    public void Carrega_Lista_Filtro_Equipe(string svalor)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sdml = "";

                    sdml = "select * from TEL_MP_GATPerfil (nolock) where idpermissao = 6 and habilitado = 1 ";
                    if (svalor != "") sdml = sdml + " and perfil like '%" + svalor + "%'";
                    sdml = sdml + " order by perfil";

                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbequipe_filtro.Items.Clear();
                    while (dr.Read())
                    {
                        oitem = new ListItem();
                        oitem.Text = dr["perfil"].ToString();
                        oitem.Value = dr["idcodperfil"].ToString();

                        cmbequipe_filtro.Items.Add(oitem);

                    }
                    dr.Close();

                }

            }

        }
        catch (Exception err)
        {
            Erro("Lista Filtro Equipe:" + err.Message);
        }
    }

    /// <summary>
    /// Carrega_Lista_Filtro_Usuarios
    /// </summary>
    /// <param name="svalor"></param>
    public void Carrega_Lista_Filtro_Usuarios(string svalor)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sdml = "";

                    sdml = "select idcodusuario,nome from TEL_MP_GATUsuario (nolock) where habilitado = 1 ";
                    if (svalor != "") sdml = sdml + " and nome like '%" + svalor + "%'";
                    sdml = sdml + " order by nome";

                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbusuarios_filtro.Items.Clear();
                    while (dr.Read())
                    {
                        oitem = new ListItem();
                        oitem.Text = dr["nome"].ToString();
                        oitem.Value = dr["idcodusuario"].ToString();

                        cmbusuarios_filtro.Items.Add(oitem);

                    }
                    dr.Close();

                }

            }

        }
        catch (Exception err)
        {
            Erro("Lista Filtro Usuario:" + err.Message);
        }
    }

    #region BLOCOS PRINCIPAIS
    /// <summary>
    /// Rel_UsuarioMapa
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_UsuarioMapa(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";
            string shtml = "";
            int ireg = 0;


            string slat = "";
            string slog = "";

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
                    ssql = "select * from TEL_MP_GATUsuario a (nolock), TEL_MP_GATStatusUsuario b (nolock) where a.CodStatusUser = b.CodStatusUser and Habilitado = 1";

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and a.idcodusuario in (" + susuario + ")";

                    }

                    //Perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and a.idcodusuario in (";
                        ssql = ssql + " select  idcodusuario ";
                        ssql = ssql + " from    TEL_MP_GATUsuarioPerfil(nolock)";
                        ssql = ssql + " where   idcodperfil in (" + sperfil + ")";
                        ssql = ssql + " )";
                    }

                    //Equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and a.idcodusuario in (";
                        ssql = ssql + " select  idcodusuario ";
                        ssql = ssql + " from    TEL_MP_GATUsuarioPerfil(nolock)";
                        ssql = ssql + " where   idcodperfil in (" + sequipe + ")";
                        ssql = ssql + " )";
                    }

                    ssql = ssql + " and a.DataUltStatus between '" + dtini + "' and '" + dtfim + "'";

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    shtml = "";
                    sPlace_Map = "";
                    string sTitles = "";
                    string sNomesVendedores = "";
                    string svirgula = "";
                    int imap = 2;

                    sPlace_Map = sPlace_Map + "var locations = [";

                    while (dr.Read())
                    {

                        if (svirgula != "")
                        {
                            sPlace_Map = sPlace_Map + svirgula;
                            sNomesVendedores = sNomesVendedores + svirgula;
                            sTitles = sTitles + svirgula;
                        }

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

                        //sPlace_Map = sPlace_Map + "var uluru" + imap.ToString() + " = { lat: " + slat.ToString().Replace(",", ".") + ", lng: " + slog.ToString().Replace(",", ".") + " };";
                        //sPlace_Map = sPlace_Map + "var marker = new google.maps.Marker({ position: uluru" + imap.ToString() + ", map: map, title: 'NOME: " + dr["Nome"].ToString() + " - STATUS: " + dr["status"].ToString() + " - LATITUDE: " + slat.ToString() + " - LONGITUDE: " + slog.ToString() + " - ENDEREÇO: " + dr["Endereco"].ToString() + "' });";

                        sPlace_Map = sPlace_Map + "{lat: " + slat.ToString().Replace(",", ".") + ", lng: " + slog.ToString().Replace(",", ".") + "}";

                        sNomesVendedores = sNomesVendedores + "'" + dr["Nome"].ToString() + "'";

                        sTitles = sTitles + "'" + "NOME: " + dr["Nome"].ToString() + " - STATUS: " + dr["status"].ToString() + " - LATITUDE: " + slat.ToString() + " - LONGITUDE: " + slog.ToString() + " - ENDEREÇO: " + dr["Endereco"].ToString() + "'";

                        svirgula = ",";

                        imap++;

                    }

                    sPlace_Map = sPlace_Map + "];";

                    sPlace_Map = sPlace_Map + " var labls = [";

                    sPlace_Map = sPlace_Map + sNomesVendedores;

                    sPlace_Map = sPlace_Map + "];";

                    sPlace_Map = sPlace_Map + " var titles = [";

                    sPlace_Map = sPlace_Map + sTitles;

                    sPlace_Map = sPlace_Map + "];";

                    dr.Close();


                }
            }

        }
        catch (Exception err)
        {
            Erro("Rel Usuario Mapa:" + err.Message);
        }
    }

    /// <summary>
    /// Rel_totvendedores
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    public void Rel_totvendedores(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    //monta select
                    ssql = @"SELECT COUNT(1)FROM TEL_MP_GATUsuario(NOLOCK)
                            where Habilitado =1
                            and IDCodUsuario IN (SELECT IDCodUsuario from TEL_MP_GATUsuarioPerfil (nolock)
						                            where 1=1";


                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and IDCodPerfil In (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and IDCodPerfil In (" + sequipe + ")";
                    }

                    ssql = ssql + " )";

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and idcodusuario in (" + susuario + ")";
                    }

                    //data
                    //if (chkData_Filtro.Checked == true)
                    //{
                    //ssql = ssql + " and DATAULTSTATUS between '" + dtini + "' and '" + dtfim + "'";
                    //}

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    string sv = "";

                    if (dr.Read())
                    {
                        sv = dr[0].ToString();
                        sTotVendedores = Convert.ToInt32(dr[0].ToString());
                        lblQuantidadeVendedores.Text = "Total de Vendedores: " + dr[0].ToString();
                    }
                    dr.Close();

                }
            }
        }
        catch (Exception err)
        {
            Erro("Vendedores total:" + err.Message);
        }
    }

    /// <summary>
    /// Rel_totvendedores_logados
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_totvendedores_logados(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    //monta select
                    ssql = @"SELECT COUNT(1)FROM TEL_MP_GATUsuario(NOLOCK)
                            where Habilitado =1
                            and CodStatusUser = 1
                            and IDCodUsuario IN (SELECT IDCodUsuario from TEL_MP_GATUsuarioPerfil (nolock)
						                            where 1=1";

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and IDCodPerfil In (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and IDCodPerfil In (" + sequipe + ")";
                    }

                    ssql = ssql + " )";

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and idcodusuario in (" + susuario + ")";
                    }


                    ssql = ssql + " and Data_Ping_Localizacao between '" + dtini + "' and '" + dtfim + "'";


                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    string sv = "";

                    if (dr.Read())
                    {
                        sv = dr[0].ToString();

                        lblvendedoreslogados.Text = dr[0].ToString();
                    }
                    dr.Close();

                }
            }
        }
        catch (Exception err)
        {
            Erro("Vendedores Logados:" + err.Message);
        }
    }

    /// <summary>
    /// Rel_totvendedores_checkin
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_totvendedores_checkin(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    //monta select
                    ssql = @"SELECT COUNT(1)FROM TEL_MP_GATUsuario(NOLOCK)
                            where Habilitado =1
                            and CodStatusUser = 3
                            and IDCodUsuario IN (SELECT IDCodUsuario from TEL_MP_GATUsuarioPerfil (nolock)
						                            where 1=1";


                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and IDCodPerfil In (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and IDCodPerfil In (" + sequipe + ")";
                    }

                    ssql = ssql + " )";

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and idcodusuario in (" + susuario + ")";
                    }

                    ssql = ssql + " and Data_Ping_Localizacao between '" + dtini + "' and '" + dtfim + "'";

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    string sv = "";

                    if (dr.Read())
                    {
                        sv = dr[0].ToString();

                        lblcheckin.Text = dr[0].ToString();
                    }
                    dr.Close();

                }
            }
        }
        catch (Exception err)
        {
            Erro("Vendedores Checkin:" + err.Message);
        }
    }

    /// <summary>
    /// Rel_totvendedores_checkin
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_totvendedores_checkout(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    //monta select
                    ssql = @"SELECT COUNT(1)FROM TEL_MP_GATUsuario(NOLOCK)
                            where Habilitado =1
                            and CodStatusUser = 4
                            and IDCodUsuario IN (SELECT IDCodUsuario from TEL_MP_GATUsuarioPerfil (nolock)
						                            where 1=1";


                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and IDCodPerfil In (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and IDCodPerfil In (" + sequipe + ")";
                    }

                    ssql = ssql + " )";

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and idcodusuario in (" + susuario + ")";
                    }


                    ssql = ssql + " and Data_Ping_Localizacao between '" + dtini + "' and '" + dtfim + "'";


                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    string sv = "";

                    if (dr.Read())
                    {
                        sv = dr[0].ToString();

                        lblcheckout.Text = dr[0].ToString();
                    }
                    dr.Close();

                }
            }
        }
        catch (Exception err)
        {
            Erro("Vendedores checkout:" + err.Message);
        }
    }


    /// <summary>
    /// Rel_totvendedores_logout
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_totvendedores_logout(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    ssql = @"SELECT COUNT(1)FROM TEL_MP_GATUsuario(NOLOCK)
                            where Habilitado =1
                            and CodStatusUser = 2
                            and IDCodUsuario IN (SELECT IDCodUsuario from TEL_MP_GATUsuarioPerfil (nolock)
						                            where 1=1";

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and IDCodPerfil In (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and IDCodPerfil In (" + sequipe + ")";
                    }

                    ssql = ssql + " )";

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and idcodusuario in (" + susuario + ")";
                    }

                    //data
                    ssql = ssql + " and Data_Ping_Localizacao between '" + dtini + "' and '" + dtfim + "'";


                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    string sv = "";

                    if (dr.Read())
                    {
                        sv = dr[0].ToString();

                        lblQtdLogout.Text = dr[0].ToString();
                    }
                    dr.Close();

                }
            }
        }
        catch (Exception err)
        {
            Erro("Vendedores Logout:" + err.Message);
        }
    }
    #endregion

    #region GRAFICOS
    /// <summary>
    /// Grafico_HoraHora
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Grafico_HoraHora(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {

                string sdml = @"select   y.HORA,
                                                              Y.TOTAL
                from 
                (
                               select   x.HORA,
                                                            (
                                                                           SELECT COUNT(1) 'TOTAL'
                                FROM TEL_MP_GATTempoUsuario B (NOLOCK)
                                WHERE DATEPART(HOUR, B.DataTempo) = x.hora";


                //usuario
                if (susuario != "")
                {
                    sdml = sdml + " AND b.idcodusuario in (" + susuario + ")";
                }

                //data
                sdml = sdml + " and b.DataTempo between '" + dtini + "' and '" + dtfim + "'";



                sdml = sdml + @"   ) 'TOTAL'
                                                            
                               from
                               (
                                              select   DATEPART(HOUR, a.DataTempo ) 'HORA'
                        from           TEL_MP_GATTempoUsuario a
                                              where  1 = 1 ";


                //usuario
                if (sperfil != "")
                {
                    sdml = sdml + " AND a.idcodperfil in (" + sperfil + ") ";
                }

                //usuario
                if (sequipe != "")
                {
                    sdml = sdml + " AND a.idcodequipe in (" + sequipe + ") ";
                }

                //usuario
                if (susuario != "")
                {
                    sdml = sdml + " AND a.idcodusuario in (" + susuario + ") ";
                }

                //data
                sdml = sdml + " and a.DataTempo between '" + dtini + "' and '" + dtfim + "'";


                sdml = sdml + @"    ) x
                               group by x.hora 
                ) y 
                     order by y.hora

                ";

                using (SqlDataAdapter objadap = new SqlDataAdapter(sdml, cn))
                {

                    using (DataSet ds = new DataSet())
                    {
                        objadap.Fill(ds);

                        string svig = "";
                        string sresul = "";

                        Literal1.Text = Literal1.Text + @"  
                            var chart = new CanvasJS.Chart('div_graf_hora',
                                   {
                                                  title:{
                                                                text: 'Usuários Logados por Hora'
                                                  },
animationEnabled: true,

                                                  legend: {
                                                                maxWidth: 350,
                                                                itemWidth: 120
                                                  },
                                                  data: [
                                                  {
                                                        
                                                            type: 'line',
                                                            dataPoints: [";

                        //carrega dados
                        for (int ihora = 5; ihora < 23; ihora++)
                        {

                            foreach (DataRow item in ds.Tables[0].Rows)
                            {
                                if (item["hora"].ToString() == ihora.ToString())
                                {
                                    sresul = item["total"].ToString();
                                    break;
                                }
                                else
                                {
                                    sresul = "0";
                                }
                            }

                            //valor nulo
                            if (sresul == "") sresul = "0";

                            Literal1.Text = Literal1.Text + svig + "{ x: " + ihora.ToString() + " , y: " + sresul + ", indexLabel: '" + sresul + "' }";
                            svig = ",";

                        }

                        Literal1.Text = Literal1.Text + @"]
                                              }
                                                  ]
                                   });
                                   chart.render();";

                    }

                }
            }
        }
        catch (Exception err)
        {
            Erro("Grafico Hora a hora " + err.Message);

        }
    }

    /// <summary>
    /// Grafico_VendasHoraHora
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Grafico_VendasHoraHora(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {

                string sdml = @"select   y.HORA,
                                                              Y.TOTAL
                from 
                (
                               select   x.HORA,
                                                            (
                                                                           SELECT COUNT(1) 'TOTAL'
                                FROM TEL_MP_GATVenda B (NOLOCK)
                                WHERE DATEPART(HOUR, B.datainsercao) = x.hora";


                //usuario
                if (susuario != "")
                {
                    sdml = sdml + " AND b.idcodusuario in (" + susuario + ")";
                }

                //data
                sdml = sdml + " and b.datainsercao between '" + dtini + "' and '" + dtfim + "'";



                sdml = sdml + @"   ) 'TOTAL'
                                                            
                               from
                               (
                                              select   DATEPART(HOUR, a.datainsercao ) 'HORA'
                        from           TEL_MP_GATVenda a
                                              where  1 = 1 ";


                //usuario
                if (sperfil != "")
                {
                    sdml = sdml + " AND a.idcodperfil in (" + sperfil + ") ";
                }

                //usuario
                if (sequipe != "")
                {
                    sdml = sdml + " AND a.idcodequipe in (" + sequipe + ") ";
                }

                //usuario
                if (susuario != "")
                {
                    sdml = sdml + " AND a.idcodusuario in (" + susuario + ") ";
                }

                //data
                sdml = sdml + " and a.datainsercao between '" + dtini + "' and '" + dtfim + "'";


                sdml = sdml + @"    ) x
                               group by x.hora 
                ) y 
                     order by y.hora

                ";

                using (SqlDataAdapter objadap = new SqlDataAdapter(sdml, cn))
                {

                    using (DataSet ds = new DataSet())
                    {
                        objadap.Fill(ds);

                        string svig = "";
                        string sresul = "";

                        Literal1.Text = Literal1.Text + @"  
                            var chart = new CanvasJS.Chart('div_graf_venda_hora',
                                   {
                                                  title:{
                                                                text: 'Vendas por Hora'
                                                  },
animationEnabled: true,

                                                  legend: {
                                                                maxWidth: 350,
                                                                itemWidth: 120
                                                  },
                                                  data: [
                                                  {
                                                        
                                                            type: 'line',
                                                            dataPoints: [";

                        //carrega dados
                        for (int ihora = 1; ihora < 23; ihora++)
                        {

                            foreach (DataRow item in ds.Tables[0].Rows)
                            {
                                if (item["hora"].ToString() == ihora.ToString())
                                {
                                    sresul = item["total"].ToString();
                                    break;
                                }
                                else
                                {
                                    sresul = "0";
                                }
                            }

                            //valor nulo
                            if (sresul == "") sresul = "0";

                            Literal1.Text = Literal1.Text + svig + "{ x: " + ihora.ToString() + " , y: " + sresul + ", indexLabel: '" + sresul + "' }";
                            svig = ",";

                        }

                        Literal1.Text = Literal1.Text + @"]
                                              }
                                                  ]
                                   });
                                   chart.render();";

                    }

                }
            }
        }
        catch (Exception err)
        {
            Erro("Vendas por Hora:" + err.Message);

        }
    }

    /// <summary>
    /// Grafico_Status
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Grafico_Status(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                string svig = "";

                string sdml = @"SELECT	B.Status,COUNT(1) AS TOTAL
                                FROM	TEL_MP_GATUsuario A (NOLOCK),
		                                TEL_MP_GATStatusUsuario B (NOLOCK)
                                WHERE	A.CodStatusUser = B.CodStatusUser
                                and		a.CodStatusUser <> 2
                                and		a.IDCodUsuario in (select IDCodUsuario from TEL_MP_GATUsuarioPerfil(nolock)
                                                            where IDCodPerfil in (select IDCodPerfil from TEL_MP_GATPerfil(nolock)
                                                                                    where IDPermissao = 5))";

                if (sperfil != "")
                {
                    sdml = sdml + " AND	A.IDCodUsuario IN (SELECT IDCodUsuario FROM TEL_MP_GATUsuarioPerfil (NOLOCK) WHERE IDCodPerfil IN (" + sperfil + "))";
                }

                if (sequipe != "")
                {
                    sdml = sdml + " AND	A.IDCodUsuario IN (SELECT IDCodUsuario FROM TEL_MP_GATUsuarioPerfil (NOLOCK) WHERE IDCodPerfil IN (" + sequipe + "))";
                }

                //usuario
                if (susuario != "")
                {
                    sdml = sdml + " AND a.idcodusuario IN (" + susuario + ")";
                }

                //data
                sdml = sdml + " and a.habilitado = 1 and a.DATAULTSTATUS between '" + dtini + "' and '" + dtfim + "'";
                sdml = sdml + " GROUP BY B.Status";

                sdml = sdml + " ORDER BY B.Status";

                using (SqlDataAdapter da = new SqlDataAdapter(sdml, cn))
                {

                    //dataset
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    //total
                    double itotal = 0;

                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        itotal = itotal + Convert.ToInt32(item["TOTAL"].ToString());
                    }
                    lblqtdstatus.Text = "Registros Por Status : " + itotal.ToString();


                    //cabecalho do grafico
                    Literal1.Text = Literal1.Text + @" 
                        var chart2 = new CanvasJS.Chart('div_graf_status',
                                       {
                                                      title:{
                                                                    text: 'Registro Por Status'
                                                      },
                                                      legend: {
                                                                    maxWidth: 350,
                                                                    itemWidth: 120
                                                      },
animationEnabled: true,
                                                      data: [
                                                      {
                                                                    showInLegend: true,
                                                                    legendText: '{indexLabel}',
                                                                    type: 'pie',
                                                                    dataPoints: [";


                    double ivalor = 0;
                    double dperc = 0;
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {

                        ivalor = Convert.ToInt32(item["TOTAL"].ToString());

                        dperc = (ivalor / itotal) * 100;

                        //dados
                        Literal1.Text = Literal1.Text + svig + "{ y: " + item["TOTAL"].ToString() + " , label: '" + item["STATUS"].ToString() + " [" + dperc.ToString("0") + "%]' }";
                        svig = ",";

                    }


                    //fim do grafico
                    Literal1.Text = Literal1.Text + @"]
                                              }
                                              ]
                               });

                               chart2.render();";
                }
            }
        }
        catch (Exception err)
        {
            Erro("Grafico Status: " + err.Message);
        }
    }

    /// <summary>
    /// Grafico_Status_Equipe
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Grafico_Status_Equipe(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                string svig = "";

                string sdml = @"SELECT	B.Perfil,COUNT(1) AS 'TOTAL'

                                FROM	TEL_MP_GATUsuario A (NOLOCK),
		                                TEL_MP_GATUsuarioPerfil C (nolock),
		                                TEL_MP_GATPerfil B (nolock)

                                WHERE	A.IDCodUsuario = c.IDCodUsuario
                                AND		c.IDCodPerfil = b.IDCodPerfil
                                and		a.CodStatusUser <> 2
                                AND		b.IDPermissao = 6";

                if (sperfil != "")
                {
                    sdml = sdml + " and b.IDCodPerfil in (" + sperfil + ")";
                }

                if (sequipe != "")
                {
                    sdml = sdml + " and b.IDCodPerfil in (" + sequipe + ")";
                }

                //usuario
                if (susuario != "")
                {
                    sdml = sdml + " AND a.idcodusuario IN (" + susuario + ")";
                }

                //data
                sdml = sdml + " and a.habilitado = 1 and a.DataUltStatus between '" + dtini + "' and '" + dtfim + "'";
                sdml = sdml + " GROUP BY b.Perfil";

                sdml = sdml + " ORDER BY b.Perfil";

                using (SqlDataAdapter da = new SqlDataAdapter(sdml, cn))
                {

                    //dataset
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    //total
                    double itotal = 0;

                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        itotal = itotal + Convert.ToInt32(item["TOTAL"].ToString());
                    }
                    lblqtdstatus_equipe.Text = "Registros Por Status : " + itotal.ToString();


                    //cabecalho do grafico
                    Literal1.Text = Literal1.Text + @" 
                        var chart2 = new CanvasJS.Chart('div_graf_status_equipe',
                                       {
                                                      title:{
                                                                    text: 'Registro Por Equipe'
                                                      },
                                                      legend: {
                                                                    maxWidth: 350,
                                                                    itemWidth: 120
                                                      },
animationEnabled: true,
                                                      data: [
                                                      {
                                                                    showInLegend: true,
                                                                    legendText: '{indexLabel}',
                                                                    type: 'pie',
                                                                    dataPoints: [";


                    double ivalor = 0;
                    double dperc = 0;
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {

                        ivalor = Convert.ToInt32(item["TOTAL"].ToString());

                        dperc = (ivalor / itotal) * 100;

                        //dados
                        Literal1.Text = Literal1.Text + svig + "{ y: " + item["TOTAL"].ToString() + " , label: '" + item["Perfil"].ToString() + " [" + dperc.ToString("0") + "%]' }";
                        svig = ",";

                    }

                    //fim do grafico
                    Literal1.Text = Literal1.Text + @"]
                                              }
                                              ]
                               });

                               chart2.render();";

                }
            }
        }
        catch (Exception err)
        {
            Erro("Grafico Status Equipe:" + err.Message);
        }
    }

    /// <summary>
    /// Grafico_Tipo_Venda
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Grafico_Tipo_Venda(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                string svig = "";

                string sdml = @"select 
		                                b.TipoVenda,count(1) as 'TOTAL'

                                from	TEL_MP_GATVenda a(nolock),
		                                TEL_MP_GATTipoVenda b(nolock)

                                where	a.IdTipoVenda = b.IdTipoVenda ";

                if (sperfil != "")
                {
                    sdml = sdml + " and a.IDCodPerfil in (" + sperfil + ")";
                }

                if (sequipe != "")
                {
                    sdml = sdml + " and a.IDCodPerfil in (" + sequipe + ")";
                }

                //usuario
                if (susuario != "")
                {
                    sdml = sdml + " AND a.idcodusuario IN (" + susuario + ")";
                }

                //data
                sdml = sdml + " and a.datainsercao between '" + dtini + "' and '" + dtfim + "'";

                sdml = sdml + " GROUP BY b.TipoVenda";

                sdml = sdml + " ORDER BY b.TipoVenda";

                using (SqlDataAdapter da = new SqlDataAdapter(sdml, cn))
                {

                    //dataset
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    //total
                    double itotal = 0;

                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        itotal = itotal + Convert.ToInt32(item["TOTAL"].ToString());
                    }

                    lblqtdtipo_venda.Text = "Registros Por Tipo de Venda : " + itotal.ToString();


                    //cabecalho do grafico
                    Literal1.Text = Literal1.Text + @" 
                        var chart2 = new CanvasJS.Chart('div_graf_tipo_venda',
                                       {
                                                      title:{
                                                                    text: 'Registro Por Tipo de Venda'
                                                      },
                                                      legend: {
                                                                    maxWidth: 350,
                                                                    itemWidth: 120
                                                      },
animationEnabled: true,
                                                      data: [
                                                      {
                                                                    showInLegend: true,
                                                                    legendText: '{indexLabel}',
                                                                    type: 'pie',
                                                                    dataPoints: [";


                    double ivalor = 0;
                    double dperc = 0;
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {

                        ivalor = Convert.ToInt32(item["TOTAL"].ToString());

                        dperc = (ivalor / itotal) * 100;

                        //dados
                        Literal1.Text = Literal1.Text + svig + "{ y: " + item["TOTAL"].ToString() + " , label: '" + item["TipoVenda"].ToString() + " [" + dperc.ToString("0") + "%]' }";
                        svig = ",";

                    }

                    //fim do grafico
                    Literal1.Text = Literal1.Text + @"]
                                              }
                                              ]
                               });

                               chart2.render();";

                }
            }
        }
        catch (Exception err)
        {
            Erro("Gradico Tipo Venda:" + err.Message);
        }
    }

    /// <summary>
    /// Grafico_Tipo_Cliente
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Grafico_Tipo_Cliente(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                string svig = "";

                string sdml = @"select 
		                                b.TipoCliente,count(1) as 'TOTAL'

                                from	TEL_MP_GATCliente a(nolock),
		                                TEL_MP_GATTipoCliente b(nolock)

                                where	a.IdcodTipoCliente = b.IdcodTipoCliente ";

                if (sperfil != "")
                {
                    sdml = sdml + " and a.IDCodPerfil in (" + sperfil + ")";
                }

                if (sequipe != "")
                {
                    sdml = sdml + " and a.IDCodPerfil in (" + sequipe + ")";
                }

                //usuario
                if (susuario != "")
                {
                    sdml = sdml + " AND a.idcodusuario IN (" + susuario + ")";
                }

                //data
                sdml = sdml + " and a.datainsercao between '" + dtini + "' and '" + dtfim + "'";

                sdml = sdml + " GROUP BY b.TipoCliente";

                sdml = sdml + " ORDER BY b.TipoCliente";

                using (SqlDataAdapter da = new SqlDataAdapter(sdml, cn))
                {

                    //dataset
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    //total
                    double itotal = 0;

                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        itotal = itotal + Convert.ToInt32(item["TOTAL"].ToString());
                    }

                    lblqtdtipo_cliente.Text = "Registros Por Tipo de Cliente : " + itotal.ToString();


                    //cabecalho do grafico
                    Literal1.Text = Literal1.Text + @" 
                        var chart2 = new CanvasJS.Chart('div_graf_tipo_cliente',
                                       {
                                                      title:{
                                                                    text: 'Registro Por Tipo de Cliente'
                                                      },
                                                      legend: {
                                                                    maxWidth: 350,
                                                                    itemWidth: 120
                                                      },
animationEnabled: true,
                                                      data: [
                                                      {
                                                                    showInLegend: true,
                                                                    legendText: '{indexLabel}',
                                                                    type: 'pie',
                                                                    dataPoints: [";


                    double ivalor = 0;
                    double dperc = 0;
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {

                        ivalor = Convert.ToInt32(item["TOTAL"].ToString());

                        dperc = (ivalor / itotal) * 100;

                        //dados
                        Literal1.Text = Literal1.Text + svig + "{ y: " + item["TOTAL"].ToString() + " , label: '" + item["TipoCliente"].ToString() + " [" + dperc.ToString("0") + "%]' }";
                        svig = ",";

                    }

                    //fim do grafico
                    Literal1.Text = Literal1.Text + @"]
                                              }
                                              ]
                               });

                               chart2.render();";

                }
            }
        }
        catch (Exception err)
        {
            Erro("Grafico Tipo Cliente:" + err.Message);
        }
    }

    #endregion

    #region VENDAS
    /// <summary>
    /// Rel_totvendas
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_totvendas(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    ssql = @"SELECT COUNT(1)FROM TEL_MP_GATVenda(NOLOCK) where 1=1 ";


                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and IDCodPerfil In (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and IDCodPerfil In (" + sequipe + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and idcodusuario in (" + susuario + ")";
                    }

                    //data

                    ssql = ssql + " and datainsercao between '" + dtini + "' and '" + dtfim + "'";


                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    string sv = "";

                    if (dr.Read())
                    {
                        sv = dr[0].ToString();

                        lbltotvendas.Text = dr[0].ToString();
                    }
                    dr.Close();

                }
            }
        }
        catch (Exception err)
        {
            Erro("Total Vendas:" + err.Message);
        }
    }

    /// <summary>
    /// Rel_totvendas_point
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_totvendas_point(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    ssql = @"SELECT COUNT(1)FROM TEL_MP_GATVenda(NOLOCK) where IdTipoVenda = 1 ";


                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and IDCodPerfil In (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and IDCodPerfil In (" + sequipe + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and idcodusuario in (" + susuario + ")";
                    }

                    //data

                    ssql = ssql + " and datainsercao between '" + dtini + "' and '" + dtfim + "'";

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    string sv = "";

                    if (dr.Read())
                    {
                        sv = dr[0].ToString();

                        lbltotvendas_point.Text = dr[0].ToString();
                    }
                    dr.Close();

                }
            }
        }
        catch (Exception err)
        {
            Erro("Total Vendas Point:" + err.Message);
        }
    }

    /// <summary>
    /// Rel_totvendas_qr
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_totvendas_qr(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    ssql = @"SELECT COUNT(1)FROM TEL_MP_GATVenda(NOLOCK) where IdTipoVenda = 2 ";

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and IDCodPerfil In (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and IDCodPerfil In (" + sequipe + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and idcodusuario in (" + susuario + ")";
                    }

                    //data
                    ssql = ssql + " and datainsercao between '" + dtini + "' and '" + dtfim + "'";


                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    string sv = "";

                    if (dr.Read())
                    {
                        sv = dr[0].ToString();

                        lbltotvendas_qr.Text = dr[0].ToString();
                    }
                    dr.Close();

                }
            }
        }
        catch (Exception err)
        {
            Erro("Total Vendas QR:" + err.Message);
        }
    }
    #endregion

    #region ROTAS

    /// <summary>
    /// Rel_totrotas
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    public void Rel_totrotas(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    //monta select
                    ssql = @"select count(1)from TEL_MP_Roteiro a(nolock), TEL_MP_Rota b(nolock)where a.IdCodRota = b.IdCodRota
                            and a.Habilitado = 1 and b.habilitado = 1";

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and a.IdCodRota in (select IdCodRota from TEL_MP_Rota_Usuario(nolock)where IdCodUsuario in (select IdCodUsuario from TEL_MP_GATUsuarioPerfil(nolock)where IDCodPerfil in (" + sperfil + ")))";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and a.IdCodRota in (select IdCodRota from TEL_MP_Rota_Usuario(nolock)where IdCodUsuario in (select IdCodUsuario from TEL_MP_GATUsuarioPerfil(nolock)where IDCodPerfil in (" + sequipe + ")))";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and a.IdCodRota in (select IdCodRota from TEL_MP_Rota_Usuario(nolock)where IdCodUsuario in (" + susuario + "))";
                    }

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    string sv = "";

                    if (dr.Read())
                    {
                        sv = dr[0].ToString();
                        lbltotrotas.Text = sv;
                    }
                    dr.Close();

                }
            }
        }
        catch (Exception err)
        {
            Erro("Total de Rotas:" + err.Message);
        }
    }

    /// <summary>
    /// Rel_totrotas_aguardando
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    public void Rel_totrotas_aguardando(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    //monta select
                    ssql = @"select count(1)from TEL_MP_Roteiro a(nolock), TEL_MP_Rota b(nolock)where a.IdCodRota = b.IdCodRota
                            and b.Habilitado = 1
                            and a.IdcodStatus = 1";


                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and a.IdCodRota in (select IdCodRota from TEL_MP_Rota_Usuario(nolock)where IdCodUsuario in (select IdCodUsuario from TEL_MP_GATUsuarioPerfil(nolock)where IDCodPerfil in (" + sperfil + ")))";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and a.IdCodRota in (select IdCodRota from TEL_MP_Rota_Usuario(nolock)where IdCodUsuario in (select IdCodUsuario from TEL_MP_GATUsuarioPerfil(nolock)where IDCodPerfil in (" + sequipe + ")))";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and a.IdCodRota in (select IdCodRota from TEL_MP_Rota_Usuario(nolock)where IdCodUsuario in (" + susuario + "))";
                    }

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    string sv = "";

                    if (dr.Read())
                    {
                        sv = dr[0].ToString();
                        lbltotrotaaguardando.Text = sv;
                    }
                    dr.Close();

                }
            }
        }
        catch (Exception err)
        {
            Erro("Rotas Aguardando:" + err.Message);
        }
    }

    /// <summary>
    /// Rel_totrotas_concluidas
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    public void Rel_totrotas_concluidas(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    //monta select
                    ssql = @"select count(1)from TEL_MP_Roteiro a(nolock), TEL_MP_Rota b(nolock)where a.IdCodRota = b.IdCodRota
                            and a.Habilitado = 1
                            and b.Habilitado = 1
                            and a.IdcodStatus = 2";


                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and a.IdCodRota in (select IdCodRota from TEL_MP_Rota_Usuario(nolock)where IdCodUsuario in (select IdCodUsuario from TEL_MP_GATUsuarioPerfil(nolock)where IDCodPerfil in (" + sperfil + ")))";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and a.IdCodRota in (select IdCodRota from TEL_MP_Rota_Usuario(nolock)where IdCodUsuario in (select IdCodUsuario from TEL_MP_GATUsuarioPerfil(nolock)where IDCodPerfil in (" + sequipe + ")))";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and a.IdCodRota in (select IdCodRota from TEL_MP_Rota_Usuario(nolock)where IdCodUsuario in (" + susuario + "))";
                    }

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    string sv = "";

                    if (dr.Read())
                    {
                        sv = dr[0].ToString();
                        lbltotrotaconcluida.Text = sv;
                    }
                    dr.Close();

                }
            }
        }
        catch (Exception err)
        {
            Erro("Rotas Concluidas:" + err.Message);
        }
    }

    // <summary>
    /// Rel_totrotas_agendadas
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    public void Rel_totrotas_agendadas(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    //monta select
                    ssql = @"select count(1)from TEL_MP_Roteiro a(nolock), TEL_MP_Rota b(nolock)where a.IdCodRota = b.IdCodRota
                            and b.Habilitado = 1
                            and a.IdcodStatus = 3";


                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and a.IdCodRota in (select IdCodRota from TEL_MP_Rota_Usuario(nolock)where IdCodUsuario in (select IdCodUsuario from TEL_MP_GATUsuarioPerfil(nolock)where IDCodPerfil in (" + sperfil + ")))";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and a.IdCodRota in (select IdCodRota from TEL_MP_Rota_Usuario(nolock)where IdCodUsuario in (select IdCodUsuario from TEL_MP_GATUsuarioPerfil(nolock)where IDCodPerfil in (" + sequipe + ")))";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and a.IdCodRota in (select IdCodRota from TEL_MP_Rota_Usuario(nolock)where IdCodUsuario in (" + susuario + "))";
                    }

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    string sv = "";

                    if (dr.Read())
                    {
                        sv = dr[0].ToString();
                        lbltotrotasagendada.Text = sv;
                    }
                    dr.Close();

                }
            }
        }
        catch (Exception err)
        {
            Erro("Rotas Agendadas:" + err.Message);
        }
    }
    #endregion

    #region PONTOS

    /// <summary>
    /// Rel_totrotas
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    public void Rel_totpontos(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    //monta select
                    ssql = @"select count(1)from TEL_MP_GATPontosAproximidade a(nolock)
                            where a.Habilitado = 1 ";

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and a.IdCodUsuario in (select IdCodUsuario from TEL_MP_GATUsuarioPerfil(nolock)where IDCodPerfil in (" + sperfil + "))";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and a.IdCodUsuario in (select IdCodUsuario from TEL_MP_GATUsuarioPerfil(nolock)where IDCodPerfil in (" + sequipe + "))";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and a.IdCodUsuario in (" + susuario + ")";
                    }

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    string sv = "";

                    if (dr.Read())
                    {
                        sv = dr[0].ToString();
                        lbltotpontos.Text = sv;
                    }
                    dr.Close();

                }
            }
        }
        catch (Exception err)
        {
            Erro("Total Pontos:" + err.Message);
        }
    }
    /// <summary>
    /// Rel_totrotas_aguardando
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    public void Rel_totpontos_aguardando(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    //monta select
                    ssql = @"select count(1)from TEL_MP_GATPontosAproximidade a(nolock)
                            where a.Habilitado = 1
                            and a.IdcodStatus = 1";


                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and a.IdCodUsuario in (select IdCodUsuario from TEL_MP_GATUsuarioPerfil(nolock)where IDCodPerfil in (" + sperfil + "))";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and a.IdCodUsuario in (select IdCodUsuario from TEL_MP_GATUsuarioPerfil(nolock)where IDCodPerfil in (" + sequipe + "))";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and a.IdCodUsuario in (" + susuario + ")";
                    }

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    string sv = "";

                    if (dr.Read())
                    {
                        sv = dr[0].ToString();
                        lbltotpontosaguardando.Text = sv;
                    }
                    dr.Close();

                }
            }
        }
        catch (Exception err)
        {
            Erro("Pontos Aguardando:" + err.Message);
        }
    }

    /// <summary>
    /// Rel_totrotas_concluidas
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    public void Rel_totpontos_concluidos(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    //monta select
                    ssql = @"select count(1)from TEL_MP_GATPontosAproximidade a(nolock)
                            where a.Habilitado = 1
                            and a.IdcodStatus = 2";


                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and a.IdCodUsuario in (select IdCodUsuario from TEL_MP_GATUsuarioPerfil(nolock)where IDCodPerfil in (" + sperfil + "))";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and a.IdCodUsuario in (select IdCodUsuario from TEL_MP_GATUsuarioPerfil(nolock)where IDCodPerfil in (" + sequipe + "))";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and a.IdCodUsuario in (" + susuario + ")";
                    }

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    string sv = "";

                    if (dr.Read())
                    {
                        sv = dr[0].ToString();
                        lbltotpontosconcluidos.Text = sv;
                    }
                    dr.Close();

                }
            }
        }
        catch (Exception err)
        {
            Erro("Pontos Concluidos:" + err.Message);
        }
    }

    #endregion

    /// <summary>
    /// CarregaGridAcompanhamentoVendedores
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void CarregaGridAcompanhamentoVendedores(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {

            string ssql = "";


            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    ssql = @"SELECT	IDCodUsuario AS 'ID',
		                            Nome,
		                            Login,
		                            b.Status,
		                            DataUltStatus,
		                            ISNULL(DATEDIFF(SECOND,Data_Login,GETDATE()),0) AS 'TEMPO_LOGADO',
		                            ISNULL(Endereco,'') AS 'Endereco',

		                            'EQUIPE' = ISNULL((SELECT	Perfil 
					                            FROM	TEL_MP_GATPerfil (NOLOCK)
					                            WHERE	IDPermissao = 6 
					                            AND		IDCodPerfil IN (
												                            SELECT	IDCodPerfil
											                                FROM	TEL_MP_GATUsuarioPerfil (NOLOCK)
												                            WHERE	A.IDCodUsuario = IDCodUsuario
											                            )
					
					                            ),''),

		                            'PRODUTO' = ISNULL((SELECT	Perfil
					                            FROM	TEL_MP_GATPerfil (NOLOCK)
					                            WHERE	IDPermissao = 7 
					                            AND		IDCodPerfil IN (
												                            SELECT	IDCodPerfil
											                                FROM	TEL_MP_GATUsuarioPerfil (NOLOCK)
												                            WHERE	A.IDCodUsuario = IDCodUsuario
											                            )
					
					                            ),'')

                            FROM	TEL_MP_GATUsuario A (NOLOCK),
		                            TEL_MP_GATStatusUsuario b (nolock)

                            WHERE	a.CodStatusUser = b.CodStatusUser
                            AND     A.HABILITADO = 1 
                            AND     A.CodStatusUser <> 2
                            AND     A.IDCodUsuario IN (
                                    SELECT  IDCodUsuario 
                                    from    TEL_MP_GATUsuarioPerfil (nolock)
                                    where   idcodperfil in (
                                                                select  idcodperfil
                                                                from    TEL_MP_GATPerfil (nolock)
                                                                where   IDPermissao = 6
                                                            )

                                    )";


                    if (sperfil != "")
                    {
                        ssql = ssql + " and		a.IDCodUsuario in (select IDCodUsuario from TEL_MP_GATUsuarioPerfil (nolock) where IDCodPerfil in (" + sperfil + "))";
                    }

                    if (sequipe != "")
                    {
                        ssql = ssql + " and		a.IDCodUsuario in (select IDCodUsuario from TEL_MP_GATUsuarioPerfil (nolock) where IDCodPerfil in (" + sequipe + "))";
                    }

                    if (susuario != "")
                    {
                        ssql = ssql + " and		a.IDCodUsuario in (" + susuario + ")";
                    }


                    ssql = ssql + " order by a.nome";

                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = ssql;

                    SqlDataReader dr = cmd.ExecuteReader();

                    gdw_dados.DataSource = dr;
                    gdw_dados.DataBind();

                    dr.Close();

                }
            }


        }
        catch (Exception err)
        {
            Erro("CarregaGridAcompanhamentoVendedores: " + err.Message);
        }
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

                //FILTRO
                Carrega_Lista_Filtro_Perfil("");
                Carrega_Lista_Filtro_Equipe("");
                Carrega_Lista_Filtro_Usuarios("");


                AplicaFiltro();

            }

        }
        catch (Exception err)
        {
            Erro("Page Load:" + err.Message);
        }
    }

    /// <summary>
    /// AplicaFiltro
    /// </summary>
    public void AplicaFiltro()
    {
        try
        {
            //variaveis

            string svig = "";

            //perfil
            svig = "";
            foreach (ListItem obj in cmbperfil_filtro.Items)
            {
                if (obj.Selected == true)
                {
                    _sperfil = _sperfil + svig + obj.Value;
                    svig = ",";
                }
            }

            //equipe
            svig = "";
            foreach (ListItem obj in cmbequipe_filtro.Items)
            {
                if (obj.Selected == true)
                {
                    _sequipe = _sequipe + svig + obj.Value;
                    svig = ",";
                }
            }

            //usuarios
            svig = "";
            foreach (ListItem obj in cmbusuarios_filtro.Items)
            {
                if (obj.Selected == true)
                {
                    _susuario = _susuario + svig + obj.Value;
                    svig = ",";
                }
            }


            //data
            if (chkData_Filtro.Checked == true)
            {
                if (txtdataini.Text.Trim() == "")
                {
                    Erro("Informe a Data Inicial");
                    return;
                }

                if (txtdatafim.Text.Trim() == "")
                {
                    Erro("Informe a Data Final");
                    return;
                }


                //monta a data
                _sdtini = txtdataini.Text.Substring(6, 4) + "-" + txtdataini.Text.Substring(3, 2) + "-" + txtdataini.Text.Substring(0, 2);
                _sdtfim = txtdatafim.Text.Substring(6, 4) + "-" + txtdatafim.Text.Substring(3, 2) + "-" + txtdatafim.Text.Substring(0, 2);

                _sdtini_min = txtdataini.Text.Substring(6, 4) + "-" + txtdataini.Text.Substring(3, 2);
                _sdtfim_min = txtdatafim.Text.Substring(6, 4) + "-" + txtdatafim.Text.Substring(3, 2);


                //coloca a hora se for preenchida
                if (txthoraini.Text.Trim() != "")
                {
                    _sdtini = _sdtini + " " + txthoraini.Text + ":00";
                }
                else
                {
                    _sdtini = _sdtini + " 00:00:00";
                }

                if (txthorafim.Text.Trim() != "")
                {
                    _sdtfim = _sdtfim + " " + txthorafim.Text + ":00";
                }
                else
                {
                    _sdtfim = _sdtfim + " 23:59:59";
                }

                //valida se a data esta ok
                DateTime resultado = DateTime.Now;
                if (DateTime.TryParse(_sdtini, out resultado))
                {
                    if (DateTime.TryParse(_sdtfim, out resultado))
                    {
                        //ok
                    }
                    else
                    {
                        Erro("Data Final inválida");
                        return;
                    }
                }
                else
                {
                    Erro("Data Inicial inválida");
                    return;
                }
            }
            else
            {
                _sdtini = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " 00:00:00";
                _sdtfim = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " 23:59:59";

                _sdtini_min = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                _sdtfim_min = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
            }

            Rel_UsuarioMapa(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);
            Rel_totvendedores(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);
            Rel_totvendedores_logados(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);
            Rel_totvendedores_checkin(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);
            Rel_totvendedores_checkout(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);
            Rel_totvendedores_logout(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);

            Rel_totvendas(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);
            Rel_totvendas_point(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);
            Rel_totvendas_qr(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);

            Rel_totrotas(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);
            Rel_totrotas_aguardando(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);
            Rel_totrotas_concluidas(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);
            Rel_totrotas_agendadas(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);

            Rel_totpontos(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);
            Rel_totpontos_aguardando(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);
            Rel_totpontos_concluidos(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);
            //Rel_totpontos_agendados(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);

            CarregaGridAcompanhamentoVendedores(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);

            //graficos
            Literal1.Text = @"<script type='text/javascript'>
	        window.onload = function () {  ";

            Grafico_HoraHora(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);
            Grafico_Status(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);
            Grafico_Status_Equipe(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);
            Grafico_Tipo_Venda(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);
            Grafico_Tipo_Cliente(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);
            Grafico_VendasHoraHora(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);

            Literal1.Text = Literal1.Text + @"}
            </script>";


        }
        catch (Exception err)
        {
            Erro("AplicaFiltro:" + err.Message);
        }

    }


    /// <summary>
    /// cmdatualizar_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdatualizar_Click(object sender, EventArgs e)
    {
        try
        {
            AplicaFiltro();
        }
        catch (Exception err)
        {
            Erro("AplicaFiltro:" + err.Message);
        }
    }

    /// <summary>
    ///  Timer1_Tick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        AplicaFiltro();
    }

    /// <summary>
    /// gdw_dados_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdw_dados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            gergatlink obj = new gergatlink();

            e.Row.Cells[6].Text = obj.horacheia(Convert.ToInt32(e.Row.Cells[6].Text));

            if (e.Row.Cells[4].Text == "Logado")
            {
                e.Row.Cells[4].Text = "<span class='label label-sucess' style='color: White; background: Green;' ;><asp:Label>" + e.Row.Cells[4].Text + "</asp:Label ></span>";
            }
            else if (e.Row.Cells[4].Text == "Deslogado")
            {
                e.Row.Cells[4].Text = "<span class='label label-sucess' style='color: White; background: Grey;' ;><asp:Label>" + e.Row.Cells[4].Text + "</asp:Label ></span>";
            }
            else if (e.Row.Cells[4].Text == "Check-In")
            {
                e.Row.Cells[4].Text = "<span class='label label-Primary' style='color: White; background: Blue;' ;><asp:Label>" + e.Row.Cells[4].Text + "</asp:Label ></span>";
            }
            else if (e.Row.Cells[4].Text == "Check-Out")
            {
                e.Row.Cells[4].Text = "<span class='label label-danger'><asp:Label>" + e.Row.Cells[4].Text + "</asp:Label ></span>";
            }
        }
    }


}
