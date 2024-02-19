-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: pos_db
-- ------------------------------------------------------
-- Server version	8.3.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `couponexchanges`
--

DROP TABLE IF EXISTS `couponexchanges`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `couponexchanges` (
  `id` varchar(36) NOT NULL,
  `member_id` varchar(36) DEFAULT NULL,
  `exchanged_points` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `couponexchanges`
--

LOCK TABLES `couponexchanges` WRITE;
/*!40000 ALTER TABLE `couponexchanges` DISABLE KEYS */;
/*!40000 ALTER TABLE `couponexchanges` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `items`
--

DROP TABLE IF EXISTS `items`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `items` (
  `id` varchar(36) NOT NULL,
  `name` varchar(100) NOT NULL,
  `Type` varchar(50) NOT NULL,
  `price` decimal(10,2) NOT NULL,
  `qty` int NOT NULL,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `items`
--

LOCK TABLES `items` WRITE;
/*!40000 ALTER TABLE `items` DISABLE KEYS */;
INSERT INTO `items` VALUES ('41c492db-cf0a-11ee-bc04-40b0769ebd81','iPhone 15 pro','non alcohol',999.00,20,'2024-02-16 10:24:45',NULL),('61c492db-cf0a-11ed-qc04-40p0769ebd81','iPhone 15 pro max','non alcohol',1199.00,20,'2024-02-16 10:24:45',NULL),('ba63632f-e862-4gdf-adec-0368f81fbc29','Spy Wine Cooler Black 275ml','alcohol',5.04,50,'2024-02-19 10:18:45',NULL),('ba73631f-e862-4adf-ad1c-0768f91fbc16','Oishi potato chip','non alcohol',0.50,50,'2024-02-16 08:18:45',NULL),('ef52482d-e862-4gdf-adgc-0368f81fb325','Samyang Spicy Hot Chicken Flavor Instant Ramen','non alcohol',4.00,50,'2024-02-16 10:22:45',NULL),('fd96542d-e862-4gdf-ffgc-0368f81eg571','Coca cola 1.25L','non alcohol',6.50,50,'2024-02-17 10:25:45',NULL),('gh76332d-e862-4gdf-adfc-0368f81gl541','Nongshim Neoguri Spicy Seafood Ramen','non alcohol',4.80,50,'2024-02-17 10:24:45',NULL);
/*!40000 ALTER TABLE `items` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `logins`
--

DROP TABLE IF EXISTS `logins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `logins` (
  `id` int NOT NULL,
  `username` varchar(50) DEFAULT NULL,
  `password` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `logins`
--

LOCK TABLES `logins` WRITE;
/*!40000 ALTER TABLE `logins` DISABLE KEYS */;
INSERT INTO `logins` VALUES (1,'pst','123456');
/*!40000 ALTER TABLE `logins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `members`
--

DROP TABLE IF EXISTS `members`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `members` (
  `id` varchar(36) NOT NULL,
  `name` varchar(100) NOT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `otp_code` varchar(10) DEFAULT NULL,
  `otp_expire_time` datetime DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `members`
--

LOCK TABLES `members` WRITE;
/*!40000 ALTER TABLE `members` DISABLE KEYS */;
INSERT INTO `members` VALUES ('754fd8dc-0165-4f29-9f63-3d0b7f5d83e7','kyaw','0912345678','test',NULL,NULL,'2024-02-16 16:04:02',NULL),('fc9013a6-1f23-4991-835e-af76babd0bbc','paing','0912365478','paing@mail.com',NULL,NULL,'2024-02-19 09:12:33',NULL);
/*!40000 ALTER TABLE `members` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `transactiondetails`
--

DROP TABLE IF EXISTS `transactiondetails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `transactiondetails` (
  `detail_id` varchar(36) NOT NULL,
  `id` varchar(36) NOT NULL,
  `item_id` varchar(36) DEFAULT NULL,
  `qty` int DEFAULT NULL,
  `amount` decimal(10,2) DEFAULT NULL,
  `point` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`detail_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `transactiondetails`
--

LOCK TABLES `transactiondetails` WRITE;
/*!40000 ALTER TABLE `transactiondetails` DISABLE KEYS */;
INSERT INTO `transactiondetails` VALUES ('21311cf3-c0dd-44ee-bc04-52b0769ebd81','41c492db-cf0a-11ee-bc04-40b0769ebd81','41c492db-cf0a-11ee-bc04-40b0769ebd81',1,999.00,99.90),('21311cf3-c0dd-44ye-bg04-52b0769ebd81','41c492db-cf0a-11ee-bc04-40b0769ebd81','61c492db-cf0a-11ed-qc04-40p0769ebd81',1,1199.00,119.90),('2dcb8fe3-cedd-11ee-bc04-40b0769ebd81','14be71d7-cedc-11ee-bc04-40b0769ebd81','fd96542d-e862-4gdf-ffgc-0368f81eg571',1,6.50,0.65),('35051c26-cedd-11ee-bc04-40b0769ebd81','cc1bb8db-cedc-11ee-bc04-40b0769ebd81','fd96542d-e862-4gdf-ffgc-0368f81eg571',1,6.50,0.65),('81311cf3-cedd-11ee-bc04-40b0769ebd81','cc1bb8db-cedc-11ee-bc04-40b0769ebd81','gh76332d-e862-4gdf-adfc-0368f81gl541',1,4.80,0.48);
/*!40000 ALTER TABLE `transactiondetails` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `transactions`
--

DROP TABLE IF EXISTS `transactions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `transactions` (
  `id` varchar(36) NOT NULL,
  `member_id` varchar(36) NOT NULL,
  `transaction_date` datetime NOT NULL,
  PRIMARY KEY (`id`),
  KEY `member_id_idx` (`member_id`),
  CONSTRAINT `member_id` FOREIGN KEY (`member_id`) REFERENCES `members` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `transactions`
--

LOCK TABLES `transactions` WRITE;
/*!40000 ALTER TABLE `transactions` DISABLE KEYS */;
INSERT INTO `transactions` VALUES ('14be71d7-cedc-11ee-bc04-40b0769ebd81','754fd8dc-0165-4f29-9f63-3d0b7f5d83e7','2024-02-18 10:26:45'),('41c492db-cf0a-11ee-bc04-40b0769ebd81','fc9013a6-1f23-4991-835e-af76babd0bbc','2024-02-19 15:26:45'),('cc1bb8db-cedc-11ee-bc04-40b0769ebd81','754fd8dc-0165-4f29-9f63-3d0b7f5d83e7','2024-02-19 10:26:45');
/*!40000 ALTER TABLE `transactions` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-02-19 16:51:08
