using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class login : System.Web.UI.Page
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    /// <summary>
    /// PopMensagem
    /// </summary>
    /// <param name="stipo"></param>
    /// <param name="stitulo"></param>
    /// <param name="smensagem"></param>
    public void PopMensagem(string stipo, string stitulo, string smensagem)
    {
        Session["tipo_alerta"] = stipo;
        Session["titulo_alerta"] = stitulo;
        Session["mensagem_alerta"] = smensagem;
    }


    /// <summary>
    /// cmdlogin_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdlogin_Click(object sender, EventArgs e)
    {
        try
        {
            gergatlink obj = new gergatlink();
            string ssql = "";
            string sperm = "";

            string ssenha = "";
            
            Session["titulo_alerta"] = "";
            Session["mensagem_alerta"] = "";
            Session["tipo_alerta"] = "";

            //Verifica o login
            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    //select
                    ssql = @"select *
                            from    TEL_MP_gatusuario a (nolock)
                            where   a.Habilitado = 1";
                    ssql = ssql + " and     a.Login = '" + txtusername.Text.Replace("'","") + "'";
                    ssql = ssql + " and     a.Senha = '" + txtpassword.Text.Replace("'", "") + "'";
                    ssql = ssql + @"and     a.IDCodUsuario in (
                                    select  IDCodUsuario

                                    from TEL_MP_GATUsuarioPerfil b (nolock)
                                    where b.IDCodPerfil in (
                                            select  IDCodPerfil

                                            from TEL_MP_GATPerfil c (nolock)
                                            where c.Habilitado = 1

                                            and     c.IDPermissao In(1, 2, 3, 4)
				                            )
		                            )";

                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = ssql;

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        //variaveis de sessao
                        Session["idcodusuario"] = dr["idcodusuario"].ToString();
                        Session["nomeusuario"] = dr["nome"].ToString();

                        //selecionad das do perfil
                        using (SqlConnection cn2 = obj.abre_cn())
                        {
                            using (SqlCommand cmd2 = new SqlCommand())
                            {

                                ssql = @"   select	* 
                                            from	TEL_MP_GATPerfil c (nolock)
                                            where	c.Habilitado=1
                                            and		c.IDPermissao In (1,2,3,4)
                                            and		c.IDCodPerfil in (
		                                            select IDCodPerfil 
		                                            from	TEL_MP_GATUsuarioPerfil d (nolock)";
                                ssql = ssql + " where d.IDCodUsuario = " + dr["idcodusuario"].ToString();
                                ssql = ssql + ")";


                                cmd2.Connection = cn2;
                                cmd2.CommandType = CommandType.Text;
                                cmd2.CommandText = ssql;

                                SqlDataReader dr2 = cmd2.ExecuteReader();

                                if (dr2.Read())
                                {

                                    Session["idcodperfil"] = dr2["idcodperfil"].ToString();
                                    Session["permissaoperfil"] = dr2["idpermissao"].ToString();

                                }
                                dr2.Close();

                            }


                        }
                        
                        Response.Redirect("dashboard.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        dr.Close();

                        PopMensagem("error", "Erro", "Login ou Senha Inválidos!");

                    }

                }
            }

        }
        catch (Exception err)
        {
            PopMensagem("error", "Erro", err.Message);
        }
    }
}