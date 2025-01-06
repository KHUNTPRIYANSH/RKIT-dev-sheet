-- File: dql.sql
-- This file demonstrates DQL (Data Query Language) commands with detailed examples covering every possibility.

-- Switching to the database
USE EmployeeManagement;

-- Basic SELECT statement
-- Retrieves all columns and rows from the Employees table.
SELECT * FROM Employees;

-- Selecting specific columns
-- Retrieves only the FirstName and LastName of employees.
SELECT FirstName, LastName FROM Employees;

-- Using WHERE clause for filtering
-- Retrieves employees with a salary greater than 50000.
SELECT * FROM Employees
WHERE Salary > 50000;

-- Using WHERE with multiple conditions
-- Retrieves employees hired after 2022 and earning less than 70000.
SELECT * FROM Employees
WHERE HireDate > '2022-01-01' AND Salary < 70000;
SELECT * FROM Employees
WHERE HireDate > '2022-01-01' OR Salary < 70000;
SELECT * FROM Employees
WHERE NOT (HireDate > '2022-01-01' AND Salary < 70000);

-- Using LIKE for pattern matching
-- Retrieves employees whose first name starts with 'S'.
SELECT * FROM Employees
WHERE FirstName LIKE 'S%';
-- here % stands for any number of char , and _ stands for only 1 char

-- Using IN operator
-- Retrieves employees working in specific departments.
SELECT * FROM Employees
WHERE DepartmentID IN (1, 2, 3);

-- Using BETWEEN for range filtering
-- Retrieves employees with salaries between 40000 and 80000.
SELECT * FROM Employees
WHERE Salary BETWEEN 40000 AND 60000;

-- Using ORDER BY for sorting
-- Retrieves employees ordered by their last name in ascending order.
SELECT * FROM Employees
ORDER BY LastName ASC;
-- we can use NULL FIRST , NULL LAST to manage null values while using order by

-- Sorting by multiple columns
-- Retrieves employees sorted by department and then by salary in descending order.
SELECT * FROM Employees
ORDER BY DepartmentID ASC, Salary DESC;

-- Using LIMIT to restrict rows returned
-- Retrieves second highest-paid employees.
SELECT * FROM Employees
ORDER BY Salary DESC
LIMIT 1 OFFSET 1;
-- Limit = limit's the count of row in result
-- offset = skips number of row from original result


-- Aggregation functions
-- 1. COUNT: Counts the number of employees.
SELECT COUNT(*) AS EmployeeCount FROM Employees;

-- 2. SUM: Calculates the total salary expenditure.
SELECT SUM(Salary) AS TotalSalary FROM Employees;

-- 3. AVG: Calculates the average salary.
SELECT AVG(Salary) AS AverageSalary FROM Employees;

-- 4. MIN: Finds the minimum salary.
SELECT MIN(Salary) AS MinimumSalary FROM Employees;

-- 5. MAX: Finds the maximum salary.
SELECT MAX(Salary) AS MaximumSalary FROM Employees;

-- Using GROUP BY
-- Retrieves total salary expenditure per department.
SELECT DepartmentID, SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY DepartmentID;

-- Using GROUP BY with HAVING
-- Retrieves departments with total salary expenditure greater than 100000.
SELECT DepartmentID, SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY DepartmentID
HAVING TotalSalary > 100000;

-- Using DISTINCT to remove duplicates
-- Retrieves distinct department IDs from Employees table.
SELECT DISTINCT DepartmentID FROM Employees;

-- Using JOIN for combining tables
-- Retrieves employee details along with their department names.
SELECT e.EmployeeID, e.FirstName, e.LastName, d.DepartmentName
FROM Employees e
JOIN Departments d ON e.DepartmentID = d.DepartmentID;

-- Using LEFT JOIN
-- Retrieves all employees and their department names, including employees without a department.
SELECT e.EmployeeID, e.FirstName, e.LastName, d.DepartmentName
FROM Employees e
LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID;

-- Using RIGHT JOIN
-- Retrieves all departments and their employees, including departments without employees.
SELECT e.EmployeeID, e.FirstName, e.LastName, d.DepartmentName
FROM Employees e
RIGHT JOIN Departments d ON e.DepartmentID = d.DepartmentID;

-- Using FULL OUTER JOIN
-- Retrieves all employees and departments, including unmatched rows.
SELECT e.EmployeeID, e.FirstName, e.LastName, d.DepartmentName
FROM Employees e
JOIN Departments d ON e.DepartmentID = d.DepartmentID;

-- Using Subqueries
-- Retrieves employees earning more than the average salary.
SELECT * FROM Employees
WHERE Salary > (SELECT AVG(Salary) FROM Employees);

-- Using UNION
-- Combines employees and departments into one result set with unique records.
SELECT EmployeeID AS ID, FirstName AS Name
FROM Employees
UNION
SELECT DepartmentID AS ID, DepartmentName AS Name
FROM Departments;

-- Using UNION ALL
-- Combines employees and departments into one result set including duplicates.
SELECT EmployeeID AS ID, FirstName AS Name
FROM Employees
UNION ALL
SELECT DepartmentID AS ID, DepartmentName AS Name
FROM Departments;

-- Demonstrating nested subqueries
-- Retrieves the highest-paid employee from the department with the highest total salary expenditure.
SELECT * FROM Employees
WHERE Salary = (
    SELECT MAX(Salary) FROM Employees
    WHERE DepartmentID = (
        SELECT DepartmentID FROM (
            SELECT DepartmentID, SUM(Salary) AS TotalSalary
            FROM Employees
            GROUP BY DepartmentID
            ORDER BY TotalSalary DESC
            LIMIT 1
        ) AS TopDepartment
    )
);

-- Advanced filtering with EXISTS
-- Retrieves departments that have employees earning more than 70000.
SELECT * FROM Departments d
WHERE EXISTS (
    SELECT 1 FROM Employees e
    WHERE e.DepartmentID = d.DepartmentID AND e.Salary > 70000
);
