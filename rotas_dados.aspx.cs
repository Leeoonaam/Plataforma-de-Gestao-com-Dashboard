﻿using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class rotas_dados : System.Web.UI.Page
{

    ///Variaveis
    string _id = "";
    string _acao = "";
    string senha = "";
    public string sPlace_Map_Roteiro = "";
    public int iContLinha = 0;

    public string steste = "";


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
    /// Carrega_Lista_Equipe
    /// </summary>
    public void Carrega_Lista_Equipe(string svalor)
    {
        try
        {

            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sdml = "";

                    sdml = "select * from TEL_MP_GATPerfil (nolock) where habilitado = 1 and idpermissao in (6)";

                    //if (svalor != "") sdml = sdml + " and idcodperfil (" + svalor + ")";
                    //sdml = sdml + " order by a.nome";

                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbEquipe.Items.Clear();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            oitem = new ListItem();
                            oitem.Text = dr["perfil"].ToString();
                            oitem.Value = dr["idcodperfil"].ToString();

                            cmbEquipe.Items.Add(oitem);

                        }
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
    public void Carrega_Lista_Usuarios(string srota, string svalor, string sequipe)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sSql = @"select	nome,idcodusuario
                                    from TEL_MP_GATUsuario (nolock)
                                    where Habilitado = 1";

                    if (srota != "")
                    {
                        sSql = sSql + " and	IDCodUsuario not in (select idcodusuario from TEL_MP_Rota_Usuario (nolock) where IDCodRota = " + srota + " )";
                    }

                    if (svalor != "")
                    {
                        sSql = sSql + " and idcodusuario in (" + svalor + ")";
                    }

                    if (sequipe != "")
                    {
                        sSql = sSql + " and IDCodUsuario in (select IDCodUsuario from TEL_MP_GATUsuarioPerfil (nolock) where IDCodPerfil in (select IDCodPerfil from TEL_MP_GATPerfil (nolock) where IDPermissao = 6) and IDCodPerfil in (" + sequipe + "))";
                    }

                    cmd.Connection = cn;
                    cmd.CommandText = sSql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbUsuarios.Items.Clear();

                    while (dr.Read())
                    {
                        oitem = new ListItem();
                        oitem.Text = dr["nome"].ToString();
                        oitem.Value = dr["idcodusuario"].ToString();

                        cmbUsuarios.Items.Add(oitem);
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
    /// CarregaUsuarioRelacionados
    /// </summary>
    /// <param name="svalor"></param>
    public void CarregaUsuarioRelacionados(string svalor)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sSql = @"select	nome,idcodusuario
                                    from TEL_MP_GATUsuario (nolock)
                                    where Habilitado = 1";


                    if (svalor != "")
                    {
                        sSql = sSql + " and idcodusuario in(select IDCodUsuario from TEL_MP_Rota_Usuario(nolock)where IDCodRota = " + svalor + ")";
                    }

                    cmd.Connection = cn;
                    cmd.CommandText = sSql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbRelacionados.Items.Clear();

                    while (dr.Read())
                    {
                        oitem = new ListItem();
                        oitem.Text = dr["nome"].ToString();
                        oitem.Value = dr["idcodusuario"].ToString();
                        oitem.Selected = true;

                        cmbRelacionados.Items.Add(oitem);
                    }

                    dr.Close();

                }

            }

            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(this.GetType(), "InicializaPesquisa", "VarreItemsRelacionados()", true);

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
    public void Carrega_Lista_Usuarios_Filtro()
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sdml = "";

                    sdml = "select idcodusuario,login from TEL_MP_GATUsuario (nolock) where habilitado = 1";

                    sdml = sdml + " order by login";

                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbUsuarios_filtro.Items.Clear();

                    while (dr.Read())
                    {
                        oitem = new ListItem();
                        oitem.Text = dr["login"].ToString();
                        oitem.Value = dr["idcodusuario"].ToString();

                        cmbUsuarios_filtro.Items.Add(oitem);

                        //Captura usuario para relacionados
                        //soitemUsuario.Text = dr["nome"].ToString();
                        //soitemUsuario.Value = dr["idcodusuario"].ToString();

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
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Carrega_Lista_Usuarios_Filtro();
            Carrega_Lista_Equipe("");

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
                        Carrega_Lista_Usuarios(_id, "", "");
                        CarregaUsuarioRelacionados(_id);
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
                    Carrega_Lista_Usuarios("", "", "");
                    Rel_RoteirosMapa("");
                    CarregaUsuarios();

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
		                            A.IDCodRota 'ID',
		                            A.ROTA

                            FROM	TEL_MP_Rota A (NOLOCK)
		
                            WHERE	A.Habilitado = 1";

                    ssql = ssql + " and a.idcodrota =" + sID;


                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        txtid.Text = dr["ID"].ToString();
                        txtnome.Text = dr["Rota"].ToString();
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
    /// Desativar rota
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

                    ssql = @"update TEL_MP_Rota set habilitado = 0 where idcodrota = '" + sID + "'";
                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    MSG_SUCCESS("Cadastro Desativado com sucesso!");
                    Response.Redirect("rotas.aspx", false);

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
            string[] ItensRelacionados;
            string[] ItensGridViewTR;
            string[] ItensGridViewTD;
            string strRoteiros = "";

            // PEGA OS VALUE DOS USUARIO RELACIONADOS
            ItensRelacionados = txtItemsRelacionados.Value.Split(';');
            ItensGridViewTR = txtDadosGrid.Value.Split('|');

            //captura os id de roteiros presentes na tabela
            for (int i = 0; i < ItensGridViewTR.Length; i++)
            {

                if (ItensGridViewTR[i] == "")
                {
                    break;
                }

                ItensGridViewTD = ItensGridViewTR[i].Split(';');
                
                strRoteiros = strRoteiros + ItensGridViewTD[5].ToString() + ",";
            }

            gergatlink obj = new gergatlink();

            // VALIDA OS CAMPOS
            if (txtnome.Text == "")
            {
                Erro("Preencha o nome da rota");
                return;
            }

            // PROCESSO DE ALTERAÇÃO DOS DADOS
            if (txtid.Text != "")
            {
                if (txtid.Text == "1")
                {
                    Erro("Não é possivel inserir/alterar dados de Venda para esse Rota!");
                    return;
                }

                // deleta usuario da rota
                using (SqlConnection cn = obj.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "delete from TEL_MP_Rota_Usuario where idcodrota = " + txtid.Text;

                        cmd.ExecuteNonQuery();
                    }

                    cn.Close();
                }

                // associa usuarios a rota
                for (int i = 0; i < ItensRelacionados.Length; i++)
                {
                    if (ItensRelacionados[i] != "")
                    {
                        using (SqlConnection cn2 = obj.abre_cn())
                        {
                            using (SqlCommand cmd2 = new SqlCommand())
                            {
                                cmd2.Connection = cn2;
                                cmd2.CommandType = CommandType.Text;
                                cmd2.CommandText = "insert into TEL_MP_Rota_Usuario(IdCodrota,Idcodusuario)values(" + txtid.Text + ",'" + ItensRelacionados[i] + "')";

                                cmd2.ExecuteNonQuery();
                            }
                            
                        }
                    }
                }

                //Altera o nome da rota
                using (SqlConnection cn = obj.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update TEL_MP_Rota set Rota = '" + txtnome.Text + "' where idcodrota = " + txtid.Text;

                        cmd.ExecuteNonQuery();

                    }
                    cn.Close();
                }

                //Verifica se tem alguma linha na tabela
                if (strRoteiros.Length > 0)
                {
                    //remove a ultima virgula
                    if (strRoteiros.Substring(strRoteiros.Length - 2, 2) == ",,")
                    {
                        strRoteiros = strRoteiros.Remove(strRoteiros.Length - 2);
                    }
                    else
                    {
                        strRoteiros = strRoteiros.Remove(strRoteiros.Length - 1);
                    }

                    // deleta usuario da rota
                    using (SqlConnection cn = obj.abre_cn())
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = cn;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "update TEL_MP_Roteiro set habilitado = 0 where idcodrota = " + txtid.Text + " and habilitado = 1 and idcodroteiro not in (" + strRoteiros + ")";

                            cmd.ExecuteNonQuery();
                        }

                        cn.Close();
                    }
                }
                else
                {
                    // deleta usuario da rota
                    using (SqlConnection cn = obj.abre_cn())
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = cn;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "update TEL_MP_Roteiro set habilitado = 0 where idcodrota = " + txtid.Text + " and habilitado = 1";

                            cmd.ExecuteNonQuery();
                        }

                        cn.Close();
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

                            //if (ItensGridViewTD.Length == 6)
                            //{
                                cmd3.CommandText = @"select idcodroteiro from TEL_MP_Roteiro(nolock)where Endereco = '" + ItensGridViewTD[1].ToString() + "' and Latitude = '" + ItensGridViewTD[2].ToString() + "' and Longitude = '" + ItensGridViewTD[3].ToString() + "'";
                            //}
                            //else
                            //{
                            //    cmd3.CommandText = @"select idcodroteiro from TEL_MP_Roteiro(nolock)where Endereco = '" + ItensGridViewTD[2].ToString() + "' and Latitude = '" + ItensGridViewTD[3].ToString() + "' and Longitude = '" + ItensGridViewTD[4].ToString() + "'";
                            //}

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
                                        cmd.CommandText = "TEL_MP_AlterarRoteiro2";
                                        cmd.CommandType = CommandType.StoredProcedure;

                                        cmd.Parameters.AddWithValue("@IDCodRoteiro", Convert.ToInt32(dr3["idcodroteiro"].ToString())); 
                                        cmd.Parameters.AddWithValue("@IDCodRota", Convert.ToInt32(txtid.Text));
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
                                        cmd.CommandText = "TEL_MP_InsereRoteiro2";
                                        cmd.CommandType = CommandType.StoredProcedure;

                                        cmd.Parameters.AddWithValue("@IDCodRota", txtid.Text);
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
                string sIdrota = "";

                //INSERE A ROTA
                using (SqlConnection cn = obj.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {

                        cmd.Connection = cn;
                        cmd.CommandText = "TEL_MP_InsereRota";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Rota", SqlDbType.VarChar).Value = txtnome.Text;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        // PEGA O ID DA NOVA ROTA CRIADA
                        sIdrota = cmd.Parameters["@ID"].Value.ToString();

                    }
                }

                if (sIdrota != "")
                {
                    for (int i = 0; i < ItensRelacionados.Length; i++)
                    {
                        if (ItensRelacionados[i] != "")
                        {
                            using (SqlConnection cn2 = obj.abre_cn())
                            {
                                using (SqlCommand cmd2 = new SqlCommand())
                                {
                                    cmd2.Connection = cn2;
                                    cmd2.CommandType = CommandType.Text;
                                    cmd2.CommandText = "insert into TEL_MP_Rota_Usuario(IdCodrota,Idcodusuario)values(" + sIdrota + ",'" + ItensRelacionados[i] + "')";

                                    cmd2.ExecuteNonQuery();
                                }

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

                            for (int x = 0; x < ItensGridViewTD.Length; x++)
                            {
                                using (SqlConnection cn = obj.abre_cn())
                                {
                                    using (SqlCommand cmd = new SqlCommand())
                                    {
                                        cmd.Connection = cn;
                                        cmd.CommandText = "TEL_MP_InsereRoteiro2";
                                        cmd.CommandType = CommandType.StoredProcedure;

                                        cmd.Parameters.AddWithValue("@IDCodRota", sIdrota);
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
                    Erro("A rota não foi inserida...");
                    return;
                }

            }

            MSG_SUCCESS("Rota cadastrada com sucesso!");

            Response.Redirect("rotas.aspx", false);

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
        Response.Redirect("rotas.aspx", false);
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
		                    a.idcodroteiro 'ID',
		                    a.idcodrota 'ID Rota',
                            isnull(a.Nome,'')'Nome'
                            
                                      
                    from	TEL_MP_Roteiro a (nolock)

					where   a.habilitado=1 ";
                    //and     a.codstatususer = b.codstatususer";

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

                    ssql = @"Select a.idcodroteiro'ID',a.endereco,a.latitude,a.longitude,a.sequencia,b.Status,isnull(a.nome,'')'Nome', 'remover' = '','Alterar' = '' from TEL_MP_Roteiro a(nolock), TEL_MP_GATStatusRota b(nolock) where a.habilitado = 1 and a.IdcodStatus = b.IdCodStatus and  a.idcodrota = " + sID + " order by a.sequencia";

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

                                from	TEL_MP_Roteiro A(nolock),
		                                TEL_MP_GATStatusRota B(nolock)

                                where	1=1
                                and		a.IdcodStatus = b.IdCodStatus";

                        ssql = ssql + " and a.idcodrota = " + sRoteiro;

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
    /// 
    /// </summary>
    public void CarregaUsuarios()
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    ssql = @"Select * from TEL_MP_GATUsuario(nolock)where habilitado = 1";

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();


                    //gdw_users.DataSource = dr;
                    // gdw_users.DataBind();



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

                //e.Row.Cells[0].Text.Replace("&nbsp;", "");
                //e.Row.Cells[4].Text = "<input type='checkbox' value = 'delete'>";
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

    /// <summary>
    /// cmdFiltrar_equipe_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdFiltrar_equipe_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtValueEquipe.Value != "" || txtvalueLogin.Value != "")
            {
                Carrega_Lista_Usuarios("", txtvalueLogin.Value, txtValueEquipe.Value);
            }
            else
            {
                Erro("Erro: É necessário selecionar um dos tipos de filtro.");
                return;
            }

        }
        catch (Exception err)
        {
            Erro("Erro: " + err.Message);
        }
    }
}