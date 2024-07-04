using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;

public partial class perfis_dados : System.Web.UI.Page
{



    string _id = "";
    string _acao = "";
    string senha = "";


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
                        Response.Redirect("perfis.aspx", false);

                    }
                    

                }
                else
                {
                    _acao = "INSERIR";
                    Carrega_Lista_Permissao("");

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

                    ssql = @"update TEL_MP_GATPerfil set habilitado = 0 where idcodperfil = '" + id + "'";
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
    /// Carrega_Lista_Permissao
    /// </summary>
    /// <param name="sperfil"></param>
    public void Carrega_Lista_Permissao(string sperfil)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sdml = "";

                    sdml = "select * from TEL_MP_GATPermissao (nolock)";
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
                            oitem.Text = dr["permissao"].ToString();
                            oitem.Value = dr["idpermissao"].ToString();


                            //verfica se existe
                            if (sperfil != "")
                            {
                                if (Perfil_In_Permissao(sperfil, dr["idpermissao"].ToString()) > 0)
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
    /// User_In_Perfil
    /// </summary>
    /// <param name="suser"></param>
    /// <param name="spermissao"></param>
    /// <returns></returns>
    public int Perfil_In_Permissao(string sperfil, string spermissao)
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

                    ssql = "select count(1) from  TEL_MP_GATPerfil (nolock) where idcodperfil='" + sperfil + "' and idpermissao='" + spermissao + "'";

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

            gergatlink obj = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    ssql = @"select * from TEL_MP_GATPerfil (nolock) where habilitado=1 ";

                    ssql = ssql + " and idcodperfil ='" + sID + "'";


                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        txtid.Text = dr["idcodperfil"].ToString();
                        txtperfil.Text = dr["perfil"].ToString();
   
                        Carrega_Lista_Permissao(txtid.Text);

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

            //verifica preenchimento
            if (txtperfil.Text == "")
            {
                Erro("Preencha o Campo Perfil!");
                return;
            }

            if (cmbpermissao.SelectedValue == "")
            {
                Erro("Selecione uma Permissão!");
                return;
            }

            if (txtid.Text != "")
            {

                using (SqlConnection cn = obj.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {

                        cmd.Connection = cn;
                        cmd.CommandText = "TEL_MP_AlteraPerfil";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@IdPerfil", SqlDbType.Int).Value = Convert.ToInt32(txtid.Text);
                        cmd.Parameters.Add("@Perfil", SqlDbType.VarChar).Value = txtperfil.Text;

                        if (cmbpermissao.SelectedValue != "")
                        {
                            cmd.Parameters.Add("@Permissao", SqlDbType.Int).Value = Convert.ToInt32(cmbpermissao.SelectedValue);
                        }
                        else
                        {
                            cmd.Parameters.Add("@Permissao", SqlDbType.Int).Value = 0;
                        }                        

                        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        //pega o retorno
                        sResult = cmd.Parameters["@status"].Value.ToString();

                        if (sResult == "1")
                        {

                            Erro("Erro ao Salvar Perfil");

                        }
                        else
                        {
                            MSG_SUCCESS("Cadastro Alterado com sucesso");
                            cmdsalvar.Enabled = false;
                            Response.Redirect("perfis.aspx", false);
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
                        cmd.CommandText = "TEL_MP_InserePerfil";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Perfil", SqlDbType.VarChar).Value = txtperfil.Text;

                        if (cmbpermissao.SelectedValue != "")
                        {
                            cmd.Parameters.Add("@Permissao", SqlDbType.Int).Value = Convert.ToInt32(cmbpermissao.SelectedValue);
                        }
                        else
                        {
                            cmd.Parameters.Add("@Permissao", SqlDbType.Int).Value = 0;
                        }
                        
                        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        //pega o retorno
                        sResult = cmd.Parameters["@status"].Value.ToString();

                        if (sResult == "1")
                        {

                            Erro("Erro ao Salvar Perfil");

                        }
                        else
                        {
                            MSG_SUCCESS("Cadastro Inserido com sucesso");
                            cmdsalvar.Enabled = false;
                            Response.Redirect("perfis.aspx", false);
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
        Response.Redirect("perfis.aspx", false);
    }

    
}