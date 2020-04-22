using System;
using System.Data.SqlClient;
using System.Data;
namespace Credit_System
{
    static class Admin
    {
        static string Login { get; set; }
        static string Password { get; set; }
        static SqlConnection connection = new SqlConnection(Connect.StrConnection);
       public static void AddAdmin()
        {
            Console.Clear();
            Console.Write("Логин нового админа: "); Login = Console.ReadLine();
            Console.Clear();
            Console.Write("Пароль нового админа: "); Password = Console.ReadLine();
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string com = $"select * from Admin";
            bool result = false;
            using (SqlCommand command = new SqlCommand(com, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (Login == reader.GetValue(0).ToString())
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            if (!(result))
            {
                string comm = $"insert into Admin([Login],[Password]) values ('{Login}','{Password}')";
                SqlCommand command = new SqlCommand(comm, connection);
                command.ExecuteNonQuery();
                Console.Clear();
                System.Console.WriteLine("\tНовый админ успешно добавлено!");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Админ с таком логина уже ест!");
                Console.ReadKey();
            }
        }
        public static bool FindAdmin()
        {
            Console.Clear();
            Console.Write("Логин: "); Login = Console.ReadLine();
            Console.Clear();
            Console.Write("Пароль: "); Password = Console.ReadLine();
            string comm = "Select * from Admin";
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            SqlCommand command = new SqlCommand(comm, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (Login == reader.GetValue(0).ToString() && Password == reader.GetValue(1).ToString())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}