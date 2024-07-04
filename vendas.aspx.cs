using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class vendas : System.Web.UI.Page
{
    //variaveis
    public string _scliente = "";
    public string _stipovenda = "";
    public string _stipocliente = "";

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
    /// Carrega_Lista_Filtro_TipoVenda
    /// </summary>
    /// <param name="svalor"></param>
    public void Carrega_Lista_Filtro_TipoVenda(string svalor)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sdml = "";

                    sdml = "select * from TEL_MP_GATTipoVenda (nolock) ";

                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbtipovenda_filtro.Items.Clear();

                    while (dr.Read())
                    {
                        oitem = new ListItem();
                        oitem.Text = dr["TipoVenda"].ToString();
                        oitem.Value = dr["idtipoVenda"].ToString();

                        cmbtipovenda_filtro.Items.Add(oitem);

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

                    sdml = "select * from TEL_MP_GATTipoCliente (nolock) ";

                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbtipocliente_filtro.Items.Clear();

                    while (dr.Read())
                    {
                        oitem = new ListItem();
                        oitem.Text = dr["TipoCliente"].ToString();
                        oitem.Value = dr["idcodtipocliente"].ToString();

                        cmbtipocliente_filtro.Items.Add(oitem);

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
            if (txtcliente_filtro.Text != "")
            {
                _scliente = _scliente + svig + txtcliente_filtro.Text;
                svig = ",";
            }

            //tipo venda
            svig = "";
            foreach (ListItem obj in cmbtipovenda_filtro.Items)
            {
                if (obj.Selected == true)
                {
                    _stipovenda = _stipovenda + svig + obj.Value;
                    svig = ",";
                }
            }

            //tipo cliente
            svig = "";
            foreach (ListItem obj in cmbtipocliente_filtro.Items)
            {
                if (obj.Selected == true)
                {
                    _stipocliente = _stipocliente + svig + obj.Value;
                    svig = ",";
                }
            }


            Rel_Vendas(_scliente, _stipovenda ,_stipocliente);

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
                e.Row.Cells[0].Text = "<a href='venda_dados.aspx?id=" + e.Row.Cells[1].Text + "' target='_self'><img src='imagens/alterar.png'/> </a>";
                e.Row.Cells[0].Text = "<a href='venda_dados.aspx?id=" + e.Row.Cells[1].Text + "&tipo=" + e.Row.Cells[2].Text + "' target='_self'><img src='imagens/alterar.png'/> </a>";

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
                Carrega_Lista_Filtro_TipoVenda("");
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
    /// Rel_Vendas
    /// </summary>
    /// <param name="scliente"></param>
    /// <param name="stipovenda"></param>
    /// <param name="stipocliente"></param>
    public void Rel_Vendas(string scliente, string stipovenda, string stipocliente)
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
		                            a.IdCodVenda 'ID',
		                            b.TipoVenda 'Tipo Venda',
                                    c.nome 'Cliente',
                                    d.TipoCliente 'Tipo Cliente',
                                    convert(varchar(10),a.DataInsercao,103) + ' ' + convert(varchar(10),a.DataInsercao,108)'Data da Venda'
                                      
                            from	TEL_MP_GATVenda a (nolock),
                                    TEL_MP_GATTipoVenda b (nolock),
                                    TEL_MP_GATCliente c(nolock),
                                    TEL_MP_GATTipoCliente d(nolock)

                            where   a.IdTipoVenda = b.IdTipoVenda
                            and     a.idcodcliente = c.idcodcliente 
                            and     c.idcodtipocliente = d.idcodtipocliente  ";


                    //cliente
                    if (scliente.Trim() != "")
                    {
                        ssql = ssql + " and a.nome like ('%" + scliente + "%')";
                    }

                    //tipo de venda
                    if (stipovenda.Trim() != "")
                    {
                        ssql = ssql + " and a.IdTipovenda in (" + stipovenda + ")";
                    }

                    //tipo de cliente
                    if (stipocliente.Trim() != "")
                    {
                        ssql = ssql + " and c.IdCodTipocliente in (" + _stipocliente + ")";
                    }

                    ssql = ssql + " order by c.nome";

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
            Response.Redirect("venda_dados.aspx", false);
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
            Response.AddHeader("content-disposition", "attachment;filename=stt_vendas.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(Session["txt_vendas"]);
            Response.Flush();
            Response.End();
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }


}