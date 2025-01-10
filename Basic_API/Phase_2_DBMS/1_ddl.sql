-- File: ddl.sql
-- This file demonstrates DDL (Data Definition Language) commands in a basic Employee Management Database.

-- Check all existing DB in system
SHOW DATABASES;

-- Creating a database
-- This command creates a new database named 'KPD_EmployeeManagement_MYSQL'.
CREATE DATABASE KPD_EmployeeManagement_MYSQL;

-- Switching to the database
-- USE switches the context to the specified database.
USE KPD_EmployeeManagement_MYSQL;

-- Check in which db we are corrently in?
SELECT DATABASE();

-- Creating a table for employees
-- This table stores employee details, including their unique ID, name, department, hire date, and salary.
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY AUTO_INCREMENT, -- Auto-increment ensures unique EmployeeID
    FirstName VARCHAR(50), 
    LastName VARCHAR(50), 
    DepartmentID INT, 
    HireDate DATE, 
    Salary DECIMAL(10, 2)  DEFAULT 0, -- Stores salary up to 10 digits with 2 decimal places
    CHECK (Salary > 0), -- Ensures salary will be positive
    IsActive BOOL DEFAULT TRUE
);
-- if you insert 2 row with id 1 , 1001 than after that when you insert new row and dont give id so auto increment will be used and it will give id as 1002 
-- auto increment will find largest id and add one to that

-- Creating a table for departments
-- This table stores department details.
CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY AUTO_INCREMENT, 
    DepartmentName VARCHAR(100)
);

-- Check table made or not and it's structure
DESC Employees;
DESCRIBE Employees;

-- Adding constraints using ALTER TABLE

-- Adding a NOT NULL constraint to the DepartmentName column
-- Ensures that every department must have a name.
ALTER TABLE Departments
MODIFY DepartmentName VARCHAR(100) NOT NULL;

-- Adding a UNIQUE constraint to the FirstName and LastName combination in Employees
-- Ensures no two employees can have the same first and last name.
ALTER TABLE Employees
ADD CONSTRAINT UniqueEmployeeName UNIQUE (FirstName, LastName);

-- Adding a CHECK constraint to ensure Salary is greater than zero
-- Prevents entry of negative or zero salary values.
ALTER TABLE Employees
ADD CONSTRAINT CheckSalary CHECK (Salary > 0);

-- Adding a foreign key constraint to link Employees to Departments
-- Ensures DepartmentID in Employees references a valid DepartmentID in Departments.
ALTER TABLE Employees
ADD CONSTRAINT FK_Department FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID) ON DELETE CASCADE;
-- on delete cascade is a constraint which deletes all the ref in all the tables if row in parent table is deleted
-- ex in employess table department  id is foreign key 
-- if their is a table like department's policy and having details of  policy_ID , FK_empId , FK_deptId
-- if empId deleted form employees table it will also delte that employee from policy table 
-- it ensure data intigrity

-- ON DELETE NULL 
-- it will replace other ref with NULL values if row from parent table is deleted and child table get's affected

-- Adding an INDEX for faster lookup of DepartmentID in Employees
-- Improves query performance when filtering by DepartmentID.
CREATE INDEX idx_department_id ON Employees (DepartmentID);

-- Adding an INDEX for Salary in Employees to improve query performance
CREATE INDEX idx_salary ON Employees (Salary);

-- Dropping constraints
-- Dropping the UNIQUE constraint
-- Removes the uniqueness requirement on FirstName and LastName.
ALTER TABLE Employees
DROP INDEX UniqueEmployeeName;

-- Dropping the CHECK constraint
-- Removes the validation rule for Salary.
ALTER TABLE Employees
DROP CHECK CheckSalary;

-- Dropping the foreign key constraint
-- Unlinks Employees from Departments.
ALTER TABLE Employees
DROP FOREIGN KEY FK_Department;

-- Renaming a table
-- Changes the table name from Employees to Staff.
ALTER TABLE Employees RENAME TO Staff;


-- Adding a new column for email in Employees
-- Adds an Email column to store employee email addresses.
ALTER TABLE Staff
ADD Email VARCHAR(100);

-- Dropping a column
-- Removes the Email column from the Staff table.
ALTER TABLE Staff
DROP COLUMN Email;

-- Demonstrating table truncation (removing all rows without dropping the structure)
-- Clears all data from the Staff table but keeps its structure intact.
TRUNCATE TABLE Staff;
-- TRUNCATE TABLE Employees;
-- Demonstrating table deletion (removing the table entirely)
-- Deletes the Staff table from the database.
DROP TABLE Staff;


ALTER TABLE Staff RENAME TO Employees;
-- Demonstrating database deletion
-- Deletes the entire EmployeeManagement database.
DROP DATABASE KPD_EmployeeManagement_MYSQL;

-- Create table for active users
CREATE TABLE Active_Employees (
    EmployeeID INT PRIMARY KEY, 
    FirstName VARCHAR(50), 
    LastName VARCHAR(50), 
    DepartmentID INT, 
    IsActive BOOL DEFAULT TRUE
);
-- Create table for Inactive users
CREATE TABLE InActive_Employees (
    EmployeeID INT PRIMARY KEY, 
    FirstName VARCHAR(50), 
    LastName VARCHAR(50), 
    DepartmentID INT, 
    IsActive BOOL DEFAULT FALSE
);
