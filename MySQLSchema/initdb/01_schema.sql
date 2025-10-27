-- Charset default
SET NAMES utf8mb4;
SET time_zone = '+00:00';

-- DB untuk demo
CREATE DATABASE IF NOT EXISTS ubermensch
  CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE ubermensch;

-- Tabel quota_series
CREATE TABLE IF NOT EXISTS  quota_series (
  id_quota_series	INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
  series    		VARCHAR(255) NOT NULL,
  quota        		INT NULL DEFAULT 0,
  last_update   	TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB;
