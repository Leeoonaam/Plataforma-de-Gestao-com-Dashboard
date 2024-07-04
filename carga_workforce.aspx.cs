using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;

public partial class carga_workforce : System.Web.UI.Page
{

    public string sstyle = "";
    long ltot = 0;


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

        if (Session["permissaoperfil"] == "")
        {
            //variaveis de sessao
            Session["idcodusuario"] = "";
            Session["nomeusuario"] = "";
            Session["idcodperfil"] = "";

            Response.Redirect("login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();

        }
        else
        {
            if (IsPostBack == false)
            {
                Rel_Dados();
            }
        }
    }

    /// <summary>
    ///  cmdImportar_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdImportar_Click(object sender, EventArgs e)
    {
        try
        {

            //VERIFICA SE SELECIONOU UM ARQUIVO
            if (FileUpload_IMP.HasFile)
            {

                //verifica se existe registros
                if (!String.IsNullOrEmpty(FileUpload_IMP.FileName))
                {

                    //Os bytes do arquivo foram recebidos pelo servidor.
                    if (FileUpload_IMP.FileBytes.Count() > 0)
                    {

                        // Create an instance of StreamReader to read from a file.
                        // The using statement also closes the StreamReader.
                        using (StreamReader sr = new StreamReader(FileUpload_IMP.FileContent))
                        {

                            //obj ger gat
                            gergatlink obj = new gergatlink();

                            //Variaveis da receber dados da carga
                            string sMATRICULA = "";
                            string sORGANIZACAO = "";
                            string sHENTRADA = "";
                            string sHSAIDA = "";
                            string sDURACAO = "";
                            string sTURNO = "";
                            string sTSEM = "";
                            string sTSAB = "";
                            string sTDOM = "";
                            string sTFER = "";
                            string sLOGIN = "";
                            string sDACLOGIN = "";
                            string sSUPERIOR = "";
                            string sSITE = "";
                            string sNOME_ARQ_CARGA = "";

                            string sIDRegistro = "";


                            //abre con
                            using (SqlConnection cn = obj.abre_cn())
                            {

                                //
                                // ZERA AS INFORMACOES
                                // 
                                using (SqlCommand cmd2 = new SqlCommand())
                                {
                                    cmd2.Connection = cn;
                                    cmd2.CommandType = CommandType.Text;
                                    cmd2.CommandText = "truncate table GATWORKFORCE";

                                    cmd2.ExecuteNonQuery();

                                }



                                //Para inserir a linha
                                string line;

                                //Pega a primeira linha.
                                int n = 0;

                                //Pega o nome do arquivo um vez.
                                int i = 0;

                                //Para ler a linhas do streamReader
                                while ((line = sr.ReadLine()) != null)
                                {
                                    if (n > 0)
                                    {
                                        //quebra array
                                        string[] dados = line.Split(new Char[] { ';' });


                                        //alimenta variaveis de valor
                                        sMATRICULA = dados[0].ToString();
                                        sORGANIZACAO = dados[1].ToString();
                                        sHENTRADA = dados[2].ToString();
                                        sHSAIDA = dados[3].ToString();
                                        sDURACAO = dados[4].ToString();
                                        sTURNO = dados[5].ToString();
                                        sTSEM = dados[6].ToString();
                                        sTSAB = dados[7].ToString();
                                        sTDOM = dados[8].ToString();
                                        sTFER = dados[9].ToString();
                                        sLOGIN = dados[10].ToString();
                                        sDACLOGIN = dados[11].ToString();
                                        sSUPERIOR = dados[12].ToString();
                                        sSITE = dados[13].ToString();

                                        sNOME_ARQ_CARGA = FileUpload_IMP.FileName.ToString();


                                        //chama a proc de insercao
                                        using (SqlCommand cmd = new SqlCommand())
                                        {
                                            cmd.Connection = cn;
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.CommandText = "SPGATCargaWorkforce";

                                            cmd.Parameters.Add("@MATRICULA", SqlDbType.VarChar, 50).Value = sMATRICULA.TrimEnd();
                                            cmd.Parameters.Add("@ORGANIZACAO", SqlDbType.VarChar, 500).Value = sORGANIZACAO.TrimEnd();
                                            cmd.Parameters.Add("@HENTRADA", SqlDbType.VarChar, 50).Value = sHENTRADA.TrimEnd();
                                            cmd.Parameters.Add("@HSAIDA", SqlDbType.VarChar, 50).Value = sHSAIDA.TrimEnd();
                                            cmd.Parameters.Add("@DURACAO", SqlDbType.VarChar, 50).Value = sDURACAO.TrimEnd();
                                            cmd.Parameters.Add("@TURNO", SqlDbType.VarChar, 50).Value = sTURNO.TrimEnd();
                                            cmd.Parameters.Add("@TSEM", SqlDbType.Int).Value = Convert.ToInt32(sTSEM);
                                            cmd.Parameters.Add("@TSAB", SqlDbType.Int).Value = Convert.ToInt32(sTSAB);
                                            cmd.Parameters.Add("@TDOM", SqlDbType.Int).Value = Convert.ToInt32(sTDOM);
                                            cmd.Parameters.Add("@TFER", SqlDbType.Int).Value = Convert.ToInt32(sTFER);
                                            cmd.Parameters.Add("@LOGIN", SqlDbType.VarChar, 50).Value = sLOGIN.TrimEnd();
                                            cmd.Parameters.Add("@DACLOGIN", SqlDbType.VarChar, 50).Value = sDACLOGIN.TrimEnd();
                                            cmd.Parameters.Add("@SUPERVISOR", SqlDbType.VarChar, 500).Value = sSUPERIOR.TrimEnd();
                                            cmd.Parameters.Add("@SITE", SqlDbType.Int).Value = Convert.ToInt32(sSITE);
                                            cmd.Parameters.Add("@NOME_ARQUIVO", SqlDbType.VarChar, 200).Value = sNOME_ARQ_CARGA.TrimEnd();

                                            cmd.Parameters.Add("@IDUSER", SqlDbType.Int).Value = Convert.ToInt32(Session["idcodusuario"].ToString());

                                            cmd.Parameters.Add("@STATUS", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                                            cmd.ExecuteNonQuery();

                                            sIDRegistro = cmd.Parameters["@STATUS"].Value.ToString();

                                        }


                                    }
                                    n++;//Para deixar de pular linhas
                                }
                            }


                        }

                        Rel_Dados();

                        //resultado
                        Panel_resposta.Visible = true;
                        sstyle = "alert-success";
                        lblMensagemCarga.Visible = true;
                        lblMensagemCarga.Text = "Carga Realizada com Sucesso !";

                    }
                    else
                    {
                        Erro("Arquivo Vazio.");
                    }

                }
                else
                {
                    Erro("Erro ao enviar arquivo para o servidor");
                }

            }
            else
            {
                Erro("Selecione o arquivo");
            }

        }
        catch (Exception err)
        {

            Erro(err.Message);
        }


    }


    /// <summary>
    /// Rel_Dados
    /// </summary>
    public void Rel_Dados()
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
                    ssql = @"select	a.*,
		                                b.nome 'usuariocarga'
                                from	gatworkforce a (nolock),
		                                GATUsuario b (nolock)
                                where	a.idcodusuario_carga = b.idcodusuario";



                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    gdw_dados.DataSource = dr;
                    gdw_dados.DataBind();

                    dr.Close();


                    lbltotal.Text = "Total de Registros : " + ltot.ToString();

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


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //conta as linhas
                ltot++;

                if (e.Row.Cells[8].Text.Trim() == "1")
                {
                    e.Row.Cells[8].Text = "<img src='imagens/disponivel.png'/>";
                }
                else
                {
                    e.Row.Cells[8].Text = "<img src='imagens/pausa.png'/>";
                }

                if (e.Row.Cells[9].Text.Trim() == "1")
                {
                    e.Row.Cells[9].Text = "<img src='imagens/disponivel.png'/>";
                }
                else
                {
                    e.Row.Cells[9].Text = "<img src='imagens/pausa.png'/>";
                }

                if (e.Row.Cells[10].Text.Trim() == "1")
                {
                    e.Row.Cells[10].Text = "<img src='imagens/disponivel.png'/>";
                }
                else
                {
                    e.Row.Cells[10].Text = "<img src='imagens/pausa.png'/>";
                }

                if (e.Row.Cells[11].Text.Trim() == "1")
                {
                    e.Row.Cells[11].Text = "<img src='imagens/disponivel.png'/>";
                }
                else
                {
                    e.Row.Cells[11].Text = "<img src='imagens/pausa.png'/>";
                }


            }

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }
}