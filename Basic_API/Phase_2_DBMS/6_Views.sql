-- File: views.sql
-- This file demonstrates the creation, modification, and use of Views in a MySQL database with detailed examples.

-- Switching to the EmployeeManagement database
USE EmployeeManagement;

-- Creating a simple view
-- This view displays EmployeeID, full name, and DepartmentID for all employees.
CREATE VIEW EmployeeSummary AS
SELECT 
    EmployeeID, 
    CONCAT(FirstName, ' ', LastName) AS FullName, 
    DepartmentID
FROM Employees;

-- Selecting data from the view
SELECT * FROM EmployeeSummary;

-- Creating a view with additional filtering
-- This view shows employees earning more than 50,000.
CREATE VIEW HighEarners AS
SELECT 
    EmployeeID, 
    FirstName, 
    LastName, 
    Salary
FROM Employees
WHERE Salary > 50000;

-- Selecting data from the HighEarners view
SELECT * FROM HighEarners;

-- Creating a view to join multiple tables
-- This view combines data from Employees and Departments tables.
CREATE VIEW EmployeeDepartmentDetails AS
SELECT 
    e.EmployeeID, 
    CONCAT(e.FirstName, ' ', e.LastName) AS FullName,
    d.DepartmentName, 
    e.Salary
FROM Employees e
JOIN Departments d ON e.DepartmentID = d.DepartmentID;

-- Selecting data from the joined view
SELECT * FROM EmployeeDepartmentDetails;

-- Updating data using a view
-- Updating salary for an employee via the EmployeeSummary view
-- Note: This requires the view to be updatable (direct mapping to base table columns).
UPDATE EmployeeSummary
SET DepartmentID = 3
WHERE EmployeeID = 1;

-- Modifying an existing view
-- Add a new column to include HireDate in the EmployeeSummary view.
CREATE OR REPLACE VIEW EmployeeSummary AS
SELECT 
    EmployeeID, 
    CONCAT(FirstName, ' ', LastName) AS FullName, 
    DepartmentID,
    HireDate
FROM Employees;

-- Selecting data from the modified view
SELECT * FROM EmployeeSummary;

-- Dropping a view
-- Removes the HighEarners view from the database.
DROP VIEW HighEarners;

-- Example: Aggregated view
-- This view calculates the average salary per department.
CREATE VIEW AvgSalaryPerDepartment AS
SELECT 
    d.DepartmentName, 
    AVG(e.Salary) AS AverageSalary
FROM Employees e
JOIN Departments d ON e.DepartmentID = d.DepartmentID
GROUP BY d.DepartmentName;

-- Selecting data from the aggregated view
SELECT * FROM AvgSalaryPerDepartment;

-- Complex view with filtering and ordering
-- This view lists the top 5 highest-paid employees.
CREATE VIEW Top5HighEarners AS
SELECT 
    EmployeeID, 
    CONCAT(FirstName, ' ', LastName) AS FullName, 
    Salary
FROM Employees
ORDER BY Salary DESC
LIMIT 5;

-- Selecting data from the complex view
SELECT * FROM Top5HighEarners;
