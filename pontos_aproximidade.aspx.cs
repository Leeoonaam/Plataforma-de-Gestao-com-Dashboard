using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;


public partial class pontos_aproximados : System.Web.UI.Page
{
    ///Variaveis
    public string _srota = "";
    int TotLinhas = 0;

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
    public void Carrega_Lista_Filtro_Rota(string svalor)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sdml = "";

                    sdml = "select * from TEL_MP_Rota (nolock) where habilitado=1 ";
                    sdml = sdml + " order by rota";

                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbRota_filtro.Items.Clear();
                    while (dr.Read())
                    {
                        oitem = new ListItem();
                        oitem.Text = dr["rota"].ToString();
                        oitem.Value = dr["idcodrota"].ToString();

                        cmbRota_filtro.Items.Add(oitem);

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

            //Rota
            svig = "";
            foreach (ListItem obj in cmbRota_filtro.Items)
            {
                if (obj.Selected == true)
                {
                    _srota = _srota + svig + obj.Value;
                    svig = ",";
                }
            }

            Rel_Rotas(_srota);

            lbl_tot.Text = TotLinhas.ToString();

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
            //Download the Text file.
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=txt_pontosdeaproximidade.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(Session["txt_pontosdeaproximidade"]);
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
            int ival = 0;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].CssClass = "col_acao";
                e.Row.Cells[1].CssClass = "col_acao";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].CssClass = "col_acao";
                e.Row.Cells[1].CssClass = "col_acao";


                //qtd de linhas
                TotLinhas++;

                //Alterar
                e.Row.Cells[0].Text = "<a href='pontos_aproximidade_dados.aspx?id=" + e.Row.Cells[2].Text + "' target='_self'><img src='imagens/alterar.png'/> </a>";

                //Excluir
                e.Row.Cells[1].Text = @"<a href=""javascript:deleta('pontos_aproximidade_dados.aspx?acao=del&id=";
                e.Row.Cells[1].Text = e.Row.Cells[1].Text + e.Row.Cells[2].Text + @"')"" ><img src=""imagens/delete.png""/> </a>";
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

            if (IsPostBack == false)
            {
                Carrega_Lista_Filtro_Rota("");
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
    /// <param name="salerta"></param>
    /// <param name="splace"></param>
    /// <param name="sequipamento"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_Rotas(string sgrupo)
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
                    ssql = @"SELECT	

		                            'EDITAR'='',
		                            'EXCLUIR'='',
		                            A.IDCodGrupo 'ID',
		                            A.Grupo
		

                            FROM	TEL_MP_GATGrupoPontoAproximidade A (NOLOCK)
		
                            WHERE	A.Habilitado = 1";


                    //nome
                    if (sgrupo.Trim() != "")
                    {
                        ssql = ssql + " and a.grupo like ('%" + sgrupo + "%')";
                    }

                    ssql = ssql + " order by a.grupo";

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
    /// cmdnovo_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdnovo_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("pontos_aproximidade_dados.aspx", false);
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    protected void gdw_dados_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}