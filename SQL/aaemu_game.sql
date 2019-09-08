-- CREATE DATABASE  IF NOT EXISTS `aaemu_game` /*!40100 DEFAULT CHARACTER SET latin1 */;
-- USE `aaemu_game`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: aaemu_game
-- ------------------------------------------------------
-- Server version	5.7.23-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `abilities`
--

DROP TABLE IF EXISTS `abilities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `abilities` (
  `id` tinyint(3) unsigned NOT NULL,
  `exp` int(11) NOT NULL,
  `owner` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id`,`owner`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `actabilities`
--

DROP TABLE IF EXISTS `actabilities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `actabilities` (
  `id` int(10) unsigned NOT NULL,
  `point` int(10) unsigned NOT NULL DEFAULT '0',
  `step` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `owner` int(10) unsigned NOT NULL,
  PRIMARY KEY (`owner`,`id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `appellations`
--

DROP TABLE IF EXISTS `appellations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `appellations` (
  `id` int(10) unsigned NOT NULL,
  `active` tinyint(1) NOT NULL DEFAULT '0',
  `owner` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`,`owner`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `blocked`
--

DROP TABLE IF EXISTS `blocked`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `blocked` (
  `owner` int(11) NOT NULL,
  `blocked_id` int(11) NOT NULL,
  PRIMARY KEY (`owner`,`blocked_id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `cash_shop_item`
--

DROP TABLE IF EXISTS `cash_shop_item`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cash_shop_item` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT 'shop_id',
  `uniq_id` int(10) unsigned DEFAULT '0' COMMENT '唯一ID',
  `cash_name` varchar(255) NOT NULL COMMENT '出售名称',
  `main_tab` tinyint(3) unsigned DEFAULT '1' COMMENT '主分类1-6',
  `sub_tab` tinyint(3) unsigned DEFAULT '1' COMMENT '子分类1-7',
  `level_min` tinyint(3) unsigned DEFAULT '0' COMMENT '等级限制',
  `level_max` tinyint(3) unsigned DEFAULT '0' COMMENT '等级限制',
  `item_template_id` int(10) unsigned DEFAULT '0' COMMENT '物品模板id',
  `is_sell` tinyint(1) unsigned DEFAULT '0' COMMENT '是否出售',
  `is_hidden` tinyint(1) unsigned DEFAULT '0' COMMENT '是否隐藏',
  `limit_type` tinyint(3) unsigned DEFAULT '0',
  `buy_count` smallint(5) unsigned DEFAULT '0',
  `buy_type` tinyint(3) unsigned DEFAULT '0',
  `buy_id` int(10) unsigned DEFAULT '0',
  `start_date` datetime DEFAULT '0001-01-01 00:00:00' COMMENT '出售开始',
  `end_date` datetime DEFAULT '0001-01-01 00:00:00' COMMENT '出售截止',
  `type` tinyint(3) unsigned DEFAULT '0' COMMENT '货币类型',
  `price` int(10) unsigned DEFAULT '0' COMMENT '价格',
  `remain` int(10) unsigned DEFAULT '0' COMMENT '剩余数量',
  `bonus_type` int(10) unsigned DEFAULT '0' COMMENT '赠送类型',
  `bouns_count` int(10) unsigned DEFAULT '0' COMMENT '赠送数量',
  `cmd_ui` tinyint(1) unsigned DEFAULT '0' COMMENT '是否限制一人一次',
  `item_count` int(10) unsigned DEFAULT '1' COMMENT '捆绑数量',
  `select_type` tinyint(3) unsigned DEFAULT '0',
  `default_flag` tinyint(3) unsigned DEFAULT '0',
  `event_type` tinyint(3) unsigned DEFAULT '0' COMMENT '活动类型',
  `event_date` datetime DEFAULT '0001-01-01 00:00:00' COMMENT '活动时间',
  `dis_price` int(10) unsigned DEFAULT '0' COMMENT '当前售价',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=20100054 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='此表来自于代码中的字段并去除重复字段生成。字段名称和内容以代码为准。';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `characters`
--

DROP TABLE IF EXISTS `characters`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `characters` (
  `id` int(11) unsigned NOT NULL,
  `account_id` int(11) unsigned NOT NULL,
  `name` varchar(128) NOT NULL,
  `access_level` int(3) unsigned NOT NULL DEFAULT '0',
  `race` tinyint(2) NOT NULL,
  `gender` tinyint(1) NOT NULL,
  `unit_model_params` blob NOT NULL,
  `level` tinyint(4) NOT NULL,
  `expirience` int(11) NOT NULL,
  `recoverable_exp` int(11) NOT NULL,
  `hp` int(11) NOT NULL,
  `mp` int(11) NOT NULL,
  `labor_power` mediumint(9) NOT NULL,
  `labor_power_modified` datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
  `consumed_lp` int(11) NOT NULL,
  `ability1` tinyint(4) NOT NULL,
  `ability2` tinyint(4) NOT NULL,
  `ability3` tinyint(4) NOT NULL,
  `world_id` int(11) unsigned NOT NULL,
  `zone_id` int(11) unsigned NOT NULL,
  `x` float NOT NULL,
  `y` float NOT NULL,
  `z` float NOT NULL,
  `rotation_x` tinyint(4) NOT NULL,
  `rotation_y` tinyint(4) NOT NULL,
  `rotation_z` tinyint(4) NOT NULL,
  `faction_id` int(11) unsigned NOT NULL,
  `faction_name` varchar(128) NOT NULL,
  `expedition_id` int(11) NOT NULL,
  `family` int(11) unsigned NOT NULL,
  `dead_count` mediumint(8) unsigned NOT NULL,
  `dead_time` datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
  `rez_wait_duration` int(11) NOT NULL,
  `rez_time` datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
  `rez_penalty_duration` int(11) NOT NULL,
  `leave_time` datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
  `money` bigint(20) NOT NULL,
  `money2` bigint(20) NOT NULL,
  `honor_point` int(11) NOT NULL,
  `vocation_point` int(11) NOT NULL,
  `crime_point` int(11) NOT NULL,
  `crime_record` int(11) NOT NULL,
  `delete_request_time` datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
  `transfer_request_time` datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
  `delete_time` datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
  `bm_point` int(11) NOT NULL,
  `auto_use_aapoint` tinyint(1) NOT NULL,
  `prev_point` int(11) NOT NULL,
  `point` int(11) NOT NULL,
  `gift` int(11) NOT NULL,
  `num_inv_slot` tinyint(3) unsigned NOT NULL DEFAULT '50',
  `num_bank_slot` smallint(5) unsigned NOT NULL DEFAULT '50',
  `expanded_expert` tinyint(4) NOT NULL,
  `slots` blob NOT NULL,
  `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
  `deleted` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`,`account_id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `completed_quests`
--

DROP TABLE IF EXISTS `completed_quests`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `completed_quests` (
  `id` int(11) unsigned NOT NULL,
  `data` tinyblob NOT NULL,
  `owner` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id`,`owner`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `expedition_members`
--

DROP TABLE IF EXISTS `expedition_members`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `expedition_members` (
  `character_id` int(11) NOT NULL,
  `expedition_id` int(11) NOT NULL,
  `name` varchar(128) NOT NULL,
  `level` tinyint(4) unsigned NOT NULL,
  `role` tinyint(4) unsigned NOT NULL,
  `last_leave_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `ability1` tinyint(4) unsigned NOT NULL,
  `ability2` tinyint(4) unsigned NOT NULL,
  `ability3` tinyint(4) unsigned NOT NULL,
  `memo` varchar(128) NOT NULL,
  PRIMARY KEY (`character_id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `expedition_role_policies`
--

DROP TABLE IF EXISTS `expedition_role_policies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `expedition_role_policies` (
  `expedition_id` int(11) NOT NULL,
  `role` tinyint(4) unsigned NOT NULL,
  `name` varchar(128) NOT NULL,
  `dominion_declare` tinyint(1) NOT NULL,
  `invite` tinyint(1) NOT NULL,
  `expel` tinyint(1) NOT NULL,
  `promote` tinyint(1) NOT NULL,
  `dismiss` tinyint(1) NOT NULL,
  `chat` tinyint(1) NOT NULL,
  `manager_chat` tinyint(1) NOT NULL,
  `siege_master` tinyint(1) NOT NULL,
  `join_siege` tinyint(1) NOT NULL,
  PRIMARY KEY (`expedition_id`,`role`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `expeditions`
--

DROP TABLE IF EXISTS `expeditions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `expeditions` (
  `id` int(11) NOT NULL,
  `owner` int(11) NOT NULL,
  `owner_name` varchar(128) NOT NULL,
  `name` varchar(128) NOT NULL,
  `mother` int(11) NOT NULL,
  `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`,`owner`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `family_members`
--

DROP TABLE IF EXISTS `family_members`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `family_members` (
  `character_id` int(11) NOT NULL,
  `family_id` int(11) NOT NULL,
  `name` varchar(45) NOT NULL,
  `role` tinyint(1) NOT NULL DEFAULT '0',
  `title` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`family_id`,`character_id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `friends`
--

DROP TABLE IF EXISTS `friends`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `friends` (
  `id` int(11) NOT NULL,
  `friend_id` int(11) NOT NULL,
  `owner` int(11) NOT NULL,
  PRIMARY KEY (`id`,`owner`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `housings`
--

DROP TABLE IF EXISTS `housings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `housings` (
  `id` int(11) NOT NULL,
  `account_id` int(10) unsigned NOT NULL,
  `owner` int(10) unsigned NOT NULL,
  `co_owner` int(10) unsigned NOT NULL,
  `template_id` int(10) unsigned NOT NULL,
  `name` varchar(128) NOT NULL,
  `x` float NOT NULL,
  `y` float NOT NULL,
  `z` float NOT NULL,
  `rotation_z` tinyint(4) NOT NULL,
  `current_step` tinyint(4) NOT NULL,
  `current_action` int(11) NOT NULL DEFAULT '0',
  `permission` tinyint(4) NOT NULL,
  PRIMARY KEY (`account_id`,`owner`,`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `items`
--

DROP TABLE IF EXISTS `items`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `items` (
  `id` bigint(20) unsigned NOT NULL,
  `type` varchar(100) NOT NULL,
  `template_id` int(11) unsigned NOT NULL,
  `slot_type` enum('Equipment','Inventory','Bank') NOT NULL,
  `slot` int(11) NOT NULL,
  `count` int(11) NOT NULL,
  `details` blob,
  `lifespan_mins` int(11) NOT NULL,
  `made_unit_id` int(11) unsigned NOT NULL DEFAULT '0',
  `unsecure_time` datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
  `unpack_time` datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
  `owner` int(11) unsigned NOT NULL,
  `grade` tinyint(1) DEFAULT '0',
  `created_at` datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
  PRIMARY KEY (`id`) USING BTREE,
  KEY `owner` (`owner`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `mates`
--

DROP TABLE IF EXISTS `mates`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `mates` (
  `id` int(11) unsigned NOT NULL,
  `item_id` bigint(20) unsigned NOT NULL,
  `name` text NOT NULL,
  `xp` int(11) NOT NULL,
  `level` tinyint(4) NOT NULL,
  `mileage` int(11) NOT NULL,
  `hp` int(11) NOT NULL,
  `mp` int(11) NOT NULL,
  `owner` int(11) unsigned NOT NULL,
  `updated_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`,`item_id`,`owner`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `options`
--

DROP TABLE IF EXISTS `options`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `options` (
  `key` varchar(100) NOT NULL,
  `value` text NOT NULL,
  `owner` int(11) unsigned NOT NULL,
  PRIMARY KEY (`key`,`owner`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `portal_book_coords`
--

DROP TABLE IF EXISTS `portal_book_coords`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `portal_book_coords` (
  `id` int(11) NOT NULL,
  `name` varchar(128) NOT NULL,
  `x` int(11) DEFAULT '0',
  `y` int(11) DEFAULT '0',
  `z` int(11) DEFAULT '0',
  `zone_id` int(11) DEFAULT '0',
  `z_rot` int(11) DEFAULT '0',
  `sub_zone_id` int(11) DEFAULT '0',
  `owner` int(11) NOT NULL,
  PRIMARY KEY (`id`,`owner`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `portal_visited_district`
--

DROP TABLE IF EXISTS `portal_visited_district`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `portal_visited_district` (
  `id` int(11) NOT NULL,
  `subzone` int(11) NOT NULL,
  `owner` int(11) NOT NULL,
  PRIMARY KEY (`id`,`subzone`,`owner`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `quests`
--

DROP TABLE IF EXISTS `quests`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `quests` (
  `id` int(11) unsigned NOT NULL,
  `template_id` int(11) unsigned NOT NULL,
  `data` tinyblob NOT NULL,
  `status` tinyint(4) NOT NULL,
  `owner` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id`,`owner`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `skills`
--

DROP TABLE IF EXISTS `skills`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `skills` (
  `id` int(11) unsigned NOT NULL,
  `level` tinyint(4) NOT NULL,
  `type` enum('Skill','Buff') NOT NULL,
  `owner` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id`,`owner`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping events for database 'aaemu_game'
--

--
-- Dumping routines for database 'aaemu_game'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-09-07 19:12:10
