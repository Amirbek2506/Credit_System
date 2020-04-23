using System.Data;
using System;
using System.Data.SqlClient;

namespace Credit_System
{
    static class Customer
    {
        public static string SerPassport { get; set; }
        public static int Login { get; set; }
        public static string Password { get; set; }
        public static DateTime BirthDate { get; set; }
        public static string Gender { get; set; }
        public static string MaritalStatus { get; set; }
        public static string Nationality { get; set; }
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string MiddleName { get; set; }
        static SqlConnection connection = new SqlConnection(Connect.StrConnection);
        public static bool Registration()
        {
            Console.Clear();
            System.Console.WriteLine("\t\tРегистрация");
            Console.Write("Фамилия: "); LastName = Console.ReadLine();
            Console.Write("Имя: "); FirstName = Console.ReadLine();
            Console.Write("Очество(не обезательный): "); MiddleName = Console.ReadLine();
            Console.Clear();
        D1: Console.Write("Дата рождения(дд-мм-гггг): ");
            DateTime A; string AA = Console.ReadLine();
            if (DateTime.TryParse(AA, out A)) BirthDate = A;
            else
            {
                Console.Clear();
                Console.WriteLine("\t\tНеверная дата рождения!!!");
                goto D1;
            }
            Console.Clear();
            Console.WriteLine("Введите свой серия паспорта");
            SerPassport = Console.ReadLine();
            Console.Clear();
        T1: Console.Write("\tCемейное положение\n1.холостой\n2.семеянин\n3.вразводе\n4.вдовец/вдова\nВыберите команду: ");
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
                        Console.Clear();
                        Console.WriteLine("Неверная команда!");
                        goto T1;
                    }
            }
            Console.Clear();
        T2: Console.Write("1.Жен\n2.Муж\nВыберите пол: ");
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
                        Console.Clear();
                        Console.WriteLine("Неверная команда!");
                        goto T2;
                    }
            }
            Console.Clear();
        T3: Console.Write("Выберите гражданство\n1.Таджикистан\n2.Другие страны\nВаш выбор: ");
            switch (Console.ReadLine())
            {
                case "1":
                    {
                        Nationality = "Таджикистан";
                    }
                    break;
                case "2":
                    {
                        Nationality = "Другие страны";
                    }
                    break;
                default:
                    {
                        Console.Clear();
                        Console.WriteLine("Неверная команда!");
                        Console.ReadKey();
                        Console.Clear();
                        goto T3;
                    }

            }
            Console.Clear();
        L1: Console.Write("Введите свой номер телефон(логин): "); string Log = Console.ReadLine();
            if (Log.Length == 9 && int.TryParse(Log, out int B))
            {
                Login = int.Parse(Log);
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("\tНеверний логин!!!\nПожалуйста введите еще раз");
                goto L1;
            }
        T4: Console.Clear();
            Console.Write("Пароль: "); Password = Console.ReadLine();
            Console.Write("Введите пароль еще раз для проверка: "); string Pass = Console.ReadLine();
            if (!(Password == Pass)) goto T4;

            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string com = "select * from Customer";
            bool result = false;
            using (SqlCommand command = new SqlCommand(com, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (SerPassport == reader.GetValue(4).ToString() || Login == int.Parse(reader.GetValue(7).ToString()))
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            if (!(result))
            {
                com = $"insert into Customer([LastName],[FirstName],[MiddleName],[BirthDate],[SeriesPassport],[MaritalStatus],[Gender],[Login],[Password],[Nationality]) Values ('{LastName}','{FirstName}','{MiddleName}','{BirthDate}','{SerPassport}','{MaritalStatus}','{Gender}',{Login},'{Password}','{Nationality}')";
                SqlCommand command = new SqlCommand(com, connection);
                command.ExecuteNonQuery();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Клиент по такой серия паспорта или логин уже существует!");
                return false;
            }
            Console.Clear();
            Console.WriteLine("Регистрация усрешно завершен!");
            Console.ReadKey();
            return true;
        }
        public static bool FindCustomer()
        {
        L1: Console.Clear();
            Console.Write("Введите свой номер телефон(логин): "); string Log = Console.ReadLine();
            int A;
            if (Log.Length == 9 && int.TryParse(Log, out A))
            {
                Login = int.Parse(Log);
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("\tНеверний логин!!!\nПожалуйста введите еще раз");
                Console.ReadKey();
                System.Console.Write("1.Назад\n2.Вход\nВыберите команду:");
                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            return false;
                        }
                    case "2":
                        {
                            goto L1;
                        }
                }
                Console.Clear();
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
                                    Console.WriteLine($"{reader.GetValue(0).ToString()}\t{reader.GetValue(1).ToString()}\t{reader.GetValue(2).ToString()}\t{reader.GetValue(3).ToString()}\t{reader.GetValue(4).ToString()}\t{reader.GetValue(5).ToString()}\t{reader.GetValue(6).ToString()}\t{reader.GetValue(7).ToString()}\t{reader.GetValue(8).ToString()}\t{reader.GetValue(9).ToString()}\n");
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
                        string com = $"select * from Customer";
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
        public static void CustomerforSer()
        {
            Console.Clear();
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string com = $"select * from Customer";
            using (SqlCommand command = new SqlCommand(com, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (SerPassport == reader.GetValue(4).ToString())
                        {
                            Console.WriteLine($"{reader.GetValue(0).ToString()}\t{reader.GetValue(1).ToString()}\t{reader.GetValue(2).ToString()}\t{reader.GetValue(3).ToString()}\t{reader.GetValue(4).ToString()}\t{reader.GetValue(5).ToString()}\t{reader.GetValue(6).ToString()}\t{reader.GetValue(7).ToString()}\t{reader.GetValue(8).ToString()}\t{reader.GetValue(9).ToString()}");

                        }
                    }
                }
            }
        }
    }
}