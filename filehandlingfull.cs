using System;
using System.IO;

class Program
{
    const int MAX_USERS = 10;
    const int MAX_PRODUCTS = 10;

    // FILE HANDLING FUNCTIONS
    static void SaveUsers(string[] users, string[] pass, int count)
    {
        StreamWriter file = new StreamWriter("users.txt");

        for (int i = 0; i < count; i++)
        {
            file.WriteLine(users[i] + "," + pass[i]);
        }

        file.Close();
    }

    static int LoadUsers(string[] users, string[] pass)
    {
        int count = 0;

        if (File.Exists("users.txt"))
        {
            string[] lines = File.ReadAllLines("users.txt");

            foreach (string line in lines)
            {
                string[] data = line.Split(',');
                users[count] = data[0];
                pass[count] = data[1];
                count++;
            }
        }

        return count;
    }

    static void SaveProducts(string[] name, int[] price, int[] stock, int count)
    {
        StreamWriter file = new StreamWriter("products.txt");

        for (int i = 0; i < count; i++)
        {
            file.WriteLine(name[i] + "," + price[i] + "," + stock[i]);
        }

        file.Close();
    }

    static int LoadProducts(string[] name, int[] price, int[] stock)
    {
        int count = 0;

        if (File.Exists("products.txt"))
        {
            string[] lines = File.ReadAllLines("products.txt");

            foreach (string line in lines)
            {
                string[] data = line.Split(',');

                name[count] = data[0];
                price[count] = Convert.ToInt32(data[1]);
                stock[count] = Convert.ToInt32(data[2]);

                count++;
            }
        }

        return count;
    }

    static void TopHeader()
    {
        Console.WriteLine("=============================");
        Console.WriteLine("      STYLESPARK STORE");
        Console.WriteLine("=============================");
    }

    static void MainMenu()
    {
        Console.WriteLine("\n--- MAIN MENU ---");
        Console.WriteLine("1. Sign Up");
        Console.WriteLine("2. Customer Login");
        Console.WriteLine("3. Admin Login");
        Console.WriteLine("4. Exit");
    }

    static void AdminMenu()
    {
        Console.WriteLine("\n--- ADMIN MENU ---");
        Console.WriteLine("1. Add Product");
        Console.WriteLine("2. View Products");
        Console.WriteLine("3. Update Product");
        Console.WriteLine("4. Delete Product");
        Console.WriteLine("5. Update User");
        Console.WriteLine("6. Delete User");
        Console.WriteLine("7. Logout");
    }

    static void CustomerMenu()
    {
        Console.WriteLine("\n--- CUSTOMER MENU ---");
        Console.WriteLine("1. View Products");
        Console.WriteLine("2. Buy Product");
        Console.WriteLine("3. Logout");
    }

    static int SignUp(string[] users, string[] pass, int count)
    {
        Console.Write("Enter username: ");
        users[count] = Console.ReadLine();

        Console.Write("Enter password: ");
        pass[count] = Console.ReadLine();

        Console.WriteLine("User registered.");
        return count + 1;
    }

    static bool SignIn(string[] users, string[] pass, int count)
    {
        Console.Write("Username: ");
        string u = Console.ReadLine();

        Console.Write("Password: ");
        string p = Console.ReadLine();

        for (int i = 0; i < count; i++)
        {
            if (users[i] == u && pass[i] == p)
            {
                Console.WriteLine("Login successful.");
                return true;
            }
        }

        Console.WriteLine("Wrong login.");
        return false;
    }

    static int UpdateUser(string[] users, string[] pass, int count)
    {
        Console.Write("User index: ");
        int i = Convert.ToInt32(Console.ReadLine());

        if (i >= 0 && i < count)
        {
            Console.Write("New username: ");
            users[i] = Console.ReadLine();

            Console.Write("New password: ");
            pass[i] = Console.ReadLine();

            Console.WriteLine("User updated.");
        }

        return count;
    }

    static int DeleteUser(string[] users, string[] pass, int count)
    {
        Console.Write("User index: ");
        int i = Convert.ToInt32(Console.ReadLine());

        if (i >= 0 && i < count)
        {
            for (int j = i; j < count - 1; j++)
            {
                users[j] = users[j + 1];
                pass[j] = pass[j + 1];
            }

            Console.WriteLine("User deleted.");
            return count - 1;
        }

        return count;
    }

    static int AddProduct(string[] name, int[] price, int[] stock, int count)
    {
        Console.Write("Product name: ");
        name[count] = Console.ReadLine();

        Console.Write("Price: ");
        price[count] = Convert.ToInt32(Console.ReadLine());

        Console.Write("Stock: ");
        stock[count] = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Product added.");
        return count + 1;
    }

    static void ViewProducts(string[] name, int[] price, int[] stock, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine(i + ". " + name[i] + " | " + price[i] + " | " + stock[i]);
        }
    }

    static void UpdateProduct(string[] name, int[] price, int[] stock, int count)
    {
        Console.Write("Product index: ");
        int i = Convert.ToInt32(Console.ReadLine());

        if (i >= 0 && i < count)
        {
            Console.Write("New name: ");
            name[i] = Console.ReadLine();

            Console.Write("New price: ");
            price[i] = Convert.ToInt32(Console.ReadLine());

            Console.Write("New stock: ");
            stock[i] = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Product updated.");
        }
    }

    static int DeleteProduct(string[] name, int[] price, int[] stock, int count)
    {
        Console.Write("Product index: ");
        int i = Convert.ToInt32(Console.ReadLine());

        if (i >= 0 && i < count)
        {
            for (int j = i; j < count - 1; j++)
            {
                name[j] = name[j + 1];
                price[j] = price[j + 1];
                stock[j] = stock[j + 1];
            }

            Console.WriteLine("Product deleted.");
            return count - 1;
        }

        return count;
    }

    static void Main()
    {
        string[] usernames = new string[MAX_USERS];
        string[] passwords = new string[MAX_USERS];

        string[] productName = new string[MAX_PRODUCTS];
        int[] productPrice = new int[MAX_PRODUCTS];
        int[] productStock = new int[MAX_PRODUCTS];

        int userCount = LoadUsers(usernames, passwords);
        int productCount = LoadProducts(productName, productPrice, productStock);

        int choice;
        bool loggedIn = false;

        TopHeader();

        do
        {
            MainMenu();
            Console.Write("Enter choice: ");
            choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1)
            {
                userCount = SignUp(usernames, passwords, userCount);
                SaveUsers(usernames, passwords, userCount);
            }

            else if (choice == 2)
            {
                loggedIn = SignIn(usernames, passwords, userCount);

                if (loggedIn)
                {
                    int custChoice;

                    do
                    {
                        CustomerMenu();
                        Console.Write("Enter choice: ");
                        custChoice = Convert.ToInt32(Console.ReadLine());

                        if (custChoice == 1)
                        {
                            ViewProducts(productName, productPrice, productStock, productCount);
                        }

                        else if (custChoice == 2)
                        {
                            int index, qty;

                            ViewProducts(productName, productPrice, productStock, productCount);

                            Console.Write("Enter product index: ");
                            index = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Enter quantity: ");
                            qty = Convert.ToInt32(Console.ReadLine());

                            if (qty <= productStock[index])
                            {
                                productStock[index] -= qty;
                                SaveProducts(productName, productPrice, productStock, productCount);
                                Console.WriteLine("Purchase successful! Total price: " + productPrice[index] * qty);
                            }
                            else
                            {
                                Console.WriteLine("Not enough stock.");
                            }
                        }

                    } while (custChoice != 3);
                }
            }

            else if (choice == 3)
            {
                if (SignIn(usernames, passwords, userCount))
                {
                    int adminChoice;

                    do
                    {
                        AdminMenu();
                        Console.Write("Enter choice: ");
                        adminChoice = Convert.ToInt32(Console.ReadLine());

                        if (adminChoice == 1)
                        {
                            productCount = AddProduct(productName, productPrice, productStock, productCount);
                            SaveProducts(productName, productPrice, productStock, productCount);
                        }

                        else if (adminChoice == 2)
                        {
                            ViewProducts(productName, productPrice, productStock, productCount);
                        }

                        else if (adminChoice == 3)
                        {
                            UpdateProduct(productName, productPrice, productStock, productCount);
                            SaveProducts(productName, productPrice, productStock, productCount);
                        }

                        else if (adminChoice == 4)
                        {
                            productCount = DeleteProduct(productName, productPrice, productStock, productCount);
                            SaveProducts(productName, productPrice, productStock, productCount);
                        }

                        else if (adminChoice == 5)
                        {
                            userCount = UpdateUser(usernames, passwords, userCount);
                            SaveUsers(usernames, passwords, userCount);
                        }

                        else if (adminChoice == 6)
                        {
                            userCount = DeleteUser(usernames, passwords, userCount);
                            SaveUsers(usernames, passwords, userCount);
                        }

                    } while (adminChoice != 7);
                }
            }

        } while (choice != 4);
    }
}