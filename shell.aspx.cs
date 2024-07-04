using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class shell : System.Web.UI.Page
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
        try
        {
            
            if (IsPostBack == false)
            {
                CarregaDados();
            }

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// cmdsalvar_shell_adm_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdsalvar_shell_adm_Click(object sender, EventArgs e)
    {
        try
        {
            gergatlink obj = new gergatlink();

            if (txtadm.Text == "")
            {
                Erro("Informe o caminho do SHELL ADM");
                return;
            }

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update gatshell set caminho='" + txtadm.Text.Replace("'","") + "' where tipo=1" ;

                    cmd.ExecuteNonQuery();

                }
                
            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// cmdsalvar_shell_fe_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdsalvar_shell_fe_Click(object sender, EventArgs e)
    {
        try
        {
            gergatlink obj = new gergatlink();

            if (txtfe.Text == "")
            {
                Erro("Informe o caminho do SHELL FRONT END");
                return;
            }

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update gatshell set caminho='" + txtfe.Text.Replace("'", "") + "' where tipo=2";

                    cmd.ExecuteNonQuery();

                }

            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// CarregaDados
    /// </summary>
    public void CarregaDados()
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
                    cmd.CommandText = "select * from gatshell (nolock)";

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        if (dr["tipo"].ToString() == "1")
                        {
                            txtadm.Text = dr["caminho"].ToString();
                        }
                        else
                        {
                            txtfe.Text = dr["caminho"].ToString();
                        }
                        
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


}