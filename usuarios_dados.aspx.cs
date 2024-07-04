using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;

public partial class usuarios_dados : System.Web.UI.Page
{

    string _id = "";
    string _acao = "";
    string senha = "";

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
                    }
                    else if (_acao == "del")
                    {
                        //funcao para remover
                        Desativar(_id);
                        Response.Redirect("usuarios.aspx", false);

                    }
                    else if (_acao == "liberar")
                    {
                        //funcao para Liberar
                        Liberar(_id);
                        Response.Redirect("usuarios.aspx", false);
                    }

                }
                else
                {
                    _acao = "INSERIR";
                    Carrega_Lista_Permissao("");
                    Carrega_Lista_Equipe("");
                    Carrega_Lista_Produto("");

                    sPlace_Map = "var uluru1 = { lat: 0, lng: 0};";
                    sPlace_Map = sPlace_Map + " var map1 = new google.maps.Map(document.getElementById('mapa'), { zoom: 15, center: uluru1 }); ";
                    sPlace_Map = sPlace_Map + "var marker = new google.maps.Marker({ position: uluru1, map: map1});";
                    
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

                    ssql = @"update TEL_MP_GATUsuario set habilitado = 0 where idcodusuario = '" + id + "'";
                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr = cmd.ExecuteReader();
                    MSG_SUCCESS("Cadastro Desativado com sucesso");

                }
            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }

    }
    
    /// <summary>
    /// Liberar
    /// </summary>
    /// <param name="id"></param>
    public void Liberar(string id)
    {
        try
        {
            gergatlink obj = new gergatlink();
            string ssql;
            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    ssql = @"update TEL_MP_GATUsuario set emuso=0, logado=0, bloqueado=0, codstatususer=2, DataUltStatus=getdate() where idcodusuario = '" + id + "'";
                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr = cmd.ExecuteReader();
                    MSG_SUCCESS("Cadastro Liberado com sucesso");

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
    public void Carrega_Lista_Permissao(string suser)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sdml = "";

                    sdml = "select * from TEL_MP_GATPerfil (nolock) where habilitado = 1 and idpermissao in (1,2,3,4,5)";
                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;


                    cmbpermissao.Items.Clear();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            oitem = new ListItem();
                            oitem.Text = dr["perfil"].ToString();
                            oitem.Value = dr["idcodperfil"].ToString();


                            //verfica se existe
                            if (suser != "")
                            {
                                if (User_In_Perfil(suser, dr["idcodperfil"].ToString()) > 0)
                                {
                                    oitem.Selected = true;
                                }
                            }

                            cmbpermissao.Items.Add(oitem);

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
    /// Carrega_Lista_Equipe
    /// </summary>
    /// <param name="suser"></param>
    public void Carrega_Lista_Equipe(string suser)
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
                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbequipe.Items.Clear();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            oitem = new ListItem();
                            oitem.Text = dr["perfil"].ToString();
                            oitem.Value = dr["idcodperfil"].ToString();


                            //verfica se existe
                            if (suser != "")
                            {
                                if (User_In_Perfil(suser, dr["idcodperfil"].ToString()) > 0)
                                {
                                    oitem.Selected = true;
                                }
                            }

                            cmbequipe.Items.Add(oitem);

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
    /// Carrega_Lista_Produto
    /// </summary>
    /// <param name="suser"></param>
    public void Carrega_Lista_Produto(string suser)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sdml = "";

                    sdml = "select * from TEL_MP_GATPerfil (nolock) where habilitado = 1 and idpermissao in (7)";
                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbproduto.Items.Clear();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            oitem = new ListItem();
                            oitem.Text = dr["perfil"].ToString();
                            oitem.Value = dr["idcodperfil"].ToString();


                            //verfica se existe
                            if (suser != "")
                            {
                                if (User_In_Perfil(suser, dr["idcodperfil"].ToString()) > 0)
                                {
                                    oitem.Selected = true;
                                }
                            }

                            cmbproduto.Items.Add(oitem);

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
    /// User_In_Perfil
    /// </summary>
    /// <param name="suser"></param>
    /// <param name="spermissao"></param>
    /// <returns></returns>
    public int User_In_Perfil(string suser, string sperfil)
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

                    ssql = "select count(1) from  TEL_MP_GATUsuarioPerfil (nolock) where idcodusuario='" + suser + "' and idcodperfil='" + sperfil + "'";

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
    public void CarregaDados(string sID)
    {

        try
        {
            sPlace_Map = "";

            gergatlink obj = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    ssql = @"select * from TEL_MP_GATUsuario (nolock) where habilitado=1 ";

                    ssql = ssql + " and idcodusuario ='" + sID + "'";


                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        txtid.Text = dr["idcodusuario"].ToString();
                        txtnome.Text = dr["Nome"].ToString();
                        txtlogin.Text = dr["Login"].ToString();
                        txtsenha.Attributes.Add("value", dr["senha"].ToString());

                        if (dr["Latitude"].ToString() == "")
                        {
                            txtLatitude.Text = "0";
                        }
                        else
                        {
                            txtLatitude.Text = dr["Latitude"].ToString();
                        }

                        if (dr["Longitude"].ToString() == "")
                        {
                            txtLongitude.Text = "0";
                        }
                        else
                        {
                            txtLongitude.Text = dr["Longitude"].ToString();
                        }
                        
                        txtEndereco.Text = dr["Endereco"].ToString();
                        
                        sPlace_Map = "var uluru1 = { lat: " + txtLatitude.Text.ToString().Replace(",", ".") + ", lng: " + txtLongitude.Text.ToString().Replace(",", ".") + " };";
                        sPlace_Map = sPlace_Map + " var map1 = new google.maps.Map(document.getElementById('mapa'), { zoom: 15, center: uluru1 }); ";
                        sPlace_Map = sPlace_Map + "var marker = new google.maps.Marker({ position: uluru1, map: map1, title: 'NOME: " + dr["Nome"].ToString() + " - ENDEREÇO: " + dr["Endereco"].ToString() + "' });";
                        
                        Carrega_Lista_Permissao(txtid.Text);
                        Carrega_Lista_Equipe(txtid.Text);
                        Carrega_Lista_Produto(txtid.Text);

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
    ///  cmbenviar_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmbenviar_Click(object sender, EventArgs e)
    {
        try
        {
            gergatlink obj = new gergatlink();
            string sResult = "";

            if (txtnome.Text == "")
            {
                Erro("Preencha o nome!");
                return;

            }

            if (txtlogin.Text == "")
            {
                Erro("Preencha o login!");
                return;

            }

            if (txtsenha.Text == "")
            {
                Erro("Preencha a senha!");
                return;
            }

            if (cmbpermissao.SelectedValue == "")
            {
                Erro("Selecione uma permissão!");
                return;

            }

            if (cmbequipe.SelectedValue == "")
            {
                Erro("Selecione uma Equipe!");
                return;
            }

            if (txtid.Text != "")
            {

                using (SqlConnection cn = obj.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {

                        cmd.Connection = cn;
                        cmd.CommandText = "TEL_MP_AlteraUsuario";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@IdUser", SqlDbType.Int).Value = Convert.ToInt32(txtid.Text);
                        cmd.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txtnome.Text;
                        cmd.Parameters.Add("@Login", SqlDbType.VarChar).Value = txtlogin.Text;
                        cmd.Parameters.Add("@Senha", SqlDbType.VarChar).Value = txtsenha.Text;
                        cmd.Parameters.Add("@Matricula", SqlDbType.VarChar).Value = "";

                        if (cmbpermissao.SelectedValue != "")
                        {
                            cmd.Parameters.Add("@Permissao", SqlDbType.Int).Value = Convert.ToInt32(cmbpermissao.SelectedValue);
                        }
                        else
                        {
                            cmd.Parameters.Add("@Permissao", SqlDbType.Int).Value = 0;
                        }

                        if (cmbequipe.SelectedValue != "")
                        {
                            cmd.Parameters.Add("@Equipe", SqlDbType.Int).Value = Convert.ToInt32(cmbequipe.SelectedValue);
                        }
                        else
                        {
                            cmd.Parameters.Add("@Equipe", SqlDbType.Int).Value = 0;
                        }

                        if (cmbproduto.SelectedValue != "")
                        {
                            cmd.Parameters.Add("@Produto", SqlDbType.Int).Value = Convert.ToInt32(cmbproduto.SelectedValue);
                        }
                        else
                        {
                            cmd.Parameters.Add("@Produto", SqlDbType.Int).Value = 0;
                        }

                        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        //pega o retorno
                        sResult = cmd.Parameters["@status"].Value.ToString();

                        if (sResult == "1")
                        {

                            Erro("Erro ao Salvar Usuario");
                            return;
                        }
                        else
                        {
                            MSG_SUCCESS("Cadastro Alterado com sucesso");
                            cmdsalvar.Enabled = false;
                            Response.Redirect("usuarios.aspx", false);
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
                        cmd.CommandText = "TEL_MP_InsereUsuario";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txtnome.Text;
                        cmd.Parameters.Add("@Login", SqlDbType.VarChar).Value = txtlogin.Text;
                        cmd.Parameters.Add("@Senha", SqlDbType.VarChar).Value = txtsenha.Text;
                        cmd.Parameters.Add("@Matricula", SqlDbType.VarChar).Value = "";

                        if (cmbpermissao.SelectedValue != "")
                        {
                            cmd.Parameters.Add("@Permissao", SqlDbType.Int).Value = Convert.ToInt32(cmbpermissao.SelectedValue);
                        }
                        else
                        {
                            cmd.Parameters.Add("@Permissao", SqlDbType.Int).Value = 0;
                        }

                        if (cmbequipe.SelectedValue != "")
                        {
                            cmd.Parameters.Add("@Equipe", SqlDbType.Int).Value = Convert.ToInt32(cmbequipe.SelectedValue);
                        }
                        else
                        {
                            cmd.Parameters.Add("@Equipe", SqlDbType.Int).Value = 0;
                        }

                        if (cmbproduto.SelectedValue != "")
                        {
                            cmd.Parameters.Add("@Produto", SqlDbType.Int).Value = Convert.ToInt32(cmbproduto.SelectedValue);
                        }
                        else
                        {
                            cmd.Parameters.Add("@Produto", SqlDbType.Int).Value = 0;
                        }

                        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        //pega o retorno
                        sResult = cmd.Parameters["@status"].Value.ToString();

                        if (sResult == "1")
                        {

                            Erro("Erro ao Salvar Usuario");
                            return;

                        }
                        else
                        {
                            MSG_SUCCESS("Cadastro Inserido com sucesso");
                            cmdsalvar.Enabled = false;
                            Response.Redirect("usuarios.aspx", false);
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
    /// cmdVoltar_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("usuarios.aspx", false);
    }


    /// <summary>
    /// cmbpermissao_SelectedIndexChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmbpermissao_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if ( Convert.ToInt32( cmbpermissao.SelectedValue)<=4 )
            {
                
                cmbequipe.SelectedIndex = -1;
                cmbequipe.SelectedValue = null;
                cmbequipe.ClearSelection();

                
                cmbproduto.SelectedIndex = -1;
                cmbproduto.SelectedValue = null;
                cmbproduto.ClearSelection();
            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

}