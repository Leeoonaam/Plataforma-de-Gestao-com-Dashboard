using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPageGATLink : System.Web.UI.MasterPage
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
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //Verifica variavel de sessão
        if (Session["idcodusuario"].ToString() == "")
        {
            Erro("Tempo de Sessão Expirada! Realize o login novamente!");
            return;
        }
        //if (Session["idcodusuario"] == "")
        //{
        //    //variaveis de sessao
        //    Session["idcodusuario"] = "";
        //    Session["nomeusuario"] = "";
        //    Session["idcodperfil"] = "";

        //    Response.Redirect("login.aspx", false);
        //    Context.ApplicationInstance.CompleteRequest();
        //}
    }

    /// <summary>
    /// cmdsair_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdsair_Click(object sender, EventArgs e)
    {
        //variaveis de sessao
        Session["idcodusuario"] = "";
        Session["nomeusuario"] = "";
        Session["idcodperfil"] = "";

        Response.Redirect("login.aspx", false);
        Context.ApplicationInstance.CompleteRequest();

    }
}
