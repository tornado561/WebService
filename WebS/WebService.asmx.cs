using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;


namespace WebS
{
    /// <summary>
    /// Opis podsumowujący dla WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Aby zezwalać na wywoływanie tej usługi sieci Web ze skryptu za pomocą kodu ASP.NET AJAX, usuń znaczniki komentarza z następującego wiersza. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        //string conString = "Data Source=ELPLC-0213;Initial Catalog=bazka;Integrated Security=True";
        string conString = "server=127.0.0.1; user = root; database = kosztorys";

        // @@@@@@@@@@@@@@@@@@@@@@@@@@@@ PRZEDMIOTY @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@


  
        [WebMethod]
        public DataTable pokazWszystko()
        {
            string querySelect = "SELECT * FROM przedmioty";
            using (MySqlConnection connection = new MySqlConnection(conString))
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(querySelect))
                {


                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Connection = connection;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            // string JSONString = string.Empty;
                            dt.TableName = "przedmioty";
                            sda.Fill(dt);
                            //  JSONString = JsonConvert.SerializeObject(dt);
                            return dt;
                        }
                    }
                }
            }
        }





        [WebMethod]
        public DataTable szukajPrzedmiot(string kategoria, string nazwa)
        {
            using (MySqlConnection connection = new MySqlConnection(conString))
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM przedmioty WHERE kategoria=@kategoria OR nazwa=@nazwa"))
                {


                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Connection = connection;
                        sda.SelectCommand = cmd;
                        MySqlParameter sqlParameter = new MySqlParameter();
                        cmd.Parameters.AddWithValue("@kategoria", kategoria);

                        sqlParameter = new MySqlParameter();
                        cmd.Parameters.AddWithValue("@nazwa", nazwa);
                        using (DataTable dt = new DataTable())
                        {
                            //  string JSONString = string.Empty;
                            dt.TableName = "przedmioty";
                            sda.Fill(dt);
                            //  JSONString = JsonConvert.SerializeObject(dt);
                            return dt;
                        }
                    }
                }
            }
        }


        [WebMethod]
        public DataTable szukajKategorie()
        {
            using (MySqlConnection connection = new MySqlConnection(conString))
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT distinct kategoria FROM przedmioty"))
                {


                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Connection = connection;
                        sda.SelectCommand = cmd;
                       

                       
                        using (DataTable dt = new DataTable())
                        {
                            //  string JSONString = string.Empty;
                            dt.TableName = "przedmioty";
                            sda.Fill(dt);
                            //  JSONString = JsonConvert.SerializeObject(dt);

                            return dt;
                        }
                    }
                }
            }
        }

        [WebMethod]
        public int newRecord(string kategoria, string nazwa, string cena)
        {
            if (kategoria == string.Empty || nazwa == string.Empty || cena == string.Empty)
            {
                return (int)errorsHandler.empty_data;
            }

            using (MySqlConnection connection = new MySqlConnection(conString))
            {
                string addRecord = "INSERT INTO przedmioty (kategoria,nazwa,cena) VALUES (@kategoria,@nazwa,@cena)";
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = connection;
                    MySqlParameter param = new MySqlParameter();
                    // param.ParameterName = "@imie";
                    // param.Value = imie;
                    // cmd.Parameters.Add(param);
                    cmd.Parameters.AddWithValue("@kategoria", kategoria);



                    param = new MySqlParameter();
                    //param.ParameterName = "@nazwisko";
                    //param.Value = nazwisko;
                    //cmd.Parameters.Add(param);
                    cmd.Parameters.AddWithValue("@nazwa", nazwa);


                    param = new MySqlParameter();
                    //param.ParameterName = "@opis";
                    // param.Value = opis;
                    //cmd.Parameters.Add(param);
                    cmd.Parameters.AddWithValue("@cena", cena);



                    cmd.CommandText = addRecord;
                    cmd.ExecuteNonQuery();

                }

            }

            return (int)errorsHandler.all_ok;
        }
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        [WebMethod]
        public int newClient(string imie, string nazwisko, string opis)
        {
            if (imie == string.Empty || nazwisko == string.Empty || opis == string.Empty)
            {
                return (int)errorsHandler.empty_data;
            }

            using (SqlConnection connection = new SqlConnection(conString))
            {
                string addClient = "INSERT INTO klienci (imie,nazwisko,opis) VALUES (@imie,@nazwisko,@opis)";
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    SqlParameter param = new SqlParameter();
                    // param.ParameterName = "@imie";
                    // param.Value = imie;
                    // cmd.Parameters.Add(param);
                    cmd.Parameters.AddWithValue("@imie", imie);



                    param = new SqlParameter();
                    //param.ParameterName = "@nazwisko";
                    //param.Value = nazwisko;
                    //cmd.Parameters.Add(param);
                    cmd.Parameters.AddWithValue("@nazwisko", nazwisko);


                    param = new SqlParameter();
                    //param.ParameterName = "@opis";
                    // param.Value = opis;
                    //cmd.Parameters.Add(param);
                    cmd.Parameters.AddWithValue("@opis", opis);



                    cmd.CommandText = addClient;
                    cmd.ExecuteNonQuery();

                }

            }

            return (int)errorsHandler.all_ok;
        }




        [WebMethod]
        public int updateClient(int id, string imie, string nazwisko, string opis)
        {
            if (imie == string.Empty || nazwisko == string.Empty || opis == string.Empty)
            {
                return (int)errorsHandler.empty_data;
            }

            using (SqlConnection connection = new SqlConnection(conString))
            {

                string updateClient = "UPDATE klienci SET imie = @imie , nazwisko = @nazwisko, opis = @opis  WHERE id = @id";
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    SqlParameter param = new SqlParameter();
                    cmd.Parameters.AddWithValue("@imie", imie);

                    param = new SqlParameter();
                    cmd.Parameters.AddWithValue("@nazwisko", nazwisko);

                    param = new SqlParameter();
                    cmd.Parameters.AddWithValue("@opis", opis);

                    param = new SqlParameter();
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.CommandText = updateClient;
                    cmd.ExecuteNonQuery();

                }

            }

            return (int)errorsHandler.all_ok;
        }

        [WebMethod]
        public int deleteClient(int id, string imie, string nazwisko)
        {
            if (imie == string.Empty || nazwisko == string.Empty)
            {
                return (int)errorsHandler.empty_data;
            }

            using (SqlConnection connection = new SqlConnection(conString))
            {

                string deleteClient = "DELETE FROM klienci WHERE imie = @imie AND nazwisko = @nazwisko AND id = @id";
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    SqlParameter param = new SqlParameter();
                    cmd.Parameters.AddWithValue("@imie", imie);

                    param = new SqlParameter();
                    cmd.Parameters.AddWithValue("@nazwisko", nazwisko);

                    param = new SqlParameter();
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.CommandText = deleteClient;
                    cmd.ExecuteNonQuery();

                }

            }

            return (int)errorsHandler.all_ok;
        }


       


        [WebMethod]
        public DataTable searchClient(string imie, string nazwisko)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM klienci WHERE imie=@imie AND nazwisko=@nazwisko"))
                {


                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = connection;
                        sda.SelectCommand = cmd;
                        SqlParameter sqlParameter = new SqlParameter();
                        cmd.Parameters.AddWithValue("@imie", imie);

                        sqlParameter = new SqlParameter(); 
                        cmd.Parameters.AddWithValue("@nazwisko", nazwisko);
                        using (DataTable dt = new DataTable())
                        {
                          //  string JSONString = string.Empty;
                            dt.TableName = "klienci";
                            sda.Fill(dt);
                          //  JSONString = JsonConvert.SerializeObject(dt);
                            return dt;
                        }
                    }
                }
            }
        }


      

        // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ WYNIKI @@@@@@@@@@@@@@@@@@@@@@@
        [WebMethod]

        public int insertRecord(string dane, string data, string liczba)

        {
            DateTime data_time;

            if (!DateTime.TryParse(data, out data_time))
            {
                return (int)errorsHandler.wrong_data;
            }

            if (dane == string.Empty)
            {
                return (int)errorsHandler.empty_data;
            }
            using (SqlConnection conn = new SqlConnection(conString))
            {

                conn.Open();


                string queryWith = "INSERT INTO wyniki(dane, data, liczba) VALUES (@dane, @data, @liczba)";
                string queryNW = "INSERT INTO wyniki(dane, data, liczba) VALUES (@dane, @data, null)";

                // string query = string.Format("Mam na imie {0} a moje nazwisko to {1}", "IMIE", "NAZWISKO");

                float liczba_f;


                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    SqlParameter parameter = new SqlParameter();
                    parameter.ParameterName = "@dane";
                    parameter.Value = dane;
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@data";
                    parameter.Value = data;
                    command.Parameters.Add(parameter);

                    if (float.TryParse(liczba, out liczba_f))
                    {
                        parameter = new SqlParameter();
                        parameter.ParameterName = "@liczba";
                        parameter.Value = Math.Round(liczba_f, 2);
                        command.Parameters.Add(parameter);

                        command.CommandText = queryWith;
                    }

                    else
                    {
                        command.CommandText = queryNW;
                    }

                    command.ExecuteNonQuery();
                }

                conn.Close();
                return (int)errorsHandler.all_ok;
            }


        }
        [WebMethod]

        public string getData()
        {
            //string constr = ConfigurationManager.ConnectionStrings["dbpomiarTA"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM wyniki"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            string JSONString = string.Empty;
                            dt.TableName = "wyniki";
                            sda.Fill(dt);
                            JSONString = JsonConvert.SerializeObject(dt);
                            return JSONString;
                        }
                    }
                }
            }

        }

    }

}
