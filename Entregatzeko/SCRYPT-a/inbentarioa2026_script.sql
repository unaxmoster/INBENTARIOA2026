-- --------------------------------------------
-- 1. DATU-BASEA SORTU ETA AUKERATU
-- --------------------------------------------
-- Sortuta badago ezabatzeko (kasu honetan, iruzkin bezala jarriko dugu
-- DROP DATABASE IF EXISTS inbentarioa2026;
CREATE DATABASE inbentarioa2026;
USE inbentarioa2026;

-- --------------------------------------------
-- 2. TAULAK SORTU
-- --------------------------------------------

-- MINTEGIAK taula
CREATE TABLE mintegiak (
    id_mintegia INT AUTO_INCREMENT PRIMARY KEY,
    izena VARCHAR(100) NOT NULL UNIQUE,
    id_arduraduna INT NULL
);

-- ERABILTZAILEAK taula (IKT-ak ere id_mintegia du)
CREATE TABLE erabiltzaileak (
    id_erabiltzailea INT AUTO_INCREMENT PRIMARY KEY,
    erabiltzailea VARCHAR(50) NOT NULL UNIQUE,
    pasahitza VARCHAR(255) NOT NULL,
    rola ENUM('Ikt', 'MintegiBurua', 'Irakaslea') NOT NULL,
    id_mintegia INT NOT NULL,  -- Edozein erabiltzaile dela, mintegi bati egon behar du esleituta
    FOREIGN KEY (id_mintegia) REFERENCES mintegiak(id_mintegia) ON DELETE RESTRICT
);

-- GAILUAK taula
CREATE TABLE gailuak (
    id_gailua INT AUTO_INCREMENT PRIMARY KEY,
    identifikazio_kodea VARCHAR(50) NOT NULL UNIQUE,
    marka_modeloa VARCHAR(100) NOT NULL,
    id_mintegia INT NOT NULL,
    eroste_data DATE NOT NULL,
    egoera TINYINT NOT NULL DEFAULT 0,
    FOREIGN KEY (id_mintegia) REFERENCES mintegiak(id_mintegia) ON DELETE CASCADE
);

-- ORDENAGAILUAK taula
CREATE TABLE ordenagailuak (
    id_gailua INT PRIMARY KEY,
    ram VARCHAR(20),
    rom VARCHAR(20),
    cpu VARCHAR(100),
    FOREIGN KEY (id_gailua) REFERENCES gailuak(id_gailua) ON DELETE CASCADE
);

-- INPRIMAGAILUAK taula
CREATE TABLE inprimagailuak (
    id_gailua INT PRIMARY KEY,
    koloretakoa BOOLEAN NOT NULL DEFAULT FALSE,
    FOREIGN KEY (id_gailua) REFERENCES gailuak(id_gailua) ON DELETE CASCADE
);

-- HONDATUTAKOAK taula
CREATE TABLE hondatutakoak (
    id_hondatzea INT AUTO_INCREMENT PRIMARY KEY,
    id_gailua INT NOT NULL UNIQUE,
    hondatutako_data DATE NOT NULL,
    matxuraKopurua INT NOT NULL DEFAULT 1,
    deskribapena VARCHAR(255),
    FOREIGN KEY (id_gailua) REFERENCES gailuak(id_gailua) ON DELETE CASCADE
);

-- EZABATUTAKOAK taula
CREATE TABLE ezabatutakoak (
    id_ezabatua INT AUTO_INCREMENT PRIMARY KEY,
    identifikazio_kodea VARCHAR(50),
    marka_modeloa VARCHAR(100),
    mota VARCHAR(20),
    eroste_data DATE,
    ezabatutako_eguna DATETIME NOT NULL
);