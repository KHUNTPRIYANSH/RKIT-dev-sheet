-- File: ddl.sql
-- This file demonstrates DDL (Data Definition Language) commands in a basic Employee Management Database.

-- Creating a database
-- This command creates a new database named 'EmployeeManagement'.
CREATE DATABASE EmployeeManagement;

-- Switching to the database
-- USE switches the context to the specified database.
USE EmployeeManagement;

-- Creating a table for employees
-- This table stores employee details, including their unique ID, name, department, hire date, and salary.
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY AUTO_INCREMENT, -- Auto-increment ensures unique EmployeeID
    FirstName VARCHAR(50), -- Employee's first name
    LastName VARCHAR(50), -- Employee's last name
    DepartmentID INT, -- Links to the department the employee belongs to
    HireDate DATE, -- Hiredate of employee
    Salary DECIMAL(10, 2) -- Stores salary up to 10 digits with 2 decimal places
);

-- Creating a table for departments
-- This table stores department details.
CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY AUTO_INCREMENT, -- Unique DepartmentID
    DepartmentName VARCHAR(100) -- Name of the department
);

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
ADD CONSTRAINT FK_Department FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID);

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
DROP DATABASE EmployeeManagement;
