-- MySQL dump 10.13  Distrib 8.0.31, for Win64 (x86_64)
--
-- Host: localhost    Database: processoseletivo
-- ------------------------------------------------------
-- Server version	8.0.31

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `funcionarios`
--

DROP TABLE IF EXISTS `funcionarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `funcionarios` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(200) NOT NULL,
  `data_nascimento` datetime NOT NULL,
  `cpf` char(11) NOT NULL,
  `email` varchar(100) DEFAULT NULL,
  `id_lotacao` int NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `funcionarios_cpf_un` (`cpf`),
  UNIQUE KEY `funcionarios_email_un` (`email`),
  KEY `funcionario_lotacao` (`id_lotacao`),
  CONSTRAINT `funcionario_lotacao` FOREIGN KEY (`id_lotacao`) REFERENCES `lotacao` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `funcionarios`
--

LOCK TABLES `funcionarios` WRITE;
/*!40000 ALTER TABLE `funcionarios` DISABLE KEYS */;
INSERT INTO `funcionarios` VALUES (1,'Clarice Ferreira','2001-02-25 00:00:00','123','clarice@ferreira.com.br',1),(2,'Brenda Caldeira','1986-01-10 00:00:00','456','brenda@caldeira.com.br',2),(3,'Elisa Silva','1943-12-10 00:00:00','789','elisa@silva.com.br',2),(4,'João Felipe da Costa','1955-02-01 00:00:00','101112','joao@felipe.com.br',3),(5,'Teste Inclusão','2023-04-25 15:01:39','131415','novo@funcionario.com',2),(6,'Teste de Inclusão 2','2023-04-25 20:22:26','161718','teste.2@inclusao.com',1),(10,'João da Silva','2023-04-25 20:29:26','999','joao@silva.com.br',1),(14,'Teste Inclusão 3','2023-03-27 03:00:00','1234','teste.3@inclusao.com',1),(15,'Teste Inclusão 4','1971-09-02 03:00:00','112233','teste.4@inclusao.com',1);
/*!40000 ALTER TABLE `funcionarios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lotacao`
--

DROP TABLE IF EXISTS `lotacao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `lotacao` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(100) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lotacao`
--

LOCK TABLES `lotacao` WRITE;
/*!40000 ALTER TABLE `lotacao` DISABLE KEYS */;
INSERT INTO `lotacao` VALUES (1,'GPC'),(2,'GES'),(3,'GEFIN'),(4,'GOV');
/*!40000 ALTER TABLE `lotacao` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'processoseletivo'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-04-27 16:45:07
