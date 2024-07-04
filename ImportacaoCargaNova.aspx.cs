using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;

public partial class ImportacaoCargaNova : System.Web.UI.Page
{
    public int totlinhas;

    //    public string sstyle = "";
    //    long ltot = 0;

    //    //soma o total de registros que entrar
    //    int total = 0;

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

    // <summary>
    /// MSG_SUCCESS
    /// </summary>
    /// <param name="smsg"></param>
    void MSG_SUCCESS(string smsg)
    {
        Session["tipo_alerta"] = "success";
        Session["titulo_alerta"] = "Ok";
        Session["mensagem_alerta"] = smsg;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// cmdImportar_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdImportar_Click(object sender, EventArgs e)
    {
        importarArqu();
    }

    /// <summary>
    /// importação dos registros
    /// </summary>
    public void importarArqu()
    {
        try
        {
            gergatlink obj = new gergatlink();
            string sNOME_ARQ_CARGA = "";
            string sIdrota = "";
            string iQtdRotas = "";

            //Captura a quantidade de rotas do arquivo
            iQtdRotas = cmbqtdrotas.SelectedValue;

            //VERIFICA SE SELECIONOU UM ARQUIVO
            if (FileUpload_IMP2.HasFile)
            {
                //verifica se selecionou a quantidade de rotas
                if (cmbqtdrotas.SelectedValue == "")
                {
                    Erro("Seleciona a quantidade de Rotas para esse arquivo!");
                }
                else
                {
                    //verifica se existe registros
                    if (!String.IsNullOrEmpty(FileUpload_IMP2.FileName))
                    {
                        //captura nome do arquivo
                        sNOME_ARQ_CARGA = FileUpload_IMP2.FileName.ToString();

                        //Os bytes do arquivo foram recebidos pelo servidor.
                        if (FileUpload_IMP2.FileBytes.Count() > 0)
                        {
                            // Create an instance oaf StreamReader to read from a file.
                            using (StreamReader sr = new StreamReader(FileUpload_IMP2.FileContent))
                            {
                                //Variaveis da receber dados da carga
                                string sEndereco = "";
                                string sLatitude = "";
                                string sLongitude = "";
                                string sNome = "";
                                string ssql = "";
                                string sidUser = "";
                                string ID_Rota = "";
                                int isequencia = 1;

                                //abre con
                                using (SqlConnection cn = obj.abre_cn())
                                {
                                    string line; //Para inserir a linha

                                    //intancia lista Array
                                    List<string> arr = new List<string>();

                                    //Para ler a linhas do streamReader
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        //verifica preenchimento e adiciona linha ao array
                                        if (line != "")
                                        {
                                            arr.Add(line); //alimenta array
                                            totlinhas = totlinhas + 1; //quantidade de linhas do arquivo
                                        }
                                    }

                                    //Verifica quantidade de enderecos x rotas
                                    if (Convert.ToInt32(totlinhas) < Convert.ToInt32(iQtdRotas))
                                    {
                                        Erro("Quantidade de Endereço é menor que a quantidade de Rotas! Este arquivo contém " + totlinhas + " endereços");
                                        return;
                                    }

                                    //Verifica quantidade de rotas x enderecos
                                    if (Convert.ToInt32(iQtdRotas) > Convert.ToInt32(totlinhas))
                                    {
                                        Erro("Quantidade de Rotas é maior que a quantidade de Endereços! Este arquivo contém " + totlinhas + " endereços");
                                        return;
                                    }

                                    //divide a quantidade de linhas(enderecos) por Rotas
                                    int divlinha = (Convert.ToInt32(totlinhas) / Convert.ToInt32(iQtdRotas));

                                    //variaveis
                                    int numrota = 1;
                                    int ilinha = 0;
                                    int ilinhaatual = 0;
                                    bool bnovarota = true;


                                    //loop de inserção de dados
                                    while (ilinha < divlinha)
                                    {
                                        //verifica inserção de uma nova rota
                                        if (bnovarota == true)
                                        {
                                            //Insere a rota
                                            using (SqlConnection cn2 = obj.abre_cn())
                                            {
                                                using (SqlCommand cmd = new SqlCommand())
                                                {
                                                    cmd.Connection = cn2;
                                                    cmd.CommandText = "TEL_MP_InsereRota";
                                                    cmd.CommandType = CommandType.StoredProcedure;

                                                    cmd.Parameters.Add("@Rota", SqlDbType.VarChar).Value = numrota + "º " + sNOME_ARQ_CARGA;
                                                    cmd.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;

                                                    cmd.ExecuteNonQuery();

                                                    sIdrota = cmd.Parameters["@ID"].Value.ToString();

                                                    bnovarota = false;
                                                    isequencia = 1; //renova sequencia

                                                    //muda o numero da rota
                                                    if (numrota < Convert.ToInt32(iQtdRotas))
                                                    {
                                                        numrota = numrota + 1;
                                                    }
                                                    else
                                                    {
                                                        numrota = numrota + 1;
                                                    }

                                                }
                                            }
                                        }

                                        //DADOS
                                        string[] dados = arr[ilinhaatual].Split(new Char[] { ';' });

                                        sLatitude = dados[0].Replace(",", "");
                                        sLongitude = dados[1];
                                        sEndereco = dados[2];
                                        sNome = dados[3];

                                        //VERIFICA PARA CAPTURA DO USUARIO PARA ASSOCIAR A ROTA
                                        if (sNome != "")
                                        {
                                            using (SqlConnection cn3 = obj.abre_cn())
                                            {
                                                using (SqlCommand cmd = new SqlCommand())
                                                {
                                                    //monta select
                                                    ssql = @"select	top 1 * from TEL_MP_GATUSUARIO(nolock)where 1=1";
                                                    ssql = ssql + " and NOME = '" + sNome + "'";

                                                    cmd.Connection = cn3;
                                                    cmd.CommandText = ssql;
                                                    cmd.CommandType = CommandType.Text;

                                                    SqlDataReader dr = cmd.ExecuteReader();


                                                    while (dr.Read())
                                                    {
                                                        sidUser = dr["IDCODUSUARIO"].ToString();
                                                    }

                                                }
                                            }
                                        }
                                        
                                        //VERIFICA SE O USUARIO NÃO EXISTIR, INSERIR OS ROTEIROS SEM VINCULAR AO USUARIO
                                        if (sidUser != "")
                                        {
                                            //VERIFICA SE O USUARIO JA FOI INSERIDO NA ROTA
                                            using (SqlConnection cnx = obj.abre_cn())
                                            {
                                                using (SqlCommand cmd = new SqlCommand())
                                                {
                                                    //monta select
                                                    ssql = @"select	top 1 * from TEL_MP_Rota_Usuario(nolock)where idcodrota = " + sIdrota + " and idcodusuario = " + sidUser;

                                                    cmd.Connection = cnx;
                                                    cmd.CommandText = ssql;
                                                    cmd.CommandType = CommandType.Text;

                                                    SqlDataReader dr = cmd.ExecuteReader();

                                                    if (dr.Read())
                                                    {
                                                        sidUser = dr["IDCODUSUARIO"].ToString();
                                                    }
                                                    else
                                                    {
                                                        //INSERE ROTA PARA USUÁRIO
                                                        using (SqlConnection cn4 = obj.abre_cn())
                                                        {
                                                            using (SqlCommand cmd2 = new SqlCommand())
                                                            {
                                                                cmd2.Connection = cn4;
                                                                cmd2.CommandType = CommandType.Text;
                                                                cmd2.CommandText = "insert into TEL_MP_Rota_Usuario(IdCodrota,Idcodusuario)values(" + sIdrota + ",'" + sidUser + "')";

                                                                cmd2.ExecuteNonQuery();
                                                            }
                                                        }
                                                    }

                                                }
                                            }
                                        }

                                        

                                        //IMPORTA ROTEIRO
                                        using (SqlConnection cn5 = obj.abre_cn())
                                        {
                                            using (SqlCommand cmd = new SqlCommand())
                                            {
                                                cmd.Connection = cn5;
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                cmd.CommandText = "TEL_MP_InsereRoteiro";

                                                cmd.Parameters.Add("@IDCodRota", SqlDbType.VarChar).Value = sIdrota;

                                                //verifica sequencia para remoçao da virgula no inicio
                                                if (isequencia == 1)
                                                {
                                                    cmd.Parameters.Add("@Latitude", SqlDbType.VarChar).Value = sLatitude.Trim();
                                                }
                                                else
                                                {
                                                    cmd.Parameters.Add("@Latitude", SqlDbType.VarChar).Value = sLatitude.Trim();
                                                }

                                                cmd.Parameters.Add("@Longitude", SqlDbType.VarChar).Value = sLongitude.Trim();
                                                cmd.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = sEndereco.Trim();
                                                cmd.Parameters.Add("@Sequencia", SqlDbType.VarChar).Value = isequencia;

                                                cmd.ExecuteNonQuery();

                                            }
                                        }




                                        //soma
                                        isequencia = isequencia + 1;
                                        ilinhaatual = ilinhaatual + 1;
                                        ilinha = ilinha + 1;

                                        //verifica linha para inserir uma nova rota
                                        if (ilinha == divlinha)
                                        {
                                            bnovarota = true;
                                            ilinha = 0;
                                        }

                                        //sai do loop ao bater o total de linhas do arquivo
                                        if (ilinhaatual == totlinhas)
                                        {
                                            break;
                                        }

                                    }

                                }

                            }

                            cmbqtdrotas.ClearSelection();
                            MSG_SUCCESS("Arquivo Importado com Sucesso !");

                        }
                        else
                        {
                            Erro("Arquivo Vazio");
                        }

                    }
                    else
                    {
                        Erro("Erro ao Importa");
                    }
                }

            }
            else
            {
                Erro("Selecione o arquivo para importação!");
            }

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// importação dos registros
    /// </summary>
    public void importarArqu2()
    {
        try
        {
            gergatlink obj = new gergatlink();

            string sNOME_ARQ_CARGA = "";
            string sIdrota = "";
            string iQtdRotas = "";

            //Captura a quantidade de rotas do arquivo
            iQtdRotas = cmbqtdrotas.SelectedValue;

            //VERIFICA SE SELECIONOU UM ARQUIVO
            if (FileUpload_IMP2.HasFile)
            {
                //verifica se selecionou a quantidade de rotas
                if (cmbqtdrotas.SelectedValue == "")
                {
                    Erro("Seleciona a quantidade de Rotas para esse arquivo!");
                }
                else
                {
                    //verifica se existe registros
                    if (!String.IsNullOrEmpty(FileUpload_IMP2.FileName))
                    {
                        //captura nome do arquivo
                        sNOME_ARQ_CARGA = FileUpload_IMP2.FileName.ToString();

                        //Os bytes do arquivo foram recebidos pelo servidor.
                        if (FileUpload_IMP2.FileBytes.Count() > 0)
                        {
                            // Create an instance oaf StreamReader to read from a file.
                            using (StreamReader sr = new StreamReader(FileUpload_IMP2.FileContent))
                            {
                                //obj ger gat
                                gergatlink obj2 = new gergatlink();

                                //Variaveis da receber dados da carga
                                string sEndereco = "";
                                string sLatitude = "";
                                string sLongitude = "";
                                int isequencia = 1;

                                //abre con
                                using (SqlConnection cn = obj2.abre_cn())
                                {
                                    string line; //Para inserir a linha

                                    //intancia lista Array
                                    List<string> arr = new List<string>();

                                    //Para ler a linhas do streamReader
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        //verifica preenchimento e adiciona linha ao array
                                        if (line != "")
                                        {
                                            arr.Add(line); //alimenta array
                                            totlinhas = totlinhas + 1; //quantidade de linhas do arquivo
                                        }
                                    }

                                    //Verifica quantidade de enderecos x rotas
                                    if (Convert.ToInt32(totlinhas) < Convert.ToInt32(iQtdRotas))
                                    {
                                        Erro("Quantidade de Endereço é menor que a quantidade de Rotas! Este arquivo contém " + totlinhas + " endereços");
                                        return;
                                    }

                                    //Verifica quantidade de rotas x enderecos
                                    if (Convert.ToInt32(iQtdRotas) > Convert.ToInt32(totlinhas))
                                    {
                                        Erro("Quantidade de Rotas é maior que a quantidade de Endereços! Este arquivo contém " + totlinhas + " endereços");
                                        return;
                                    }

                                    //divide a quantidade de linhas(enderecos) por Rotas
                                    int divlinha = (Convert.ToInt32(totlinhas) / Convert.ToInt32(iQtdRotas));

                                    //variaveis
                                    int numrota = 1;
                                    int ilinha = 0;
                                    int ilinhaatual = 0;
                                    bool bnovarota = true;

                                    //loop de inserção de dados
                                    while (ilinha < divlinha)
                                    {
                                        //verifica inserção de uma nova rota
                                        if (bnovarota == true)
                                        {
                                            //Insere a rota
                                            using (SqlConnection cn2 = obj.abre_cn())
                                            {
                                                using (SqlCommand cmd = new SqlCommand())
                                                {
                                                    cmd.Connection = cn;
                                                    cmd.CommandText = "TEL_MP_InsereRota";
                                                    cmd.CommandType = CommandType.StoredProcedure;

                                                    cmd.Parameters.Add("@Rota", SqlDbType.VarChar).Value = numrota + "º " + sNOME_ARQ_CARGA;
                                                    cmd.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;

                                                    cmd.ExecuteNonQuery();

                                                    sIdrota = cmd.Parameters["@ID"].Value.ToString();

                                                    bnovarota = false;
                                                    isequencia = 1; //renova sequencia

                                                    //muda o numero da rota
                                                    if (numrota < Convert.ToInt32(iQtdRotas))
                                                    {
                                                        numrota = numrota + 1;
                                                    }
                                                    else
                                                    {
                                                        numrota = numrota + 1;
                                                    }

                                                }
                                            }
                                        }

                                        //DADOS
                                        string[] dados = arr[ilinhaatual].Split(new Char[] { ';' });

                                        sLatitude = dados[0].Replace(",", "");
                                        sLongitude = dados[1];
                                        sEndereco = dados[2];

                                        //chama a proc de insercao
                                        using (SqlCommand cmd = new SqlCommand())
                                        {
                                            cmd.Connection = cn;
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.CommandText = "TEL_MP_InsereRoteiro";

                                            cmd.Parameters.Add("@IDCodRota", SqlDbType.VarChar).Value = sIdrota;

                                            //verifica sequencia para remoçao da virgula no inicio
                                            if (isequencia == 1)
                                            {
                                                cmd.Parameters.Add("@Latitude", SqlDbType.VarChar).Value = sLatitude.Trim();
                                            }
                                            else
                                            {
                                                cmd.Parameters.Add("@Latitude", SqlDbType.VarChar).Value = sLatitude.Trim();
                                            }

                                            cmd.Parameters.Add("@Longitude", SqlDbType.VarChar).Value = sLongitude.Trim();
                                            cmd.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = sEndereco.Trim();
                                            cmd.Parameters.Add("@Sequencia", SqlDbType.VarChar).Value = isequencia;

                                            cmd.ExecuteNonQuery();

                                        }

                                        //soma
                                        isequencia = isequencia + 1;
                                        ilinhaatual = ilinhaatual + 1;
                                        ilinha = ilinha + 1;

                                        //verifica linha para inserir uma nova rota
                                        if (ilinha == divlinha)
                                        {
                                            bnovarota = true;
                                            ilinha = 0;
                                        }

                                        //sai do loop ao bater o total de linhas do arquivo
                                        if (ilinhaatual == totlinhas)
                                        {
                                            break;
                                        }

                                    }

                                }

                            }

                            cmbqtdrotas.ClearSelection();
                            MSG_SUCCESS("Arquivo Importado com Sucesso !");

                        }
                        else
                        {
                            Erro("Arquivo Vazio");
                        }

                    }
                    else
                    {
                        Erro("Erro ao Importa");
                    }
                }

            }
            else
            {
                Erro("Selecione o arquivo para importação!");
            }

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// cmdVOltar_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdVOltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("dashboard.aspx", false);
    }

    /// <summary>
    /// cmdRotas_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdRotas_Click(object sender, EventArgs e)
    {
        Response.Redirect("rotas.aspx", false);
    }


    /// <summary>
    /// cmdremoveer_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdremoveer_Click(object sender, EventArgs e)
    {
        try
        {
            gergatlink obj = new gergatlink();

            //VERIFICA SE SELECIONOU UM ARQUIVO
            if (FileRemove.HasFile)
            {

                //verifica se existe registros
                if (!String.IsNullOrEmpty(FileRemove.FileName))
                {

                    //Os bytes do arquivo foram recebidos pelo servidor.
                    if (FileRemove.FileBytes.Count() > 0)
                    {
                        // Create an instance oaf StreamReader to read from a file.
                        using (StreamReader sr = new StreamReader(FileRemove.FileContent))
                        {
                            //obj ger gat
                            gergatlink obj2 = new gergatlink();

                            //Variaveis da receber dados da carga
                            string sEndereco = "";
                            string sLatitude = "";
                            string sLongitude = "";
                            int isequencia = 1;

                            //abre con
                            using (SqlConnection cn = obj2.abre_cn())
                            {
                                string line; //Para inserir a linha

                                //intancia lista Array
                                List<string> arr = new List<string>();

                                //Para ler a linhas do streamReader
                                while ((line = sr.ReadLine()) != null)
                                {
                                    //verifica preenchimento e adiciona linha ao array
                                    if (line != "")
                                    {
                                        arr.Add(line); //alimenta array
                                        totlinhas = totlinhas + 1; //quantidade de linhas do arquivo
                                    }
                                }

                                //variaveis
                                int ilinha = 0;
                                int ilinhaatual = 0;

                                //loop de inserção de dados
                                while (ilinha < totlinhas)
                                {
                                    
                                    //DADOS
                                    string[] dados = arr[ilinhaatual].Split(new Char[] { ';' });

                                    sLatitude = dados[0].Replace(",", "");
                                    sLongitude = dados[1];
                                    sEndereco = dados[2];

                                    //chama a proc de insercao
                                    using (SqlCommand cmd = new SqlCommand())
                                    {
                                        cmd.Connection = cn;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.CommandText = "TEL_MP_RemoveRoteiro";


                                        //verifica sequencia para remoçao da virgula no inicio
                                        cmd.Parameters.Add("@Latitude", SqlDbType.VarChar).Value = sLatitude.Trim();
                                        cmd.Parameters.Add("@Longitude", SqlDbType.VarChar).Value = sLongitude.Trim();
                                        cmd.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = sEndereco.Trim();

                                        cmd.ExecuteNonQuery();

                                    }

                                    //soma
                                    isequencia = isequencia + 1;
                                    ilinhaatual = ilinhaatual + 1;
                                    ilinha = ilinha + 1;

                                    //sai do loop ao bater o total de linhas do arquivo
                                    if (ilinhaatual == totlinhas)
                                    {
                                        break;
                                    }

                                }

                            }

                        }

                        MSG_SUCCESS("Remoção com Sucesso!");

                    }
                    else
                    {
                        Erro("Arquivo Vazio");
                    }

                }
                else
                {
                    Erro("Erro ao Remover!");
                }


            }
            else
            {
                Erro("Selecione o arquivo para Remoção!");
            }

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// cmdalterar_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdalterar_Click(object sender, EventArgs e)
    {
        try
        {
            gergatlink obj = new gergatlink();

            //VERIFICA SE SELECIONOU UM ARQUIVO
            if (FileRemove.HasFile)
            {

                //verifica se existe registros
                if (!String.IsNullOrEmpty(FileRemove.FileName))
                {

                    //Os bytes do arquivo foram recebidos pelo servidor.
                    if (FileRemove.FileBytes.Count() > 0)
                    {
                        // Create an instance oaf StreamReader to read from a file.
                        using (StreamReader sr = new StreamReader(FileRemove.FileContent))
                        {
                            //obj ger gat
                            gergatlink obj2 = new gergatlink();

                            //Variaveis da receber dados da carga
                            string sEndereco = "";
                            string sLatitude = "";
                            string sLongitude = "";
                            string snovoendereco = "";
                            

                            //abre con
                            using (SqlConnection cn = obj2.abre_cn())
                            {
                                string line; //Para inserir a linha

                                //intancia lista Array
                                List<string> arr = new List<string>();

                                //Para ler a linhas do streamReader
                                while ((line = sr.ReadLine()) != null)
                                {
                                    //verifica preenchimento e adiciona linha ao array
                                    if (line != "")
                                    {
                                        arr.Add(line); //alimenta array
                                        totlinhas = totlinhas + 1; //quantidade de linhas do arquivo
                                    }
                                }

                                //variaveis
                                int ilinha = 0;
                                int ilinhaatual = 0;

                                //loop de inserção de dados
                                while (ilinha < totlinhas)
                                {

                                    //DADOS
                                    string[] dados = arr[ilinhaatual].Split(new Char[] { ';' });

                                    sLatitude = dados[0].Replace(",", "");
                                    sLongitude = dados[1];
                                    sEndereco = dados[2];
                                    snovoendereco = dados[3];

                                    //chama a proc de insercao
                                    using (SqlCommand cmd = new SqlCommand())
                                    {
                                        cmd.Connection = cn;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.CommandText = "TEL_MP_AlterarRoteiro";


                                        //verifica sequencia para remoçao da virgula no inicio
                                        cmd.Parameters.Add("@Latitude", SqlDbType.VarChar).Value = sLatitude.Trim();
                                        cmd.Parameters.Add("@Longitude", SqlDbType.VarChar).Value = sLongitude.Trim();
                                        cmd.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = sEndereco.Trim();
                                        cmd.Parameters.Add("@EnderecoNovo", SqlDbType.VarChar).Value = snovoendereco.Trim();

                                        cmd.ExecuteNonQuery();

                                    }

                                    //soma
                                    ilinhaatual = ilinhaatual + 1;
                                    ilinha = ilinha + 1;

                                    //sai do loop ao bater o total de linhas do arquivo
                                    if (ilinhaatual == totlinhas)
                                    {
                                        break;
                                    }

                                }

                            }

                        }

                        MSG_SUCCESS("Alteração com Sucesso!");

                    }
                    else
                    {
                        Erro("Arquivo Vazio");
                    }

                }
                else
                {
                    Erro("Erro ao Alterar!");
                }


            }
            else
            {
                Erro("Selecione o arquivo para Alteração!");
            }

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }
}