using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class monitorar_operacao : System.Web.UI.Page
{

    //variaveis
    int iCont = 0;
    public string _sperfil = "";
    public string _sequipe = "";
    public string _susuario = "";
    public string _sstatus = "";
    public string _sdtini = "";
    public string _sdtfim = "";
    public string _sdtini_min = "";
    public string _sdtfim_min = "";

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

    #region FILTRAR
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

                    sdml = "select  * from tel_mp_gatperfil (nolock) where idpermissao not in (6,7) and habilitado = 1 ";
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
            Erro(err.Message);
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

                    sdml = "select * from tel_mp_gatperfil (nolock) where idpermissao = 6 and habilitado = 1 ";
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
            Erro(err.Message);
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

                    sdml = "select idcodusuario,nome from tel_mp_gatusuario (nolock) where habilitado = 1 ";
                    if (svalor != "") sdml = sdml + " and nome like '%" + svalor + "%'";
                    sdml = sdml + " order by nome asc, dataultstatus";

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
            Erro(err.Message);
        }
    }

    /// <summary>
    /// Carrega_Lista_Filtro_Produtos
    /// </summary>
    /// <param name="svalor"></param>
    public void Carrega_Lista_Filtro_Status(string svalor)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sdml = "";

                    sdml = "select * from tel_mp_gatstatususuario (nolock) where 1 = 1 ";
                    if (svalor != "") sdml = sdml + " and status like '%" + svalor + "%'";
                    sdml = sdml + " order by status";

                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbstatus_filtro.Items.Clear();
                    while (dr.Read())
                    {
                        oitem = new ListItem();
                        oitem.Text = dr["status"].ToString();
                        oitem.Value = dr["codstatususer"].ToString();

                        cmbstatus_filtro.Items.Add(oitem);

                    }
                    dr.Close();

                }

            }

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    #endregion

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

            //status
            svig = "";
            foreach (ListItem obj in cmbstatus_filtro.Items)
            {
                if (obj.Selected == true)
                {
                    _sstatus = _sstatus + svig + obj.Value;
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


            Rel_Operacao(_sperfil, _sequipe, _susuario, _sstatus, _sdtini, _sdtfim);

            //graficos
            Literal1.Text = @"<script type='text/javascript'>
	        window.onload = function () {  ";

            //Grafico_Operacao(_scampanha, _sperfil, _sequipe, _susuario, _sproduto, _sdtini, _sdtfim);
            Literal1.Text = Literal1.Text + @"}
            </script>";

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }

    }

    /// <summary>
    /// cmdexportar_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdexportar_Click(object sender, EventArgs e)
    {
        try
        {
            //Build the Text file data.
            string txt = string.Empty;

            foreach (TableCell cell in gdw_dados.HeaderRow.Cells)
            {
                //Add the Header row for Text file.
                txt += cell.Text.Replace("&nbsp;", "") + ";";
            }

            //Add new line.
            txt += "\r\n";

            foreach (GridViewRow row in gdw_dados.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    //Add the Data rows.
                    txt += cell.Text + ";";
                }

                //Add new line.
                txt += "\r\n";
            }

            //Download the Text file.
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=stt_monitorar_operacao.txt");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(txt);
            Response.Flush();
            Response.End();
        }
        catch (Exception err)
        {
            Erro(err.Message);
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
            Erro(err.Message);
        }
    }

    /// <summary>
    /// gdw_dados_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdw_dados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            gergatlink obj = new gergatlink();
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Cells[0].Text = "<a style='text-decoration:none; color:black;' href='DetalhesMapaUsuario.aspx?_id=" + e.Row.Cells[1].Text + "&_User="+ e.Row.Cells[2].Text + "' target='_blank'><img src='imagens/mapa.png'/></a>";
                e.Row.Cells[0].ToolTip = "Abrir Mapa com localização do usuário";

                e.Row.Cells[6].Text = obj.horacheia(Convert.ToInt32(e.Row.Cells[6].Text));

                if (e.Row.Cells[4].Text == "Logado")
                {
                    e.Row.Cells[4].Text = "<span class='label label-sucess' style='color: White; background: Green;' ><asp:Label>" + e.Row.Cells[4].Text + "</asp:Label ></span>";
                }
                else if (e.Row.Cells[4].Text == "Deslogado")
                {
                    e.Row.Cells[4].Text = "<span class='label label-default' style='color: White; background: Grey;' ><asp:Label>" + e.Row.Cells[4].Text + "</asp:Label ></span>";
                }
                else if (e.Row.Cells[4].Text == "Check-In")
                {
                    e.Row.Cells[4].Text = "<span class='label label-primary' style='color: white; background: blue;' ><asp:Label>" + e.Row.Cells[4].Text + "</asp:Label ></span>";
                }
                else if (e.Row.Cells[4].Text == "Check-Out")
                {
                    e.Row.Cells[4].Text = "<span class='label label-warning' style='color: white; background: red;' ><asp:Label>" + e.Row.Cells[4].Text + "</asp:Label ></span>";
                }
                iCont++;
                
            }
            lbltotuser.Text = "Total de Registros: " + iCont.ToString();


        }
        catch (Exception err)
        {
            Erro(err.Message);
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
                Carrega_Lista_Filtro_Status("");

                AplicaFiltro();

            }

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }

    }

    /// <summary>
    /// Rel_Operacao
    /// </summary>
    /// <param name="scampanha"></param>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="sproduto"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_Operacao(string sperfil, string sequipe, string susuario, string sstatus, string dtini, string dtfim)
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
                    ssql = @"SELECT	'img' = '',
                                    IDCodUsuario AS 'ID',
		                            Nome,
		                            Login,
		                            b.Status,
		                            DataUltStatus,
		                            ISNULL(DATEDIFF(SECOND,Data_Login,GETDATE()),0) AS 'TEMPO_LOGADO',
		                            ISNULL(Endereco,'') AS 'Endereco',

		                            'EQUIPE' = ISNULL((SELECT	Perfil 
					                            FROM	tel_mp_GATPerfil (NOLOCK)
					                            WHERE	IDPermissao = 6 
					                            AND		IDCodPerfil IN (
												                            SELECT	IDCodPerfil
											                                FROM	tel_mp_GATUsuarioPerfil (NOLOCK)
												                            WHERE	A.IDCodUsuario = IDCodUsuario
											                            )
					
					                            ),''),

		                            'PRODUTO' = ISNULL((SELECT	Perfil
					                            FROM	tel_mp_GATPerfil (NOLOCK)
					                            WHERE	IDPermissao = 7 
					                            AND		IDCodPerfil IN (
												                            SELECT	IDCodPerfil
											                                FROM	tel_mp_GATUsuarioPerfil (NOLOCK)
												                            WHERE	A.IDCodUsuario = IDCodUsuario
											                            )
					
					                            ),'')

                            FROM	tel_mp_GATUsuario A (NOLOCK),
		                            tel_mp_GATStatusUsuario b (nolock)

                            WHERE	a.CodStatusUser = b.CodStatusUser
                            AND     A.HABILITADO = 1
                            AND     A.IDCodUsuario IN (SELECT IDCodUsuario from tel_mp_GATUsuarioPerfil (nolock))
                            AND		A.IDCodUsuario IN (SELECT IDCodUsuario from tel_mp_GATUsuarioPerfil (nolock) WHERE IDCodUsuario = A.IDCodUsuario AND IDCodPerfil IN (SELECT IDCodPerfil FROM tel_mp_GATPerfil (NOLOCK) WHERE IDPermissao = 6))";


                    if (sperfil != "")
                    {
                        ssql = ssql + " and		a.IDCodUsuario in (select IDCodUsuario from tel_mp_GATUsuarioPerfil (nolock) where IDCodPerfil in (" + sperfil + "))";
                    }

                    if (sequipe != "")
                    {
                        ssql = ssql + " and		a.IDCodUsuario in (select IDCodUsuario from tel_mp_GATUsuarioPerfil (nolock) where IDCodPerfil in (" + sequipe + "))";
                    }

                    if (susuario != "")
                    {
                        ssql = ssql + " and		a.IDCodUsuario in (" + susuario + ")";
                    }

                    if (sstatus != "")
                    {
                        ssql = ssql + " and		a.CodStatusUser in (" + sstatus + ")";
                    }

                    if (dtini != "")
                    {
                        ssql = ssql + " and a.dataultstatus between '" + dtini + "' and '" + dtfim + "'";
                    }

                    ssql = ssql + " order by a.nome";


                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    gdw_dados.DataSource = dr;
                    gdw_dados.DataBind();

                    dr.Close();

                }
            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }


    /// <summary>
    /// Grafico_Operacao
    /// </summary>
    /// <param name="scampanha"></param>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="sproduto"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Grafico_Operacao(string sperfil, string sequipe, string susuario, string sstatus, string dtini, string dtfim)
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
                    ssql = @"SELECT	A.CodStatusUser,
		                            B.STATUS,
                                    COUNT(1) 'TOTAL'
		                            
                            FROM	tel_mp_GATUsuario A (NOLOCK),
		                            tel_mp_GATSTATUSUSUARIO B (NOLOCK)
		
                            WHERE	A.CODSTATUSUSER = B.CODSTATUSUSER";

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and a.idcodusuario in ( select idcodusuario from tel_mp_gatusuarioperfil (nolock) where idcodperfil in (" + sperfil + "))";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and a.idcodusuario in ( select idcodusuario from tel_mp_gatusuarioperfil (nolock) where idcodperfil in (" + sequipe + "))";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and a.idcodusuario in (" + susuario + ")";
                    }

                    //status
                    if (sstatus.Trim() != "")
                    {
                        ssql = ssql + " and a.codstatususer in (" + sstatus + ")";
                    }
                    else
                    {
                        ssql = ssql + " and a.codstatususer not in (0)";
                    }

                    //data
                    ssql = ssql + " and a.idcodusuario in ( select distinct idcodusuario from tel_mp_gattempousuario (nolock) where datatempo between '" + dtini + "' and '" + dtfim + "')";

                    ssql = ssql + " GROUP BY A.CodStatusUser, B.STATUS ORDER  BY TOTAL";


                    using (SqlDataAdapter da = new SqlDataAdapter(ssql, cn))
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


                        //cabecalho do grafico
                        Literal1.Text = Literal1.Text + @" 
                        var chart1 = new CanvasJS.Chart('div_graf_operadores',
	                        {
		                        title:{
			                        text: ''
		                        },
		                        legend: {
			                        maxWidth: 350,
			                        itemWidth: 120
		                        },
		                        data: [
		                        {
			                        
			                        type: 'bar',
			                        dataPoints: [";


                        double ivalor = 0;
                        double dperc = 0;
                        string[] separators = { " " };
                        int it = 0;
                        string svig = "";
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {

                            ivalor = Convert.ToInt32(item["TOTAL"].ToString());

                            dperc = (ivalor / itotal) * 100;

                            //dados
                            Literal1.Text = Literal1.Text + svig + "{ y: " + item["TOTAL"].ToString() + " , label: '" + item["status"].ToString() + " [" + dperc.ToString("0") + "%]' }";
                            svig = ",";
                            it++;
                            if (it == 5) break;
                        }


                        //fim do grafico
                        Literal1.Text = Literal1.Text + @"]
		                }
		                ]
	                });

	                chart1.render();";

                    }

                }
            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }


    /// <summary>
    /// Timer1_Tick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        AplicaFiltro();
    }

    /// <summary>
    /// gdw_dados_RowCommand     
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdw_dados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            gergatlink obj = new gergatlink();

            if (e.CommandName == "liberar")
            {

                using (SqlConnection cn = obj.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string ssql = "update tel_mp_GATUsuario set Data_Login = null, DataUltStatus = getdate(),CodStatusUser = 2, emuso = 0, Logado = 0 where IDCodUsuario = " + e.CommandArgument.ToString();

                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = ssql;

                        cmd.ExecuteNonQuery();

                        AplicaFiltro();
 
                    }

                }
            }
        }
        catch (Exception err)
        {

            Erro(err.Message);
        }
    }



}