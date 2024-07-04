using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;

public partial class venda_dados : System.Web.UI.Page
{
    ///variaveis
    string _id = "";
    string _tipovenda = "";
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
                            _tipovenda = Request.QueryString[item].ToString();
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
                       
                        CarregaDados(_id, _tipovenda);
                    }
                    else if (_acao == "del")
                    {
                        Response.Redirect("vendas.aspx", false);
                    }
                }
                else
                {
                    _acao = "INSERIR";
                    Carrega_Lista_Cliente("");
                }

            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// Carrega_Lista_TipoVenda
    /// </summary>
    /// <param name="suser"></param>
    public void Carrega_Lista_TipoVenda(string sid)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sdml = "";

                    sdml = "select * from TEL_MP_GATTipoVenda (nolock)";
                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbtipovenda.Items.Clear();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            oitem = new ListItem();
                            oitem.Text = dr["TipoVenda"].ToString();
                            oitem.Value = dr["IdTipoVenda"].ToString();

                            if (sid != "")
                            {
                                if (CarregaComboVenda(sid, dr["TipoVenda"].ToString()) > 0)
                                {
                                    oitem.Selected = true;
                                }
                            }
                            
                            cmbtipovenda.Items.Add(oitem);

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
    /// Carrega_Lista_TipoVenda
    /// </summary>
    /// <param name="suser"></param>
    public void Carrega_Lista_TipoCheck(string sid)
    {
        try
        {
            for (int i = 0; i < cmbCheck.Items.Count; i++)
            {
                if (cmbCheck.Items[i].Value == sid)
                {
                    cmbCheck.Items[i].Selected = true;
                }
                
            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// Carrega_Lista_Cliente
    /// </summary>
    /// <param name="suser"></param>
    public void Carrega_Lista_Cliente(string sid)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sdml = "";

                    sdml = "select * from TEL_MP_GATCliente (nolock)where habilitado = 1 ";

                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbcliente.Items.Clear();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            oitem = new ListItem();
                            oitem.Text = dr["Nome"].ToString();
                            oitem.Value = dr["IdcodCliente"].ToString();

                            //verifica se existe valor
                            if (sid != "")
                            {

                                if (CarregaComboCliente(sid, dr["Nome"].ToString()) > 0)
                                {
                                    oitem.Selected = true;
                                }
                            }

                            cmbcliente.Items.Add(oitem);
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
    /// CarregaComboCliente
    /// </summary>
    /// <param name="sid"></param>
    /// <returns></returns>
    public int CarregaComboCliente(string sid, string stipo)
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

                    ssql = "select count(1) from  TEL_MP_GATCliente(nolock) where idcodcliente = " + sid + " and nome = '" + stipo + "'";

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
    /// CarregaComboCliente
    /// </summary>
    /// <param name="sid"></param>
    /// <returns></returns>
    public int CarregaComboVenda(string sid, string stipo)
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

                    ssql = "select count(1) from  TEL_MP_GATTipoVenda(nolock) where idtipovenda = " + sid + " and TipoVenda = '" + stipo + "'";

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
                    //query
                    ssql = @"select * from TEL_MP_GATVenda (nolock) where 1=1 ";

                    ssql = ssql + " and idcodvenda =" + sID;

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr = cmd.ExecuteReader();

                    //retorno
                    if (dr.Read())
                    {
                        //campos padrões
                        txtid.Text = sID;
                        Carrega_Lista_Cliente(dr["idcodcliente"].ToString());
                        Carrega_Lista_TipoVenda(dr["idtipovenda"].ToString());
                        

                        if (dr["IdCodTipoCheckVenda"].ToString() != "")
                        {
                            //cmbCheck.SelectedValue = dr["IdCodTipoCheckVenda"].ToString();
                            Carrega_Lista_TipoCheck(dr["IdCodTipoCheckVenda"].ToString());
                        }

                        //cliente point
                        if (stipo == "Point")
                        {
                            cmbtipomaquina.SelectedValue = dr["TipoMaquina_Point"].ToString();
                            txtnumcontrole.Text = dr["NumeroControle_Point"].ToString();
                            cmbformapagamento.SelectedValue = dr["FormaPagamento_Point"].ToString();
                            //filefoto = dr["Foto_Point"].ToString();

                        }
                        //qr
                        else if (stipo == "QR")
                        {
                            cmbstatusvenda.SelectedValue = dr["IdCodStatusVenda_QR"].ToString();
                            cmbcadastropix.SelectedValue = dr["CadatroPix_QR"].ToString();
                            cmbclientepossui.SelectedValue = dr["PossuiConta_QR"].ToString();
                            txtemailQR.Text = dr["Email_QR"].ToString();

                        }
                        //farmer
                        else
                        {


                        }

                        //campos padrões
                        txttpv.Text = dr["TPV_Prometido"].ToString();
                        txtnomeresp.Text = dr["Nome_Responsavel"].ToString();
                        txttelefoneresp.Text = dr["Telefone_Responsavel"].ToString();
                        txtemailmercado.Text = dr["Email_MercadoPago"].ToString();
                        txtnumop1.Text = dr["Numero_Operacao1"].ToString();
                        txtnumop2.Text = dr["Numero_Operacao2"].ToString();
                        txtnumop3.Text = dr["Numero_Operacao3"].ToString();
                        txtnumcard.Text = dr["Numero_Card"].ToString();
                        txtchipanterior.Text = dr["Chip_Anterior"].ToString();
                        txtchipatual.Text = dr["Chip_Atual"].ToString();
                        txtObservacao.Text = dr["Observacao"].ToString();

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
        Response.Redirect("vendas.aspx", false);
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
            //Verifica variavel de sessão
            if (Session["idcodusuario"].ToString() == "")
            {
                Erro("Perca de Conexão! Realize o login novamente!");
                return;
            }

            if (txtObservacao.Text == "")
            {
                Erro("Preencha uma Observação!");
                return;
            }

            //verifica preenchimento
            if (cmbcliente.SelectedValue == "")
            {
                Erro("Selecione um Cliente!!");
                return;
            }
            else if(cmbtipovenda.SelectedValue == "")
            {
                Erro("Selecione um tipo de Venda!!");
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
                            cmd.CommandText = "TEL_MP_AlteraVenda";
                            cmd.CommandType = CommandType.StoredProcedure;

                            //verifica tipo de venda
                            if (cmbtipovenda.SelectedValue == "Point" || cmbtipovenda.SelectedValue == "1")
                            {

                                cmd.Parameters.Add("@IdCodVenda", SqlDbType.Int).Value = txtid.Text;
                                cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = 1; //ARRUMAR
                                cmd.Parameters.Add("@IdTipoVenda", SqlDbType.Int).Value = 1;

                                cmd.Parameters.Add("@TipoMaquina_Point", SqlDbType.VarChar, 500).Value = cmbtipomaquina.SelectedValue;
                                cmd.Parameters.Add("@NumeroControle_Point", SqlDbType.VarChar, 500).Value = txtnumcontrole.Text;
                                cmd.Parameters.Add("@FormaPagamento_Point", SqlDbType.VarChar, 500).Value = cmbformapagamento.SelectedValue;
                                cmd.Parameters.Add("@Foto_Point", SqlDbType.VarChar, 500).Value = "";

                                cmd.Parameters.Add("@IdCodStatusVenda_QR", SqlDbType.Int).Value = 0;
                                cmd.Parameters.Add("@CadatroPix_QR", SqlDbType.VarChar, 500).Value = "";
                                cmd.Parameters.Add("@PossuiConta_QR", SqlDbType.VarChar, 500).Value = "";
                                cmd.Parameters.Add("@Email_QR", SqlDbType.VarChar, 500).Value = "";
                                                                

                            }

                            if (cmbtipovenda.SelectedValue == "QR" || cmbtipovenda.SelectedValue == "2")
                            {
                                cmd.Parameters.Add("@IdCodVenda", SqlDbType.Int).Value = txtid.Text;
                                cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = 1; //ARRUMAR
                                cmd.Parameters.Add("@IdTipoVenda", SqlDbType.Int).Value = 2;

                                cmd.Parameters.Add("@TipoMaquina_Point", SqlDbType.VarChar, 500).Value = "";
                                cmd.Parameters.Add("@NumeroControle_Point", SqlDbType.VarChar, 500).Value = "";
                                cmd.Parameters.Add("@FormaPagamento_Point", SqlDbType.VarChar, 500).Value = "";
                                cmd.Parameters.Add("@Foto_Point", SqlDbType.VarChar, 500).Value = "";

                                cmd.Parameters.Add("@IdCodStatusVenda_QR", SqlDbType.Int).Value = cmbstatusvenda.SelectedValue;
                                cmd.Parameters.Add("@CadatroPix_QR", SqlDbType.VarChar, 500).Value = cmbcadastropix.SelectedValue;
                                cmd.Parameters.Add("@PossuiConta_QR", SqlDbType.VarChar, 500).Value = cmbclientepossui.SelectedValue;
                                cmd.Parameters.Add("@Email_QR", SqlDbType.VarChar, 500).Value = txtemailQR.Text;

                            }


                            cmd.Parameters.Add("@IdCodCliente", SqlDbType.Int).Value = cmbcliente.SelectedValue;
                            cmd.Parameters.Add("@Check", SqlDbType.Int).Value = cmbCheck.SelectedValue;
                            cmd.Parameters.Add("@TPV_Prometido", SqlDbType.VarChar, 500).Value = txttpv.Text;
                            cmd.Parameters.Add("@Nome_Responsavel", SqlDbType.VarChar, 500).Value = txtnomeresp.Text;
                            cmd.Parameters.Add("@Telefone_Responsavel", SqlDbType.VarChar, 500).Value = txttelefoneresp.Text;
                            cmd.Parameters.Add("@Email_MercadoPago", SqlDbType.VarChar, 500).Value = txtemailmercado.Text;
                            cmd.Parameters.Add("@Numero_Operacao1", SqlDbType.VarChar, 500).Value = txtnumop1.Text;
                            cmd.Parameters.Add("@Numero_Operacao2", SqlDbType.VarChar, 500).Value = txtnumop2.Text;
                            cmd.Parameters.Add("@Numero_Operacao3", SqlDbType.VarChar, 500).Value = txtnumop3.Text;
                            cmd.Parameters.Add("@Numero_Card", SqlDbType.VarChar, 500).Value = txtnumcard.Text;
                            cmd.Parameters.Add("@Chip_Anterior", SqlDbType.VarChar, 500).Value = txtchipanterior.Text;
                            cmd.Parameters.Add("@Chip_Atual", SqlDbType.VarChar, 500).Value = txtchipatual.Text;
                            cmd.Parameters.Add("@Observacao", SqlDbType.VarChar, 500).Value = txtObservacao.Text;

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
                                MSG_SUCCESS("Venda Alterada com sucesso!");
                                cmdsalvar.Enabled = false;
                                Response.Redirect("vendas.aspx", false);
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
                            cmd.CommandText = "TEL_MP_InsereVenda2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            //verifica tipo de venda
                            if (cmbtipovenda.SelectedValue == "Point" || cmbtipovenda.SelectedValue == "1")
                            {

                                cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = Convert.ToInt32(Session["IdCodUsuario"].ToString());
                                cmd.Parameters.Add("@IdTipoVenda", SqlDbType.Int).Value = 1;

                                cmd.Parameters.Add("@TipoMaquina_Point", SqlDbType.VarChar, 500).Value = cmbtipomaquina.SelectedValue;
                                cmd.Parameters.Add("@NumeroControle_Point", SqlDbType.VarChar, 500).Value = txtnumcontrole.Text;
                                cmd.Parameters.Add("@FormaPagamento_Point", SqlDbType.VarChar, 500).Value = cmbformapagamento.SelectedValue;

                                //byte[] imageBytes = Convert.FromBase64String(sFoto_Point);

                                cmd.Parameters.Add("@Foto_Point", SqlDbType.Image).Value = DBNull.Value;

                                cmd.Parameters.Add("@IdCodStatusVenda_QR", SqlDbType.Int).Value = 0;
                                cmd.Parameters.Add("@CadatroPix_QR", SqlDbType.VarChar, 500).Value = "";
                                cmd.Parameters.Add("@PossuiConta_QR", SqlDbType.VarChar, 500).Value = "";
                                cmd.Parameters.Add("@Email_QR", SqlDbType.VarChar, 500).Value = "";

                            }

                            if (cmbtipovenda.SelectedValue == "QR" || cmbtipovenda.SelectedValue == "2")
                            {
                                cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = Convert.ToInt32(Session["IdCodUsuario"].ToString()); //ARRUMAR
                                cmd.Parameters.Add("@IdTipoVenda", SqlDbType.Int).Value = 2;

                                cmd.Parameters.Add("@TipoMaquina_Point", SqlDbType.VarChar, 500).Value = "";
                                cmd.Parameters.Add("@NumeroControle_Point", SqlDbType.VarChar, 500).Value = "";
                                cmd.Parameters.Add("@FormaPagamento_Point", SqlDbType.VarChar, 500).Value = "";
                                cmd.Parameters.Add("@Foto_Point", SqlDbType.VarChar, 500).Value = "";

                                cmd.Parameters.Add("@IdCodStatusVenda_QR", SqlDbType.Int).Value = cmbstatusvenda.SelectedValue;
                                cmd.Parameters.Add("@CadatroPix_QR", SqlDbType.VarChar, 500).Value = cmbcadastropix.SelectedValue;
                                cmd.Parameters.Add("@PossuiConta_QR", SqlDbType.VarChar, 500).Value = cmbclientepossui.SelectedValue;
                                cmd.Parameters.Add("@Email_QR", SqlDbType.VarChar, 500).Value = txtemailQR.Text;
                                
                            }

                            cmd.Parameters.Add("@IdCodCliente", SqlDbType.Int).Value = cmbcliente.SelectedValue;
                            cmd.Parameters.Add("@Check", SqlDbType.Int).Value = cmbCheck.SelectedValue;
                            cmd.Parameters.Add("@TPV_Prometido", SqlDbType.VarChar, 500).Value = txttpv.Text;
                            cmd.Parameters.Add("@Nome_Responsavel", SqlDbType.VarChar, 500).Value = txtnomeresp.Text;
                            cmd.Parameters.Add("@Telefone_Responsavel", SqlDbType.VarChar, 500).Value = txttelefoneresp.Text;
                            cmd.Parameters.Add("@Email_MercadoPago", SqlDbType.VarChar, 500).Value = txtemailmercado.Text;
                            cmd.Parameters.Add("@Numero_Operacao1", SqlDbType.VarChar, 500).Value = txtnumop1.Text;
                            cmd.Parameters.Add("@Numero_Operacao2", SqlDbType.VarChar, 500).Value = txtnumop2.Text;
                            cmd.Parameters.Add("@Numero_Operacao3", SqlDbType.VarChar, 500).Value = txtnumop3.Text;
                            cmd.Parameters.Add("@Numero_Card", SqlDbType.VarChar, 500).Value = txtnumcard.Text;
                            cmd.Parameters.Add("@Chip_Anterior", SqlDbType.VarChar, 500).Value = txtchipanterior.Text;
                            cmd.Parameters.Add("@Chip_Atual", SqlDbType.VarChar, 500).Value = txtchipatual.Text;
                            cmd.Parameters.Add("@Latitude", SqlDbType.VarChar, 500).Value = "";
                            cmd.Parameters.Add("@Longitude", SqlDbType.VarChar, 500).Value = "";
                            cmd.Parameters.Add("@Observacao", SqlDbType.VarChar, 500).Value = txtObservacao.Text.Replace(";","|");
                            cmd.Parameters.Add("@Foto_Point2", SqlDbType.Image).Value = DBNull.Value;
                            cmd.Parameters.Add("@Foto_Point3", SqlDbType.Image).Value = DBNull.Value;

                            cmd.Parameters.Add("@status", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;

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
                                MSG_SUCCESS("Venda Inserida com sucesso!");
                                cmdsalvar.Enabled = false;
                                Response.Redirect("vendas.aspx", false);
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


}