using System.Data;
using System;
using System.Data.SqlClient;

namespace Credit_System
{
    static class Customer
    {
        static string SerPassport { get; set; }
        static int Login { get; set; }
        static string Password { get; set; }
        static DateTime BirthDate { get; set; }
        static string Gender { get; set; }
        static string MaritalStatus { get; set; }
        static string Nationality { get; set; }
        static string FirstName { get; set; }
        static string LastName { get; set; }
        static string MiddleName { get; set; }
        static SqlConnection connection = new SqlConnection(Connect.StrConnection);
        public static bool Registration()
        {
            System.Console.WriteLine("\t\tРегистрация");
            Console.Write("Фамилия: "); LastName = Console.ReadLine();
            Console.Write("Имя: "); FirstName = Console.ReadLine();
            Console.Write("Очество(не обезательный): "); MiddleName = Console.ReadLine();
            Console.Write("Дата рождения(дд-мм-гггг): "); Convert.ToDateTime(BirthDate = Convert.ToDateTime(Console.ReadLine()));
        T1: Console.Write("Выберите семейное положение: 1.холостой\n2.семеянин\n3.вразводе\n4.вдовец/вдова\nВаш выбор: ");
            switch (Console.ReadLine())
            {
                case "1":
                    {
                        MaritalStatus = "холостой";
                    }
                    break;
                case "2":
                    {
                        MaritalStatus = "семеянин";
                    }
                    break;
                case "3":
                    {
                        MaritalStatus = "вразводе";
                    }
                    break;
                case "4":
                    {
                        MaritalStatus = "вдовец/вдова";
                    }
                    break;
                default:
                    {
                        Console.WriteLine("Неверная команда!");
                        goto T1;
                    }
            }
        T2: Console.Write("Выберите пол: 1.Жен\n2.Муж\nВаш выбор: ");
            switch (Console.ReadLine())
            {
                case "1":
                    {
                        Gender = "Жен";
                    }
                    break;
                case "2":
                    {
                        Gender = "Муж";
                    }
                    break;
                default:
                    {
                        Console.WriteLine("Неверная команда!");
                        goto T2;
                    }
            }
        T3: Console.Write("Выберите гражданство: 1.Таджикистан\n2.Другие страны\nВаш выбор: ");
            switch (Console.ReadLine())
            {
                case "1":
                    {
                        Gender = "Таджикистан";
                    }
                    break;
                case "2":
                    {
                        Gender = "Другие страны";
                    }
                    break;
                default:
                    {
                        Console.WriteLine("Неверная команда!");
                        goto T3;
                    }

            }
            Console.Write("Логин: "); Login = int.Parse(Console.ReadLine());
        T4: Console.Write("Пароль: "); Password = Console.ReadLine();
            Console.Write("Введите пароль еще раз для проверка: "); string Pass = Console.ReadLine();
            if (!(Password == Pass)) goto T4;

            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string com = $"select * from Customer";
            bool result = false;
            using (SqlCommand command = new SqlCommand(com, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (SerPassport == reader.GetValue(0).ToString() || Login == int.Parse(reader.GetValue(1).ToString()))
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            if (!(result))
            {
                com = $"insert into Customer([LastName],[FirstName],[MiddleName],[BirthDate],[SeriesPassport],[MaritalStatus],[Gender],[Login],[Password]) values ('{LastName}',{FirstName},'{MiddleName}','{BirthDate}','{SerPassport}','{MaritalStatus}','{Gender}','{Login}','{Password}')";
                SqlCommand command = new SqlCommand(com, connection);
                command.ExecuteNonQuery();
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("Клиент по такой серия паспорта или логин уже существует!");
                return false;
            }
            Console.Clear();
            Console.WriteLine("Регистрация усрешно завершен!");
            return true;
        }
        public static bool FindCustomer()
        {
            Console.Clear();
           L1: Console.Write("Введите свой номер телефон(логин): ");string Log =Console.ReadLine();
            int A;
            if(Log.Length==9 && int.TryParse(Log,out A))
            {
                Login=int.Parse(Log);
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("\tНеверний логин!!!\nПожалуйста введите еще раз");
                goto L1;
            }
            Console.Clear();
            Console.Write("Пароль: "); Password = Console.ReadLine();
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string com = $"select * from Customer";
            SqlCommand command = new SqlCommand(com, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (Login == int.Parse(reader.GetValue(7).ToString()) && Password == reader.GetValue(8).ToString())
                    {
                        LastName = reader.GetValue(0).ToString();
                        FirstName = reader.GetValue(1).ToString();
                        MiddleName = reader.GetValue(2).ToString();
                        BirthDate = Convert.ToDateTime(reader.GetValue(3).ToString());
                        SerPassport = reader.GetValue(4).ToString();
                        MaritalStatus = reader.GetValue(5).ToString();
                        Gender = reader.GetValue(6).ToString();
                        Nationality = reader.GetValue(9).ToString();
                        return true;
                    }
                }
            }
            return false;
        }
        public static void TableCustomer()
        {
            Console.Clear();
        P1: Console.WriteLine("1.Посмотреть все\n2.Посмотреть по серии паспорта или логин\n3.Назад\nВыберите команду: ");
            switch (Console.ReadLine())
            {
                case "1":
                    {
                        Console.Clear();
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();
                        string com = $"Select * from Customer";
                        using (SqlCommand command = new SqlCommand(com, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Console.Write($"{reader.GetValue(0).ToString()}\t{reader.GetValue(1).ToString()}\t{reader.GetValue(2).ToString()}\t{reader.GetValue(3).ToString()}\t{reader.GetValue(4).ToString()}\t{reader.GetValue(5).ToString()}\t{reader.GetValue(6).ToString()}\t{reader.GetValue(7).ToString()}\t{reader.GetValue(8).ToString()}\t{reader.GetValue(9).ToString()}");
                                }
                            }
                        }
                        goto P1;
                    }
                case "2":
                    {
                        Console.Clear();
                        Console.WriteLine("Введите серии паспорта или логин! "); SerPassport = Console.ReadLine();
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();
                        string com = $"select * from Admin";
                        using (SqlCommand command = new SqlCommand(com, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (SerPassport == reader.GetValue(4).ToString() || SerPassport == reader.GetValue(7).ToString())
                                    {
                                        Console.WriteLine($"{reader.GetValue(0).ToString()}\t{reader.GetValue(1).ToString()}\t{reader.GetValue(2).ToString()}\t{reader.GetValue(3).ToString()}\t{reader.GetValue(4).ToString()}\t{reader.GetValue(5).ToString()}\t{reader.GetValue(6).ToString()}\t{reader.GetValue(7).ToString()}\t{reader.GetValue(8).ToString()}\t{reader.GetValue(9).ToString()}");

                                    }
                                }
                            }
                        }
                        goto P1;
                    }
                case "3":
                    {
                        Console.Clear();
                        return;
                    }
                default:
                    {
                        Console.Clear();
                        Console.WriteLine("Неверная команда!");
                        goto P1;
                    }
            }
        }
    }
}