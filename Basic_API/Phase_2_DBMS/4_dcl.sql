-- File: dcl.sql
-- This file demonstrates DCL (Data Control Language) commands in detail with examples for managing user access and privileges.

SELECT user FROM mysql.user;

-- Creating a new user
-- The following command creates a new user named 'EmployeeManager' with a password.

CREATE USER 'EmployeeManager'@'localhost' IDENTIFIED BY 'StrongPassword123';

-- Granting privileges
-- 1. Granting ALL privileges to the user for the EmployeeManagement database.
GRANT ALL PRIVILEGES ON EmployeeManagement.* TO 'EmployeeManager'@'localhost';

-- 2. Granting specific privileges (SELECT, INSERT, UPDATE) to a user on the Employees table.
GRANT SELECT, INSERT, UPDATE ON EmployeeManagement.Employees TO 'EmployeeManager'@'localhost';

-- 3. Granting EXECUTE privileges for stored procedures or functions.
GRANT EXECUTE ON PROCEDURE EmployeeManagement.* TO 'EmployeeManager'@'localhost';

-- Viewing privileges
-- Shows the privileges granted to the user 'EmployeeManager'.
SHOW GRANTS FOR 'EmployeeManager'@'localhost';

-- Revoking privileges
-- 1. Revoking specific privileges (INSERT, UPDATE) from a user.
REVOKE INSERT, UPDATE ON EmployeeManagement.Employees FROM 'EmployeeManager'@'localhost';

-- 2. Revoking all privileges from a user for the EmployeeManagement database.
REVOKE ALL PRIVILEGES ON EmployeeManagement.*;

-- Dropping a user
-- Removes the user from the MySQL server.
DROP USER 'EmployeeManager'@'localhost';

-- Example scenario: Setting up a read-only user
-- Creating a new user for read-only access.
CREATE USER 'ReadOnlyUser'@'localhost' IDENTIFIED BY 'SecurePassword!';

-- Granting SELECT privileges only.
GRANT SELECT ON EmployeeManagement.* TO 'ReadOnlyUser'@'localhost';

-- Revoking SELECT privilege to restrict access later if needed.
REVOKE SELECT ON EmployeeManagement.* FROM 'ReadOnlyUser'@'localhost';

-- Example scenario: Restricting user access to specific tables
-- Creating a user limited to specific operations on the Departments table.
CREATE USER 'DeptManager'@'localhost' IDENTIFIED BY 'DeptSecure123';

-- Granting INSERT and UPDATE privileges on the Departments table only.
GRANT INSERT, UPDATE ON EmployeeManagement.Departments TO 'DeptManager'@'localhost';

-- Revoking specific privilege (INSERT) while retaining others (UPDATE).
REVOKE INSERT ON EmployeeManagement.Departments FROM 'DeptManager'@'localhost';

-- Flushing privileges
-- Ensures that the privilege changes take immediate effect.
FLUSH PRIVILEGES;
