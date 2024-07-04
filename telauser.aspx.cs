using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

public partial class telauser : System.Web.UI.Page
{

    static int iUs = 0;

    //[WebMethod()]
    //public static void fechou()
    //{

    //    //desativa captura de tela
    //    gergatlink obj = new gergatlink();

    //    using (SqlConnection cn = obj.abre_cn())
    //    {
    //        SqlCommand cmd = new SqlCommand();
    //        cmd.Connection = cn;
    //        cmd.CommandType = CommandType.Text;
    //        cmd.CommandText = "update gatusuario set hab_captura_tela=0 where idcodusuario=" + iUs.ToString();

    //        cmd.ExecuteNonQuery();

    //    }

    //}

    string _iduser = "";

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
        try
        {
            if (IsPostBack == false)
            {

                foreach (string item in Request.QueryString)
                {
                    //valores
                    switch (item)
                    {
                        case "iduser":
                            _iduser = Request.QueryString[item].ToString();
                            break;
                    }
                }

                iUs = Convert.ToInt32(_iduser);

                gergatlink obj = new gergatlink();

                using (SqlConnection cn = obj.abre_cn())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update gatusuario set hab_captura_tela = 1 where idcodusuario=" + iUs.ToString();

                    cmd.ExecuteNonQuery();

                }


                //carrega
                h_iduser.Value = _iduser;
                CarregaImagem(_iduser);
            }

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// CarregaImagem
    /// </summary>
    /// <param name="iduser"></param>
    public void CarregaImagem(string iduser)
    {
        try
        {
            
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select nome,tela from gatusuario (nolock) where idcodusuario=" + iduser;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {

                    lblusuario.Text = "Operador(a): " + dr["nome"].ToString();

                    byte[] bytes = (byte[])dr["tela"];
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    Image1.ImageUrl = "data:image/png;base64," + base64String;
                    Image1.Width = 1000;
                    //Image1.Height = 800;
                }
                dr.Close();

            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }


    /// <summary>
    /// Timer1_Tick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        foreach (string item in Request.QueryString)
        {
            //valores
            switch (item)
            {
                case "iduser":
                    _iduser = Request.QueryString[item].ToString();
                    break;
            }
        }

        CarregaImagem(_iduser);
    }
}