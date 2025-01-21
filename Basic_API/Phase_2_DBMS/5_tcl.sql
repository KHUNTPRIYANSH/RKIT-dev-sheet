-- File: tcl.sql
-- This file demonstrates TCL (Transaction Control Language) commands in detail with examples for managing transactions.

-- Switching to the EmployeeManagement database
USE EmployeeManagement;

-- Example scenario: Managing transactions for salary updates
-- Starting a transaction
START TRANSACTION;

-- Updating the salary of an employee
-- Suppose EmployeeID = 1 is getting a raise.
UPDATE Employees
SET Salary = Salary + 5000
WHERE EmployeeID = 1;

-- Deleting an employee mistakenly added to the database
DELETE FROM Employees
WHERE EmployeeID = 999;

-- Rolling back changes if something goes wrong
-- If the DELETE statement was incorrect, revert changes.
ROLLBACK;

-- Committing the transaction
-- If everything is correct, save changes permanently.
COMMIT;

-- Savepoints demonstration
-- Starting a transaction for managing departments
START TRANSACTION;

-- Inserting a new department
INSERT INTO Departments (DepartmentName) VALUES ('Research and Development');

-- Setting a savepoint
SAVEPOINT InsertedDepartment;

-- Updating the name of an existing department
UPDATE Departments
SET DepartmentName = 'R&D'
WHERE DepartmentName = 'Research and Development';

-- Rolling back to the savepoint
-- Reverting the update while keeping the insert intact.
ROLLBACK TO InsertedDepartment;

-- Committing the transaction to finalize the insertion
COMMIT;

-- Example scenario: Ensuring data integrity in bulk updates
-- Starting a transaction for multiple updates
START TRANSACTION;

-- Updating salaries for employees in a specific department
UPDATE Employees
SET Salary = Salary * 1.1
WHERE DepartmentID = 2;

-- Simulating an error to decide on rollback
-- Assuming a condition that invalidates the update (e.g., incorrect DepartmentID).
-- Rolling back all changes in this transaction.
ROLLBACK;



