using System;
using System.Data;
using System.Data.SqlClient;

namespace Credit_System
{
    public static class Zayavki
    {
        public static string SerPassport;
        public static Decimal SummCredit;
        public static Decimal OpshDokhod;
        public static string SelCredit;
        public static int SrokCredit;
        static SqlConnection connection = new SqlConnection(Connect.StrConnection);
        public static void TableZayavki()
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
                        string com = $"Select * from Zayavki";
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
                        Console.WriteLine("Введите серии паспорта"); SerPassport = Console.ReadLine();
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();
                        string com = $"select * from Zayavki";
                        using (SqlCommand command = new SqlCommand(com, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (SerPassport == reader.GetValue(0).ToString())
                                    {
                                        Console.WriteLine($"{reader.GetValue(0).ToString()}\t{reader.GetValue(1).ToString()}\t{reader.GetValue(2).ToString()}\t{reader.GetValue(3).ToString()}\t{reader.GetValue(4).ToString()}\t{reader.GetValue(5).ToString()}");
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
        public static bool AddZayavka()
        {
            Console.Clear();
            Console.Write("Сумма кредита: "); SummCredit = Decimal.Parse(Console.ReadLine());
            Console.Write("Общий доход: "); OpshDokhod = Decimal.Parse(Console.ReadLine());
            Console.Clear();
        S1: Console.Write("\tВыберите цель кредита!\n1.Бытовая техника\n2.Ремонт\n3.Телефон\n4.Прочее\n");
            switch (Console.ReadLine())
            {
                case "1":
                    {
                        SelCredit = "Бытовая техника";
                    }
                    break;
                case "2":
                    {
                        SelCredit = "Ремонт";
                    }
                    break;
                case "3":
                    {
                        SelCredit = "Телефон";
                    }
                    break;
                case "4":
                    {
                        SelCredit = "Прочее";
                    }
                    break;
                default:
                    {
                        Console.Clear();
                        Console.WriteLine("\tНеверная команда!");
                        goto S1;
                    }
            }
        S2: Console.Write("Срок кредита(на месяц): "); SrokCredit = int.Parse(Console.ReadLine());
            if (!(SrokCredit > 3 && SrokCredit <= 60))
            {
                Console.WriteLine("Cрок кредита от 3 до 60 месяц!!!");
                goto S2;
            }

            return true;
        }
    }
}