using System;
using System.Data;
using MySql.Data.MySqlClient;
using Inventarioa.Objetuak; // Ziurtatu namespace hau zure klasearena dela

namespace Inbentarioa.DatuBasie
{
    internal class DBGailuak
    {
        public static bool GehituOrdenagailuaPOO(Ordenagailua orde)
        {
            using (MySqlConnection conn = new MySqlConnection(DbKonexioa.Instantzia.GetKonexioString()))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Gurasoa txertatu (Gailuak)
                        string sqlGailua = @"INSERT INTO Gailuak (identifikazio_kodea, marka_modeloa, id_mintegia, eroste_data, egoera) 
                                     VALUES (@kode, @mod, @min, @data, @egoera);
                                     SELECT LAST_INSERT_ID();";

                        MySqlCommand cmdGailua = new MySqlCommand(sqlGailua, conn, trans);
                        cmdGailua.Parameters.AddWithValue("@kode", orde.IdentifikazioKodea);
                        cmdGailua.Parameters.AddWithValue("@mod", orde.MarkaModeloa);
                        cmdGailua.Parameters.AddWithValue("@min", orde.IdMintegia);
                        cmdGailua.Parameters.AddWithValue("@data", orde.ErosteData);
                        cmdGailua.Parameters.AddWithValue("@egoera", orde.Egoera);

                        int berriaId = Convert.ToInt32(cmdGailua.ExecuteScalar());

                        // 2. Umea txertatu (Ordenagailuak)
                        string sqlOrde = "INSERT INTO Ordenagailuak (id_gailua, ram, rom, cpu) VALUES (@id, @ram, @rom, @cpu)";
                        MySqlCommand cmdOrde = new MySqlCommand(sqlOrde, conn, trans);
                        cmdOrde.Parameters.AddWithValue("@id", berriaId);
                        cmdOrde.Parameters.AddWithValue("@ram", orde.Ram);
                        cmdOrde.Parameters.AddWithValue("@rom", orde.Rom);
                        cmdOrde.Parameters.AddWithValue("@cpu", orde.Cpu);

                        cmdOrde.ExecuteNonQuery();
                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public static bool EzabatuOrdenagailuaPOO(Ordenagailua orde)
        {
            using (MySqlConnection conn = new MySqlConnection(DbKonexioa.Instantzia.GetKonexioString()))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Historikora mugitu (Argazkian ikusten denez: ezabatutakoak)
                        string insertQuery = @"
                    INSERT INTO ezabatutakoak (identifikazio_kodea, marka_modeloa, mota, ezabatutako_eguna) 
                    VALUES (@kode, @mod, @mota, NOW())";

                        MySqlCommand cmdInsert = new MySqlCommand(insertQuery, conn, trans);
                        cmdInsert.Parameters.AddWithValue("@kode", orde.IdentifikazioKodea);
                        cmdInsert.Parameters.AddWithValue("@mod", orde.MarkaModeloa);
                        cmdInsert.Parameters.AddWithValue("@mota", "Ordenagailua");
                        cmdInsert.ExecuteNonQuery();

                        // 1.5 Hondatutakoak taulatik kendu (Argazkian ikusten denez: hondatutakoak)
                        // FOREIGN KEY (fk_gailua_hondatu) askatzeko ezinbestekoa da
                        string deleteHondatuta = "DELETE FROM hondatutakoak WHERE id_gailua = @id";
                        MySqlCommand cmdHondatuta = new MySqlCommand(deleteHondatuta, conn, trans);
                        cmdHondatuta.Parameters.AddWithValue("@id", orde.Id);
                        cmdHondatuta.ExecuteNonQuery();

                        // 2. Umea ezabatu (Argazkian ikusten denez: ordenagailuak)
                        string deleteUmea = "DELETE FROM ordenagailuak WHERE id_gailua = @id";
                        MySqlCommand cmdUmea = new MySqlCommand(deleteUmea, conn, trans);
                        cmdUmea.Parameters.AddWithValue("@id", orde.Id);
                        cmdUmea.ExecuteNonQuery();

                        // 3. Gurasoa ezabatu (Argazkian ikusten denez: gailuak)
                        string deleteGurasoa = "DELETE FROM gailuak WHERE id_gailua = @id";
                        MySqlCommand cmdGurasoa = new MySqlCommand(deleteGurasoa, conn, trans);
                        cmdGurasoa.Parameters.AddWithValue("@id", orde.Id);
                        cmdGurasoa.ExecuteNonQuery();

                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        // Hemen errorea jaurtiko dugu zer gertatzen den ikusteko
                        throw new Exception("Errore teknikoa: " + ex.Message);
                    }
                }
            }
        }
        public static bool EguneratuEgoeraPOO(Ordenagailua orde)
        {
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    conn.Open();
                    // SQLan objektuaren propietateak erabiltzen ditugu
                    string sql = "UPDATE Gailuak SET egoera = @egoera WHERE id_gailua = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@egoera", orde.Egoera);
                    cmd.Parameters.AddWithValue("@id", orde.Id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool EzabatuInprimagailuaPOO(Inprimagailua inp)
        {
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();
            using (MySqlConnection conn = new MySqlConnection(konexioa))
            {
                conn.Open();
                MySqlTransaction trans = conn.BeginTransaction();

                try
                {
                    // 1. Historikoan sartu (Gailuaren datu nagusiak)
                    // 1. Historikoan sartu
                    string sqlHist = "INSERT INTO ezabatutakoak (identifikazio_kodea, marka_modeloa, mota) VALUES (@kode, @mod, @mota)";
                    MySqlCommand cmdHist = new MySqlCommand(sqlHist, conn, trans);
                    cmdHist.Parameters.AddWithValue("@kode", inp.IdentifikazioKodea);
                    cmdHist.Parameters.AddWithValue("@mod", inp.MarkaModeloa);
                    // GAKOA: Parametro hau falta zen!
                    cmdHist.Parameters.AddWithValue("@mota", "Inprimagailua");
                    cmdHist.ExecuteNonQuery();

                    // 2. Inprimagailua taulatik ezabatu (Umea)
                    string sqlUmea = "DELETE FROM inprimagailuak WHERE id_gailua = @id";
                    MySqlCommand cmdUmea = new MySqlCommand(sqlUmea, conn, trans);
                    cmdUmea.Parameters.AddWithValue("@id", inp.Id);
                    cmdUmea.ExecuteNonQuery();

                    // 3. Gailuak taulatik ezabatu (Gurasoa)
                    string sqlGurasoa = "DELETE FROM gailuak WHERE id_gailua = @id";
                    MySqlCommand cmdGurasoa = new MySqlCommand(sqlGurasoa, conn, trans);
                    cmdGurasoa.Parameters.AddWithValue("@id", inp.Id);
                    cmdGurasoa.ExecuteNonQuery();

                    trans.Commit();
                    return true;
                }

                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }
        public static bool EzabatuInprimagailuaOsoa(int id)
        {
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();
            using (MySqlConnection conn = new MySqlConnection(konexioa))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. GARRANTZITSUA: Lehenik datuak lortu historikorako
                        // SELECT bidez txertatzen dugu gailuak taulatik
                        string sqlHist = @"INSERT INTO ezabatutakoak (identifikazio_kodea, marka_modeloa, mota, ezabatutako_eguna) 
                                   SELECT identifikazio_kodea, marka_modeloa, 'Inprimagailua', NOW() 
                                   FROM gailuak WHERE id_gailua = @id";
                        MySqlCommand cmdHist = new MySqlCommand(sqlHist, conn, trans);
                        cmdHist.Parameters.AddWithValue("@id", id);
                        cmdHist.ExecuteNonQuery();

                        // 2. ORDENA: Umeak ezabatu gurasoa baino lehen
                        // Ezabatu 'hondatutakoak' taulatik (Errorerik ez emateko)
                        string sqlHondatu = "DELETE FROM hondatutakoak WHERE id_gailua = @id";
                        MySqlCommand cmdHondatu = new MySqlCommand(sqlHondatu, conn, trans);
                        cmdHondatu.Parameters.AddWithValue("@id", id);
                        cmdHondatu.ExecuteNonQuery();

                        // Ezabatu 'inprimagailuak' taulatik
                        string sqlInp = "DELETE FROM inprimagailuak WHERE id_gailua = @id";
                        MySqlCommand cmdInp = new MySqlCommand(sqlInp, conn, trans);
                        cmdInp.Parameters.AddWithValue("@id", id);
                        cmdInp.ExecuteNonQuery();

                        // 3. AZKENIK: Gurasoa ezabatu
                        string sqlGailu = "DELETE FROM gailuak WHERE id_gailua = @id";
                        MySqlCommand cmdGailu = new MySqlCommand(sqlGailu, conn, trans);
                        cmdGailu.Parameters.AddWithValue("@id", id);
                        cmdGailu.ExecuteNonQuery();

                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("Datu-base errorea: " + ex.Message);
                    }
                }
            }
        }
        public static bool EguneratuEgoeraPOO(Gailua gailua)
        {
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    conn.Open();
                    string sql = "UPDATE gailuak SET egoera = @egoera WHERE id_gailua = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@egoera", gailua.Egoera);
                    cmd.Parameters.AddWithValue("@id", gailua.Id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch { return false; }
        }
        public static bool GehituInprimagailuaPOO(Inprimagailua inp)
        {
            string konexioaString = DbKonexioa.Instantzia.GetKonexioString();

            using (MySqlConnection conn = new MySqlConnection(konexioaString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Gailu orokorra txertatu
                        string sqlGailua = @"INSERT INTO Gailuak (identifikazio_kodea, marka_modeloa, id_mintegia, eroste_data, egoera) 
                                   VALUES (@kode, @marka, @mintegia, @data, @egoera);
                                   SELECT LAST_INSERT_ID();";

                        MySqlCommand cmdGailua = new MySqlCommand(sqlGailua, conn, trans);
                        cmdGailua.Parameters.AddWithValue("@kode", inp.IdentifikazioKodea);
                        cmdGailua.Parameters.AddWithValue("@marka", inp.MarkaModeloa);
                        cmdGailua.Parameters.AddWithValue("@mintegia", inp.IdMintegia);
                        cmdGailua.Parameters.AddWithValue("@data", DateTime.Now);
                        cmdGailua.Parameters.AddWithValue("@egoera", inp.Egoera);

                        // Lortu sortu berri den ID-a
                        int berriaId = Convert.ToInt32(cmdGailua.ExecuteScalar());

                        // 2. Inprimagailu espezifikoa txertatu
                        string sqlInp = "INSERT INTO Inprimagailuak (id_gailua, koloretakoa) VALUES (@id, @tinta)";
                        MySqlCommand cmdInp = new MySqlCommand(sqlInp, conn, trans);
                        cmdInp.Parameters.AddWithValue("@id", berriaId);
                        // MySQL-en tinyint(1) denez, bool-a 0 edo 1 bezala gordetzen da automatikoki
                        cmdInp.Parameters.AddWithValue("@tinta", inp.Koloretakoa);

                        cmdInp.ExecuteNonQuery();

                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex; // Formularioak errorea kudeatu dezan
                    }
                }
            }
        }

        public static DataTable GetGailuakMintegiBuruarentzat(int idArduraduna)
        {
            DataTable dt = new DataTable();
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();

            // Query honek Gailuak -> Mintegiak -> Arduraduna lotzen ditu
            string sql = @"
        SELECT g.id_gailua AS ID, g.identifikazio_kodea, g.izena AS marka_modeloa, g.egoera AS egoera_balioa, 'Ordenagailua' AS mota 
        FROM gailuak g 
        INNER JOIN ordenagailuak o ON g.id_gailua = o.id_gailua
        INNER JOIN mintegiak m ON g.id_mintegia = m.id_mintegia
        WHERE m.id_arduraduna = @id_arduraduna
        UNION
        SELECT g.id_gailua AS ID, g.identifikazio_kodea, g.izena AS marka_modeloa, g.egoera AS egoera_balioa, 'Inprimagailua' AS mota 
        FROM gailuak g 
        INNER JOIN inprimagailuak i ON g.id_gailua = i.id_gailua
        INNER JOIN mintegiak m ON g.id_mintegia = m.id_mintegia
        WHERE m.id_arduraduna = @id_arduraduna";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id_arduraduna", idArduraduna);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errorea mintegiko gailuak lortzean: " + ex.Message);
            }
            return dt;
        }

        // Gehitu 'int idArduraduna' parametro gisa
        public static DataTable GetGailuGuztiakMB(int idArduraduna)
        {
            DataTable dt = new DataTable();
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();

            string sql = @"
        SELECT g.id_gailua AS ID, g.identifikazio_kodea, g.marka_modeloa AS marka_modeloa, 
               g.egoera AS egoera_balioa, m.id_arduraduna, 'Ordenagailua' AS mota 
        FROM gailuak g 
        INNER JOIN ordenagailuak o ON g.id_gailua = o.id_gailua
        INNER JOIN mintegiak m ON g.id_mintegia = m.id_mintegia
        WHERE m.id_arduraduna = @id_arduraduna
        UNION
        SELECT g.id_gailua AS ID, g.identifikazio_kodea, g.marka_modeloa AS marka_modeloa, 
               g.egoera AS egoera_balioa, m.id_arduraduna, 'Inprimagailua' AS mota 
        FROM gailuak g 
        INNER JOIN inprimagailuak i ON g.id_gailua = i.id_gailua
        INNER JOIN mintegiak m ON g.id_mintegia = m.id_mintegia
        WHERE m.id_arduraduna = @id_arduraduna";

            using (MySqlConnection conn = new MySqlConnection(konexioa))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                // GARRANTZITSUA: Parametroa SQL-ari lotu
                cmd.Parameters.AddWithValue("@id_arduraduna", idArduraduna);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
        public static DataTable GetGailuGuztiakPOO()
        {
            DataTable dt = new DataTable();
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();

            // Gehitu ditugu: g.marka_modeloa, g.egoera eta m.id_arduraduna
            string sql = @"
        SELECT g.id_gailua AS ID, g.identifikazio_kodea, g.marka_modeloa, 
               g.egoera AS egoera_balioa, m.id_arduraduna, 'Ordenagailua' AS mota 
        FROM gailuak g 
        INNER JOIN ordenagailuak o ON g.id_gailua = o.id_gailua
        INNER JOIN mintegiak m ON g.id_mintegia = m.id_mintegia
        UNION
        SELECT g.id_gailua AS ID, g.identifikazio_kodea, g.marka_modeloa, 
               g.egoera AS egoera_balioa, m.id_arduraduna, 'Inprimagailua' AS mota 
        FROM gailuak g 
        INNER JOIN inprimagailuak i ON g.id_gailua = i.id_gailua
        INNER JOIN mintegiak m ON g.id_mintegia = m.id_mintegia";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errorea gailu guztiak kargatzean: " + ex.Message);
            }
            return dt;
        }

        public static DataTable GetGailuGuztiakGuztientzat()
        {
            DataTable dt = new DataTable();
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();

            // ZUZENKETA: g.izena -> g.marka_modeloa
            // GARRANTZITSUA: m.id_arduraduna gehitu dugu konparatu ahal izateko
            string sql = @"
        SELECT g.id_gailua AS ID, g.identifikazio_kodea, g.marka_modeloa, 
               g.egoera AS egoera_balioa, m.id_arduraduna, 'Ordenagailua' AS mota 
        FROM gailuak g 
        INNER JOIN ordenagailuak o ON g.id_gailua = o.id_gailua
        INNER JOIN mintegiak m ON g.id_mintegia = m.id_mintegia
        UNION
        SELECT g.id_gailua AS ID, g.identifikazio_kodea, g.marka_modeloa, 
               g.egoera AS egoera_balioa, m.id_arduraduna, 'Inprimagailua' AS mota 
        FROM gailuak g 
        INNER JOIN inprimagailuak i ON g.id_gailua = i.id_gailua
        INNER JOIN mintegiak m ON g.id_mintegia = m.id_mintegia";

            using (MySqlConnection conn = new MySqlConnection(konexioa))
            {
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                da.Fill(dt);
            }
            return dt;
        }

        public static DataTable GetErabiltzaileGuztiakPOO()
        {
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(konexioa))
            {
                // JOIN bat egiten dugu mintegiaren izena ikusteko Grid-ean
                string query = @"SELECT e.id_erabiltzailea AS 'ID', 
                                e.erabiltzailea AS 'Erabiltzailea', 
                                e.rola AS 'Rola', 
                                m.izena AS 'Mintegia'
                         FROM erabiltzaileak e
                         LEFT JOIN mintegiak m ON e.id_mintegia = m.id_mintegia";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                adapter.Fill(dt);
            }
            return dt;
        }

        //__________________________POO___________________________
        //________________________________________________________
        //________________________________________________________
        // 1. GAILU GUZTIAK (GailuGuztiak.cs-rako)
        public static DataTable GetGailuGuztiak()
        {
            DataTable dt = new DataTable();
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();

            // CASE WHEN erabiliko dugu 0, 1, 2 horiek testu bihurtzeko SQLan bertan
            string sql = @"
        SELECT 
            g.id_gailua AS ID, 
            g.identifikazio_kodea, 
            g.marka_modeloa, 
            g.egoera AS egoera_balioa,
            CASE 
                WHEN g.egoera = 0 THEN 'Ondo'
                WHEN g.egoera = 1 THEN 'Hondatuta'
                WHEN g.egoera = 2 THEN 'Konpontzen'
                ELSE 'Ezezaguna'
            END AS Egoera,
            'Ordenagailua' AS mota 
        FROM gailuak g 
        INNER JOIN ordenagailuak o ON g.id_gailua = o.id_gailua
        UNION
        SELECT 
            g.id_gailua AS ID, 
            g.identifikazio_kodea, 
            g.marka_modeloa, 
            g.egoera AS egoera_balioa,
            CASE 
                WHEN g.egoera = 0 THEN 'Ondo'
                WHEN g.egoera = 1 THEN 'Hondatuta'
                WHEN g.egoera = 2 THEN 'Konpontzen'
                ELSE 'Ezezaguna'
            END AS Egoera,
            'Inprimagailua' AS mota 
        FROM gailuak g 
        INNER JOIN inprimagailuak i ON g.id_gailua = i.id_gailua";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea SQL-an: " + ex.Message);
            }
            return dt;
        }

        // 2. MINTEGI-BURUAREN GAILUAK (Login rolaren arabera)
        public static DataTable GetGailuakMintegiBurua(int idArduraduna)
        {
            string query = $@"SELECT g.id_gailua AS 'ID', g.identifikazio_kodea AS 'Kodea', 
                             g.marka_modeloa AS 'Modeloa', m.izena AS 'Mintegia', 
                             g.egoera AS 'egoera_balioa' 
                             FROM gailuak g 
                             JOIN mintegiak m ON g.id_mintegia = m.id_mintegia
                             WHERE m.id_arduraduna = {idArduraduna}";
            return ExekutatuQuery(query);
        }

        // 3. ORDENAGAILU GUZTIAK (OrdenagailuGuztiak.cs-rako)
        public static DataTable GetOrdenagailuGuztiak()
        {
            DataTable dt = new DataTable();
            // LEFT JOIN beharrean INNER JOIN erabili
            string sql = @"SELECT G.id_gailua AS 'ID', 
                          G.identifikazio_kodea AS 'Kodea', 
                          G.marka_modeloa AS 'Modeloa', 
                          O.ram, O.rom, O.cpu, 
                          G.egoera AS 'egoera_balioa' 
                   FROM Gailuak G 
                   INNER JOIN Ordenagailuak O ON G.id_gailua = O.id_gailua";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbKonexioa.Instantzia.GetKonexioString()))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    da.Fill(dt);
                }
            }
            catch (Exception ex) { /* Log errorea */ }
            return dt;
        }

        // 4. INPRIMAGAILU GUZTIAK (InprimagailuGuztiak.cs-rako)
        public static DataTable GetInprimagailuGuztiak()
        {
            DataTable dt = new DataTable();

            // CASE WHEN erabiliko dugu 0/1 hori testu bihurtzeko
            string sql = @"SELECT G.id_gailua AS ID, 
                          G.identifikazio_kodea AS Kodea, 
                          G.marka_modeloa AS Modeloa, 
                          CASE 
                             WHEN I.koloretakoa = 1 THEN 'Koloretakoa' 
                             ELSE 'Zuri-beltza' 
                          END AS Mota,
                          G.egoera AS egoera_balioa 
                   FROM gailuak G 
                   INNER JOIN inprimagailuak I ON G.id_gailua = I.id_gailua";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbKonexioa.Instantzia.GetKonexioString()))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("SQL Errorea: " + ex.Message);
            }
            return dt;
        }

        // 5. EGOERA EGUNERATU (HondatutakoGailuak edo GailuGuztiak-erako)
        public static bool EguneratuEgoera(int id, int egoeraBerria)
        {
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    conn.Open();
                    string sql = "UPDATE Gailuak SET egoera = @egoera WHERE id_gailua = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@egoera", egoeraBerria);
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch { return false; }
        }

        // 6. GAILUA EZABATU (Historikora eramanez transakzio bidez)
        public static bool EzabatuGailua(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(DbKonexioa.Instantzia.GetKonexioString()))
            {
                conn.Open();
                MySqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // Historikoan sartu lehenik
                    string insertSql = "INSERT INTO ezabatutakoak (identifikazio_kodea, marka_modeloa) " +
                                       "SELECT identifikazio_kodea, marka_modeloa FROM gailuak WHERE id_gailua = @id";
                    MySqlCommand cmdInsert = new MySqlCommand(insertSql, conn, trans);
                    cmdInsert.Parameters.AddWithValue("@id", id);
                    cmdInsert.ExecuteNonQuery();

                    // Gailuetatik ezabatu
                    string deleteSql = "DELETE FROM gailuak WHERE id_gailua = @id";
                    MySqlCommand cmdDelete = new MySqlCommand(deleteSql, conn, trans);
                    cmdDelete.Parameters.AddWithValue("@id", id);
                    cmdDelete.ExecuteNonQuery();

                    trans.Commit();
                    return true;
                }
                catch { trans.Rollback(); return false; }
            }
        }

        // 7. ORDENAGAILU BERRIA SORTU (Transakzioa)
        public static bool GehituOrdenagailua(string kodea, string modeloa, int mintegia, string ram, string rom, string cpu)
        {
            using (MySqlConnection conn = new MySqlConnection(DbKonexioa.Instantzia.GetKonexioString()))
            {
                conn.Open();
                MySqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string sqlGailua = "INSERT INTO gailuak (identifikazio_kodea, marka_modeloa, id_mintegia, egoera) VALUES (@kode, @mod, @min, 0)";
                    MySqlCommand cmdGailua = new MySqlCommand(sqlGailua, conn, trans);
                    cmdGailua.Parameters.AddWithValue("@kode", kodea);
                    cmdGailua.Parameters.AddWithValue("@mod", modeloa);
                    cmdGailua.Parameters.AddWithValue("@min", mintegia);
                    cmdGailua.ExecuteNonQuery();

                    long idBerria = cmdGailua.LastInsertedId;

                    string sqlOrde = "INSERT INTO ordenagailuak (id_gailua, ram, rom, prozesatzailea) VALUES (@id, @ram, @rom, @cpu)";
                    MySqlCommand cmdOrde = new MySqlCommand(sqlOrde, conn, trans);
                    cmdOrde.Parameters.AddWithValue("@id", idBerria);
                    cmdOrde.Parameters.AddWithValue("@ram", ram);
                    cmdOrde.Parameters.AddWithValue("@rom", rom);
                    cmdOrde.Parameters.AddWithValue("@cpu", cpu);
                    cmdOrde.ExecuteNonQuery();

                    trans.Commit();
                    return true;
                }
                catch { trans.Rollback(); return false; }
            }
        }

        // --- LAGUNTZA METODOAK (Kodea ez errepikatzeko) ---

        private static DataTable ExekutatuQuery(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbKonexioa.Instantzia.GetKonexioString()))
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex) { Console.WriteLine("SQL Errorea: " + ex.Message); }
            return dt;
        }

        private static bool ExekutatuNonQuery(string sql, MySqlParameter[] parametroak)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbKonexioa.Instantzia.GetKonexioString()))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    if (parametroak != null) cmd.Parameters.AddRange(parametroak);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch { return false; }
        }
        public static bool EzabatuGailuaOsoa(int id, string mota)
        {
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();

            using (MySqlConnection conn = new MySqlConnection(konexioa))
            {
                conn.Open();
                // Transakzioa hasten dugu segurtasunagatik
                MySqlTransaction trans = conn.BeginTransaction();

                try
                {
                    // 1. HISTORIKORA PASA (Ezabatutakoak taulara)
                    // Lehenik gailuaren datu orokorrak kopiatzen ditugu ezabatu aurretik
                    string sqlInsert = @"INSERT INTO ezabatutakoak (id_gailua, identifikazio_kodea, marka_modeloa, ezabatutako_eguna) 
                               SELECT id_gailua, identifikazio_kodea, marka_modeloa, NOW() 
                               FROM gailuak WHERE id_gailua = @id";

                    MySqlCommand cmdInsert = new MySqlCommand(sqlInsert, conn, trans);
                    cmdInsert.Parameters.AddWithValue("@id", id);
                    cmdInsert.ExecuteNonQuery();

                    // 2. UMEA EZABATU (Ordenagailua edo Inprimagailua)
                    // Taula izena 'mota' aldagaiaren arabera aukeratzen dugu
                    string taulaUmea = (mota == "Ordenagailua") ? "ordenagailuak" : "inprimagailuak";
                    string sqlDeleteUmea = $"DELETE FROM {taulaUmea} WHERE id_gailua = @id";

                    MySqlCommand cmdDeleteUmea = new MySqlCommand(sqlDeleteUmea, conn, trans);
                    cmdDeleteUmea.Parameters.AddWithValue("@id", id);
                    cmdDeleteUmea.ExecuteNonQuery();

                    // 3. GURASOA EZABATU (Gailuak taula)
                    string sqlDeleteGurasoa = "DELETE FROM gailuak WHERE id_gailua = @id";
                    MySqlCommand cmdDeleteGurasoa = new MySqlCommand(sqlDeleteGurasoa, conn, trans);
                    cmdDeleteGurasoa.Parameters.AddWithValue("@id", id);
                    cmdDeleteGurasoa.ExecuteNonQuery();

                    // Dena ondo badoa, aldaketak berretsi
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    // Errore bat badago, dena atzera bota (rollback)
                    trans.Rollback();
                    MessageBox.Show("Errorea ezabatzean: " + ex.Message);
                    return false;
                }
            }
        }
        public static DataTable GetHondatutakoGailuak()
        {
            DataTable dt = new DataTable();
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();

            // UNION: Lehenengo zatiak Ordenagailuak hartzen ditu, bigarrenak Inprimagailuak
            string sql = @"
        SELECT 
            g.id_gailua AS ID, 
            g.identifikazio_kodea AS Kodea, 
            g.marka_modeloa AS Modeloa, 
            m.izena AS Mintegia,
            g.egoera AS egoera_balioa,
            'Ordenagailua' AS Mota
        FROM gailuak g 
        INNER JOIN ordenagailuak o ON g.id_gailua = o.id_gailua
        INNER JOIN mintegiak m ON g.id_mintegia = m.id_mintegia
        WHERE g.egoera = 1
        
        UNION
        
        SELECT 
            g.id_gailua AS ID, 
            g.identifikazio_kodea AS Kodea, 
            g.marka_modeloa AS Modeloa, 
            m.izena AS Mintegia,
            g.egoera AS egoera_balioa,
            'Inprimagailua' AS Mota
        FROM gailuak g 
        INNER JOIN inprimagailuak i ON g.id_gailua = i.id_gailua
        INNER JOIN mintegiak m ON g.id_mintegia = m.id_mintegia
        WHERE g.egoera = 1";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea SQL-an: " + ex.Message);
            }
            return dt;
        }
        public static bool EzabatuGailuBatenDatuak(int id)
        {
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();
            using (MySqlConnection conn = new MySqlConnection(konexioa))
            {
                conn.Open();
                // Transakzio bat erabiltzea gomendatzen da segurtasunagatik
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Umeak ezabatu lehenago (CASCADE ez baduzu DB-an)
                        // DELETE FROM Ordenagailuak WHERE id_gailua = @id;
                        // DELETE FROM Inprimagailuak WHERE id_gailua = @id;

                        // 2. Gurasoa ezabatu
                        string sqlGurasoa = "DELETE FROM Gailuak WHERE id_gailua = @id";
                        MySqlCommand cmd = new MySqlCommand(sqlGurasoa, conn, trans);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();

                        trans.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
        }

        public static bool EzabatuOrdenagailuaOsoa(int id)
        {
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();
            using (MySqlConnection conn = new MySqlConnection(konexioa))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Historikora mugitu
                        string insertQuery = @"
                    INSERT INTO Ezabatutakoak (identifikazio_kodea, id_gailua, marka_modeloa, mota, eroste_data) 
                    SELECT identifikazio_kodea, id_gailua, marka_modeloa, 'Ordenagailua', eroste_data
                    FROM Gailuak WHERE id_gailua = @id";

                        MySqlCommand cmdInsert = new MySqlCommand(insertQuery, conn, trans);
                        cmdInsert.Parameters.AddWithValue("@id", id);
                        cmdInsert.ExecuteNonQuery();

                        // 2. Garbiketa (FK-ak errespetatuz)
                        new MySqlCommand("DELETE FROM Hondatutakoak WHERE id_gailua = " + id, conn, trans).ExecuteNonQuery();
                        new MySqlCommand("DELETE FROM Ordenagailuak WHERE id_gailua = " + id, conn, trans).ExecuteNonQuery();
                        new MySqlCommand("DELETE FROM Gailuak WHERE id_gailua = " + id, conn, trans).ExecuteNonQuery();

                        trans.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
        }

      
        public static bool MugituEtaEzabatu(int id, string mota)
        {
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();
            using (MySqlConnection conn = new MySqlConnection(konexioa))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. KOPIATU Historikora (Lehen konpondu dugun SQL-a)
                        string insertQuery = @"
                    INSERT INTO Ezabatutakoak (identifikazio_kodea, id_gailua, marka_modeloa, mota, eroste_data) 
                    SELECT identifikazio_kodea, id_gailua, marka_modeloa, @mota, eroste_data
                    FROM Gailuak
                    WHERE id_gailua = @id";

                        using (MySqlCommand cmdInsert = new MySqlCommand(insertQuery, conn, trans))
                        {
                            cmdInsert.Parameters.AddWithValue("@id", id);
                            cmdInsert.Parameters.AddWithValue("@mota", mota);
                            cmdInsert.ExecuteNonQuery();
                        }

                        // 2. EZABATU UMEA (Hondatutakoak, Ordenagailuak...)
                        string deleteUmea = (mota == "Ordenagailua") ?
                            "DELETE FROM Ordenagailuak WHERE id_gailua = @id" :
                            "DELETE FROM Inprimagailuak WHERE id_gailua = @id";

                        using (MySqlCommand cmdUmea = new MySqlCommand(deleteUmea, conn, trans))
                        {
                            cmdUmea.Parameters.AddWithValue("@id", id);
                            cmdUmea.ExecuteNonQuery();
                        }

                        // 3. EZABATU GURASOA
                        string deleteGurasoa = "DELETE FROM Gailuak WHERE id_gailua = @id";
                        using (MySqlCommand cmdGurasoa = new MySqlCommand(deleteGurasoa, conn, trans))
                        {
                            cmdGurasoa.Parameters.AddWithValue("@id", id);
                            cmdGurasoa.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}