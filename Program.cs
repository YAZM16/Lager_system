using Microsoft.Data.SqlClient;
using System.Text;
using Microsoft.EntityFrameworkCore;
using My_Project_03.Models;
using BCrypt.Net;
using System.Security.Cryptography;

namespace My_Project_03
{

    
    internal class Program
    {
      
        private static string connectionString = "Server=DESKTOP-8SDDV0E\\SQLEXPRESS;Database=VehiclePartsManagement;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;";
        static void Main()
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Vehicle Parts Management ====");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterUser();
                        break;
                    case "2":
                        LoginUser();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        static void  AdminPanel()
        {
            Console.WriteLine("Hello, World!");
            using var db = new VehiclePartsManagementContext();
            db.Database.EnsureCreated(); // Ensure the database is created

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Vehicle Parts Management ====\n==== ADMIN PANEL ====");
                Console.WriteLine("1. Add Manufacturer");
                Console.WriteLine("2. View Manufacturers");
                Console.WriteLine("3. Update Manufacturer");
                Console.WriteLine("4. Delete Manufacturer");
                Console.WriteLine("5. Add Vehicle Model");
                Console.WriteLine("6. View Vehicle Models");
                Console.WriteLine("7. Update Vehicle Model");
                Console.WriteLine("8. Delete Vehicle Model");
                Console.WriteLine("9. Add Part");
                Console.WriteLine("10. View Parts");
                Console.WriteLine("11. Logout");
                Console.WriteLine("14. Exit");
                Console.Write("Choose an option: ");



                switch (Console.ReadLine())
                {
                    case "1":
                        AddManufacturer();
                        break;
                    case "2":
                        ViewManufacturers();
                        break;
                    case "3":
                        UpdateManufacturer();
                        break;
                    case "4":
                        DeleteManufacturer();
                        break;
                    case "5":
                        AddVehicleModel();
                        break;
                    case "6":
                        ViewVehicleModels();
                        break;
                    case "7":
                        UpdateVehicleModel();
                        break;
                    case "8":
                        DeleteVehicleModel();
                        break;
                    case "9": AddParts();
                        break;
                    case "10": ViewParts();
                        break;
                     case "11":
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
        static void UserMenu(string username)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"==== USER MENU ({username}) ====");
                Console.WriteLine("1. View Parts");
                Console.WriteLine("2. Add Part to Cart");
                Console.WriteLine("3. View Cart");
                Console.WriteLine("4. Logout");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        ViewParts();
                        break;
                    case "2":
                        AddPartToCart();
                        break;
                    case "3":
                        ViewCart();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
        static void RegisterUser()
        {
            Console.Clear();
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            RegisterUser(username, email, password);
        }

        static void RegisterUser(string username, string email, string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Users (Username, Email, PasswordHash, Role) VALUES (@Username, @Email, @PasswordHash, @Role)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                    cmd.Parameters.AddWithValue("@Role", "User");
                    try
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("✅ User registered successfully!");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("❌ Error: " + ex.Message);
                    }
                }
            }
        }

        static void LoginUser()
        {
            Console.Write("Enter username or email: ");
            string userInput = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT UserId, Username, Role, PasswordHash FROM Users WHERE Username = @UserInput OR Email = @UserInput";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserInput", userInput);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int userId = reader.GetInt32(0);
                            string username = reader.GetString(1);
                            string role = reader.GetString(2);
                            string storedHash = reader.GetString(3);
                            _ = HashPassword(password);

                            if (BCrypt.Net.BCrypt.Verify(password, storedHash))
                            {
                                Console.WriteLine("✅ Password is correct!");

                                if (role.ToLower() == "admin")
                                {
                                    Console.WriteLine("✅ Redirecting to Admin Panel...");
                                    AdminPanel();
                                }
                                else
                                {
                                    Console.WriteLine("✅ Redirecting to User Menu...");
                                    UserMenu(username);
                                }
                            }
                            else
                            {
                                Console.WriteLine("❌ Incorrect password.");
                            }
                        }
                    }
                }
            }
            Console.WriteLine("❌ Invalid username/email or password.");
        }
       

        static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private static void Exit()
        {
            throw new NotImplementedException();
        }

        static void AddManufacturer()
        {
            Console.Clear();
            using var db = new VehiclePartsManagementContext();
            Console.Write("Enter Manufacturer Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Country: ");
            string country = Console.ReadLine();
            Console.Write("Enter Description: ");
            string description = Console.ReadLine();

            var manufacturer = new Manufacturer { Name = name, CountryOfOrigin = country, Description = description };
            db.Manufacturers.Add(manufacturer);
            db.SaveChanges();

            Console.WriteLine("Manufacturer Added Successfully!");
            Console.ReadKey();
    }

    static void ViewManufacturers()
    {
            Console.Clear();
            using var db = new VehiclePartsManagementContext();
            var manufacturers = db.Manufacturers.ToList();

            Console.WriteLine("\n=== Manufacturer List ===");
            foreach (var m in manufacturers)
            {
                Console.WriteLine($"ID: {m.ManufacturerId}, Name: {m.Name}, Country: {m.CountryOfOrigin}");
            }
            Console.ReadKey();
    }






        static void UpdateManufacturer()
        {
            Console.Clear();
            using var db = new VehiclePartsManagementContext();
            Console.Write("Enter Manufacturer ID to Update: ");
          int id = int.Parse(Console.ReadLine());
          var manufacturer = db.Manufacturers.Find(id);

           if (manufacturer != null)
           {
             Console.Write("Enter New Name (leave empty to keep current): ");
             string name = Console.ReadLine();
             Console.Write("Enter New Country: ");
             string country = Console.ReadLine();
             Console.Write("Enter New Description: ");
             string description = Console.ReadLine();

              if (!string.IsNullOrWhiteSpace(name)) manufacturer.Name = name;
              if (!string.IsNullOrWhiteSpace(country)) manufacturer.CountryOfOrigin = country;
              if (!string.IsNullOrWhiteSpace(description)) manufacturer.Description = description;

              db.SaveChanges();
              Console.WriteLine("Manufacturer Updated Successfully!");
           }
        else
        {
           Console.WriteLine("Manufacturer Not Found!");
        }
           Console.ReadKey();
        }

        static void DeleteManufacturer()
        {
            using var db = new VehiclePartsManagementContext();
            Console.Write("Enter Manufacturer ID to Delete: ");
            int id = int.Parse(Console.ReadLine());
            var manufacturer = db.Manufacturers.Find(id);

            if (manufacturer != null)
            {
                db.Manufacturers.Remove(manufacturer);
                db.SaveChanges();
                Console.WriteLine("Manufacturer Deleted Successfully!");
            }
            else
            {
                Console.WriteLine("Manufacturer Not Found!");
            }
            Console.ReadKey();
        }

        static void AddVehicleModel()
        {
            using var db = new VehiclePartsManagementContext();
            Console.Write("Enter Model Name: ");
            string modelName = Console.ReadLine();
            Console.Write("Enter Manufacturer ID: ");
            int manufacturerId = int.Parse(Console.ReadLine());
            Console.Write("Enter Year Range: ");
            string yearRange = Console.ReadLine();

            var model = new VehicleModel { ModelName = modelName, ManufacturerId = manufacturerId, YearRange = yearRange };
            db.VehicleModels.Add(model);
            db.SaveChanges();
            Console.WriteLine("Vehicle Model Added Successfully!");
            Console.ReadKey();
        }

        static void ViewVehicleModels()
        {
            Console.Clear();
            using var db = new VehiclePartsManagementContext();
            var models = db.VehicleModels.ToList();
            Console.WriteLine("\n=== Vehicle Models ===");
            foreach (var vm in models)
            {
                Console.WriteLine($"ID: {vm.ModelId}, Name: {vm.ModelName}, Manufacturer: {vm.ManufacturerId}, Year Range: {vm.YearRange}");
            }
            Console.ReadKey();
        }

        static void UpdateVehicleModel()
        {
            using var db = new VehiclePartsManagementContext();
            Console.Write("Enter Model ID to Update: ");
            int id = int.Parse(Console.ReadLine());
            var model = db.VehicleModels.Find(id);

            if (model != null)
            {
                Console.Write("Enter New Model Name: ");
                string name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name)) model.ModelName = name;
                db.SaveChanges();
                Console.WriteLine("Vehicle Model Updated Successfully!");
            }
            else
            {
                Console.WriteLine("Model Not Found!");
            }
            Console.ReadKey();
        }

        static void DeleteVehicleModel()
        {
            using var db = new VehiclePartsManagementContext();
            Console.Write("Enter Model ID to Delete: ");
            int id = int.Parse(Console.ReadLine());
            var model = db.VehicleModels.Find(id);

            if (model != null)
            {
                db.VehicleModels.Remove(model);
                db.SaveChanges();
                Console.WriteLine("Vehicle Model Deleted Successfully!");
            }
            else
            {
                Console.WriteLine("Model Not Found!");
            }
            Console.ReadKey();
        }

        static void AddParts()
        {
            using var db = new VehiclePartsManagementContext();
            Console.Write("Enter Part Name: ");
            string partName = Console.ReadLine();
            Console.Write("Enter Model ID: ");
            int modelId = int.Parse(Console.ReadLine());
            Console.Write("Enter Price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            var part = new Parts { Name = partName, Price = price };
            db.Parts.Add(part);
            db.SaveChanges();
            Console.WriteLine("Part Added Successfully!");
            Console.ReadKey();
        }

        static void ViewParts()
        {
            Console.Clear();
            using var db = new VehiclePartsManagementContext();
            var viewparts = db.Parts.ToList();
            Console.WriteLine("\n=== Parts List ===");
            foreach (var pt in viewparts)
            {
                   Console.WriteLine($"ID: {pt.Id}, Name: {pt.Name}, Model: {pt.PartNumber}, Price: {pt.Price:C}");
            }
            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey(true);
        }

        static void AddPartToCart()
        {
            using var db = new VehiclePartsManagementContext();
            Console.Write("Enter User ID: ");
            int userId = int.Parse(Console.ReadLine());
            Console.Write("Enter Part ID to Add to Cart: ");
            int partId = int.Parse(Console.ReadLine());

            var cartItem = new Cart { UserId = userId, PartId = partId, Quantity = 1, AddedDate = DateTime.Now };
            db.Carts.Add(cartItem);
            db.SaveChanges();
            Console.WriteLine("Part added to cart!");
        }

        static void ViewCart()
        {
            Console.Clear();
            using var db = new VehiclePartsManagementContext();
            Console.Write("Enter User ID: ");
            int userId = int.Parse(Console.ReadLine());

            var cartItems = db.Carts.Include(c => c.Parts).Where(c => c.UserId == userId).ToList();
            Console.WriteLine("\n=== Shopping Cart ===");
            foreach (var item in cartItems)
            {
                Console.WriteLine($"Part: {item.Parts.Name}, Quantity: {item.Quantity}, Price: {item.Parts.Price:C}");
            }
            Console.WriteLine($"Total: {cartItems.Sum(c => c.Parts.Price * c.Quantity):C}");
            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey(true);
        }

        static void UpdateCart()
        {
            using var db = new VehiclePartsManagementContext();
            Console.Write("Enter User ID: ");
            int userId = int.Parse(Console.ReadLine());
            Console.Write("Enter Part ID to Update Quantity: ");
            int partId = int.Parse(Console.ReadLine());
            Console.Write("Enter New Quantity: ");
            int newQuantity = int.Parse(Console.ReadLine());

            var cartItem = db.Carts.FirstOrDefault(c => c.UserId == userId && c.PartId == partId);
            if (cartItem != null)
            {
                cartItem.Quantity = newQuantity;
                db.SaveChanges();
                Console.WriteLine("Cart updated successfully!");
            }
            else
            {
                Console.WriteLine("Cart item not found.");
            }
        }
    }
}
