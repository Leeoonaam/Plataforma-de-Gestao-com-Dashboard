using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

public partial class telasatendimento : System.Web.UI.Page
{
    //variaveis
    string _idcliente = "";

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
        //idcliente
        try
        {
          
            
            if (IsPostBack == false)
            {

                //seleciona as variaveis
                foreach (string item in Request.QueryString)
                {
                    //valores
                    switch (item)
                    {
                        case "idcliente":
                            _idcliente = Request.QueryString[item].ToString();
                            break;


                    }
                }

                CarregarTelas(_idcliente);

            }

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// CarregarTelas
    /// </summary>
    /// <param name="sID"></param>
    public void CarregarTelas(string sID)
    {
        try
        {
          

            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select a.*,b.Nome  from GATTELAS_CLIENTE a (nolock), GATUsuario b (nolock) where a.IDCODUSUARIO= b.IDCodUsuario and a.IDCODCLIENTE=" + sID + " order by DATA ";

                SqlDataReader dr = cmd.ExecuteReader();

                Image Image1;

                while (dr.Read())
                {
                    Image1 = new Image();
                    lblusuario.Text = "Operador(a): " + dr["nome"].ToString();

                    byte[] bytes = (byte[])dr["tela"];
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    Image1.ImageUrl = "data:image/png;base64," + base64String;
                    Image1.Width = 1000;

                    Panel1.Controls.Add(Image1);

                }
                dr.Close();

            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }


}