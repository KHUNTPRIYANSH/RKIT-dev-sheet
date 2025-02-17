// following code push local data to mock api

// $(function () {
//     const mockApiUrl = "https://67aee3c19e85da2f020eac23.mockapi.io/dummy_product_mock/api/products/products";

//     const productData = [
//         { id: 1, emoji: "💻", name: "Laptop", category: "Electronics", link: "#", price: 1200 },
//         { id: 2, emoji: "📱", name: "Smartphone", category: "Electronics", link: "#", price: 800 },
//         { id: 3, emoji: "🎧", name: "Bluetooth Headphones", category: "Electronics", link: "#", price: 150 },
//         { id: 4, emoji: "👟", name: "Sneakers", category: "Fashion", link: "#", price: 100 },
//         { id: 5, emoji: "👖", name: "Jeans", category: "Fashion", link: "#", price: 25 },
//         { id: 6, emoji: "👕", name: "T-Shirt", category: "Fashion", link: "#", price: 25 },
//         { id: 7, emoji: "🔥", name: "Microwave Oven", category: "Home Appliances", link: "#", price: 200 },
//         { id: 8, emoji: "🧹", name: "Vacuum Cleaner", category: "Home Appliances", link: "#", price: 250 },
//         { id: 9, emoji: "❄️", name: "Air Conditioner", category: "Home Appliances", link: "#", price: 1000 },
//         { id: 10, emoji: "📺", name: "Television", category: "Electronics", link: "#", price: 500 },
//         { id: 11, emoji: "🎮", name: "Gaming Console", category: "Electronics", link: "#", price: 300 },
//         { id: 12, emoji: "🖥️", name: "Desktop Computer", category: "Electronics", link: "#", price: 900 },
//         { id: 13, emoji: "💡", name: "Smart Light Bulb", category: "Home Appliances", link: "#", price: 25 },
//         { id: 14, emoji: "📸", name: "Camera", category: "Electronics", link: "#", price: 450 },
//         { id: 15, emoji: "📚", name: "Bookshelf", category: "Furniture", link: "#", price: 80 },
//         { id: 16, emoji: "🛋️", name: "Sofa", category: "Furniture", link: "#", price: 500 },
//         { id: 17, emoji: "🪑", name: "Chair", category: "Furniture", link: "#", price: 75 },
//         { id: 18, emoji: "🚲", name: "Bicycle", category: "Sports", link: "#", price: 200 },
//         { id: 19, emoji: "🏀", name: "Basketball", category: "Sports", link: "#", price: 20 },
//         { id: 20, emoji: "⚽", name: "Soccer Ball", category: "Sports", link: "#", price: 15 },
//         { id: 21, emoji: "🧑", name: "Chef Knife", category: "Kitchen", link: "#", price: 50 },
//         { id: 22, emoji: "🍴", name: "Cutlery Set", category: "Kitchen", link: "#", price: 30 },
//         { id: 23, emoji: "🥄", name: "Spoon Set", category: "Kitchen", link: "#", price: 10 },
//         { id: 24, emoji: "🍽️", name: "Dinner Plates", category: "Kitchen", link: "#", price: 20 },
//         { id: 25, emoji: "⛺", name: "Camping Tent", category: "Outdoor", link: "#", price: 150 },
//         { id: 26, emoji: "🛶", name: "Kayak", category: "Outdoor", link: "#", price: 300 },
//         { id: 27, emoji: "🥾", name: "Hiking Boots", category: "Outdoor", link: "#", price: 120 },
//         { id: 28, emoji: "🎒", name: "Backpack", category: "Outdoor", link: "#", price: 40 },
//         { id: 29, emoji: "🖨️", name: "Printer", category: "Electronics", link: "#", price: 150 },
//         { id: 30, emoji: "💾", name: "External Hard Drive", category: "Electronics", link: "#", price: 100 },
//         { id: 31, emoji: "🖋️", name: "Fountain Pen", category: "Office Supplies", link: "#", price: 20 },
//         { id: 32, emoji: "📎", name: "Paper Clips", category: "Office Supplies", link: "#", price: 5 },
//         { id: 33, emoji: "📏", name: "Ruler", category: "Office Supplies", link: "#", price: 3 },
//         { id: 34, emoji: "🗂️", name: "File Organizer", category: "Office Supplies", link: "#", price: 15 },
//         { id: 35, emoji: "🎁", name: "Gift Box", category: "Miscellaneous", link: "#", price: 10 },
//         { id: 36, emoji: "🎀", name: "Ribbon", category: "Miscellaneous", link: "#", price: 5 },
//         { id: 37, emoji: "🌺", name: "Flower Pot", category: "Decor", link: "#", price: 25 },
//         { id: 38, emoji: "🖼️", name: "Wall Art", category: "Decor", link: "#", price: 80 },
//         { id: 39, emoji: "🕯️", name: "Candle Set", category: "Decor", link: "#", price: 15 },
//         { id: 40, emoji: "🪞", name: "Mirror", category: "Decor", link: "#", price: 50 },
//         { id: 41, emoji: "👗", name: "Dress", category: "Fashion", link: "#", price: 50 },
//         { id: 42, emoji: "👚", name: "Blouse", category: "Fashion", link: "#", price: 35 },
//         { id: 43, emoji: "👒", name: "Hat", category: "Fashion", link: "#", price: 20 },
//         { id: 44, emoji: "👛", name: "Purse", category: "Fashion", link: "#", price: 80 },
//         { id: 45, emoji: "🕶️", name: "Sunglasses", category: "Fashion", link: "#", price: 40 },
//         { id: 46, emoji: "👠", name: "Heels", category: "Fashion", link: "#", price: 60 },
//         { id: 47, emoji: "🧳", name: "Luggage", category: "Fashion", link: "#", price: 150 },
//         { id: 48, emoji: "🧴", name: "Perfume", category: "Beauty", link: "#", price: 100 },
//         { id: 49, emoji: "🛀", name: "Shower Gel", category: "Beauty", link: "#", price: 15 },
//         { id: 50, emoji: "🧼", name: "Soap Bar", category: "Beauty", link: "#", price: 5 }
//     ];

//     // Function to push dummy data to the API
//     function pushProductData() {
//         productData.forEach(product => {
//             $.ajax({
//                 url: mockApiUrl,
//                 method: "POST",
//                 data: JSON.stringify(product),
//                 contentType: "application/json",
//                 success: function () {
//                     console.log(`Product ${product.name} inserted successfully.`);
//                 },
//                 error: function () {
//                     console.log(`Error inserting product ${product.name}.`);
//                 }
//             });
//         });
//     }

//     // Trigger the push of dummy data
//     pushProductData();
// });

