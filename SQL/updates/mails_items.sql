/*
Navicat MySQL Data Transfer

Source Server         : aaemu
Source Server Version : 80012
Source Host           : localhost:3306
Source Database       : aaemu_game12

Target Server Type    : MYSQL
Target Server Version : 80012
File Encoding         : 65001

Date: 2019-09-08 13:55:00
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for mails_items
-- ----------------------------
DROP TABLE IF EXISTS `mails_items`;
CREATE TABLE `mails_items` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `item0` bigint(20) unsigned NOT NULL,
  `item1` bigint(20) unsigned NOT NULL,
  `item2` bigint(20) unsigned NOT NULL,
  `item3` bigint(20) unsigned NOT NULL,
  `item4` bigint(20) unsigned NOT NULL,
  `item5` bigint(20) unsigned NOT NULL,
  `item6` bigint(20) unsigned NOT NULL,
  `item7` bigint(20) unsigned NOT NULL,
  `item8` bigint(20) unsigned NOT NULL,
  `item9` bigint(20) unsigned NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
SET FOREIGN_KEY_CHECKS=1;
