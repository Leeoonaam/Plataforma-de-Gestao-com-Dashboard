using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ServiceReference1;


public partial class pontos_aproximidade_dados : System.Web.UI.Page
{

    ///Variaveis
    string _id = "";
    string _acao = "";
    string senha = "";
    public string sPlace_Map_Roteiro = "";
    public int iContLinha = 0;

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

    // <summary>
    /// MSG_SUCCESS
    /// </summary>
    /// <param name="smsg"></param>
    void MSG_SUCCESS(string smsg)
    {
        Session["tipo_alerta"] = "success";
        Session["titulo_alerta"] = "Ok";
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
                        case "id":
                            _id = Request.QueryString[item].ToString();
                            break;

                        case "acao":
                            _acao = Request.QueryString[item].ToString();
                            break;
                    }
                }

                //carrega dados
                if (_id != "")
                {
                    //acao
                    if (_acao == "")
                    {
                        _acao = "EDITAR";
                        CarregaDados(_id);
                        CarregaRoteirosCadastrados(_id);
                        Rel_RoteirosMapa(_id);
                    }
                    else if (_acao == "del")
                    {
                        //funcao para remover
                        Desativar(_id);
                        //Desativar_roteiro(_id);

                    }
                }
                else
                {
                    _acao = "INSERIR";

                    CriarGrid();
                    Rel_RoteirosMapa("");

                }

            }

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// CarregaDados
    /// </summary>
    /// <param name="sID"></param>
    public void CarregaDados(string sID)
    {
        try
        {
            gergatlink obj = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    ssql = @"SELECT	
		                            A.IdCodGrupo  'ID',
		                            A.Grupo

                            FROM	TEL_MP_GATGrupoPontoAproximidade  A (NOLOCK)
		
                            WHERE	A.Habilitado = 1";

                    ssql = ssql + " and a.IdCodGrupo  =" + sID;


                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        txtid.Text = dr["ID"].ToString();
                        txtnome.Text = dr["Grupo"].ToString();
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
    /// Desativar grupo
    /// </summary>
    /// <param name="id"></param>
    public void Desativar(string sID)
    {
        try
        {
            gergatlink obj = new gergatlink();
            string ssql;
            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    ssql = @"update TEL_MP_GATGrupoPontoAproximidade set habilitado = 0 where idcodgrupo = '" + sID + "'";
                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    MSG_SUCCESS("Cadastro Desativado com sucesso!");
                    Response.Redirect("pontos_aproximidade.aspx", false);

                }
            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }

    }

    /// <summary>
    /// cmdsalvar_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdsalvar_Click(object sender, EventArgs e)
    {
        try
        {
            int iresultproc = 0;
            string[] ItensGridViewTR;
            string[] ItensGridViewTD;
            string strPontos = "";

            // PEGA OS VALORES
            ItensGridViewTR = txtDadosGrid.Value.Split('|');

            //captura os id de roteiros presentes na tabela
            for (int i = 0; i < ItensGridViewTR.Length; i++)
            {

                if (ItensGridViewTR[i] == "")
                {
                    break;
                }

                ItensGridViewTD = ItensGridViewTR[i].Split(';');

                strPontos = strPontos + ItensGridViewTD[5].ToString() + ",";
            }

            gergatlink obj = new gergatlink();

            // VALIDA OS CAMPOS
            if (txtnome.Text == "")
            {
                Erro("Preencha o nome do Grupo!");
                return;
            }

            // PROCESSO DE ALTERAÇÃO DOS DADOS
            if (txtid.Text != "")
            {
                //Altera o nome da grupo
                using (SqlConnection cn = obj.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update TEL_MP_GATGrupoPontoAproximidade set Grupo = '" + txtnome.Text + "' where idcodgrupo  = " + txtid.Text;

                        cmd.ExecuteNonQuery();

                    }
                    cn.Close();
                }

                if (strPontos != "")
                {
                    if (strPontos != ",")
                    {
                        //remove a ultima virgula
                        if (strPontos.Substring(strPontos.Length - 2, 2) == ",,")
                        {
                            strPontos = strPontos.Remove(strPontos.Length - 2);
                        }
                        else
                        {
                            strPontos = strPontos.Remove(strPontos.Length - 1);
                        }

                        // desabilita o ponto
                        using (SqlConnection cn = obj.abre_cn())
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cmd.Connection = cn;
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = "update TEL_MP_GATPontosAproximidade set habilitado = 0 where idcodgrupo = " + txtid.Text + " and habilitado = 1 and idcodmarcador not in (" + strPontos + ")";

                                cmd.ExecuteNonQuery();
                            }

                            cn.Close();
                        }
                    }
                }
                
               
                //Verifica quantidade de linhas inseridas na tabela
                if (txtDadosGrid.Value != "")
                {
                    for (int i = 0; i < ItensGridViewTR.Length; i++)
                    {
                        if (ItensGridViewTR[i] == "")
                        {
                            break;
                        }

                        ItensGridViewTD = ItensGridViewTR[i].Split(';');

                        //Verifica se roteiro já está cadastrado
                        using (SqlConnection cn3 = obj.abre_cn())
                        {
                            SqlCommand cmd3 = new SqlCommand();
                            cmd3.Connection = cn3;
                            cmd3.CommandType = CommandType.Text;

                            cmd3.CommandText = @"select IDCodMarcador from TEL_MP_GATPontosAproximidade(nolock)where Endereco = '" + ItensGridViewTD[1].ToString() + "' and Latitude = '" + ItensGridViewTD[2].ToString() + "' and Longitude = '" + ItensGridViewTD[3].ToString() + "'";

                            SqlDataReader dr3 = cmd3.ExecuteReader();

                            string sResult = "";

                            //retorno para inserção ou alteração
                            if (dr3.Read())
                            {
                                using (SqlConnection cn = obj.abre_cn())
                                {
                                    using (SqlCommand cmd = new SqlCommand())
                                    {
                                        cmd.Connection = cn;
                                        cmd.CommandText = "TEL_MP_AlterarPonto";
                                        cmd.CommandType = CommandType.StoredProcedure;

                                        cmd.Parameters.AddWithValue("@IDCodMarcador", Convert.ToInt32(dr3["IDCodMarcador"].ToString()));
                                        cmd.Parameters.AddWithValue("@IdCodGrupo", Convert.ToInt32(txtid.Text));
                                        cmd.Parameters.AddWithValue("@Nome", ItensGridViewTD[0]);
                                        cmd.Parameters.AddWithValue("@Endereco", ItensGridViewTD[1]);
                                        cmd.Parameters.AddWithValue("@Latitude", ItensGridViewTD[2]);
                                        cmd.Parameters.AddWithValue("@Longitude", ItensGridViewTD[3]);
                                        cmd.Parameters.AddWithValue("@sequencia", ItensGridViewTD[4]);

                                        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output;

                                        cmd.ExecuteNonQuery();

                                        //pega o retorno
                                        sResult = cmd.Parameters["@status"].Value.ToString();

                                        if (sResult == "1")
                                        {

                                            Erro("Erro ao Atualizar");

                                        }
                                        else
                                        {
                                            MSG_SUCCESS("Roteiro atualizado com sucesso!");
                                        }

                                    }
                                }

                            }
                            else
                            {
                                using (SqlConnection cn = obj.abre_cn())
                                {
                                    using (SqlCommand cmd = new SqlCommand())
                                    {
                                        cmd.Connection = cn;
                                        cmd.CommandText = "TEL_MP_InserePonto";
                                        cmd.CommandType = CommandType.StoredProcedure;

                                        cmd.Parameters.AddWithValue("@IdCodGrupo", txtid.Text);
                                        cmd.Parameters.AddWithValue("@Nome", ItensGridViewTD[0]);
                                        cmd.Parameters.AddWithValue("@Endereco", ItensGridViewTD[1]);
                                        cmd.Parameters.AddWithValue("@Latitude", ItensGridViewTD[2]);
                                        cmd.Parameters.AddWithValue("@Longitude", ItensGridViewTD[3]);
                                        cmd.Parameters.AddWithValue("@Sequencia", ItensGridViewTD[4]);

                                        iresultproc = cmd.ExecuteNonQuery();

                                        // valida o retorno da proc
                                        if (iresultproc <= 0)
                                        {
                                            Erro("Erro ao atualizar o Roteiro!");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            // PROCESSO DE INSERÇÃO DE REGISTRO
            else
            {
                string sIdponto = "";

                //INSERE A GRUPO
                using (SqlConnection cn = obj.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {

                        cmd.Connection = cn;
                        cmd.CommandText = "TEL_MP_InsereGrupoPonto";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Grupo", SqlDbType.VarChar).Value = txtnome.Text;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        // PEGA O ID DO NOVO GRUPO CRIADO
                        sIdponto = cmd.Parameters["@ID"].Value.ToString();

                    }
                }

                if (sIdponto != "")
                {
                    //Verifica quantidade de linhas inseridas na tabela
                    if (txtDadosGrid.Value != "")
                    {
                        for (int i = 0; i < ItensGridViewTR.Length; i++)
                        {
                            if (ItensGridViewTR[i] == "")
                            {
                                break;
                            }

                            ItensGridViewTD = ItensGridViewTR[i].Split(';');

                            for (int x = 0; x < ItensGridViewTD.Length; x++)
                            {
                                using (SqlConnection cn = obj.abre_cn())
                                {
                                    using (SqlCommand cmd = new SqlCommand())
                                    {
                                        cmd.Connection = cn;
                                        cmd.CommandText = "TEL_MP_InserePonto";
                                        cmd.CommandType = CommandType.StoredProcedure;

                                        cmd.Parameters.AddWithValue("@IdCodGrupo", sIdponto);
                                        cmd.Parameters.AddWithValue("@Nome", ItensGridViewTD[0]);
                                        cmd.Parameters.AddWithValue("@Endereco", ItensGridViewTD[1]);
                                        cmd.Parameters.AddWithValue("@Latitude", ItensGridViewTD[2]);
                                        cmd.Parameters.AddWithValue("@Longitude", ItensGridViewTD[3]);
                                        cmd.Parameters.AddWithValue("@Sequencia", ItensGridViewTD[4]);


                                        iresultproc = cmd.ExecuteNonQuery();

                                        // valida o retorno da proc
                                        if (iresultproc <= 0)
                                        {
                                            Erro("Erro ao cadastrar o Roteiro!");
                                        }
                                        else
                                        {
                                            MSG_SUCCESS("Roteiro cadastrado com sucesso!");
                                        }

                                    }
                                }
                                break;
                            }

                        }
                    }

                }
                else
                {
                    Erro("O grupo não foi inserido...");
                    return;
                }

            }

            MSG_SUCCESS("Grupo cadastrado com sucesso!");

            Response.Redirect("pontos_aproximidade.aspx", false);

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// cmdCancelar_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("pontos_aproximidade.aspx", false);
    }

    /// <summary>
    /// CriarGrid
    /// </summary>
    public void CriarGrid()
    {
        try
        {
            DataTable dt = new DataTable();

            dt.Columns.AddRange(new DataColumn[8]
            {
                new DataColumn("Nome",typeof(string)),
                new DataColumn("Endereco",typeof(string)),
                new DataColumn("Latitude",typeof(string)),
                new DataColumn("Longitude",typeof(string)),
                new DataColumn("Sequencia",typeof(string)),
                new DataColumn("remover",typeof(string)),
                new DataColumn("alterar",typeof(string)),
                new DataColumn("ID",typeof(string)),
            });

            gdw_dados.DataSource = dt;
            gdw_dados.DataBind();

        }
        catch (Exception err)
        {
            Erro("Erro: " + err.Message);
        }
    }

    /// <summary>
    /// Rel_Operacao
    /// </summary>
    /// <param name="salerta"></param>
    /// <param name="splace"></param>
    /// <param name="sequipamento"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void CarregaRoteiros()
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

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
                    ssql = @"select	'Excluir'='',
		                    a.idcodmarcador 'ID',
		                    a.idcodgrupo 'ID Grupo',
                            isnull(a.Nome,'')'Nome'
                            
                                      
                    from	TEL_MP_GATPontosAproximidade a (nolock)

					where   a.habilitado=1 ";

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
    /// CarregaRoteirosCadastrados
    /// </summary>
    /// <param name="sID"></param>
    public void CarregaRoteirosCadastrados(string sID)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";


            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    ssql = @"Select a.idcodmarcador'ID',a.endereco,a.latitude,a.longitude,a.sequencia,b.Status,isnull(a.nome,'')'Nome', 'remover' = '','Alterar' = '' from TEL_MP_GATPontosAproximidade a(nolock), TEL_MP_GATStatusPonto b(nolock) where a.habilitado = 1 and a.IdcodStatus = b.IdCodStatusPonto and  a.idcodgrupo = " + sID + " order by a.sequencia";

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
    /// Rel_RoteirosMapa
    /// </summary>
    /// <param name="sRoteiro"></param>
    public void Rel_RoteirosMapa(string sRoteiro)
    {
        try
        {

            if (sRoteiro != "")
            {
                gergatlink ogat = new gergatlink();
                string ssql = "";
                string shtml = "";
                int ireg = 0;
                txtRenderizaMapa.Value = "";

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
                        ssql = @"select	a.Latitude,a.Longitude,a.Endereco,a.sequencia,b.Status, a.nome

                                from	TEL_MP_GATPontosAproximidade A(nolock),
		                                TEL_MP_GATStatusPonto B(nolock)

                                where	1=1
                                and		a.IdcodStatus = b.IdCodStatusPonto";

                        ssql = ssql + " and a.idcodgrupo = " + sRoteiro;

                        cmd.Connection = cn;
                        cmd.CommandText = ssql;
                        cmd.CommandType = CommandType.Text;

                        SqlDataReader dr = cmd.ExecuteReader();


                        shtml = "";
                        sPlace_Map_Roteiro = "";
                        int imap = 2;
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

                            sPlace_Map_Roteiro = sPlace_Map_Roteiro + "var uluru" + imap.ToString() + " = { lat: " + slat.ToString().Replace(",", ".") + ", lng: " + slog.ToString().Replace(",", ".") + " };";

                            sPlace_Map_Roteiro = sPlace_Map_Roteiro + "var marker = new google.maps.Marker({ position: uluru" + imap.ToString() + ", map: map2, title: '" + dr["sequencia"].ToString() + " - " + dr["Endereco"].ToString() + "',animation: google.maps.Animation.DROP,label: labels[" + dr["sequencia"].ToString() + " - 1] });";//
                            sPlace_Map_Roteiro = sPlace_Map_Roteiro + " marker.addListener('click', toggleBounce);";
                            imap++;

                        }

                        dr.Close();


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
    /// gdw_dados_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdw_dados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            iContLinha = 1;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (e.Row.Cells[0].Text == "&nbsp;")
                {
                    e.Row.Cells[0].Text = "";
                }
                
                e.Row.Cells[6].Text = "<input type='image' src='Imagens/delete.png' onclick='delrow(" + e.Row.Cells[4].Text + "); return false;' >";
                e.Row.Cells[7].Text = "<input type='image' src='Imagens/Alterar.png' style='background-image:url('your_url')' value='Alterar' onclick='carregaDadosAlter(" + e.Row.Cells[4].Text + "); return false;' >";

                iContLinha++;

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Visible = false;
            }
        }
        catch (Exception err)
        {
            Erro("Erro:" + err.Message);
        }
    }

}