/*
Navicat MySQL Data Transfer

Source Server         : aaemu
Source Server Version : 80012
Source Host           : localhost:3306
Source Database       : aaemu_game12

Target Server Type    : MYSQL
Target Server Version : 80012
File Encoding         : 65001

Date: 2019-09-08 13:54:52
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for mails
-- ----------------------------
DROP TABLE IF EXISTS `mails`;
CREATE TABLE `mails` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `type` int(11) unsigned NOT NULL,
  `status` int(11) NOT NULL,
  `title` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `sender_name` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `attachments` int(11) NOT NULL,
  `receiver_name` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `open_date` datetime NOT NULL,
  `returned` int(11) NOT NULL,
  `extra` int(11) NOT NULL,
  `text` varchar(300) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `money_amount_1` int(11) NOT NULL,
  `money_amount_2` int(11) NOT NULL,
  `money_amount_3` int(11) NOT NULL,
  `send_date` datetime NOT NULL,
  `received_date` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
SET FOREIGN_KEY_CHECKS=1;
