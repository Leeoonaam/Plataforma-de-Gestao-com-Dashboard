using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class clientes : System.Web.UI.Page
{
    //variaveis
    public string _snome = "";
    public string _stipo = "";
    public string _scpfcnpj = "";

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
    /// Carrega_Lista_Filtro_TipoCliente
    /// </summary>
    /// <param name="svalor"></param>
    public void Carrega_Lista_Filtro_TipoCliente(string svalor)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sdml = "";

                    sdml = "select * from TEL_MP_GATTipoCliente (nolock) where habilitado = 1";
                    sdml = sdml + " order by TipoCliente";

                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbtipociente_filtro.Items.Clear();

                    while (dr.Read())
                    {
                        oitem = new ListItem();
                        oitem.Text = dr["TipoCliente"].ToString();
                        oitem.Value = dr["idcodtipocliente"].ToString();

                        cmbtipociente_filtro.Items.Add(oitem);

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

            //nome
            svig = "";
            if (txtnome_filtro.Text != "")
            {
                _snome = _snome + svig + txtnome_filtro.Text;
                svig = ",";
            }

            //cnpcnpj
            svig = "";
            if (txtcpfcnpj_filtro.Text != "")
            {
                _scpfcnpj = _scpfcnpj + svig + txtcpfcnpj_filtro.Text;
                svig = ",";
            }

            //tipo
            svig = "";
            foreach (ListItem obj in cmbtipociente_filtro.Items)
            {
                if (obj.Selected == true)
                {
                    _stipo = _stipo + svig + obj.Value;
                    svig = ",";
                }
            }

            Rel_Clientes(_snome, _stipo, _scpfcnpj);

            lbl_tot.Text = TotLinhas.ToString();

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

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].CssClass = "col_acao";
                e.Row.Cells[1].CssClass = "col_acao";

                Session["txt_clientes"] = "";

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (i > 1)
                    {
                        Session["txt_clientes"] += obj.TrataHTMLASCII(e.Row.Cells[i].Text.Replace("&nbsp;", "")) + ";";
                    }
                }
                    
                //Add new line.
                Session["txt_clientes"] += "\r\n";

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].CssClass = "col_acao";
                e.Row.Cells[1].CssClass = "col_acao";

                //qtd de linhas
                TotLinhas++;

                //Alterar
                e.Row.Cells[0].Text = "<a href='clientes_dados.aspx?id=" + e.Row.Cells[2].Text + "' target='_self'><img src='imagens/alterar.png'/> </a>";
                e.Row.Cells[0].Text = "<a href='clientes_dados.aspx?id=" + e.Row.Cells[2].Text + "&tipo=" + e.Row.Cells[4].Text + "' target='_self'><img src='imagens/alterar.png'/> </a>";

                //Excluir
                e.Row.Cells[1].Text = @"<a href=""javascript:deleta('clientes_dados.aspx?acao=del&id=";
                e.Row.Cells[1].Text = e.Row.Cells[1].Text + e.Row.Cells[2].Text + @"')"" ><img src=""imagens/delete.png""/> </a>";

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (i > 1)
                    {
                        Session["txt_clientes"] += obj.TrataHTMLASCII(e.Row.Cells[i].Text.Replace("&nbsp;", "")) + ";";
                    }
                }

                //Add new line.
                Session["txt_clientes"] += "\r\n";

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
                Carrega_Lista_Filtro_TipoCliente("");
                AplicaFiltro();

            }

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }

    }


    /// <summary>
    /// Rel_Clientes
    /// </summary>
    /// <param name="snome"></param>
    /// <param name="stipo"></param>
    /// <param name="scpfcnpj"></param>
    /// <param name="semail"></param>
    /// <param name="snummaquina"></param>
    public void Rel_Clientes(string snome, string stipo, string scpfcnpj)
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
                    ssql = @"select	'Editar'='', 
		                            'Excluir'='',
		                            a.IdCodCliente 'ID',
		                            a.Nome,
		                            b.TipoCliente 'Tipo'
                                      
                            from	TEL_MP_GATCliente a (nolock),
                                    TEL_MP_GATTipoCliente b (nolock)

                            where   a.IdCodTipoCliente = b.IdCodTipoCliente
                            and		a.habilitado=1 ";


                    //nome
                    if (snome.Trim() != "")
                    {
                        ssql = ssql + " and a.nome like ('%" + snome + "%')";
                    }

                    //tipo de cliente
                    if (stipo.Trim() != "")
                    {
                        ssql = ssql + " and a.IdCodTipoCliente in (1,2)";
                    }

                    //cpf / cnpj
                    if (scpfcnpj.Trim() != "")
                    {
                        ssql = ssql + " and a.CNPJ like ('%" + scpfcnpj + "%')";
                    }

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
    /// cmdnovo_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdnovo_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("clientes_dados.aspx", false);
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// gdw_dados_RowCommand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdw_dados_RowCommand(object sender, GridViewCommandEventArgs e)
    {

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
            Response.AddHeader("content-disposition", "attachment;filename=stt_clientes.txt");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(Session["txt_clientes"]);
            Response.Flush();
            Response.End();
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }
}