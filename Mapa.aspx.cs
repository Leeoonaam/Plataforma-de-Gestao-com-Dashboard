using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mapa : System.Web.UI.Page
{

    public string sPlace_Map = "";
    string _Nome = "";
    string _Status = "";
    string _Latitude = "";
    string _Longitude = "";
    string _Endereco = "";

    protected void Page_Load(object sender, EventArgs e)
    {
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
                        case "_Nome":
                            _Nome = Request.QueryString[item].ToString();
                            break;
                        case "_Status":
                            _Status = Request.QueryString[item].ToString();
                            break;
                        case "_Latitude":
                            _Latitude = Request.QueryString[item].ToString();
                            break;
                        case "_Longitude":
                            _Longitude = Request.QueryString[item].ToString();
                            break;
                        case "_Endereco":
                            _Endereco = Request.QueryString[item].ToString();
                            break;
                    }
                }
                
                RenderizaMapa(_Nome, _Status, _Latitude,_Longitude,_Endereco);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    /// <summary>
    /// RenderizaMapa
    /// </summary>
    /// <param name="sLatitude"></param>
    /// <param name="sLongitude"></param>
    /// <param name="sEndereco"></param>
    public void RenderizaMapa(string sNome, string sStatus, string sLatitude, string sLongitude, string sEndereco)
    {
        try
        {

            if (sNome == "" || sStatus == "" || sLatitude == "" || sLongitude == "" || sEndereco == "")
            {
                sPlace_Map = sPlace_Map + "var uluru1 = { lat: 0, lng: 0 };";
                sPlace_Map = sPlace_Map + " var map1 = new google.maps.Map(document.getElementById('mapa'), { zoom: 15, center: uluru1 }); ";
                sPlace_Map = sPlace_Map + "var marker = new google.maps.Marker({ position: uluru1, map: map1, title: 'Nenhum Registro' });";
            }
            else
            {
                sPlace_Map = sPlace_Map + "var uluru1 = { lat: " + sLatitude.ToString().Replace(",", ".") + ", lng: " + sLongitude.ToString().Replace(",", ".") + " };";
                sPlace_Map = sPlace_Map + " var map1 = new google.maps.Map(document.getElementById('mapa'), { zoom: 15, center: uluru1 }); ";
                sPlace_Map = sPlace_Map + "var marker = new google.maps.Marker({ position: uluru1, map: map1, title: 'NOME: " + sNome + " - STATUS: " + sStatus + " - LATITUDE: " + sLatitude.ToString() + " - LONGITUDE: " + sLongitude.ToString() + " ENDEREÇO: " + sEndereco.ToString() + "' });";//
            }
            
        }
        catch (Exception)
        {
            throw;
        }
    }
    
}