using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;
using MySqlX.XDevAPI.Relational;

namespace ORM_Dapper
{
    internal class Program
    {
        //Wanted to do the bonus exercises, ran out of time. Maybe I'll come back to it later.
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);
            var deptRepo = new DapperDepartmentRepository(conn);
            var productRepo = new DapperProductRepository(conn);
            bool run = true;
            string userInput;
            string[] selections = new string[] { "1", "2", "3", "4" };
            bool valid;
            var categories = new Dictionary<int, string>();
            categories.Add(1, "computers");
            categories.Add(2, "appliances");
            categories.Add(3, "phones");
            categories.Add(4, "audio");
            categories.Add(5, "home theater");
            categories.Add(6, "printers");
            categories.Add(7, "music");
            categories.Add(8, "games");
            categories.Add(9, "services");
            categories.Add(10, "other");

            while (run)
            {
                Console.WriteLine($"Please make a selection.\n1. See all departments\n2. Create department\n3. See all products\n4. Create product");
                do
                {
                    userInput = Console.ReadLine();
                    if (selections.Contains(userInput))
                    {
                        switch (userInput)
                        {
                            case "1":
                                var departments = deptRepo.GetAllDepartments();
                                Console.WriteLine();
                                foreach (var department in departments)
                                {
                                    Console.WriteLine(department.Name);
                                }
                                break;
                            case "2":
                                Console.WriteLine("\nEnter a new Department name");
                                var newDepartment = Console.ReadLine();
                                deptRepo.InsertDepartment(newDepartment);
                                break;
                            case "3":
                                var products = productRepo.GetAllProducts();
                                Console.WriteLine();
                                foreach (var product in products)
                                {
                                    Console.WriteLine(product.Name);
                                }
                                break;
                            case "4":
                                Console.WriteLine("\nEnter a new product name");
                                var newProductName = Console.ReadLine();
                                Console.WriteLine("\nEnter the price of the product");
                                double newProductPrice;
                                do
                                {
                                    userInput = Console.ReadLine();
                                    if (double.TryParse(userInput, out newProductPrice))
                                    {
                                        valid = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid selection. Please enter a monetary value");
                                        valid = false;
                                    }
                                } while (!valid);
                                Console.WriteLine("\nEnter a category:");
                                for (int i = 0; i < categories.Count; i++)
                                {
                                    Console.WriteLine($"{categories.ElementAt(i).Key}. {Methods.FirstToUpper(categories.ElementAt(i).Value)}");
                                }
                                int newProductCategory = 0;
                                do
                                {
                                    userInput = Console.ReadLine();
                                    Int32.TryParse(userInput, out int intInput);
                                    if (categories.ContainsKey(intInput))
                                    {
                                        newProductCategory = intInput;
                                        valid = true;
                                    }
                                    else if (categories.ContainsValue(userInput))
                                    {
                                        newProductCategory = categories.FirstOrDefault(x => x.Value == userInput).Key;
                                        valid = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid selection. Please input a number or a category name.");
                                        valid = false;
                                    }
                                } while (!valid);
                                productRepo.CreateProduct(newProductName, newProductPrice, newProductCategory);
                                Console.WriteLine($"{newProductName} added at ${newProductPrice} in the {Methods.FirstToUpper(categories[newProductCategory])} category. ");
                                break;
                        }
                        valid = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Select a number");
                        valid = false;
                    }
                } while (!valid);
                Console.WriteLine("Would you like to make another selection? yes/no");
                do
                {
                    userInput = (Console.ReadLine().ToLower());
                    if (userInput == "yes")
                    {
                        run = true;
                        valid = true;
                    }
                    else if (userInput == "no")
                    {
                        run = false;
                        valid = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please type yes or no");
                        run = false;
                        valid = false;
                    }
                } while (!valid);
            }
        }
    }
}