-- File: dml.sql
-- This file demonstrates DML (Data Manipulation Language) commands in a basic Employee Management Database.

-- Switching to the database
USE EmployeeManagement;

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
INSERT INTO Employees (FirstName, LastName, DepartmentID, HireDate, Salary)
VALUES
    ('Rajesh', 'Kumar', 1, '2021-02-12', 52000.00),
    ('Pooja', 'Sharma', 2, '2020-05-18', 58000.00),
    ('Amit', 'Verma', 3, '2022-07-19', 64000.00),
    ('Neha', 'Gupta', 4, '2023-03-30', 70000.00),
    ('Anil', 'Yadav', 1, '2021-09-21', 47000.00),
    ('Meera', 'Patel', 2, '2022-06-11', 65000.00),
    ('Vikram', 'Singh', 3, '2020-01-05', 56000.00),
    ('Sanjay', 'Chopra', 4, '2023-05-27', 48000.00),
    ('Sneha', 'Reddy', 6, '2022-10-14', 54000.00),
    ('Ravi', 'Mishra', 2, '2021-12-25', 62000.00),
    ('Anjali', 'Nair', 3, '2023-01-09', 74000.00),
    ('Harish', 'Iyer', 7, '2020-07-03', 46000.00),
    ('Kavita', 'Bansal', 1, '2021-04-17', 59000.00),
    ('Arjun', 'Desai', 2, '2022-11-08', 61000.00),
    ('Priya', 'Joshi', 7, '2020-09-14', 68000.00),
    ('Ramesh', 'Kulkarni', 5, '2023-02-19', 47000.00);

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

-- Selecting data from the Departments table
-- Retrieves all department records.
SELECT * FROM Departments;

-- Selecting specific columns from the Employees table
-- Retrieves the first name, last name, and salary of all employees.
SELECT FirstName, LastName, Salary FROM Employees;

-- Filtering data with WHERE clause
-- Retrieves employees in the Engineering department.
SELECT * FROM Employees
WHERE DepartmentID = 3;

-- Using ORDER BY to sort data
-- Retrieves employees ordered by salary in descending order.
SELECT * FROM Employees
ORDER BY Salary DESC;

-- Using LIMIT to restrict the number of rows returned
-- Retrieves the top 2 highest-paid employees.
SELECT * FROM Employees
ORDER BY Salary DESC
LIMIT 2;

-- Demonstrating data aggregation
-- Retrieves the total salary expenditure per department.
SELECT DepartmentID, SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY DepartmentID;

-- Using HAVING to filter aggregated data
-- Retrieves departments with total salary expenditure greater than 100000.
SELECT DepartmentID, SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY DepartmentID
HAVING TotalSalary > 100000;

-- Demonstrating JOIN operation
-- Retrieves employee details along with their department names.
SELECT e.EmployeeID, e.FirstName, e.LastName, d.DepartmentName, e.Salary
FROM Employees e
JOIN Departments d ON e.DepartmentID = d.DepartmentID;

-- Demonstrating INSERT INTO SELECT
-- Copies employees from Engineering department into a backup table.
CREATE TABLE EmployeeBackup AS
SELECT * FROM Employees
WHERE DepartmentID = 3;

-- Demonstrating transaction control
-- Starts a transaction, updates salary, and rolls back the changes.
START TRANSACTION;
UPDATE Employees
SET Salary = Salary + 5000
WHERE DepartmentID = 1;
ROLLBACK;

-- Starts a transaction, updates salary, and commits the changes.
START TRANSACTION;
UPDATE Employees
SET Salary = Salary + 5000
WHERE DepartmentID = 1;
COMMIT;
