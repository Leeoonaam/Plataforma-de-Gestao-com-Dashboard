using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class ger_naotabulados : System.Web.UI.Page
{
    /// <summary>
    /// Erro
    /// </summary>
    /// <param name="smsg"></param>
    void Erro(string smsg)
    {
        panel_erro.Visible = true;
        lblerro.Text = smsg;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string recebe = Session["alt"].ToString();

            if (recebe == "alt")
            {
                Carrega_Dados(Session["idcodregistro"].ToString());
            }
        }
    }

    /// <summary>
    /// Carrega os dados da Tabela
    /// </summary>
    /// <param name="sIdCodRegistro_Real"></param>
    public void Carrega_Dados(string sIdCodRegistro_Real)
    {
        try
        {
            gergatlink ogat = new gergatlink();

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    //Monta query
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT	
		                                        A.*,
                                                B.IDCODCLIENTE,
		                                        B.NOME 'CLIENTE',
                                                B.CPF,
                                                B.CIDADE,
                                                B.CONTRATO,
                                                C.IDCODUSUARIO,
		                                        C.Nome 'OPERADOR',
                                                C.LOGIN,
                                                C.LoginNetSMS

                                        FROM	GATLogAtendNaoTabuladosNETSMS A(NOLOCK),
		                                        GATCliente B(NOLOCK),
		                                        GATUsuario C(NOLOCK)

		
                                        WHERE	A.IDCODCLIENTE = B.IDCodCliente 
                                        AND		A.IDCODUSUARIO = C.IDCodUsuario
                                        AND     A.IDCODREGISTRO = " + sIdCodRegistro_Real;

                    SqlDataReader dr = cmd.ExecuteReader();

                    //Carrega os campos
                    if (dr.Read())
                    {
                        txtNome.Text = dr["CLIENTE"].ToString();
                        txtCPF.Text = dr["CPF"].ToString();
                        txtCidade.Text = dr["CIDADE"].ToString();
                        txtContrato.Text = dr["CONTRATO"].ToString();

                        txtNomeOperador.Text = dr["OPERADOR"].ToString();
                        txtLogin.Text = dr["LOGIN"].ToString();
                        txtMatricula.Text = dr["LoginNetSMS"].ToString();

                        //txtObs.Text = dr["OBS_NAO_TABULACAO"].ToString();    
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
    //protected void cmdSalvar_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //Verifica preenchimento
    //        if (txtObs.Text == "")
    //        {
    //            Erro("Preencha o Campo Observação!");
    //            return;
    //        }
    //        else //Atualiza dados
    //        {
    //            //Monta query
    //            gergatlink ogat = new gergatlink();
    //            string ssql = "";

    //            ssql = "UPDATE GATLogAtendNaoTabuladosNETSMS SET TABULADO = 1, DATA_TABULACAO = GETDATE(), OBS_NAO_TABULACAO = '" + txtObs.Text.Replace("'", "") + "', IDCODUSUARIO_ADM = " + Session["IdCodUsuario"].ToString() +
    //                    " WHERE IDCODREGISTRO = " + Session["idcodregistro"].ToString();

    //            //Executa o comando
    //            using (SqlConnection cn = ogat.abre_cn())
    //            {

    //                using (SqlCommand cmd = new SqlCommand())
    //                {
    //                    cmd.Connection = cn;
    //                    cmd.CommandText = ssql;
    //                    cmd.CommandType = CommandType.Text;

    //                    cmd.ExecuteNonQuery();
    //                }
    //            }
    //            // Limpa os Campos
    //            LimpaCampos();
    //            Session["alt"] = "";
    //            Session["idcodregistro"] = "";
    //            Response.Redirect("~/controle_naotabulados.aspx",false);
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        Erro(err.Message);
    //    }
    //}

    /// <summary>
    /// Cancela e limpa 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdCancelar_Click(object sender, EventArgs e)
    {
        LimpaCampos();
        Session["alt"] = "";
    }

    /// <summary>
    /// Limpa os campos do formulario
    /// </summary>
    public void LimpaCampos()
    {
        txtNome.Text = "";
        txtCPF.Text = "";
        txtCidade.Text = "";
        txtContrato.Text = "";

        txtNomeOperador.Text = "";
        txtLogin.Text = "";
        txtMatricula.Text = "";

        //txtObs.Text = "";
    }
}