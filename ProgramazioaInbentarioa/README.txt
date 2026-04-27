================================================
   INBENTARIOA 2026 - GAILUEN KUDEAKETA APLIKAZIOA
================================================

Aplikazio honek gailu informatikoen (ordenagailuak eta inprimagailuak) inbentarioa kudeatzeko aukera ematen du, 
erabiltzaile desberdinen roletan oinarritutako baimen sistema batekin.

-----------------------------------------------
1. DESKARGA
-----------------------------------------------

Aplikazioa deskargatu webgunetik;

-----------------------------------------------
2. INSTALAZIOA
-----------------------------------------------

2.1 Datu-basea konfiguratu

    1. Joan MySQL zerbitzarira (MySQL Workbench)
    2. Exekutatu script-a: "inbentarioa2026_script.sql"
    3. Script-ak datu-basea, taulak eta hasierako datuak sortuko ditu

2.2 Aplikazioa konfiguratu

    1. Ireki "DbKonexioa.cs" fitxategia (Inbentarioa.DatuBasie karpetan)
    2. Aldatu konexio-katea zure MySQL zerbitzarira egokitzeko:
       
       konexioString = "Server=ZURE_SERBIDOREA;Database=inbentarioa2026;Uid=erailtzaileIZENA;Pwd=zurePASAHITZA;";
       
    3. Konpilatu proiektua Visual Studio-rekin (Ctrl+Shift+B)

2.3 Exekutagarria sortu

    - "Release" moduan konpilatu eta "bin/Release/" karpetan aurkituko duzu .exe fitxategia

-----------------------------------------------
3. HASIERAKO DATUAK (LOGIN)
-----------------------------------------------

Jarraian erabiltzaileekin has zaitezke:

| Erabiltzailea | Pasahitza | Rola         | Mintegia (id) |
|---------------|-----------|--------------|---------------|
| admin         | admin     | IKT          | Informatika (1) |
| itxaso        | pass      | MintegiBurua | Informatika (1) |
| mertxe        | pass      | MintegiBurua | Idazkaritza (2) |
| irakasle1     | pass      | Irakaslea    | Informatika (1) |

-----------------------------------------------
4. NOLA FUNZIONATZEN DU
-----------------------------------------------

4.1 ROLAK ETA BAIMENAK

    IKTa (Ikt):
        - Gailu guztiak ikusi, aldatu, sortu eta ezabatu ditzake
        - Erabiltzaileak eta mintegiak kudeatu ditzake
    
    MintegiBurua:
        - Bere mintegiko gailuak BAKARRIK ikusi eta aldatu ditzake
        - Ezin ditu gailu berririk sortu edo ezabatu
        - Ezin ditu erabiltzaileak edo mintegiak kudeatu
    
    Irakaslea:
        - Gailu guztiak ikusi ditzake, baina EZIN DITU ALDATU
        - Ezin ditu erabiltzaileak edo mintegiak kudeatu

4.2 NABIGAZIOA

    Menua nagusia:
        - GAILUAK: Gailu guztien zerrendak ikusteko (ordenagailuak, inprimagailuak, mintegika...)
        - HONDATUTAKO GAILUAK: Matxuratuta dauden gailuak ikusteko eta beraien egoera aldatzeko
        - EZABATUTAKO GAILUAK: Historikora mugitutako gailuak ikusteko. Behin ezabatzuz gero, ezin dira berriro berreskuratu.
        - MINTEGIAK: Mintegiak sortu eta ezabatzeko (IKT bakarrik)
        - ERABILTZAILEAK: Erabiltzaileak sortu eta ezabatzeko (IKT bakarrik)

4.3 EGOERA ALDATZEA

    Gailu baten egoera aldatzeko:
        1. Hautatu gailua DataGridView-an
        2. Aukeratu egoera berria "Egoera Berria" ComboBox-ean
           - Ondo: Gailua egoera onean
           - Hondatuta: Gailua matxuratuta (hondatutakoak taulan gordeko da)
           - Konpontzen: Gailua konpontzen ari da
	3. Hautatu zutabea
        4. Sakatu "EGOERA ALDATU" botoia
	----Berak automatikoki gordetzen du eta DataGridView-a kargatzen du datu berriekin ----

    OHARRA: "Hondatuta" aukeratzen duzun bakoitzean, "matxuraKopurua" +1 egingo da.

4.4 GAILU BERRIA SORTZEA (IKT bakarrik)

    Ordenagailu berria:
        1. Gailuak -> Ordenagailuak -> Berria Sortu
        2. Bete datuak: identifikazio kodea, marka/modeloa, mintegia, RAM, ROM, CPU
        3. Sakatu "BERRIA SORTU"

    Inprimagailu berria:
        1. Gailuak -> Inprimagailuak -> Berria Sortu
        2. Bete datuak: identifikazio kodea, marka/modeloa, mintegia, koloretakoa (koloretakoa/Zuri-beltza)
        3. Sakatu "BERRIA SORTU"

4.5 GAILUA EZABATZEA (IKT bakarrik)

    1. Hautatu ezabatu nahi duzun gailua
    2. Sakatu "EZABATU" botoia
    3. Berretsi ekintza
    4. Gailua "ezabatutakoak" taulan gordeko da (historikoa) eta gailu nagusietatik ezabatuko da. Hori egitean, ezingo da gailu hori berreskuratu.

4.6 ERABILTZAILE BERRIA SORTZEA (IKT bakarrik)

    1. Menuan -> ERABILTZAILEAK -> Berria Sortu
    2. Bete datuak: erabiltzaile izena, pasahitza, rola, mintegia
    3. Sakatu "GORDE"

-----------------------------------------------
5. DATU-BASEAREN EGITURA (TABLAK)
-----------------------------------------------

- mintegiak: Mintegiak gordetzeko
- erabiltzaileak: Erabiltzaileak gordetzeko (rola, pasahitza...)
- gailuak: Gailu guztien datu orokorrak (identifikazioa, marka, mintegia, egoera)
- ordenagailuak: Ordenagailuen datu espezifikoak (RAM, ROM, CPU)
- inprimagailuak: Inprimagailuen datu espezifikoak (koloretakoa)
- hondatutakoak: Gailu hondatuen jarraipena (matxura kopurua, azken data)
- ezabatutakoak: Ezabatutako gailuen historiala

-----------------------------------------------
6. ARAZOAK ETA KONPONBIDEAK (TROUBLESHOOTING)
-----------------------------------------------

6.1 "Ezin da datu-basearekin konektatu" errorea

    Konponbidea: Egiaztatu "DbKonexioa.cs" karpetako konexio-katea zuzena dela.

6.2 "No se pudo encontrar la columna denominada egoera_balioa" errorea

    Konponbidea: Datu-basea eguneratu (script berriro exekutatu).

-----------------------------------------------
7. EGILEA
-----------------------------------------------

Aplikazioa: ByteGuardians taldea
Ikasturtea: 2025-2026
Modulua: Programazioa (PAAG)
Beñat Etxeberria-Unax Muñoz

-----------------------------------------------
8. LIZENTZIA
-----------------------------------------------

Aplikazio hau hezkuntza erabilerarako garatutako proiektu bat da. Ez da erantzukizunik hartzen erabilera komertzialagatik.

================================================
                     BUKAERA
================================================