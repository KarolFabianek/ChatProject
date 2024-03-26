-- phpMyAdmin SQL Dump
-- version 5.1.1deb5ubuntu1
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Czas generowania: 25 Mar 2024, 20:49
-- Wersja serwera: 10.6.16-MariaDB-0ubuntu0.22.04.1
-- Wersja PHP: 8.1.2-1ubuntu2.14

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Baza danych: `karol-testowy`
--

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `Clients`
--

CREATE TABLE `Clients` (
  `ID` int(11) NOT NULL,
  `Email` varchar(60) NOT NULL,
  `Password` varchar(40) NOT NULL,
  `Nickname` varchar(40) NOT NULL,
  `Guid_ID` varbinary(128) NOT NULL,
  `Age` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `MessagesHistory`
--

CREATE TABLE `MessagesHistory` (
  `ID` int(50) NOT NULL,
  `Nickname` varchar(50) NOT NULL,
  `Message` varchar(500) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Indeksy dla zrzutów tabel
--

--
-- Indeksy dla tabeli `Clients`
--
ALTER TABLE `Clients`
  ADD PRIMARY KEY (`ID`);

--
-- Indeksy dla tabeli `MessagesHistory`
--
ALTER TABLE `MessagesHistory`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT dla zrzuconych tabel
--

--
-- AUTO_INCREMENT dla tabeli `Clients`
--
ALTER TABLE `Clients`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
