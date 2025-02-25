-- Skapa databasen
CREATE DATABASE VehiclePartsManagement;
GO

USE VehiclePartsManagement;
GO

-- Tabell för tillverkare
CREATE TABLE Manufacturers (
    ManufacturerId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    CountryOfOrigin NVARCHAR(50),
    Description NVARCHAR(MAX)
);
GO

-- Tabell för fordonsmodeller
CREATE TABLE VehicleModels (
    ModelId INT PRIMARY KEY IDENTITY(1,1),
    ModelName NVARCHAR(100) NOT NULL,
    ManufacturerId INT,
    YearRange NVARCHAR(20),
    Description NVARCHAR(MAX),
    CONSTRAINT FK_VehicleModels_Manufacturers FOREIGN KEY (ManufacturerId) REFERENCES Manufacturers(ManufacturerId)
);
GO

-- Tabell för kategorierna
CREATE TABLE Categories (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    ParentCategoryId INT NULL,
    CategoryName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    CONSTRAINT PK_CategoryId PRIMARY KEY (CategoryId),
    CONSTRAINT FK_Category_ParentCategory FOREIGN KEY (ParentCategoryId) REFERENCES Categories(CategoryId)
);
GO

-- Tabell för delar
CREATE TABLE Parts (
    PartId INT PRIMARY KEY IDENTITY(1,1),
    PartNumber NVARCHAR(50) UNIQUE,
    Name NVARCHAR(200) NOT NULL,
    BasePrice DECIMAL(10,2) NOT NULL,
    CategoryId INT,
    CompatibilityNotes NVARCHAR(MAX),
    IsTireRimCompatible BIT DEFAULT 0,
    CONSTRAINT FK_Parts_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
);
GO

-- Tabell för lager
CREATE TABLE Inventory (
    InventoryId INT PRIMARY KEY IDENTITY(1,1),
    PartId INT,
    Quantity INT NOT NULL,
    LastUpdated DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Inventory_Parts FOREIGN KEY (PartId) REFERENCES Parts(PartId)
);
GO

-- Tabell för beställningar
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY IDENTITY(1,1),
    OrderDate DATETIME DEFAULT GETDATE(),
    Status NVARCHAR(20) CHECK(Status IN ('pending', 'shipped', 'delivered', 'cancelled')),
    TotalAmount DECIMAL(10,2)
);
GO

-- Tabell för orderrader
CREATE TABLE OrderItems (
    OrderItemId INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT,
    PartId INT,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    CONSTRAINT FK_OrderItems_Parts FOREIGN KEY (PartId) REFERENCES Parts(PartId)
);
GO


	


-- Skapa tabell för delar (parts)
CREATE TABLE parts (
    id INT PRIMARY KEY IDENTITY(1,1),
    part_number VARCHAR(50),
    name VARCHAR(200),
    price DECIMAL(10,2),
    category_id INT,
    vehicle_type VARCHAR(20) CHECK (vehicle_type IN ('car', 'heavy_vehicle')),
    compatibility_notes TEXT,
   
);

-- Skapa tabell för lager (inventory)
CREATE TABLE inventory (
    id INT PRIMARY KEY IDENTITY(1,1),
    part_id INT,
    quantity INT,
    last_updated DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (part_id) REFERENCES parts(id)
);
GO
CREATE TABLE order_items (
    id INT PRIMARY KEY IDENTITY(1,1),
    order_id INT,
    inventory_id INT,
    quantity INT,
    unit_price DECIMAL(10,2),
   
    FOREIGN KEY (inventory_id) REFERENCES inventory(id)
);
-- Create VEHICLE_TYPES table
CREATE TABLE vehicle_types (
    id INT PRIMARY KEY IDENTITY(1,1),
    type_name VARCHAR(50) NOT NULL,
    description TEXT
);
-- Insert into vehicle_types
INSERT INTO vehicle_types (type_name, description) VALUES
('Car', 'Passenger vehicles'),
('Truck', 'Commercial trucks'),
('MC & Scooter', 'Motorcycles and scooters'),
('Motorhome & Caravan', 'Recreational vehicles'),
('Snowmobile', 'Winter vehicles');
SET IDENTITY_INSERT Categories ON;
INSERT INTO Categories (CategoryId, ParentCategoryId, CategoryName, Description)
VALUES 
    (1, NULL, 'Vehicle Parts', 'Root category for all vehicle parts'),
    (2, 1, 'Car Parts', 'Parts for passenger vehicles'),
    (3, 1, 'Truck Parts', 'Heavy duty truck parts'),
    (4, 1, 'MC & Scooter Parts', 'Motorcycle and scooter components');

-- Insert into vehicle_types
INSERT INTO vehicle_types (type_name, description) VALUES
('Car', 'Passenger vehicles'),
('Truck', 'Commercial trucks'),
('MC & Scooter', 'Motorcycles and scooters');


INSERT INTO parts (part_number, name, price, category_id, vehicle_type, compatibility_notes)
VALUES 
('P001', 'Brake Pad Set', 35.99, 2, 'car', 'Compatible with most sedans'),
('P002', 'Oil Filter', 15.50, 2, 'car', 'Fits various car engines'),
('P003', 'Air Filter', 22.75, 2, 'car', 'Standard air filter for sedans'),
('P004', 'Truck Brake Disc', 120.00, 3, 'heavy_vehicle', 'Heavy-duty disc for trucks'),
('P005', 'Truck Suspension Kit', 250.99, 3, 'heavy_vehicle', 'Reinforced for heavy loads'),
('P006', 'Car Battery', 110.00, 2, 'car', '12V battery for passenger cars'),
('P007', 'Truck Battery', 220.00, 3, 'heavy_vehicle', '24V battery for heavy trucks'),
('P008', 'Radiator', 85.50, 2, 'car', 'Fits compact and midsize cars'),
('P009', 'Alternator', 199.99, 3, 'heavy_vehicle', 'Heavy-duty alternator for trucks'),
('P010', 'Spark Plug Set', 50.00, 2, 'car', 'Performance spark plugs for sedans'),
('P011', 'Transmission Fluid', 35.75, 2, 'car', 'High-performance transmission oil'),
('P012', 'Clutch Kit', 175.00, 3, 'heavy_vehicle', 'Designed for high torque applications'),
('P013', 'Fuel Pump', 130.00, 2, 'car', 'Delivers fuel to the engine efficiently'),
('P014', 'Shock Absorber', 89.99, 3, 'heavy_vehicle', 'Heavy-duty suspension component'),
('P015', 'Starter Motor', 160.50, 2, 'car', 'Reliable starter motor for most cars'),
('P016', 'Truck Engine Mount', 78.99, 3, 'heavy_vehicle', 'Reduces engine vibrations'),
('P017', 'Disc Brake Rotor', 49.99, 2, 'car', 'High-performance brake disc'),
('P018', 'Power Steering Pump', 140.75, 2, 'car', 'Enhances steering efficiency'),
('P019', 'Headlight Assembly', 99.99, 2, 'car', 'Bright LED headlight set'),
('P020', 'Truck Air Compressor', 285.00, 3, 'heavy_vehicle', 'For commercial truck air systems'),
-- Continue adding more entries up to 150
('P150', 'Heavy-Duty Wheel Bearing', 89.99, 3, 'heavy_vehicle', 'Long-lasting bearing for trucks');


SELECT COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'parts';


CREATE TABLE Cart (
    CartId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    PartId INT NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    AddedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)

);

SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Cart';

SET IDENTITY_INSERT VehiclePartsManagement.dbo.Users ON;

INSERT INTO VehiclePartsManagement.dbo.Users (UserId, Username, Email)
VALUES 
    (1, 'john_doe', 'john.doe@vehicleparts.com'),
    (2, 'jane_smith', 'jane.smith@vehicleparts.com'),
    (3, 'mike_brown', 'mike.brown@vehicleparts.com'),
    (4, 'sarah_jones', 'sarah.jones@vehicleparts.com'),
    (5, 'tom_wilson', 'tom.wilson@vehicleparts.com');

SET IDENTITY_INSERT VehiclePartsManagement.dbo.Users OFF;

CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(100) NOT NULL,
    Email NVARCHAR(200) NOT NULL
);
CREATE INDEX IX_Cart_UserId ON Cart(UserId);
CREATE INDEX IX_Cart_PartId ON Cart(PartId);

-- Kontrollera om tabellen finns
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Cart')
    PRINT 'Tabellen finns'
ELSE
    PRINT 'Tabellen finns inte';


-- Skapa tabellen om den inte finns
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Cart')
BEGIN
CREATE TABLE Cart (
    CartId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    PartId INT NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    AddedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (PartId) REFERENCES Parts(PartId));
END;
-- Skapa Parts-tabellen om den inte finns
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Parts')
BEGIN
    CREATE TABLE Parts (
        PartId INT PRIMARY KEY IDENTITY(1,1),
        PartNumber NVARCHAR(50) UNIQUE,
        Name NVARCHAR(200) NOT NULL,
        BasePrice DECIMAL(10,2) NOT NULL,
        CategoryId INT,
        CompatibilityNotes NVARCHAR(MAX),
        IsTireRimCompatible BIT DEFAULT 0
    );
END;