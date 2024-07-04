using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class detalhes_pontos : System.Web.UI.Page
{
    //campos filtro
    int itotdados = 0;
    string _scampanha = "";
    string _sperfil = "";
    string _sequipe = "";
    string _susuario = "";
    string _sstatus = "";
    string _sdtini = "";
    string _sdtfim = "";
    string _statusponto = "";

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
    /// AplicaFiltro
    /// </summary>
    public void AplicaFiltro()
    {
        try
        {
            //data padrao
            if (_sdtini == "")
            {
                _sdtini = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " 00:00:00";
                _sdtfim = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " 23:59:59";
            }


            Rel_DetalhesPontos(_sperfil, _susuario, _sdtini, _sdtfim, _statusponto);

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
            itotdados = 0;
            if (IsPostBack == false)
            {
                //seleciona as variaveis
                foreach (string item in Request.QueryString)
                {
                    //valores
                    switch (item)
                    {
                        case "_sperfil":
                            _sperfil = Request.QueryString[item].ToString();
                            break;

                        case "_sequipe":
                            _sequipe = Request.QueryString[item].ToString();
                            break;

                        case "_susuario":
                            _susuario = Request.QueryString[item].ToString();
                            break;

                        case "_sstatus":
                            _sstatus = Request.QueryString[item].ToString();
                            break;

                        case "_sdtini":
                            _sdtini = Request.QueryString[item].ToString();
                            break;

                        case "_sdtfim":
                            _sdtfim = Request.QueryString[item].ToString();
                            break;

                        case "_statusponto":
                            _statusponto = Request.QueryString[item].ToString();
                            break;

                    }
                }

                AplicaFiltro();

            }

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
            gergatlink obj = new gergatlink();
            //Build the Text file data.
            string txt = string.Empty;

            for (int i = 1; i < gdw_dados.Columns.Count - 1; i++)
            {
                //Add the Header row for Text file.
                txt += obj.TrataHTMLASCII(gdw_dados.Columns[i].HeaderText).Replace("&nbsp;", "") + ";";

            }

            //Add new line.
            txt += "\r\n";

            for (int l = 0; l < gdw_dados.Rows.Count; l++)
            {
                for (int i = 1; i < gdw_dados.Columns.Count - 1; i++)
                {
                    //Add the Data rows.
                    txt += obj.TrataHTMLASCII(gdw_dados.Rows[l].Cells[i].Text).Replace("&nbsp;", "") + ";";
                }

                //Add new line.
                txt += "\r\n";
            }

            //Download the Text file.
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=stt_detalhes_pontos_aproximidade.csv");
            Response.Charset = "UTF-8";
            Response.ContentType = "application/text";
            Response.Output.Write(txt);
            Response.Flush();
            Response.End();
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }


    /// <summary>
    /// Rel_DetalhesVenda
    /// </summary>
    /// <param name="scampanha"></param>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="sproduto"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_DetalhesPontos(string sperfil, string susuario, string dtini, string dtfim, string sstatus)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    ssql = @"select	* from vw_Detalhes_Pontos_Aproximidade_MP_GEO a (nolock) where 1=1";

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and a.idcodperfil in (" + sperfil + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and a.idcodusuario in (" + susuario + ")";
                    }

                    //status rota
                    if (sstatus.Trim() != "")
                    {
                        ssql = ssql + " and a.idcodstatus in (" + sstatus + ")";
                    }

                    //data
                    //if (dtini.Trim() != "")
                    //{
                    //    ssql = ssql + " and a.datainsercao between '" + dtini + "' and '" + dtfim + "'";
                    //}


                    SqlDataAdapter da = new SqlDataAdapter(ssql, cn);

                    DataSet ds = new DataSet();
                    da.Fill(ds);


                    Session["ds"] = ds;

                    gdw_dados.DataSource = ds;
                    gdw_dados.DataBind();

                    lbltotal.Text = "<b>Total de Registros : " + itotdados.ToString() + "</b>";
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
            gergatlink obj = new gergatlink();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                itotdados++;
            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }


    /// <summary>
    /// gdw_dados_PageIndexChanging
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdw_dados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            itotdados = 0;
            gdw_dados.PageIndex = e.NewPageIndex;
            gdw_dados.DataSource = Session["ds"];
            gdw_dados.DataBind();

            lbltotal.Text = "<b>Total de Registros : " + itotdados.ToString() + "</b>";

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }
}