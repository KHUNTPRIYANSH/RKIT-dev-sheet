CREATE database KPD_Final_Web_Api;
USE KPD_Final_Web_Api;

CREATE TABLE Employees (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100),
    City VARCHAR(50),
    IsActive BOOLEAN
);

INSERT INTO Employees (Name, City, IsActive) VALUES
('KPD', 'Mumbai', TRUE),
('PARTH', 'Delhi', FALSE),
('KEYUR', 'Ahmedabad', TRUE),
('YASH', 'Bangalore', TRUE);
