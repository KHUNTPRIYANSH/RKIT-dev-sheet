CREATE DATABASE IF NOT EXISTS KPD_Core_Final;
CREATE DATABASE KPD_Core_Final;
 
USE KPD_Core_Final;


CREATE TABLE IF NOT EXISTS Emp01 (
    P01F01 INT AUTO_INCREMENT PRIMARY KEY,  -- Employee ID (Primary Key)
    P01F02 VARCHAR(100) NOT NULL,           -- Employee Name
    P01F03 VARCHAR(150) NOT NULL,           -- Employee Email
    P01F04 VARCHAR(15) NOT NULL,            -- Employee Phone Number
    P01F05 DATETIME DEFAULT CURRENT_TIMESTAMP, -- Created At
    P01F06 DATETIME NULL,                  -- Updated At
    P01F07 BOOLEAN DEFAULT TRUE,           -- Is Active (0 = Inactive, 1 = Active)
    P01F08 DECIMAL(10, 2) NOT NULL,         -- Salary
    P01F09 VARCHAR(50) NULL                -- Department
);

CREATE TABLE api_logs (
    LogID BIGINT AUTO_INCREMENT PRIMARY KEY,
    Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
    RequestMethod VARCHAR(10),
    RequestPath TEXT,
    Route TEXT,
    RequestBody TEXT,
    ResponseStatusCode INT,
    ResponseBody TEXT,
    ClientIP VARCHAR(45)
);
SELECT * FROM api_Logs;
