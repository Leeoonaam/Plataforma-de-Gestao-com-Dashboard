using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
public partial class ger_usuarios : System.Web.UI.Page
{
    public string _scampanha = "";
    public string _sCampTelefonia = "";
    public string _sperfil = "";
    public string _sequipe = "";
    public string _susuario = "";
    public string _sproduto = "";
    public string _sdtini = "";
    public string _sdtfim = "";
    public string _sdtini_min = "";
    public string _sdtfim_min = "";

    public string DtNasc = "";
    public string IdCodUsuario_real = "";

    public string sPermissao = "";
    public string sEquipe = "";
    public string sOperacao = "";

    public string sPermissaoNovo = "";
    public string sEquipeNovo = "";
    public string sOperacaoNovo = "";

    public string sRetorno = "";

    public string myStrPerf = "";
    public int intX = 0;
    public int intY = 0;

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
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Carrega_Combo_Permissao();
            Carrega_Combo_Equipe();
            Carrega_Combo_Produto();
            Carrega_Combo_Turno();
            Carrega_Combo_Funcao();
            Carrega_Combo_TipoLogin();

            if (!IsPostBack)
            {
                string recebe = Session["alt"].ToString();

                if (recebe == "alt")
                {
                    Carrega_Dados_Usuario(Session["codusuario"].ToString());
                    panel_cadastrar.Visible = false;
                    panel_alterar.Visible = true;
                }
                else
                {
                    panel_cadastrar.Visible = true;
                    panel_alterar.Visible = false;
                }
            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    #region CARREGA OS COMBOS
    /// <summary>
    /// Carrega Combo
    /// </summary>
    public void Carrega_Combo_Permissao()
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {

                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT*FROM GATPERFIL (NOLOCK) WHERE IDPERMISSAO IN (1,2,4,6,7,8,9,11,12) and HABILITADO=1 ORDER BY PERFIL ";

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem objI = new ListItem();

                    //objI.Text = "Selecione:";
                    cmbPermissao.Items.Add(objI);

                    while (dr.Read())
                    {
                        objI = new ListItem();

                        objI.Text = dr["PERFIL"].ToString();
                        objI.Value = dr["idcodPerfil"].ToString();

                        cmbPermissao.Items.Add(objI);

                        objI = null;
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
    /// Carrega Combo
    /// </summary>
    public void Carrega_Combo_Equipe()
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {

                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT*FROM GATPERFIL (NOLOCK) WHERE IDPERMISSAO IN (10) and HABILITADO=1 ORDER BY PERFIL ";

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem objI = new ListItem();

                    //objI.Text = "Selecione:";
                    cmbEquipe.Items.Add(objI);

                    while (dr.Read())
                    {
                        objI = new ListItem();

                        objI.Text = dr["PERFIL"].ToString();
                        objI.Value = dr["idcodPerfil"].ToString();

                        cmbEquipe.Items.Add(objI);

                        objI = null;
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
    /// Carrega Combo
    /// </summary>
    public void Carrega_Combo_Produto()
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {

                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT*FROM GATPERFIL (NOLOCK) WHERE IDPERMISSAO IN (3) and HABILITADO=1 ORDER BY PERFIL ";

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem objI = new ListItem();

                    //objI.Text = "Selecione:";
                    cmbProduto.Items.Add(objI);

                    while (dr.Read())
                    {
                        objI = new ListItem();

                        objI.Text = dr["PERFIL"].ToString();
                        objI.Value = dr["idcodPerfil"].ToString();

                        cmbProduto.Items.Add(objI);

                        objI = null;
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
    /// Carrega Combo
    /// </summary>
    public void Carrega_Combo_Turno()
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {

                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from gatturnousuario (nolock) order by turno";

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem objI = new ListItem();

                    //objI.Text = "Selecione:";
                    cmbTurno.Items.Add(objI);

                    while (dr.Read())
                    {
                        objI = new ListItem();

                        objI.Text = dr["turno"].ToString();
                        objI.Value = dr["idcodturno"].ToString();

                        cmbTurno.Items.Add(objI);

                        objI = null;
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
    /// Carrega Combo
    /// </summary>
    public void Carrega_Combo_Funcao()
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {

                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from gatfuncaousuario (nolock) order by funcao";

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem objI = new ListItem();

                    //objI.Text = "Selecione:";
                    cmbFuncao.Items.Add(objI);

                    while (dr.Read())
                    {
                        objI = new ListItem();

                        objI.Text = dr["funcao"].ToString();
                        objI.Value = dr["idcodfuncao"].ToString();

                        cmbFuncao.Items.Add(objI);

                        objI = null;
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
    /// Carrega Combo
    /// </summary>
    public void Carrega_Combo_TipoLogin()
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {

                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from gattipologin (nolock) order by Idcodtipo";

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem objI = new ListItem();

                    //objI.Text = "Selecione:";
                    cmbTipoLogin.Items.Add(objI);

                    while (dr.Read())
                    {
                        objI = new ListItem();

                        objI.Text = dr["Descricao"].ToString();
                        objI.Value = dr["Idcodtipo"].ToString();

                        cmbTipoLogin.Items.Add(objI);

                        objI = null;
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
    /// Carrega_Dados_usuario
    /// </summary>
    /// <param name="scodigo"></param>
    public void Carrega_Dados_Usuario(string scodigo)
    {
        try
        {
            gergatlink ogat = new gergatlink();

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT idcodUsuario,Nome,Apelido,Login,Senha,IDCODPermissao FROM GATUSUARIO(NOLOCK)WHERE IDCODUSUARIO  = " + scodigo;

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        txtidcodusuario.Value = dr["idcodUsuario"].ToString();
                        txtNome_Usuario.Text = dr["Nome"].ToString();
                        txtApelido.Text = dr["Apelido"].ToString();
                        txtCad_Login_Usuario.Text = dr["Login"].ToString();
                        txtCad_Senha_Usuario.Attributes.Add("value", dr["Senha"].ToString());
                        cmbPermissao.SelectedIndex = int.Parse(dr["IDCODPermissao"].ToString());
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
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdCancelar_Click(object sender, EventArgs e)
    {
        LimpaCampos();
        Session["alt"] = "";
    }

    /// <summary>
    /// Limpa os campos
    /// </summary>
    public void LimpaCampos()
    {
        txtNome_Usuario.Text = "";
        txtApelido.Text = "";
        cmbTurno.Text = "";
        txtCad_Login_Usuario.Text = "";
        txtCad_Senha_Usuario.Text = "";
        cmbFuncao.Text = "";
        txtRG.Text = "";
        txtCPF.Text = "";
        txtCodVendedor.Text = "";
        txtRamal.Text = "";
        txtDataNascimento.Text = "";
        cmbSexo.Text = "";
        cmbTipoLogin.Text = "";
        txtLoginNETSales.Text = "";
        txtSenhaNETSales.Text = "";
        txtLoginNETSMS.Text = "";
        txtSenhaNETSMS.Text = "";
        cmbPermissao.Text = "";
        cmbEquipe.Text = "";
        cmbProduto.Text = "";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sTipo"></param>
    /// <param name="CodUsuario"></param>
    /// <returns></returns>
    public string CapturaDadosAntigo(string sTipo, string CodUsuario)
    {
        try
        {
            gergatlink ogat = new gergatlink();

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;

                    //Verifica qual o tipo do select
                    if (sTipo == "Permissao")
                    {
                        cmd.CommandText = "select * from gatperfil (nolock) where idpermissao in (1,2,4,6,7,8,9,11) and idcodperfil in (Select idcodperfil from gatusuarioperfil(nolock) where idcodusuario=" + CodUsuario + ")";
                    }
                    if (sTipo == "Equipe")
                    {
                        cmd.CommandText = "select* from gatperfil(nolock) where idpermissao in (10) and idcodperfil in (Select idcodperfil from gatusuarioperfil(nolock) where idcodusuario = " + CodUsuario + ")";
                    }
                    if (sTipo == "Produto")
                    {
                        cmd.CommandText = "select * from gatperfil (nolock) where idpermissao in (3) and idcodperfil in (Select idcodperfil from gatusuarioperfil(nolock) where idcodusuario=" + CodUsuario + ")";
                    }

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        return dr["idcodPerfil"].ToString();
                    }

                    dr.Close();
                }
            }

            return null;
        }
        catch (Exception err)
        {
            return "";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdSalvar_Usuario_Click(object sender, EventArgs e)
    {
        try
        {
            //Verifica preenchimento
            if (txtNome_Usuario.Text == "" || cmbTurno.SelectedValue == "" || txtCad_Login_Usuario.Text == "" ||
                txtCad_Senha_Usuario.Text == "" || cmbFuncao.SelectedValue == "" || txtRG.Text == "" || txtCPF.Text == "" ||
                txtCodVendedor.Text == "" || txtRamal.Text == "" || txtDataNascimento.Text == "" || cmbSexo.SelectedValue == "" ||
                cmbTipoLogin.SelectedValue == "")
            {
                Erro("Preencha todos os Campos Obrigatórios!");
                return;
            }

            if (chkPermissao.Checked == true)
            {
                if (cmbPermissao.Text == "")
                {
                    Erro("Selecione uma Permissão!");
                    return;
                }
            }

            if (chkEquipe.Checked == true)
            {
                if (cmbEquipe.Text == "")
                {
                    Erro("Selecione uma Equipe!");
                    return;
                }
            }

            if (chkProduto.Checked == true)
            {
                if (cmbProduto.Text == "")
                {
                    Erro("Selecione um Produto");
                    return;
                }
            }

            #region MONTA ARRAY E CAPTURA DADOS ATUAIS
            //Monta array
            if (cmbPermissao.SelectedIndex == -1 || cmbPermissao.SelectedIndex == 0)
            {
                //
            }
            else
            {
                myStrPerf = cmbPermissao.SelectedValue + ",";
                sPermissaoNovo = cmbPermissao.SelectedValue;
            }

            if (cmbEquipe.SelectedIndex == -1 || cmbEquipe.SelectedIndex == 0)
            {
                //
            }
            else
            {
                myStrPerf =  myStrPerf + cmbEquipe.SelectedValue + ",";
                sEquipeNovo = cmbEquipe.SelectedValue;
            }

            if (cmbProduto.SelectedIndex == -1 || cmbProduto.SelectedIndex == 0)
            {
                //
            }
            else
            {
                myStrPerf = myStrPerf + cmbProduto.SelectedValue + ",";
                sOperacaoNovo = cmbProduto.SelectedValue;
            }
            #endregion

            gergatlink obj = new gergatlink();

            //Insere
            if (txtidcodusuario.Value == "")
            {
                using (SqlConnection cn = obj.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        //Formata a data de nascimento para inserção
                        DtNasc = txtDataNascimento.Text.Substring(6, 4) + "-" + txtDataNascimento.Text.Substring(3, 2) + "-" + txtDataNascimento.Text.Substring(0, 2);

                        #region PROC DE INSERIR USUARIO
                        cmd.Connection = cn;
                        cmd.CommandText = "stp_InsertUser4";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Nome", SqlDbType.VarChar, 50).Value = txtNome_Usuario.Text;

                        if (txtApelido.Text == "")
                        {
                            cmd.Parameters.Add("@Apelido", SqlDbType.VarChar, 50).Value = "";
                        }
                        else
                        {
                            cmd.Parameters.Add("@Apelido", SqlDbType.VarChar, 50).Value = txtApelido.Text;
                        }

                        cmd.Parameters.Add("@IDTURNO", SqlDbType.VarChar, 50).Value = cmbTurno.SelectedValue;
                        cmd.Parameters.Add("@Login", SqlDbType.VarChar, 50).Value = txtCad_Login_Usuario.Text;
                        cmd.Parameters.Add("@Senha", SqlDbType.VarChar, 50).Value = txtCad_Senha_Usuario.Text;
                        cmd.Parameters.Add("@IDFUNCAO", SqlDbType.VarChar, 50).Value = cmbFuncao.SelectedValue;
                        cmd.Parameters.Add("@RGVendedor", SqlDbType.VarChar, 50).Value = txtRG.Text;
                        cmd.Parameters.Add("@Cpfvendedor", SqlDbType.VarChar, 50).Value = txtCPF.Text;
                        cmd.Parameters.Add("@Codvendedor", SqlDbType.VarChar, 50).Value = txtCodVendedor.Text;
                        cmd.Parameters.Add("@IDCodRamal", SqlDbType.VarChar, 50).Value = txtRamal.Text;
                        cmd.Parameters.Add("@DataNascimento", SqlDbType.VarChar, 50).Value = DtNasc;
                        cmd.Parameters.Add("@Sexo", SqlDbType.VarChar, 50).Value = cmbSexo.Text;
                        cmd.Parameters.Add("@TipoLogin", SqlDbType.VarChar, 50).Value = cmbTipoLogin.SelectedValue;

                        if (txtLoginNETSales.Text == "")
                        {
                            cmd.Parameters.Add("@LoginNETSales", SqlDbType.VarChar, 50).Value = "";
                        }
                        else
                        {
                            cmd.Parameters.Add("@LoginNETSales", SqlDbType.VarChar, 50).Value = txtLoginNETSales.Text;
                        }

                        if (txtSenhaNETSales.Text == "")
                        {
                            cmd.Parameters.Add("@SenhaNETSales", SqlDbType.VarChar, 50).Value = "";
                        }
                        else
                        {
                            cmd.Parameters.Add("@SenhaNETSales", SqlDbType.VarChar, 50).Value = txtSenhaNETSales.Text;
                        }

                        if (txtLoginNETSMS.Text == "")
                        {
                            cmd.Parameters.Add("@LoginNETSMS", SqlDbType.VarChar, 50).Value = "";
                        }
                        else
                        {
                            cmd.Parameters.Add("@LoginNETSMS", SqlDbType.VarChar, 50).Value = txtLoginNETSMS.Text;
                        }

                        if (txtSenhaNETSMS.Text == "")
                        {
                            cmd.Parameters.Add("@SenhaNETSMS", SqlDbType.VarChar, 50).Value = "";
                        }
                        else
                        {
                            cmd.Parameters.Add("@SenhaNETSMS", SqlDbType.VarChar, 50).Value = txtSenhaNETSMS.Text;
                        }

                        cmd.Parameters.Add("@Status", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@IdCodUsuario", SqlDbType.Int).Direction = ParameterDirection.Output;
                        #endregion

                        cmd.ExecuteNonQuery();

                        //Verifica
                        if (cmd.Parameters["@IdCodUsuario"].Value.ToString() != "0")
                        {
                            //Captura Id inserido
                            IdCodUsuario_real = cmd.Parameters["@IdCodUsuario"].Value.ToString();

                            #region INSERIR HISTORICO
                            //Chama função de captura os dados atuais
                            sPermissao = CapturaDadosAntigo("Permissao", IdCodUsuario_real);

                            //Verifica para gravar log
                            if (sPermissao != sPermissaoNovo)
                            {
                                using (SqlConnection cn2 = obj.abre_cn())
                                {
                                    using (SqlCommand cmd2 = new SqlCommand())
                                    {
                                        cmd2.Connection = cn2;
                                        cmd2.CommandType = CommandType.Text;
                                        cmd2.CommandText = "Insert GatUsuario_HistoricoPerfil (IDPERFIL_ANTERIOR, IDPERFIL_NOVO, IDUsuario, IDUSUARIO_ALTEROU, DATAINSERCAO) values('" + sPermissao + "', '" + sPermissaoNovo + "', '" + IdCodUsuario_real + "', '" + Session["idcodusuario"].ToString() + "', GETDATE())";

                                        cmd2.ExecuteNonQuery();
                                    }
                                }
                            }

                            //Chama função de captura os dados atuais
                            sEquipe = CapturaDadosAntigo("Equipe", IdCodUsuario_real);

                            //Verifica para gravar log
                            if (sEquipe != sEquipeNovo)
                            {
                                using (SqlConnection cn2 = obj.abre_cn())
                                {
                                    using (SqlCommand cmd2 = new SqlCommand())
                                    {
                                        cmd2.Connection = cn2;
                                        cmd2.CommandType = CommandType.Text;
                                        cmd2.CommandText = "Insert GatUsuario_HistoricoPerfil (IDPERFIL_ANTERIOR, IDPERFIL_NOVO, IDUsuario, IDUSUARIO_ALTEROU, DATAINSERCAO) values('" + sEquipe + "', '" + sEquipeNovo + "', '" + IdCodUsuario_real + "', '" + Session["idcodusuario"].ToString() + "', GETDATE())";

                                        cmd2.ExecuteNonQuery();
                                    }
                                }
                            }

                            //Chama função de captura os dados atuais
                            sOperacao = CapturaDadosAntigo("Operacao", IdCodUsuario_real);

                            //Verifica para gravar log
                            if (sOperacao != sOperacaoNovo)
                            {
                                using (SqlConnection cn2 = obj.abre_cn())
                                {
                                    using (SqlCommand cmd2 = new SqlCommand())
                                    {
                                        cmd2.Connection = cn2;
                                        cmd2.CommandType = CommandType.Text;
                                        cmd2.CommandText = "Insert GatUsuario_HistoricoPerfil (IDPERFIL_ANTERIOR, IDPERFIL_NOVO, IDUsuario, IDUSUARIO_ALTEROU, DATAINSERCAO) values('" + sOperacao + "', '" + sOperacaoNovo + "', '" + IdCodUsuario_real + "', '" + Session["idcodusuario"].ToString() + "', GETDATE())";

                                        cmd2.ExecuteNonQuery();
                                    }
                                }
                            }
                            #endregion

                            #region DELETANDO REGISTROS
                            //Verifica Checks de Perfil
                            if (chkPermissao.Checked == true)
                            {
                                using (SqlConnection cn3 = obj.abre_cn())
                                {
                                    using (SqlCommand cmd3 = new SqlCommand())
                                    {
                                        cmd3.Connection = cn3;
                                        cmd3.CommandType = CommandType.Text;
                                        cmd3.CommandText = "delete from gatusuarioperfil where idcodperfil in (select idcodperfil from gatperfil (nolock) where idpermissao in (1,2,4,6,7,8,9,11)) and idcodusuario= " + IdCodUsuario_real;

                                        cmd3.ExecuteNonQuery();
                                    }
                                }
                            }

                            if (chkEquipe.Checked == true)
                            {
                                using (SqlConnection cn3 = obj.abre_cn())
                                {
                                    using (SqlCommand cmd3 = new SqlCommand())
                                    {
                                        cmd3.Connection = cn3;
                                        cmd3.CommandType = CommandType.Text;
                                        cmd3.CommandText = "delete from gatusuarioperfil where idcodperfil in (select idcodperfil from gatperfil (nolock) where idpermissao in (10)) and idcodusuario= " + IdCodUsuario_real;

                                        cmd3.ExecuteNonQuery();
                                    }
                                }
                            }

                            if (chkProduto.Checked == true)
                            {
                                using (SqlConnection cn3 = obj.abre_cn())
                                {
                                    using (SqlCommand cmd3 = new SqlCommand())
                                    {
                                        cmd3.Connection = cn3;
                                        cmd3.CommandType = CommandType.Text;
                                        cmd3.CommandText = "delete from gatusuarioperfil where idcodperfil in (select idcodperfil from gatperfil (nolock) where idpermissao in (3)) and idcodusuario= " + IdCodUsuario_real;

                                        cmd3.ExecuteNonQuery();
                                    }
                                }
                            }
                            #endregion

                            //Remove a ultima virgula
                            myStrPerf = myStrPerf.Remove(myStrPerf.Length - 1);

                            //Captura quantidade de Ids separado por virgula
                            //string[] arrPerf = myStrPerf.Split(',');

                            int arrPerf = Convert.ToInt32(myStrPerf.Split(','));

                           // int[] iarrPerf = new int[myStrPerf.Split(',')];

                            //Insere relacao Usuario x Perfil
                            using (SqlConnection cn4 = obj.abre_cn())
                            {
                                using (SqlCommand cmd4 = new SqlCommand())
                                {
                                    cmd4.Connection = cn4;
                                    cmd4.CommandType = CommandType.Text;

                                    for (intX = 1; intX <= 4; intX++)
                                    {
                                        cmd4.CommandText = "insert into gatusuarioperfil (idcodusuario,idcodperfil) values (" + IdCodUsuario_real + "," + ")";
                                    }

                                    cmd.ExecuteNonQuery();
                                }
                            }

                            LimpaCampos();

                            //Redireciona para outra tela
                            Response.Redirect("~/controle_usuarios.aspx");
                        }
                    }
                }
            }
            //else
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    #region CHECKS
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkProduto_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkProduto.Checked == true)
            {
                cmbProduto.Enabled = true;
            }
            else
            {
                cmbProduto.Enabled = false;
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
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkPermissao_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkPermissao.Checked == true)
            {
                chkPermissao.Enabled = true;
            }
            else
            {
                chkPermissao.Enabled = false;
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
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkEquipe_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkEquipe.Checked == true)
            {
                chkEquipe.Enabled = true;
            }
            else
            {
                chkEquipe.Enabled = false;
            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }
    #endregion

}