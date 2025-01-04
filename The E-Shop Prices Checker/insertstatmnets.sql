INSERT INTO Categories (Name, Description) VALUES
('Electronics', 'Devices like phones, laptops, and tablets'),
('Groceries', 'Everyday food and household items'),
('Clothing', 'Apparel including shirts, pants, and dresses');
INSERT INTO Entities (Name, Type, Address, ContactInfo) VALUES
('TechWorld', 'Supermarket', '123 Tech Street', 'contact@techworld.com'),
('FreshMart', 'Supermarket', '456 Green Avenue', 'info@freshmart.com'),
('FashionHub', 'Mall', '789 Style Blvd', 'hello@fashionhub.com');
INSERT INTO Products (Name, Description, CategoryId, EntityId) VALUES
('Smartphone X', 'Latest model with advanced features', 1, 1),
('Laptop Pro', 'Lightweight and powerful laptop', 1, 1),
('Wireless Headphones', 'Noise-cancelling, over-ear headphones', 1, 1),
('Organic Apples', 'Freshly picked organic apples', 2, 2),
('Almond Milk', 'Non-dairy milk alternative', 2, 2),
('Cereal Mix', 'Healthy whole-grain cereal', 2, 2),
('T-shirt', 'Comfortable cotton T-shirt', 3, 3),
('Jeans', 'Classic denim jeans', 3, 3),
('Jacket', 'Warm winter jacket', 3, 3),
('Tablet Y', 'Portable and lightweight tablet', 1, 1);
INSERT INTO ProductEntities (ProductId, EntityId, DateAdded, Price, IsAvailable) VALUES
-- Product 1
(1, 1, '2024-11-24', 999.99, 1),
(1, 2, '2024-11-24', 950.00, 1),
(1, 3, '2024-11-24', 970.00, 1),

-- Product 2
(2, 1, '2024-11-24', 1299.99, 1),
(2, 2, '2024-11-24', 1250.00, 1),
(2, 3, '2024-11-24', 1275.00, 1),

-- Product 3
(3, 1, '2024-11-24', 199.99, 1),
(3, 2, '2024-11-24', 180.00, 1),
(3, 3, '2024-11-24', 190.00, 1),

-- Product 4
(4, 2, '2024-11-24', 3.49, 1),
(4, 3, '2024-11-24', 3.25, 1),

-- Product 5
(5, 2, '2024-11-24', 2.99, 1),
(5, 3, '2024-11-24', 2.75, 1),

-- Product 6
(6, 2, '2024-11-24', 4.99, 1),
(6, 3, '2024-11-24', 4.50, 1),

-- Product 7
(7, 3, '2024-11-24', 9.99, 1),
(7, 1, '2024-11-24', 10.50, 1),

-- Product 8
(8, 3, '2024-11-24', 29.99, 1),
(8, 1, '2024-11-24', 30.99, 1),

-- Product 9
(9, 3, '2024-11-24', 59.99, 1),
(9, 1, '2024-11-24', 58.99, 1),

-- Product 10
(10, 1, '2024-11-24', 499.99, 1),
(10, 2, '2024-11-24', 480.00, 1),
(10, 3, '2024-11-24', 490.00, 1);
