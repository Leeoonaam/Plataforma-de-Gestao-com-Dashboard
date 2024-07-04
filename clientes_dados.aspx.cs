using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;

public partial class clientes_dados : System.Web.UI.Page
{
    ///variaveis
    string _id = "";
    string _tipo = "";
    string _acao = "";

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
    /// 
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

                        case "tipo":
                            _tipo = Request.QueryString[item].ToString();
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
                        CarregaDados(_id, _tipo);
                    }
                    else if (_acao == "del")
                    {
                        //funcao para remover
                        Desativar(_id);
                        Response.Redirect("clientes.aspx", false);
                    }
                }
                else
                {
                    _acao = "INSERIR";
                    Carrega_Lista_Promotor("");
                }



            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// Desativar
    /// </summary>
    /// <param name="id"></param>
    public void Desativar(string id)
    {
        try
        {
            gergatlink obj = new gergatlink();
            string ssql;
            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    ssql = @"update TEL_MP_GATCliente set habilitado = 0 where IdCodCliente = '" + id + "'";
                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr = cmd.ExecuteReader();
                    MSG_SUCCESS("Cliente Desativado com sucesso!");

                }
            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }

    }

    /// <summary>
    /// Carrega_Lista_Permissao
    /// </summary>
    /// <param name="suser"></param>
    public void Carrega_Lista_TipoCliente(string sid, string stipo)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sdml = "";

                    sdml = "select * from TEL_MP_GATTipoCliente (nolock)";
                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbtipocliente.Items.Clear();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            oitem = new ListItem();
                            oitem.Text = dr["TipoCliente"].ToString();
                            oitem.Value = dr["IdCodTipoCliente"].ToString();


                            //verfica se existe
                            if (sid != "")
                            {
                                if (Cliente_In_Tipo(sid, dr["IdCodTipoCliente"].ToString()) > 0)
                                {
                                    oitem.Selected = true;
                                }
                            }

                            cmbtipocliente.Items.Add(oitem);

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
    /// Carrega_Lista_Promotor
    /// </summary>
    /// <param name="suser"></param>
    public void Carrega_Lista_Promotor(string sid)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sdml = "";

                    sdml = @"SELECT	IDCodUsuario,Nome FROM TEL_MP_GATUsuario(NOLOCK)
                            WHERE Habilitado = 1
                            and IDCodUsuario IN(SELECT IDCodUsuario FROM TEL_MP_GATUsuarioPerfil(NOLOCK)

                                                WHERE IDCodPerfil in (select idcodperfil from TEL_MP_GATPerfil(nolock) where IDPermissao = 5))";

                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbpromotor.Items.Clear();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            oitem = new ListItem();
                            oitem.Text = dr["Nome"].ToString();
                            oitem.Value = dr["IDCodUsuario"].ToString();


                            //verfica se existe
                            if (sid != "")
                            {
                                if (Promotor_In_Tipo(sid, dr["IDCodUsuario"].ToString()) > 0)
                                {
                                    oitem.Selected = true;
                                }
                            }

                            cmbpromotor.Items.Add(oitem);

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
    /// CarregaDados
    /// </summary>
    /// <param name="sID"></param>
    public void CarregaDados(string sID, string stipo)
    {
        try
        {
            gergatlink obj = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    ssql = @"select * from TEL_MP_GATCliente (nolock) where habilitado=1 ";

                    ssql = ssql + " and idcodcliente =" + sID;

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {

                        txtid.Text = sID;

                        //cliente point / qr
                        if (stipo == "Point / QR Code")
                        {
                            cmbtipocliente.SelectedValue = "clienteqr";
                            cmbtiposegmento.SelectedValue = dr["tiposegmento"].ToString();
                            txtnome.Text = dr["nome"].ToString();
                            txtcnpj.Text = dr["cnpj"].ToString();
                            txtendereco.Text = dr["endereco"].ToString();
                            txtcontato.Text = dr["contato"].ToString();
                            txtdecisor.Text = dr["decisor"].ToString();

                            Carrega_Lista_Promotor(sID);

                        }
                        else
                        {
                            //cliente farmer
                            cmbtipocliente.SelectedValue = "clientefarmer";
                            txtnomefarmer.Text = dr["nome"].ToString();
                            txtemailfarmer.Text = dr["email"].ToString();
                            txtcontatofarmer.Text = dr["contato"].ToString();

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
    /// cmdVoltar_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("clientes.aspx", false);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdsalvar_Click(object sender, EventArgs e)
    {
        try
        {
            int Iduser = 0;

            if (Session["idcodusuario"].ToString() == "")
            {
                Erro("Perca de Conexão! Realize o Login novamente!");
                return;
            }
            else
            {
                Iduser = Convert.ToInt32(Session["idcodusuario"].ToString());
            }

            //verifica preenchimento
            if (cmbtipocliente.SelectedValue == "")
            {
                Erro("Selecione um tipo de Cliente!!");
                return;
            }
            else
            {
                gergatlink obj = new gergatlink();
                string sResult = "";

                //verifica ação inserir ou editar
                if (txtid.Text != "")
                {
                    //alterar
                    using (SqlConnection cn = obj.abre_cn())
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {

                            cmd.Connection = cn;
                            cmd.CommandText = "TEL_MP_AlteraCliente3";
                            cmd.CommandType = CommandType.StoredProcedure;


                            if (cmbtipocliente.SelectedValue == "clienteqr")
                            {
                                cmd.Parameters.Add("@IdCodCliente", SqlDbType.Int).Value = txtid.Text;

                                if (cmbpromotor.SelectedValue == "")
                                {
                                    cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = 0;
                                    cmd.Parameters.Add("@Indicacao", SqlDbType.Int).Value = 0;
                                }
                                else
                                {
                                    cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = cmbpromotor.SelectedValue;
                                    cmd.Parameters.Add("@Indicacao", SqlDbType.Int).Value = 1;
                                }
                                
                                cmd.Parameters.Add("@IdCodTipoCliente", SqlDbType.Int).Value = 1;
                                cmd.Parameters.Add("@TipoSegmento", SqlDbType.VarChar, 50).Value = cmbtiposegmento.SelectedValue;
                                cmd.Parameters.Add("@Nome", SqlDbType.VarChar, 500).Value = txtnome.Text;
                                cmd.Parameters.Add("@CNPJ", SqlDbType.VarChar, 20).Value = txtcnpj.Text;
                                cmd.Parameters.Add("@Endereco", SqlDbType.VarChar, 5000).Value = txtendereco.Text;
                                cmd.Parameters.Add("@Contato", SqlDbType.VarChar, 20).Value = txtcontato.Text;
                                cmd.Parameters.Add("@Decisor", SqlDbType.VarChar, 20).Value = txtdecisor.Text;
                            }

                            cmd.Parameters.Add("@status", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();

                            //pega o retorno
                            sResult = cmd.Parameters["@status"].Value.ToString();

                            if (sResult != "OK")
                            {
                                Erro("Erro ao Salvar Cliente!");
                                return;
                            }
                            else
                            {
                                MSG_SUCCESS("Cliente Alterado com sucesso!");
                                cmdsalvar.Enabled = false;
                                Response.Redirect("clientes.aspx", false);
                            }

                        }
                    }
                }
                else
                {
                    //inserir
                    using (SqlConnection cn = obj.abre_cn())
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {

                            cmd.Connection = cn;
                            cmd.CommandText = "TEL_MP_InsereCliente3";
                            cmd.CommandType = CommandType.StoredProcedure;

                            if (cmbpromotor.SelectedValue == "")
                            {
                                cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = 0;
                                cmd.Parameters.Add("@Indicacao", SqlDbType.Int).Value = 0;
                            }
                            else
                            {
                                cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = cmbpromotor.SelectedValue;
                                cmd.Parameters.Add("@Indicacao", SqlDbType.Int).Value = 1;
                            }

                            cmd.Parameters.Add("@IdCodTipoCliente", SqlDbType.Int).Value = 1;
                            cmd.Parameters.Add("@TipoSegmento", SqlDbType.VarChar,50).Value = cmbtiposegmento.SelectedValue;
                            cmd.Parameters.Add("@Nome", SqlDbType.VarChar,500).Value = txtnome.Text;
                            cmd.Parameters.Add("@CNPJ", SqlDbType.VarChar,20).Value = txtcnpj.Text;
                            cmd.Parameters.Add("@Endereco", SqlDbType.VarChar,5000).Value = txtendereco.Text;
                            cmd.Parameters.Add("@Contato", SqlDbType.VarChar,20).Value = txtcontato.Text;
                            cmd.Parameters.Add("@Decisor", SqlDbType.VarChar,20).Value = txtdecisor.Text;
                            cmd.Parameters.Add("@IdCodUsuario_insercao", SqlDbType.Int).Value = Iduser;
                            
                            cmd.Parameters.Add("@status", SqlDbType.VarChar,1000).Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();

                            //pega o retorno
                            sResult = cmd.Parameters["@status"].Value.ToString();

                            //verifica retorno
                            if (sResult != "OK")
                            {
                                Erro("Erro ao Salvar Usuario!");
                                return;
                            }
                            else
                            {
                                MSG_SUCCESS("Cadastro Inserido com sucesso!");
                                cmdsalvar.Enabled = false;
                                Response.Redirect("clientes.aspx", false);
                            }

                        }
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
    /// Cliente_In_Tipo
    /// </summary>
    /// <param name="sid"></param>
    /// <param name="stipo"></param>
    /// <returns></returns>
    public int Cliente_In_Tipo(string sid, string stipo)
    {
        try
        {
            gergatlink obj = new gergatlink();
            string ssql = "";
            int iret = 0;

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    ssql = "select count(1) from  TEL_MP_GATCliente (nolock) where idcodcliente =" + sid + " and IdCodTipoCliente=" + stipo;

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        iret = Convert.ToInt32(dr[0].ToString());
                    }
                    dr.Close();
                }

            }

            return iret;

        }
        catch (Exception err)
        {
            Erro(err.Message);
            return 0;

        }
    }

    /// <summary>
    /// Promotor_In_Tipo
    /// </summary>
    /// <param name="sid"></param>
    /// <param name="siduser"></param>
    /// <returns></returns>
    public int Promotor_In_Tipo(string sid, string siduser)
    {
        try
        {
            gergatlink obj = new gergatlink();
            string ssql = "";
            int iret = 0;

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    ssql = "select count(1) from  TEL_MP_GATCliente (nolock) where idcodcliente =" + sid + " and IdCodUsuario=" + siduser;

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        iret = Convert.ToInt32(dr[0].ToString());
                    }
                    dr.Close();
                }

            }

            return iret;

        }
        catch (Exception err)
        {
            Erro(err.Message);
            return 0;

        }
    }
}