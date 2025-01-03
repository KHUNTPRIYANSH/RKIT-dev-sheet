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

-- Example scenario: Using implicit transactions
-- MySQL automatically commits each statement unless a transaction is explicitly started.
-- To demonstrate implicit transactions, no START TRANSACTION is used here.
INSERT INTO Departments (DepartmentName) VALUES ('Quality Assurance');
-- This insert is committed automatically by default.

-- Best practices
-- Always use START TRANSACTION, COMMIT, and ROLLBACK for critical operations.
-- Use SAVEPOINT for finer control during long transactions.
-- FLUSH TABLES can be used after a commit to ensure data is written to disk (optional).
