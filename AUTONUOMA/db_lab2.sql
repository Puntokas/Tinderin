-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 22, 2023 at 10:21 AM
-- Server version: 10.4.27-MariaDB
-- PHP Version: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `db_lab2`
--

-- --------------------------------------------------------

--
-- Table structure for table `automobiliai`
--

CREATE TABLE `automobiliai` (
  `vin_kodas` varchar(50) NOT NULL,
  `pagaminimo_data` date NOT NULL,
  `mase` float NOT NULL,
  `fk_marke` varchar(50) NOT NULL,
  `fk_modelis` varchar(50) NOT NULL,
  `fk_pavaru_deze` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `automobiliai`
--

INSERT INTO `automobiliai` (`vin_kodas`, `pagaminimo_data`, `mase`, `fk_marke`, `fk_modelis`, `fk_pavaru_deze`) VALUES
('1FTEW1CP5GKD05169', '2016-08-10', 2185.6, 'Ford', 'F-150', 2),
('1G1FE1R79H0189064', '2017-09-05', 1512.1, 'Chevrolet', 'Camaro', 3),
('1G1ZD5ST7JF171744', '2018-08-02', 2025.6, 'Chevrolet', 'Silverado', 4),
('1N4AL3APXHC279752', '2017-12-01', 1585.3, 'Nissan', 'GT-R', 3),
('23SASQW2434STI2121', '2000-01-01', 1234, 'Hyundai', 'Accent', 1),
('343434STI232', '2000-01-01', 1, 'Chevrolet', '5 Series', 1),
('3434DD89JA219195', '1994-01-20', 1234, 'Subaru', 'WRX STI', 1),
('343ASER434STI232', '2000-01-01', 1234, 'Audi', 'A6', 1),
('3C4PDCBG8ET106360', '2014-12-19', 1780.4, 'Audi', 'A4', 4),
('4433XSTG3434STI232', '2000-01-01', 1212, 'BMW', '3 Series', 1),
('5UXKR0C50H0U19000', '2017-03-15', 1950, 'BMW', 'X5', 2),
('ASRX3434STI232', '2000-01-01', 1234.2, 'Volvo', '240', 3),
('BMW12342002AQ', '1987-01-01', 1212, 'BMW', '2002', 3),
('JTDKN3DU9A0193552', '2010-05-20', 1520.5, 'Nissan', 'Pathfinder', 1),
('JTEBU5JR3F5246851', '2015-11-03', 2075, 'Toyota', 'Camry', 1),
('LGC3432AWS221', '1996-07-19', 1200, 'Subaru', 'Legacy', 2),
('OBK343233ASW3', '2008-01-25', 1539, 'Subaru', 'Outback', 1),
('W5QAE32323345', '2002-01-08', 1564, 'Subaru', 'WRX', 1),
('WDDGF8AB3DR297033', '2013-06-24', 1780, 'Mercedes-Benz', 'CLS-Class', 3),
('WRX232323345', '2002-01-24', 1564, 'Subaru', 'WRX STI', 1),
('WRX343233ASW3', '2008-01-25', 1232, 'Subaru', 'WRX STI', 1),
('WRX343233I2121', '2000-01-01', 1232, 'Subaru', 'WRX STI', 1),
('WRX3432AWS221', '1991-07-18', 1200, 'Subaru', 'WRX', 3),
('WWEWSTG3434STI232', '2000-01-01', 1123, 'Honda', 'Accord', 3);

-- --------------------------------------------------------

--
-- Table structure for table `markes`
--

CREATE TABLE `markes` (
  `pavadinimas` varchar(50) NOT NULL,
  `kompanija` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `markes`
--

INSERT INTO `markes` (`pavadinimas`, `kompanija`) VALUES
('Audi', 'Volkswagen Group'),
('BMW', 'Bayerische Motoren Werke AG'),
('Chevrolet', 'General Motors Company'),
('Ferrari', 'Ferrari S.p.A'),
('Ford', 'Ford Motor Company'),
('Honda', 'Honda Motor Company, Ltd.'),
('Hyundai', 'Hyundai Motor Company'),
('Mercedes-Benz', 'Daimler AG'),
('Mitsubishi', 'Mitsubishi Heavy Industries, Ltd.'),
('Nissan', 'Nissan Motor Co., Ltd.'),
('Subaru', 'Fuji Heavy Industries'),
('Toyota', 'Toyota Motor Corporation'),
('Volvo', 'Volvo Car Corporation');

-- --------------------------------------------------------

--
-- Table structure for table `modeliai`
--

CREATE TABLE `modeliai` (
  `pavadinimas` varchar(50) NOT NULL,
  `isleidimo_metai` int(4) UNSIGNED NOT NULL,
  `fk_marke` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `modeliai`
--

INSERT INTO `modeliai` (`pavadinimas`, `isleidimo_metai`, `fk_marke`) VALUES
('A4', 1994, 'Audi'),
('A6', 1994, 'Audi'),
('Q5', 2008, 'Audi'),
('2002', 1987, 'BMW'),
('3 Series', 1975, 'BMW'),
('M3', 1985, 'BMW'),
('X5', 1999, 'BMW'),
('5 Series', 1990, 'Chevrolet'),
('Camaro', 1966, 'Chevrolet'),
('Silverado', 1998, 'Chevrolet'),
('Explorer', 1990, 'Ford'),
('F-150', 1948, 'Ford'),
('Mustang', 1964, 'Ford'),
('Accord', 1976, 'Honda'),
('Civic', 1972, 'Honda'),
('CR-V', 1995, 'Honda'),
('Accent', 1994, 'Hyundai'),
('Elantra', 1990, 'Hyundai'),
('Tucson', 2004, 'Hyundai'),
('CLS-Class', 2004, 'Mercedes-Benz'),
('E-Class', 1993, 'Mercedes-Benz'),
('S-Class', 1972, 'Mercedes-Benz'),
('GT-R', 1969, 'Nissan'),
('Pathfinder', 1985, 'Nissan'),
('Rogue', 2007, 'Nissan'),
('Legacy', 1996, 'Subaru'),
('Outback', 1994, 'Subaru'),
('WRX', 1994, 'Subaru'),
('WRX STI', 1996, 'Subaru'),
('Camry', 1982, 'Toyota'),
('Corolla', 1966, 'Toyota'),
('RAV4', 1994, 'Toyota'),
('240', 1967, 'Volvo'),
('S60', 2000, 'Volvo'),
('XC60', 2008, 'Volvo');

-- --------------------------------------------------------

--
-- Table structure for table `pavaru_dezes`
--

CREATE TABLE `pavaru_dezes` (
  `kodas` int(11) NOT NULL,
  `pavaru_sk` int(11) NOT NULL,
  `maks_leistina_galia` int(11) NOT NULL,
  `tipas` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `pavaru_dezes`
--

INSERT INTO `pavaru_dezes` (`kodas`, `pavaru_sk`, `maks_leistina_galia`, `tipas`) VALUES
(1, 5, 250, 'Mechaninė'),
(2, 6, 350, 'Mechaninė'),
(3, 8, 600, 'Automatinė'),
(4, 4, 300, 'Automatinė');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `automobiliai`
--
ALTER TABLE `automobiliai`
  ADD PRIMARY KEY (`vin_kodas`),
  ADD KEY `fkc_modelis` (`fk_marke`,`fk_modelis`),
  ADD KEY `fkc_pavaru_deze` (`fk_pavaru_deze`);

--
-- Indexes for table `markes`
--
ALTER TABLE `markes`
  ADD PRIMARY KEY (`pavadinimas`);

--
-- Indexes for table `modeliai`
--
ALTER TABLE `modeliai`
  ADD PRIMARY KEY (`fk_marke`,`pavadinimas`);

--
-- Indexes for table `pavaru_dezes`
--
ALTER TABLE `pavaru_dezes`
  ADD PRIMARY KEY (`kodas`);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `automobiliai`
--
ALTER TABLE `automobiliai`
  ADD CONSTRAINT `fkc_modelis` FOREIGN KEY (`fk_marke`,`fk_modelis`) REFERENCES `modeliai` (`fk_marke`, `pavadinimas`),
  ADD CONSTRAINT `fkc_pavaru_deze` FOREIGN KEY (`fk_pavaru_deze`) REFERENCES `pavaru_dezes` (`kodas`);

--
-- Constraints for table `modeliai`
--
ALTER TABLE `modeliai`
  ADD CONSTRAINT `fkc_marke` FOREIGN KEY (`fk_marke`) REFERENCES `markes` (`pavadinimas`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
