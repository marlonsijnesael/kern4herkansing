-- phpMyAdmin SQL Dump
-- version 4.7.1
-- https://www.phpmyadmin.net/
--
-- Host: sql7.freemysqlhosting.net
-- Generation Time: Aug 06, 2020 at 05:06 PM
-- Server version: 5.5.62-0ubuntu0.14.04.1
-- PHP Version: 7.0.33-0ubuntu0.16.04.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `sql7358392`
--

-- --------------------------------------------------------

--
-- Table structure for table `score`
--

CREATE TABLE `score` (
  `ID` int(11) NOT NULL,
  `username` varchar(16) DEFAULT NULL,
  `score` int(6) NOT NULL DEFAULT '0',
  `date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `score`
--

INSERT INTO `score` (`ID`, `username`, `score`, `date`) VALUES
(258, 'marlonsijnesael', 100, '2020-08-06 15:50:22'),
(259, 'marlontest', 80, '2020-08-06 15:50:22'),
(260, 'marlontest', 10, '2020-08-06 15:59:15'),
(261, 'marlonsijnesael', 20, '2020-08-06 15:59:15'),
(262, 'marlonsijnesael', 10, '2020-08-06 16:42:57'),
(263, 'nogeenuser', 10, '2020-08-06 16:42:57');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `ID` int(10) NOT NULL,
  `mail` varchar(256) NOT NULL,
  `username` varchar(50) NOT NULL DEFAULT '0',
  `password` varchar(255) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`ID`, `mail`, `username`, `password`) VALUES
(83, 'm.a.sijnesael@gmail.com', 'marlonsijnesael', '$2y$10$kbuH.LpJuS/acyRCMGeLS.JSJrdZqHSBP5xJeZQkwwIwXYLmBJFAy'),
(84, 'jcjislief@gmail.com', 'joellalief', '$2y$10$NWNyGyKwWNpGcGiYbj5ZFOaXtBcBw7mqv/e4SVIIMOsce9SnkqJt2'),
(85, 'asdasdasda@gmail.com', 'sadasdsadasasd', '$2y$10$XN5pnr0jEKYA606YIpuOHe64ktwq3PLFyMNjzzN5xpdJYlP.qo2LC'),
(86, 'test@gmail.com', 'marlontest', '$2y$10$dMQjpkiGZc9Au5WTOyPmo.2O7pGjZrGwY3QBONrzZ4gPZHcvGxBne'),
(87, 'asdasdasddas@gmail.com', 'nogeenuser', '$2y$10$EeOqIavwfe1vkcNMASQXvuV4TsyHZzTltrdUnutzg8MwgvH0CODJi');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `score`
--
ALTER TABLE `score`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `ID` (`ID`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `score`
--
ALTER TABLE `score`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=264;
--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `ID` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=88;COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
