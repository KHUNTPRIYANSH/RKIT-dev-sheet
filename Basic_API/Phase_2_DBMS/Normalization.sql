-- #######################################################
-- # DATABASE: Indian E-Commerce Normalization Example
-- # PURPOSE: Demonstrate normalization from UNF to BCNF
-- #######################################################

-- #################
-- # CLEANUP CODE  #
-- #################

DROP DATABASE KPD_Normalization;

DROP TABLE IF EXISTS BCNF_OrderItems, BCNF_Products, BCNF_Orders, BCNF_Customers;
DROP TABLE IF EXISTS 3NF_OrderItems, 3NF_Products, 3NF_Orders, 3NF_Customers;
DROP TABLE IF EXISTS 2NF_OrderItems, 2NF_Products, 2NF_Orders, 2NF_Customers;
DROP TABLE IF EXISTS 1NF_OrderItems, 1NF_Orders;
DROP TABLE IF EXISTS UnnormalizedOrders;

-- ####################################
-- # STEP 0: Create Unnormalized Table 
-- ####################################
CREATE DATABASE IF NOT EXISTS KPD_Normalization;
USE KPD_Normalization;

CREATE TABLE UnnormalizedOrders (
    OrderID INT,
    OrderDate DATE,
    CustomerID INT,
    CustomerName VARCHAR(50),
    CustomerPhone VARCHAR(15),
    ProductIDs VARCHAR(100),    -- Comma-separated list
    ProductNames VARCHAR(200),  -- Comma-separated list
    Quantities VARCHAR(50),     -- Comma-separated list
    Prices VARCHAR(100),        -- Comma-separated list
    TotalAmount DECIMAL(10,2),
    ShippingAddress VARCHAR(200)
);

INSERT INTO UnnormalizedOrders VALUES
(1, '2023-10-01', 101, 'Amit Sharma', '9998887776', 'P001,P002', 'Laptop,Wireless Mouse', '1,2', '50000.00,500.00', 51000.00, 'Delhi, India'),
(2, '2023-10-02', 102, 'Priya Singh', '8887776665', 'P003', 'Mechanical Keyboard', '1', '2500.00', 2500.00, 'Mumbai, India'),
(3, '2023-10-05', 103, 'Rahul Verma', '7776665554', 'P002,P004', 'Wireless Mouse,Smartphone', '1,1', '500.00,20000.00', 20500.00, 'Bangalore, India');


SELECT * FROM UnnormalizedOrders;

-- Data Redundancy & Repeating Groups
-- Issue: In UnnormalizedOrders, 
-- multiple products are stored in a single row using comma-separated values 
-- (ProductIDs, ProductNames, Quantities, Prices).

-- ############################
-- # STEP 1: First Normal Form 
-- ############################
-- Create separate tables for Orders and Order Items.
-- Each order item should be one row per product.
CREATE TABLE 1NF_Orders (
    OrderID INT PRIMARY KEY,
    OrderDate DATE,
    CustomerID INT,
    CustomerName VARCHAR(50),
    CustomerPhone VARCHAR(15),
    TotalAmount DECIMAL(10,2),
    ShippingAddress VARCHAR(200)
);

CREATE TABLE 1NF_OrderItems (
    OrderID INT,
    ProductID VARCHAR(10),
    ProductName VARCHAR(50),
    Quantity INT,
    Price DECIMAL(10,2),
    PRIMARY KEY (OrderID, ProductID)
);

INSERT INTO 1NF_Orders SELECT DISTINCT OrderID, OrderDate, CustomerID, CustomerName, CustomerPhone, TotalAmount, ShippingAddress FROM UnnormalizedOrders;

INSERT IGNORE INTO 1NF_OrderItems VALUES  
(1, 'P001', 'Laptop', 1, 50000.00), 
(1, 'P002', 'Wireless Mouse', 2, 500.00), 
(2, 'P003', 'Mechanical Keyboard', 1, 2500.00), 
(3, 'P002', 'Wireless Mouse', 1, 500.00), 
(3, 'P004', 'Smartphone', 1, 20000.00);

SELECT * FROM 1NF_Orders;
SELECT * FROM 1NF_OrderItems;

-- Issue: The table 1NF_OrderItems has columns like ProductName and Price, 
-- but they depend only on ProductID, not OrderID.
-- Redundancy: If the same product appears in multiple orders, its name and price are repeated.
-- Solution: Separate Products into a dedicated table (2NF).

-- #############################
-- # STEP 2: Second Normal Form 
-- #############################
CREATE TABLE 2NF_Orders (
    OrderID INT PRIMARY KEY,
    OrderDate DATE,
    CustomerID INT,
    TotalAmount DECIMAL(10,2),
    ShippingAddress VARCHAR(200)
);
-- (CustomerID → CustomerName, CustomerPhone)
CREATE TABLE 2NF_Customers (
    CustomerID INT PRIMARY KEY,
    CustomerName VARCHAR(50) NOT NULL,
    CustomerPhone VARCHAR(15)
);
-- (ProductID → ProductName, Price)
CREATE TABLE 2NF_Products (
    ProductID VARCHAR(10) PRIMARY KEY,
    ProductName VARCHAR(50) NOT NULL,
    Price DECIMAL(10,2) CHECK (Price > 0)
);

CREATE TABLE 2NF_OrderItems (
    OrderID INT,
    ProductID VARCHAR(10),
    Quantity INT CHECK (Quantity > 0),
    PRIMARY KEY (OrderID, ProductID)
);

INSERT INTO 2NF_Orders SELECT OrderID, OrderDate, CustomerID, TotalAmount, ShippingAddress FROM 1NF_Orders;
INSERT INTO 2NF_Customers SELECT DISTINCT CustomerID, CustomerName, CustomerPhone FROM 1NF_Orders;
INSERT INTO 2NF_Products SELECT DISTINCT ProductID, ProductName, Price FROM 1NF_OrderItems;
INSERT INTO 2NF_OrderItems SELECT OrderID, ProductID, Quantity FROM 1NF_OrderItems;

SELECT * FROM 2NF_Orders;
SELECT * FROM 2NF_Customers;
SELECT * FROM 2NF_Products;
SELECT * FROM 2NF_OrderItems;

-- Problem in 2NF: Transitive Dependency
-- Issue: ShippingAddress in 2NF_Orders contains both city and address.
-- The city depends on ShippingAddress, not OrderID.
-- Solution: Extract city into a separate attribute (3NF).

-- ############################
-- # STEP 3: Third Normal Form 
-- ############################

-- Removed ShippingAddress from 3NF_Orders and replaced it with ShippingCity
CREATE TABLE 3NF_Orders (
    OrderID INT PRIMARY KEY,
    OrderDate DATE,
    CustomerID INT,
    TotalAmount DECIMAL(10,2),
    ShippingCity VARCHAR(50) -- Separated City
);


CREATE TABLE 3NF_Customers LIKE 2NF_Customers;
CREATE TABLE 3NF_Products LIKE 2NF_Products;
CREATE TABLE 3NF_OrderItems LIKE 2NF_OrderItems;

INSERT INTO 3NF_Orders (OrderID, OrderDate, CustomerID, TotalAmount, ShippingCity)
SELECT OrderID, OrderDate, CustomerID, TotalAmount, SUBSTRING_INDEX(ShippingAddress, ',', 1) AS ShippingCity FROM UnnormalizedOrders;
INSERT INTO 3NF_Customers SELECT * FROM 2NF_Customers;
INSERT INTO 3NF_Products SELECT * FROM 2NF_Products;
INSERT INTO 3NF_OrderItems SELECT * FROM 2NF_OrderItems;

SELECT * FROM 3NF_Orders;
SELECT * FROM 3NF_Customers;
SELECT * FROM 3NF_Products;
SELECT * FROM 3NF_OrderItems;
