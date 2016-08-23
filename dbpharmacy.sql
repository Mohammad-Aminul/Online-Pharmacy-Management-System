-- phpMyAdmin SQL Dump
-- version 4.0.4
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Aug 12, 2015 at 09:30 AM
-- Server version: 5.5.32
-- PHP Version: 5.4.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `dbpharmacy`
--
CREATE DATABASE IF NOT EXISTS `dbpharmacy` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `dbpharmacy`;

-- --------------------------------------------------------

--
-- Table structure for table `tbl_admin`
--

CREATE TABLE IF NOT EXISTS `tbl_admin` (
  `username` varchar(40) NOT NULL,
  `password` varchar(40) NOT NULL,
  UNIQUE KEY `username` (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tbl_admin`
--

INSERT INTO `tbl_admin` (`username`, `password`) VALUES
('aa', '11');

-- --------------------------------------------------------

--
-- Table structure for table `tbl_customer_view`
--

CREATE TABLE IF NOT EXISTS `tbl_customer_view` (
  `medicine_name` varchar(30) NOT NULL DEFAULT '',
  `medicine_type` varchar(25) NOT NULL DEFAULT '',
  `unit_price` float DEFAULT NULL,
  `company_name` varchar(40) DEFAULT NULL,
  PRIMARY KEY (`medicine_name`,`medicine_type`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `tbl_online_client`
--

CREATE TABLE IF NOT EXISTS `tbl_online_client` (
  `client_no` int(11) NOT NULL AUTO_INCREMENT,
  `user_name` varchar(20) NOT NULL,
  `address` varchar(50) DEFAULT NULL,
  `mobile_no` varchar(15) NOT NULL,
  `password` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`client_no`),
  UNIQUE KEY `mobile_no` (`mobile_no`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `tbl_order`
--

CREATE TABLE IF NOT EXISTS `tbl_order` (
  `order_no` int(11) NOT NULL AUTO_INCREMENT,
  `client_no` int(11) DEFAULT NULL,
  `medicine_name` varchar(30) NOT NULL,
  `medicine_type` varchar(30) DEFAULT NULL,
  `quantity` int(11) DEFAULT NULL,
  `order_date` varchar(20) NOT NULL,
  `dely_date` varchar(20) NOT NULL,
  `order_status` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`order_no`),
  KEY `fk_order` (`client_no`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `tbl_purchase`
--

CREATE TABLE IF NOT EXISTS `tbl_purchase` (
  `Medicine_id` int(10) NOT NULL AUTO_INCREMENT,
  `Medicine_name` varchar(30) DEFAULT NULL,
  `Medicine_type` varchar(30) DEFAULT NULL,
  `Purchase_quantity` int(50) DEFAULT NULL,
  `Available_quantity` int(20) DEFAULT NULL,
  `Unit_price` float(50,2) DEFAULT NULL,
  `Total_price` double(50,2) DEFAULT NULL,
  `Purchase_date` varchar(9) DEFAULT NULL,
  `Medicine_position` int(10) DEFAULT NULL,
  `Company` varchar(40) DEFAULT NULL,
  PRIMARY KEY (`Medicine_id`),
  UNIQUE KEY `Medicine_name` (`Medicine_name`,`Medicine_type`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=61 ;

--
-- Dumping data for table `tbl_purchase`
--

INSERT INTO `tbl_purchase` (`Medicine_id`, `Medicine_name`, `Medicine_type`, `Purchase_quantity`, `Available_quantity`, `Unit_price`, `Total_price`, `Purchase_date`, `Medicine_position`, `Company`) VALUES
(55, 'Prosma', 'Capsule', 2, 100, 2.00, 200.00, '06-Aug-15', 22, 'Square'),
(56, 'Napa', 'Tablet', 100, 388, 2.00, 3080.00, '06-Aug-15', 11, 'Navana'),
(57, 'Tuska', 'Syrup', 25, 123, 30.00, 3690.00, '5/15/2015', 55, 'Square'),
(58, 'Napa Extra', 'Tablet', 200, 186, 2.00, 372.00, '5/15/2015', 55, 'Square'),
(59, 'Flona Spray', 'Spray', 200, 300, 240.00, 72000.00, '14-Jul-15', 99, 'Square'),
(60, 'Infladex-TN', 'Drop', 200, 199, 120.00, 23880.00, '14-Jul-15', 102, 'Biopharma');

-- --------------------------------------------------------

--
-- Table structure for table `tbl_purchase_history`
--

CREATE TABLE IF NOT EXISTS `tbl_purchase_history` (
  `Medicine_id` int(11) NOT NULL AUTO_INCREMENT,
  `Medicine_type` varchar(20) DEFAULT NULL,
  `Medicine_name` varchar(50) DEFAULT NULL,
  `Last_Purchase_quantity` int(11) DEFAULT NULL,
  `Unit_price` double(20,2) DEFAULT NULL,
  `Total_price` double(50,2) DEFAULT NULL,
  `Comapany_name` varchar(50) DEFAULT NULL,
  `Medicine_position` int(11) DEFAULT NULL,
  `Availble_quantity` int(11) DEFAULT NULL,
  `Purchase_date` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`Medicine_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=112 ;

--
-- Dumping data for table `tbl_purchase_history`
--

INSERT INTO `tbl_purchase_history` (`Medicine_id`, `Medicine_type`, `Medicine_name`, `Last_Purchase_quantity`, `Unit_price`, `Total_price`, `Comapany_name`, `Medicine_position`, `Availble_quantity`, `Purchase_date`) VALUES
(100, 'Capsule', 'Prosma', 50, 2.00, 500.00, 'Square', 22, 250, '5/15/2015'),
(101, 'Syrup', 'Tuska', 80, 30.00, 3000.00, 'Square', 55, 100, '5/15/2015'),
(102, 'Syrup', 'Tuska', 25, 30.00, 3750.00, 'Square', 55, 125, '5/15/2015'),
(103, 'Tablet', 'Napa Extra', 200, 2.00, 400.00, 'Square', 55, 200, '5/15/2015'),
(104, 'Spray', 'Flona Spray', 100, 240.00, 24000.00, 'Square', 99, 100, '5/16/2015'),
(105, 'Drop', 'Infladex-TN', 100, 120.00, 12000.00, 'Biopharma', 108, 100, '14-Jul-15'),
(106, 'Tablet', 'Napa', 200, 2.00, 642.00, 'Navana', 11, 321, '14-Jul-15'),
(107, 'Spray', 'Flona Spray', 200, 240.00, 72000.00, 'Square', 99, 300, '14-Jul-15'),
(108, 'Drop', 'Infladex-TN', 200, 120.00, 24000.00, 'Biopharma', 102, 200, '14-Jul-15'),
(109, 'Capsule', 'Prosma', 55, 2.00, 400.00, 'Square', 22, 200, '14-Jul-15'),
(110, 'Tablet', 'Napa', 100, 2.00, 3080.00, 'Navana', 11, 388, '06-Aug-15'),
(111, 'Capsule', 'Prosma', 2, 2.00, 200.00, 'Square', 22, 100, '06-Aug-15');

-- --------------------------------------------------------

--
-- Table structure for table `tbl_sales`
--

CREATE TABLE IF NOT EXISTS `tbl_sales` (
  `Medicine_name` varchar(30) NOT NULL,
  `Medicine_type` varchar(30) NOT NULL,
  `Sales_quantity` int(11) DEFAULT NULL,
  `unit_price` double(20,2) NOT NULL,
  `total_price` double(50,2) DEFAULT NULL,
  `sell_date` varchar(30) DEFAULT NULL,
  `Company_name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tbl_sales`
--

INSERT INTO `tbl_sales` (`Medicine_name`, `Medicine_type`, `Sales_quantity`, `unit_price`, `total_price`, `sell_date`, `Company_name`) VALUES
('Napa', 'Tablet', 50, 2.00, 100.00, '5/15/2015', 'Navana'),
('Napa', 'Tablet', 50, 2.00, 100.00, '5/15/2015', 'Navana'),
('Prosma', 'Capsule', 50, 2.00, 100.00, '5/15/2015', 'Square'),
('Prosma', 'Capsule', 25, 2.00, 50.00, '5/16/2015', 'Square'),
('Prosma', 'Capsule', 25, 2.00, 50.00, '5/13/2015', 'Square'),
('Napa', 'Tablet', 2, 2.00, 4.00, '5/16/2015', 'Navana'),
('Napa', 'Tablet', 0, 2.00, 0.00, '5/16/2015', 'Navana'),
('Napa', 'Tablet', 2, 2.00, 4.00, '5/16/2015', 'Navana'),
('Napa Extra', 'Tablet', 2, 2.00, 4.00, '5/16/2015', 'Square'),
('Prosma', 'Capsule', 5, 2.00, 10.00, '14-Jul-15', 'Square'),
('Napa', 'Tablet', 21, 2.00, 42.00, '17-Jul-15', 'Navana'),
('Prosma', ' ', 20, 2.00, 40.00, '17-Jul-15', 'Square'),
('Prosma', 'Capsule', 100, 2.00, 200.00, '17-Jul-15', 'Square'),
('Prosma', 'Capsule', 2, 2.00, 4.00, '17-Jul-15', 'Square'),
('Prosma', ' ', 8, 2.00, 16.00, '17-Jul-15', 'Square'),
('Napa', 'Tablet', 12, 10.00, 120.00, '06-Aug-15', 'Navana'),
('Infladex-TN', 'Drop', 1, 120.00, 120.00, '06-Aug-15', 'Biopharma'),
('Napa Extra', 'Tablet', 12, 2.00, 24.00, '06-Aug-15', 'Square'),
('Tuska', 'Syrup', 2, 30.00, 60.00, '06-Aug-15', 'Square');

--
-- Constraints for dumped tables
--

--
-- Constraints for table `tbl_order`
--
ALTER TABLE `tbl_order`
  ADD CONSTRAINT `fk_order` FOREIGN KEY (`client_no`) REFERENCES `tbl_online_client` (`client_no`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
