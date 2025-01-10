-- File: dml.sql
-- This file demonstrates DML (Data Manipulation Language) commands in a basic Employee Management Database.

-- Switching to the database
USE KPD_EmployeeManagement_MYSQL;

-- Inserting data into the Departments table
-- Adds initial department records to the Departments table.
INSERT INTO Departments (DepartmentName)
VALUES
    ('Human Resources'),
    ('Finance'),
    ('Engineering'),
    ('Marketing'),
    ('QA');

-- Inserting data into the Employees table
-- Adds employee records with various details.
-- If we are inserting all the field's as per DESC table's order then no need of mentioning all the field while typing insert query. 

INSERT INTO Employees (FirstName, LastName, DepartmentID, HireDate, Salary, IsActive)
VALUES
    ('Rajesh bhai', 'patel', 1, '2021-02-12', 52000.00 , false);
INSERT INTO Employees (FirstName, LastName, DepartmentID, HireDate, Salary, IsActive)
VALUES
    ('Rajesh', 'Kumar', 1, '2021-02-12', 52000.00 , false),
    ('Pooja', 'Sharma', 2, '2020-05-18', 58000.00, true),
    ('Amit', 'Verma', 3, '2022-07-19', 64000.00, true),
    ('Neha', 'Gupta', 4, '2023-03-30', 70000.00, false),
    ('Anil', 'Yadav', 1, '2021-09-21', 47000.00 , true),
    ('Meera', 'Patel', 2, '2022-06-11', 65000.00 , true),
    ('Vikram', 'Singh', 3, '2020-01-05', 56000.00 , false),
    ('Sanjay', 'Chopra', 4, '2023-05-27', 48000.00 , true),
    ('Sneha', 'Reddy', 6, '2022-10-14', 54000.00, false),
    ('Ravi', 'Mishra', 2, '2021-12-25', 62000.00, true),
    ('Anjali', 'Nair', 3, '2023-01-09', 74000.00, false),
    ('Harish', 'Iyer', 7, '2020-07-03', 46000.00 , true),
    ('Kavita', 'Bansal', 1, '2021-04-17', 59000.00, false),
    ('Arjun', 'Desai', 2, '2022-11-08', 61000.00 , true),
    ('Priya', 'Joshi', 7, '2020-09-14', 68000.00, true),
    ('Ramesh', 'Kulkarni', 5, '2023-02-19', 47000.00, true);

-- REPLACE INTO employees (FirstName, LastName, DepartmentID, HireDate, Salary, IsActive) VALUES ('Rajesh', 'Kumar', 7, '2021-02-12', 52000.00, false),
-- Updating data in the Employees table
-- Updates the salary of an employee.
UPDATE Employees
SET Salary = 80000.00
WHERE EmployeeID = 3;

-- Updating multiple columns
-- Updates the department and hire date for a specific employee.
UPDATE Employees
SET DepartmentID = 2, HireDate = '2023-01-01'
WHERE EmployeeID = 4;

-- Deleting data from the Employees table
-- Removes a specific employee from the table.
DELETE FROM Employees
WHERE EmployeeID = 2;

-- Deleting multiple rows based on a condition
-- Removes employees with a salary less than 50000.
DELETE FROM Employees
WHERE Salary < 50000;


-- Demonstrating INSERT INTO SELECT
-- Copies employees from Engineering department into a backup table.
CREATE TABLE EmployeeBackup AS
SELECT * FROM Employees
WHERE DepartmentID = 3;

-- Select active employees
SELECT * FROM employees WHERE IsActive = true;
-- Select inactive employees

SELECT * FROM employees WHERE IsActive = false;

