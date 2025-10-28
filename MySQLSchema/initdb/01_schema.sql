-- Charset default
SET NAMES utf8mb4;
SET time_zone = '+00:00';

-- DB untuk demo
-- Dumping database structure for ubermensch
CREATE DATABASE IF NOT EXISTS ubermensch
  CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE ubermensch;

-- Dumping structure for table ubermensch.quota_series
CREATE TABLE IF NOT EXISTS quota_series (
  id_quota_series int(11) NOT NULL AUTO_INCREMENT,
  series varchar(255) DEFAULT '0',
  quota bigint(20) DEFAULT 0,
  last_update datetime DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY(id_quota_series)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping structure for table ubermensch.counterparty
CREATE TABLE IF NOT EXISTS counterparty (
  id_counterparty int(11) NOT NULL AUTO_INCREMENT,
  counterparty_name varchar(255) DEFAULT NULL,
  counterparty_code varchar(255) DEFAULT NULL,
  rating varchar(1) DEFAULT NULL,
  persentase_spread decimal(18,4) DEFAULT NULL,
  PRIMARY KEY(id_counterparty)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping structure for table ubermensch.bond_trx
CREATE TABLE IF NOT EXISTS bond_trx (
  id_bond_trx int(11) NOT NULL AUTO_INCREMENT,
  side varchar(1) DEFAULT NULL,
  series varchar(255) DEFAULT NULL,
  counterparty_code varchar(255) DEFAULT NULL,
  deal_time datetime DEFAULT current_timestamp(),
  qty int(11) DEFAULT 0,
  price decimal(18,4) DEFAULT 0.0000,
  yield decimal(18,4) DEFAULT 0.0000,
  channel varchar(255) DEFAULT NULL,
  status varchar(255) DEFAULT NULL,
  last_update datetime DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY(id_bond_trx)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;