use kpd_finaldemo;
Drop table ATT01;
CREATE TABLE ATT01 (
    ATT01F01 INT AUTO_INCREMENT PRIMARY KEY,  -- AttendanceId (Primary Key)
    ATT01F02 INT NOT NULL,                    -- EmployeeId (Foreign Key to EMP01)
    ATT01F03 INT NOT NULL,                    -- DepartmentId (Foreign Key to DEP01)
    ATT01F04 DATE NOT NULL,                   -- Attendance Date
    ATT01F05 VARCHAR(50) DEFAULT NULL,         -- CheckInTime (HH:mm:ss)
    ATT01F06 VARCHAR(50) DEFAULT NULL,         -- CheckOutTime (HH:mm:ss)

    -- WorkedHours (Auto-calculated in hours)
    ATT01F07 DECIMAL(5,2) GENERATED ALWAYS AS (
        CASE 
            WHEN ATT01F05 IS NOT NULL AND ATT01F06 IS NOT NULL THEN 
                CAST(TIMESTAMPDIFF(SECOND, 
                    STR_TO_DATE(CONCAT('1970-01-01 ', ATT01F05), '%Y-%m-%d %H:%i:%s'),
                    STR_TO_DATE(CONCAT('1970-01-01 ', ATT01F06), '%Y-%m-%d %H:%i:%s')
                ) / 3600.0 AS DECIMAL(5,2))
            ELSE 0
        END
    ) STORED,

    ATT01F08 VARCHAR(20) DEFAULT 'Present',   -- Status (Present, Absent, etc.)
    ATT01F09 VARCHAR(255) DEFAULT NULL,       -- Remarks

    -- IsLate (Auto-calculated: 1 if check-in is after 09:00:00, otherwise 0)
    ATT01F10 BOOLEAN GENERATED ALWAYS AS (
        CASE 
            WHEN ATT01F05 IS NOT NULL AND ATT01F05 > '09:00:00' THEN 1 
            ELSE 0 
        END
    ) STORED,

    ATT01F11 DATETIME DEFAULT CURRENT_TIMESTAMP,  -- CreatedDate
    ATT01F12 DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,  -- UpdatedDate

    -- Foreign Key Constraints
    CONSTRAINT FK_ATT01_EMP01 FOREIGN KEY (ATT01F02) REFERENCES USR01(R01F01) ON DELETE CASCADE,
    CONSTRAINT FK_ATT01_DEP01 FOREIGN KEY (ATT01F03) REFERENCES DEPT01(T01F01) ON DELETE CASCADE
);


Select * from ATT01;

DROP TABLE IF EXISTS `emp01`;
CREATE TABLE `emp01` (
  `P01F01` INT NOT NULL AUTO_INCREMENT,       -- Primary Key (Employee ID)
  `P01F02` VARCHAR(255) DEFAULT NULL,         -- Employee Name
  `P01F03` VARCHAR(255) DEFAULT NULL,         -- Email or some other attribute
  `P01F04` DATETIME NOT NULL,                 -- Some datetime field (Joining Date, etc.)
  `P01F05` INT NOT NULL,                      -- Foreign Key to `dept01` (Department)
  `P01F06` VARCHAR(255) DEFAULT NULL,         -- Optional field
  `P01F07` TINYINT(1) NOT NULL,               -- Boolean/Status field (0 or 1)
  `P01F08` DECIMAL(10,0) NOT NULL DEFAULT '0',-- Some numeric field
  `P01F09` INT NOT NULL,                      -- Foreign Key to `usr01`

  PRIMARY KEY (`P01F01`),

  -- Index for performance on foreign keys
  KEY `fk_emp01_usr01_idx` (`P01F09`),
  KEY `fk_emp01_dept01_idx` (`P01F05`),

  -- Foreign Key Constraints
  CONSTRAINT `FK_EMP01_USR01` FOREIGN KEY (`P01F09`) 
  REFERENCES `usr01` (`R01F01`) ON DELETE CASCADE,

  CONSTRAINT `FK_EMP01_DEPT01` FOREIGN KEY (`P01F05`) 
  REFERENCES `dept01` (`T01F01`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


INSERT INTO EMP01 (P01F02, P01F03, P01F04, P01F05, P01F06, P01F07, P01F08, P01F09)
VALUES
('Priyansh', 'Sharma', '1995-03-15', 1, 'User', TRUE, 50000.00, 1),
('Admin', 'Kumar', '1988-06-21', 6, 'Admin', TRUE, 80000.00, 2),
('Editor', 'Patel', '1992-07-12', 2, 'Editor', TRUE, 55000.00, 3),
('User', 'Singh', '1994-09-10', 3, 'User', TRUE, 48000.00, 4),
('Parth', 'Trivedi', '1991-05-25', 2, 'Editor', TRUE, 60000.00, 5),
('Keyur', 'Mehta', '1990-11-30', 1, 'User', TRUE, 52000.00, 7),
('Champak', 'Lal', '1985-08-19', 8, 'User', TRUE, 49000.00, 8),
('Mehta', 'Desai', '1987-10-05', 3, 'User', TRUE, 47000.00, 11),
('Anjli', 'Verma', '1993-12-22', 4, 'User', TRUE, 46000.00, 12),
('Madhvi', 'Bose', '1996-01-14', 3, 'User', TRUE, 51000.00, 13),
('Bhide', 'Joshi', '1989-04-09', 1, 'User', TRUE, 53000.00, 14),
('Hathi', 'Goyal', '1982-07-31', 6, 'Admin', TRUE, 85000.00, 15),
('Goli', 'Pandey', '1990-09-29', 2, 'Editor', TRUE, 58000.00, 16),
('Komal', 'Roy', '1994-06-13', 1, 'User', TRUE, 54000.00, 17),
('Babita', 'Kapoor', '1986-03-24', 6, 'Admin', TRUE, 82000.00, 18),
('Tapu', 'Shah', '1995-02-16', 2, 'Editor', TRUE, 57000.00, 19),
('Dimple', 'Chauhan', '1993-11-28', 6, 'Admin', TRUE, 83000.00, 20),
('Ram', 'Rathore', '1988-05-03', 2, 'Editor', TRUE, 60000.00, 21);


INSERT INTO ATT01 (ATT01F02, ATT01F03, ATT01F04, ATT01F05, ATT01F06, ATT01F08, ATT01F09)
VALUES
-- Employee 5
(5, 2, '2025-03-01', '09:00:00', '17:30:00', 'Present', 'Regular shift'),
(5, 2, '2025-03-02', '09:10:00', '18:00:00', 'Present', 'Late arrival'),
(5, 2, '2025-03-03', NULL, NULL, 'Absent', 'Sick leave'),
(5, 2, '2025-03-04', '08:50:00', '17:40:00', 'Present', 'Early arrival'),
(5, 2, '2025-03-05', '09:05:00', '18:00:00', 'Present', 'Regular hours'),
(5, 2, '2025-03-06', '08:45:00', '17:35:00', 'Present', 'Early check-in'),
(5, 2, '2025-03-07', '09:20:00', '18:10:00', 'Present', 'Late arrival'),
(5, 2, '2025-03-08', '08:40:00', '17:25:00', 'Present', 'On time'),
(5, 2, '2025-03-09', '09:05:00', '18:00:00', 'Present', 'Regular shift'),
(5, 2, '2025-03-10', '08:55:00', '17:30:00', 'Present', 'On time'),

-- Employee 6
(6, 3, '2025-03-01', '08:55:00', '17:30:00', 'Present', 'On time'),
(6, 3, '2025-03-02', '09:15:00', '18:00:00', 'Present', 'Late check-in'),
(6, 3, '2025-03-03', '08:45:00', '17:35:00', 'Present', 'Early shift'),
(6, 3, '2025-03-04', NULL, NULL, 'Absent', 'Personal work'),
(6, 3, '2025-03-05', '09:05:00', '18:00:00', 'Present', 'Regular hours'),
(6, 3, '2025-03-06', '08:50:00', '17:40:00', 'Present', 'On time'),
(6, 3, '2025-03-07', '09:20:00', '18:10:00', 'Present', 'Late arrival'),
(6, 3, '2025-03-08', '08:40:00', '17:25:00', 'Present', 'On time'),
(6, 3, '2025-03-09', '09:05:00', '18:00:00', 'Present', 'Regular shift'),
(6, 3, '2025-03-10', '08:55:00', '17:30:00', 'Present', 'On time'),

-- Employee 7
(7, 1, '2025-03-01', '09:00:00', '17:30:00', 'Present', 'Regular shift'),
(7, 1, '2025-03-02', '09:10:00', '18:00:00', 'Present', 'Late arrival'),
(7, 1, '2025-03-03', NULL, NULL, 'Absent', 'Sick leave'),
(7, 1, '2025-03-04', '08:50:00', '17:40:00', 'Present', 'Early arrival'),
(7, 1, '2025-03-05', '09:05:00', '18:00:00', 'Present', 'Regular hours'),
(7, 1, '2025-03-06', '08:45:00', '17:35:00', 'Present', 'Early check-in'),
(7, 1, '2025-03-07', '09:20:00', '18:10:00', 'Present', 'Late arrival'),
(7, 1, '2025-03-08', '08:40:00', '17:25:00', 'Present', 'On time'),
(7, 1, '2025-03-09', '09:05:00', '18:00:00', 'Present', 'Regular shift'),
(7, 1, '2025-03-10', '08:55:00', '17:30:00', 'Present', 'On time'),

-- Continue for employees 8 to 15...
-- Employee 8
(8, 4, '2025-03-01', '08:55:00', '17:30:00', 'Present', 'Regular shift'),
(8, 4, '2025-03-02', '09:10:00', '18:15:00', 'Present', 'Overtime'),
(8, 4, '2025-03-03', NULL, NULL, 'Absent', 'Leave approved'),
(8, 4, '2025-03-04', '08:50:00', '17:40:00', 'Present', 'Early arrival'),
(8, 4, '2025-03-05', '09:00:00', '18:00:00', 'Present', 'Regular shift'),
(8, 4, '2025-03-06', '08:45:00', '17:35:00', 'Present', 'On time'),
(8, 4, '2025-03-07', '09:20:00', '18:00:00', 'Present', 'Late arrival'),
(8, 4, '2025-03-08', '08:40:00', '17:25:00', 'Present', 'Normal hours'),
(8, 4, '2025-03-09', '09:05:00', '18:10:00', 'Present', 'Overtime'),
(8, 4, '2025-03-10', '08:55:00', '17:30:00', 'Present', 'On time');



-- Repeat for Employees 10 to 15...
INSERT INTO ATT01 (ATT01F02, ATT01F03, ATT01F04, ATT01F05, ATT01F06, ATT01F08, ATT01F09)
VALUES
-- Employee 11 (Mehta)
(11, 4, '2025-03-01', '08:55:00', '17:30:00', 'Present', 'Regular shift'),
(11, 4, '2025-03-02', '09:10:00', '18:15:00', 'Present', 'Overtime'),
(11, 4, '2025-03-03', NULL, NULL, 'Absent', 'Leave approved'),
(11, 4, '2025-03-04', '08:50:00', '17:40:00', 'Present', 'Early arrival'),
(11, 4, '2025-03-05', '09:00:00', '18:00:00', 'Present', 'Regular shift'),
(11, 4, '2025-03-06', '08:45:00', '17:35:00', 'Present', 'On time'),
(11, 4, '2025-03-07', '09:20:00', '18:00:00', 'Present', 'Late arrival'),
(11, 4, '2025-03-08', '08:40:00', '17:25:00', 'Present', 'Normal hours'),
(11, 4, '2025-03-09', '09:05:00', '18:10:00', 'Present', 'Overtime'),
(11, 4, '2025-03-10', '08:55:00', '17:30:00', 'Present', 'On time'),

-- Employee 12 (Anjli)
(12, 4, '2025-03-01', '08:50:00', '17:35:00', 'Present', 'Regular shift'),
(12, 4, '2025-03-02', '09:15:00', '18:10:00', 'Present', 'Overtime'),
(12, 4, '2025-03-03', NULL, NULL, 'Absent', 'Sick leave'),
(12, 4, '2025-03-04', '08:45:00', '17:30:00', 'Present', 'Early arrival'),
(12, 4, '2025-03-05', '09:00:00', '18:00:00', 'Present', 'Regular shift'),
(12, 4, '2025-03-06', '08:55:00', '17:40:00', 'Present', 'On time'),
(12, 4, '2025-03-07', '09:30:00', '18:20:00', 'Present', 'Late arrival'),
(12, 4, '2025-03-08', '08:35:00', '17:20:00', 'Present', 'Normal hours'),
(12, 4, '2025-03-09', '09:10:00', '18:15:00', 'Present', 'Overtime'),
(12, 4, '2025-03-10', '08:50:00', '17:30:00', 'Present', 'On time'),

-- Employee 13 (Madhvi)
(13, 4, '2025-03-01', '08:45:00', '17:25:00', 'Present', 'Regular shift'),
(13, 4, '2025-03-02', '09:20:00', '18:00:00', 'Present', 'Overtime'),
(13, 4, '2025-03-03', NULL, NULL, 'Absent', 'Personal leave'),
(13, 4, '2025-03-04', '08:50:00', '17:30:00', 'Present', 'Early arrival'),
(13, 4, '2025-03-05', '09:05:00', '18:10:00', 'Present', 'Regular shift'),
(13, 4, '2025-03-06', '08:55:00', '17:45:00', 'Present', 'On time'),
(13, 4, '2025-03-07', '09:15:00', '18:00:00', 'Present', 'Late arrival'),
(13, 4, '2025-03-08', '08:40:00', '17:25:00', 'Present', 'Normal hours'),
(13, 4, '2025-03-09', '09:00:00', '18:00:00', 'Present', 'Overtime'),
(13, 4, '2025-03-10', '08:55:00', '17:30:00', 'Present', 'On time'),

-- Employee 14 (Bhide)
(14, 4, '2025-03-01', '08:55:00', '17:30:00', 'Present', 'Regular shift'),
(14, 4, '2025-03-02', '09:30:00', '18:30:00', 'Present', 'Overtime'),
(14, 4, '2025-03-03', NULL, NULL, 'Absent', 'Vacation'),
(14, 4, '2025-03-04', '08:45:00', '17:40:00', 'Present', 'Early arrival'),
(14, 4, '2025-03-05', '09:10:00', '18:20:00', 'Present', 'Regular shift'),
(14, 4, '2025-03-06', '08:50:00', '17:30:00', 'Present', 'On time'),
(14, 4, '2025-03-07', '09:25:00', '18:15:00', 'Present', 'Late arrival'),
(14, 4, '2025-03-08', '08:40:00', '17:20:00', 'Present', 'Normal hours'),
(14, 4, '2025-03-09', '09:05:00', '18:10:00', 'Present', 'Overtime'),
(14, 4, '2025-03-10', '08:55:00', '17:30:00', 'Present', 'On time'),

-- Employee 15 (Hathi)
(15, 4, '2025-03-01', '08:50:00', '17:30:00', 'Present', 'Regular shift'),
(15, 4, '2025-03-02', '09:20:00', '18:10:00', 'Present', 'Overtime'),
(15, 4, '2025-03-03', NULL, NULL, 'Absent', 'Medical leave'),
(15, 4, '2025-03-04', '08:45:00', '17:35:00', 'Present', 'Early arrival'),
(15, 4, '2025-03-05', '09:00:00', '18:00:00', 'Present', 'Regular shift'),
(15, 4, '2025-03-06', '08:55:00', '17:40:00', 'Present', 'On time'),
(15, 4, '2025-03-07', '09:15:00', '18:05:00', 'Present', 'Late arrival'),
(15, 4, '2025-03-08', '08:40:00', '17:20:00', 'Present', 'Normal hours'),
(15, 4, '2025-03-09', '09:05:00', '18:10:00', 'Present', 'Overtime'),
(15, 4, '2025-03-10', '08:55:00', '17:30:00', 'Present', 'On time');


INSERT INTO ATT01 (ATT01F02, ATT01F03, ATT01F04, ATT01F05, ATT01F06, ATT01F08, ATT01F09)
VALUES
-- Employee 16 (Goli)
(16, 4, '2025-03-01', '08:55:00', '17:30:00', 'Present', 'Regular shift'),
(16, 4, '2025-03-02', '09:10:00', '18:15:00', 'Present', 'Overtime'),
(16, 4, '2025-03-03', NULL, NULL, 'Absent', 'Leave approved'),
(16, 4, '2025-03-04', '08:50:00', '17:40:00', 'Present', 'Early arrival'),
(16, 4, '2025-03-05', '09:00:00', '18:00:00', 'Present', 'Regular shift'),
(16, 4, '2025-03-06', '08:45:00', '17:35:00', 'Present', 'On time'),
(16, 4, '2025-03-07', '09:20:00', '18:00:00', 'Present', 'Late arrival'),
(16, 4, '2025-03-08', '08:40:00', '17:25:00', 'Present', 'Normal hours'),
(16, 4, '2025-03-09', '09:05:00', '18:10:00', 'Present', 'Overtime'),
(16, 4, '2025-03-10', '08:55:00', '17:30:00', 'Present', 'On time'),

-- Employee 17 (Komal)
(17, 4, '2025-03-01', '08:50:00', '17:35:00', 'Present', 'Regular shift'),
(17, 4, '2025-03-02', '09:15:00', '18:10:00', 'Present', 'Overtime'),
(17, 4, '2025-03-03', NULL, NULL, 'Absent', 'Sick leave'),
(17, 4, '2025-03-04', '08:45:00', '17:30:00', 'Present', 'Early arrival'),
(17, 4, '2025-03-05', '09:00:00', '18:00:00', 'Present', 'Regular shift'),
(17, 4, '2025-03-06', '08:55:00', '17:40:00', 'Present', 'On time'),
(17, 4, '2025-03-07', '09:30:00', '18:20:00', 'Present', 'Late arrival'),
(17, 4, '2025-03-08', '08:35:00', '17:20:00', 'Present', 'Normal hours'),
(17, 4, '2025-03-09', '09:10:00', '18:15:00', 'Present', 'Overtime'),
(17, 4, '2025-03-10', '08:50:00', '17:30:00', 'Present', 'On time'),

-- Employee 18 (Babita)
(18, 4, '2025-03-01', '08:45:00', '17:25:00', 'Present', 'Regular shift'),
(18, 4, '2025-03-02', '09:20:00', '18:00:00', 'Present', 'Overtime'),
(18, 4, '2025-03-03', NULL, NULL, 'Absent', 'Personal leave'),
(18, 4, '2025-03-04', '08:50:00', '17:30:00', 'Present', 'Early arrival'),
(18, 4, '2025-03-05', '09:05:00', '18:10:00', 'Present', 'Regular shift'),
(18, 4, '2025-03-06', '08:55:00', '17:45:00', 'Present', 'On time'),
(18, 4, '2025-03-07', '09:15:00', '18:00:00', 'Present', 'Late arrival'),
(18, 4, '2025-03-08', '08:40:00', '17:25:00', 'Present', 'Normal hours'),
(18, 4, '2025-03-09', '09:00:00', '18:00:00', 'Present', 'Overtime'),
(18, 4, '2025-03-10', '08:55:00', '17:30:00', 'Present', 'On time'),

-- Employee 19 (Tapu)
(19, 4, '2025-03-01', '08:55:00', '17:30:00', 'Present', 'Regular shift'),
(19, 4, '2025-03-02', '09:30:00', '18:30:00', 'Present', 'Overtime'),
(19, 4, '2025-03-03', NULL, NULL, 'Absent', 'Vacation'),
(19, 4, '2025-03-04', '08:45:00', '17:40:00', 'Present', 'Early arrival'),
(19, 4, '2025-03-05', '09:10:00', '18:20:00', 'Present', 'Regular shift'),
(19, 4, '2025-03-06', '08:50:00', '17:30:00', 'Present', 'On time'),
(19, 4, '2025-03-07', '09:25:00', '18:15:00', 'Present', 'Late arrival'),
(19, 4, '2025-03-08', '08:40:00', '17:20:00', 'Present', 'Normal hours'),
(19, 4, '2025-03-09', '09:05:00', '18:10:00', 'Present', 'Overtime'),
(19, 4, '2025-03-10', '08:55:00', '17:30:00', 'Present', 'On time'),

-- Employee 20 (Dimple)
(20, 4, '2025-03-01', '08:50:00', '17:30:00', 'Present', 'Regular shift'),
(20, 4, '2025-03-02', '09:20:00', '18:10:00', 'Present', 'Overtime'),
(20, 4, '2025-03-03', NULL, NULL, 'Absent', 'Medical leave'),
(20, 4, '2025-03-04', '08:45:00', '17:35:00', 'Present', 'Early arrival'),
(20, 4, '2025-03-05', '09:00:00', '18:00:00', 'Present', 'Regular shift'),
(20, 4, '2025-03-06', '08:55:00', '17:40:00', 'Present', 'On time'),
(20, 4, '2025-03-07', '09:15:00', '18:05:00', 'Present', 'Late arrival'),
(20, 4, '2025-03-08', '08:40:00', '17:20:00', 'Present', 'Normal hours'),
(20, 4, '2025-03-09', '09:05:00', '18:10:00', 'Present', 'Overtime'),
(20, 4, '2025-03-10', '08:55:00', '17:30:00', 'Present', 'On time'),

-- Employee 21 (Ram)
(21, 4, '2025-03-01', '08:55:00', '17:30:00', 'Present', 'Regular shift'),
(21, 4, '2025-03-02', '09:10:00', '18:15:00', 'Present', 'Overtime'),
(21, 4, '2025-03-03', NULL, NULL, 'Absent', 'Leave approved'),
(21, 4, '2025-03-04', '08:50:00', '17:40:00', 'Present', 'Early arrival'),
(21, 4, '2025-03-05', '09:00:00', '18:00:00', 'Present', 'Regular shift'),
(21, 4, '2025-03-06', '08:45:00', '17:35:00', 'Present', 'On time'),
(21, 4, '2025-03-07', '09:20:00', '18:00:00', 'Present', 'Late arrival'),
(21, 4, '2025-03-08', '08:40:00', '17:25:00', 'Present', 'Normal hours');

