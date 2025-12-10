-- Test data: Create tables and insert sample data for GPlanner (MySQL)
-- Run with: docker-compose exec -T mysql_db_container_name mysql -uroot -pmy_secure_password gplanner_db < scripts/test_data.sql

SET FOREIGN_KEY_CHECKS=0;

CREATE DATABASE IF NOT EXISTS `gplanner_db` DEFAULT CHARACTER SET = 'utf8mb4' COLLATE = 'utf8mb4_general_ci';
USE `gplanner_db`;

DROP TABLE IF EXISTS `ScheduledTasks`;
DROP TABLE IF EXISTS `UserTasks`;
DROP TABLE IF EXISTS `DaillyPlanItems`;
DROP TABLE IF EXISTS `Users`;

-- Users table
CREATE TABLE `Users` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(100) NOT NULL,
  `Username` VARCHAR(50) NOT NULL,
  `SchoolName` VARCHAR(150) NULL,
  `BirthDate` DATETIME NOT NULL,
  `IsNotified` TINYINT(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UQ_Users_Username` (`Username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- UserTasks table
CREATE TABLE `UserTasks` (
  `TaskId` INT NOT NULL AUTO_INCREMENT,
  `UserId` INT NOT NULL,
  `Title` VARCHAR(255) NOT NULL,
  `Description` VARCHAR(1000) NULL,
  `Type` INT NOT NULL,
  `Date` DATETIME NOT NULL,
  `Priority` INT NOT NULL,
  `IsArchived` TINYINT(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`TaskId`),
  KEY `IX_UserTasks_UserId` (`UserId`),
  CONSTRAINT `FK_UserTasks_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- DaillyPlanItems table (note: typo in original DbContext)
CREATE TABLE `DaillyPlanItems` (
  `Date` DATETIME NOT NULL,
  `DayOfWeek` VARCHAR(20) NOT NULL,
  `LastGenerated` DATETIME NOT NULL,
  PRIMARY KEY (`Date`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ScheduledTasks table
CREATE TABLE `ScheduledTasks` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Title` VARCHAR(255) NOT NULL,
  `ActivityType` VARCHAR(100) NOT NULL,
  `StartTime` TIME NOT NULL,
  `EndTime` TIME NOT NULL,
  `Description` VARCHAR(500) NULL,
  `IsCompleted` TINYINT(1) NOT NULL DEFAULT 0,
  `OriginalTaskTitle` VARCHAR(255) NULL,
  `OriginalUserTaskId` INT NULL,
  `DailyPlanDate` DATETIME NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ScheduledTasks_DailyPlanDate` (`DailyPlanDate`),
  CONSTRAINT `FK_ScheduledTasks_DaillyPlanItems_DailyPlanDate` FOREIGN KEY (`DailyPlanDate`) REFERENCES `DaillyPlanItems` (`Date`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

SET FOREIGN_KEY_CHECKS=1;

-- Insert seed data from DbContext HasData
INSERT INTO `Users` (`Id`, `Name`, `Username`, `SchoolName`, `BirthDate`, `IsNotified`) VALUES
(1, 'Victor De Marez', 'victordemarez', 'Sint-Franciscuscollege', '2007-05-15 00:00:00', 1);

INSERT INTO `UserTasks` (`TaskId`, `UserId`, `Title`, `Description`, `Type`, `Date`, `Priority`, `IsArchived`) VALUES
(1, 1, 'Math Exam', 'Prepare for the upcoming math exam on algebra and geometry.', 0, '2025-12-10 00:00:00', 5, 0),
(2, 1, 'Appointment with the doctor', 'Annual health check-up appointment at the clinic.', 3, '2025-12-12 00:00:00', 4, 0),
(3, 1, 'Science Project Submission', 'Submit the final report and presentation for the science project.', 4, '2025-12-15 00:00:00', 3, 0),
(4, 1, 'English Toets', 'English spelling and grammar test.', 1, '2025-12-11 00:00:00', 2, 0);
 -- Test data: Create tables and insert sample data for GPlanner (MySQL)

-- Run with: docker-compose exec -T mysql_db_container_name mysql -uroot -pmy_secure_password gplanner_db < scripts/test_data.sql


SET FOREIGN_KEY_CHECKS=0;


CREATE DATABASE IF NOT EXISTS `gplanner_db` DEFAULT CHARACTER SET = 'utf8mb4' COLLATE = 'utf8mb4_general_ci';

USE `gplanner_db`;


DROP TABLE IF EXISTS `ScheduledTasks`;

DROP TABLE IF EXISTS `UserTasks`;

DROP TABLE IF EXISTS `DaillyPlanItems`;

DROP TABLE IF EXISTS `Users`;


-- Users table

CREATE TABLE `Users` (

`Id` INT NOT NULL AUTO_INCREMENT,

`Name` VARCHAR(100) NOT NULL,

`Username` VARCHAR(50) NOT NULL,

`SchoolName` VARCHAR(150) NULL,

`BirthDate` DATETIME NOT NULL,

`IsNotified` TINYINT(1) NOT NULL DEFAULT 0,

PRIMARY KEY (`Id`),

UNIQUE KEY `UQ_Users_Username` (`Username`)

) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;


-- UserTasks table

CREATE TABLE `UserTasks` (

`TaskId` INT NOT NULL AUTO_INCREMENT,

`UserId` INT NOT NULL,

`Title` VARCHAR(255) NOT NULL,

`Description` VARCHAR(1000) NULL,

`Type` INT NOT NULL,

`Date` DATETIME NOT NULL,

`Priority` INT NOT NULL,

`IsArchived` TINYINT(1) NOT NULL DEFAULT 0,

PRIMARY KEY (`TaskId`),

KEY `IX_UserTasks_UserId` (`UserId`),

CONSTRAINT `FK_UserTasks_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE

) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;


-- DaillyPlanItems table (note: typo in original DbContext)

CREATE TABLE `DaillyPlanItems` (

`Date` DATETIME NOT NULL,

`DayOfWeek` VARCHAR(20) NOT NULL,

`LastGenerated` DATETIME NOT NULL,

PRIMARY KEY (`Date`)

) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;


-- ScheduledTasks table

CREATE TABLE `ScheduledTasks` (

`Id` INT NOT NULL AUTO_INCREMENT,

`Title` VARCHAR(255) NOT NULL,

`ActivityType` VARCHAR(100) NOT NULL,

`StartTime` TIME NOT NULL,

`EndTime` TIME NOT NULL,

`Description` VARCHAR(500) NULL,

`IsCompleted` TINYINT(1) NOT NULL DEFAULT 0,

`OriginalTaskTitle` VARCHAR(255) NULL,

`OriginalUserTaskId` INT NULL,

`DailyPlanDate` DATETIME NOT NULL,

PRIMARY KEY (`Id`),

KEY `IX_ScheduledTasks_DailyPlanDate` (`DailyPlanDate`),

CONSTRAINT `FK_ScheduledTasks_DaillyPlanItems_DailyPlanDate` FOREIGN KEY (`DailyPlanDate`) REFERENCES `DaillyPlanItems` (`Date`) ON DELETE CASCADE ON UPDATE CASCADE

) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;


SET FOREIGN_KEY_CHECKS=1;


-- Insert seed data from DbContext HasData

INSERT INTO `Users` (`Id`, `Name`, `Username`, `SchoolName`, `BirthDate`, `IsNotified`) VALUES

(1, 'Victor De Marez', 'victordemarez', 'Sint-Franciscuscollege', '2007-05-15 00:00:00', 1);


INSERT INTO `UserTasks` (`TaskId`, `UserId`, `Title`, `Description`, `Type`, `Date`, `Priority`, `IsArchived`) VALUES

(1, 1, 'Math Exam', 'Prepare for the upcoming math exam on algebra and geometry.', 0, '2025-12-10 00:00:00', 5, 0),

(2, 1, 'Appointment with the doctor', 'Annual health check-up appointment at the clinic.', 3, '2025-12-12 00:00:00', 4, 0),

(3, 1, 'Science Project Submission', 'Submit the final report and presentation for the science project.', 4, '2025-12-15 00:00:00', 3, 0),

(4, 1, 'English Toets', 'English spelling and grammar test.', 1, '2025-12-11 00:00:00', 2, 0);




could you create more user tasks

Here are additional INSERT statements to populate the UserTasks table with more varied data.

I have continued the TaskId sequence starting from 5 and added a mix of different subjects, priorities, and dates (centered around December 2025 and January 2026) to help test sorting and filtering.
Extended SQL Data
SQL

-- Continued from previous ID sequence
-- Assumptions on 'Type': 0=Exam, 1=Test, 2=Homework, 3=Appointment, 4=Deadline/Project, 5=Personal/Other

INSERT INTO `UserTasks` (`TaskId`, `UserId`, `Title`, `Description`, `Type`, `Date`, `Priority`, `IsArchived`) VALUES
-- Academic Tasks (Homework & Studying)
(5, 1, 'History Essay', 'Write a 2-page essay on the French Revolution causes.', 2, '2025-12-13 00:00:00', 3, 0),
(6, 1, 'Physics Lab Report', 'Complete the write-up for the pendulum experiment.', 2, '2025-12-14 00:00:00', 4, 0),
(7, 1, 'Read Dutch Novel', 'Read chapters 1-5 of "De Aanslag".', 2, '2025-12-12 00:00:00', 2, 0),
(8, 1, 'Geography Summary', 'Summarize the chapter on climate change for revision.', 5, '2025-12-16 00:00:00', 2, 0),

-- Exams and Tests (High Priority)
(9, 1, 'French Oral Exam', 'Prepare speaking topics: Family, Hobbies, and School.', 0, '2025-12-18 00:00:00', 5, 0),
(10, 1, 'Chemistry Quiz', 'Small test on the Periodic Table elements.', 1, '2025-12-17 00:00:00', 3, 0),
(11, 1, 'IT Programming Logic', 'Final exam on Python basics and logic gates.', 0, '2025-12-20 00:00:00', 5, 0),

-- Deadlines
(12, 1, 'Art Portfolio', 'Submit the final sketches for the semester portfolio.', 4, '2025-12-19 00:00:00', 4, 0),

-- Personal & Appointments
(13, 1, 'Football Practice', 'Regular training session at the local club.', 3, '2025-12-10 00:00:00', 2, 0),
(14, 1, 'Buy Christmas Gifts', 'Go to the city center to buy gifts for family.', 5, '2025-12-21 00:00:00', 1, 0),
(15, 1, 'Dentist Appointment', 'Routine cleaning and check-up.', 3, '2026-01-05 00:00:00', 3, 0),
(16, 1, 'Return Library Books', 'Drop off the borrowed history books.', 5, '2025-12-22 00:00:00', 1, 0),

-- Archived / Completed Tasks (For testing history/archive views)
(17, 1, 'Biology Presentation', 'Group presentation on ecosystem diversity.', 4, '2025-11-20 00:00:00', 3, 0),
(18, 1, 'Sign up for Gym', 'Hand in the registration form for the school gym.', 5, '2025-11-15 00:00:00', 1, 0);



