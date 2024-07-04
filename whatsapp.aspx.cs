using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class whatsapp : System.Web.UI.Page
{
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

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void cmdenviar_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }
}