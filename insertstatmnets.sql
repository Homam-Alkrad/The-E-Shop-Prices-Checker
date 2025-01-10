INSERT INTO Categories (Name, Description) VALUES
('Electronics', 'Devices like phones, laptops, and tablets'),
('Groceries', 'Everyday food and household items'),
('Clothing', 'Apparel including shirts, pants, and dresses'),
('Electronics', 'Devices, gadgets, and accessories related to technology'),
('Fashion', 'Clothing, footwear, and accessories for men, women, and children');

-- Acme Corporation Products
INSERT INTO [Products] ([Name], [Description], [CategoryId], [Price], [ApplicationUserId])
VALUES
('Phone', 'A high-quality smartphone.', 1, 699.99, '9ccf2b91-773b-4344-8d22-998378ef6e4d'),
('Book', 'An interesting book.', 2, 19.99, '9ccf2b91-773b-4344-8d22-998378ef6e4d'),
('Shoes', 'Comfortable running shoes.', 3, 49.99, '9ccf2b91-773b-4344-8d22-998378ef6e4d'),
('Laptop', 'A powerful laptop.', 4, 1199.99, '9ccf2b91-773b-4344-8d22-998378ef6e4d'),
('T-Shirt', 'A stylish T-shirt.', 5, 15.99, '9ccf2b91-773b-4344-8d22-998378ef6e4d');

-- GlobalTech Solutions Products
INSERT INTO [Products] ([Name], [Description], [CategoryId], [Price], [ApplicationUserId])
VALUES
('Phone', 'A high-quality smartphone.', 1, 799.99, '42c2f4bf-69b4-4ada-9a3e-126c029ccfab'),
('Book', 'An interesting book.', 2, 25.99, '42c2f4bf-69b4-4ada-9a3e-126c029ccfab'),
('Shoes', 'Comfortable running shoes.', 3, 59.99, '42c2f4bf-69b4-4ada-9a3e-126c029ccfab'),
('Laptop', 'A powerful laptop.', 4, 1299.99, '42c2f4bf-69b4-4ada-9a3e-126c029ccfab'),
('T-Shirt', 'A stylish T-shirt.', 5, 18.99, '42c2f4bf-69b4-4ada-9a3e-126c029ccfab');

-- FreshMart Enterprises Products
INSERT INTO [Products] ([Name], [Description], [CategoryId], [Price], [ApplicationUserId])
VALUES
('Phone', 'A high-quality smartphone.', 1, 649.99, '15c3f21f-5511-493b-939c-ccb6ec23ef23'),
('Book', 'An interesting book.', 2, 17.99, '15c3f21f-5511-493b-939c-ccb6ec23ef23'),
('Shoes', 'Comfortable running shoes.', 3, 45.99, '15c3f21f-5511-493b-939c-ccb6ec23ef23'),
('Laptop', 'A powerful laptop.', 4, 1149.99, '15c3f21f-5511-493b-939c-ccb6ec23ef23'),
('T-Shirt', 'A stylish T-shirt.', 5, 14.99, '15c3f21f-5511-493b-939c-ccb6ec23ef23');

-- Super Admin Products
INSERT INTO [Products] ([Name], [Description], [CategoryId], [Price], [ApplicationUserId])
VALUES
('Phone', 'A high-quality smartphone.', 1, 899.99, '047920ec-7069-4462-b73a-1b7c324c0e97'),
('Book', 'An interesting book.', 2, 29.99, '047920ec-7069-4462-b73a-1b7c324c0e97'),
('Shoes', 'Comfortable running shoes.', 3, 69.99, '047920ec-7069-4462-b73a-1b7c324c0e97'),
('Laptop', 'A powerful laptop.', 4, 1399.99, '047920ec-7069-4462-b73a-1b7c324c0e97'),
('T-Shirt', 'A stylish T-shirt.', 5, 19.99, '047920ec-7069-4462-b73a-1b7c324c0e97');
