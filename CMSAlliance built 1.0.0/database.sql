-- phpMyAdmin SQL Dump
-- version 3.3.9.2
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Feb 28, 2011 at 03:01 PM
-- Server version: 5.5.8
-- PHP Version: 5.3.5

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";

--
-- Database: `cmsalliance`
--

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE IF NOT EXISTS `users` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Primary Auto-Incrementing key. Unique ID for each of the users',
  `image` text COMMENT 'Filename of the avatar of this user. All files are given a random name and put in the resources/uploads/ folder',
  `company_name` varchar(255) DEFAULT NULL COMMENT 'Name of the company',
  `company_website` varchar(255) DEFAULT NULL COMMENT 'Website of the company',
  `company_size` int(11) DEFAULT '0' COMMENT 'Size of the company',
  `email_confirmation_code` text COMMENT 'Code that we are expecting so that the account can be activated',
  `email` varchar(255) NOT NULL COMMENT 'E-Mail of the user',
  `firstname` varchar(150) NOT NULL COMMENT 'Firstname of the user',
  `lastname` varchar(150) NOT NULL COMMENT 'Surname of the User',
  `password` varchar(60) NOT NULL COMMENT 'SHA1-Hashed Password of the user',
  `public_viewable` tinyint(1) NOT NULL DEFAULT '0' COMMENT 'Does the user want his/her profile to be viewable to website visitors',
  `created` datetime NOT NULL COMMENT 'Date Time when the user record was created',
  `approved` datetime DEFAULT NULL COMMENT 'Date Time when the user was approved, NULL = Not YET',
  `email_confirmed` datetime DEFAULT NULL COMMENT 'Date Time when the account was confirmed to have a read/active e-mail',
  `lastlogin` datetime DEFAULT NULL COMMENT 'Date Time when the record last logged-in',
  `lastupdated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Date Time when the record was last updated',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=11 ;

-- --------------------------------------------------------

--
-- Table structure for table `votes`
--

CREATE TABLE IF NOT EXISTS `votes` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Auto Incremented ID of the voting Relation',
  `voter` int(11) NOT NULL COMMENT 'ID of the user that voted',
  `votee` int(11) NOT NULL COMMENT 'ID of the User this vote is for',
  `answer` tinyint(1) NOT NULL DEFAULT '0' COMMENT 'Boolean indicating Yes/No',
  `created` datetime NOT NULL COMMENT 'Date Time when the record was created',
  `lastupdated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Date Time when record was last updated',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;