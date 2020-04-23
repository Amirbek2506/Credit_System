using System;
using System.Data;
using System.Data.SqlClient;

namespace Credit_System
{
    public static class GraphicPog
    {
        static SqlConnection connection = new SqlConnection(Connect.StrConnection);

        public static void TableGraphicPog()
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
                        string com = $"select LastName,FirstName,SummPay,DatePay,Prosrochka,SummOpl,DateOpl from GraphicPog join Customer on Customer.SeriesPassport = GraphicPog.SeriasPassport";
                        using (SqlCommand command = new SqlCommand(com, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Console.Write($"{reader.GetValue(0).ToString()}\t{reader.GetValue(1).ToString()}\t{reader.GetValue(2).ToString()}\t{reader.GetValue(3).ToString()}\t{reader.GetValue(4).ToString()}\t{reader.GetValue(5).ToString()}\t{reader.GetValue(6).ToString()}");
                                }
                            }
                        }
                        Console.ReadKey();
                        Console.Clear();
                        goto P1;
                    }
                case "2":
                    {
                        Console.Clear();
                        Console.WriteLine("Введите серии паспорта!"); string SerPassport = Console.ReadLine();
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();
                        string com =$"select LastName,FirstName,SummPay,DatePay,Prosrochka,SummOpl,DateOpl from GraphicPog join Customer on Customer.SerPassport = GraphicPog.SerPassport";
                        using (SqlCommand command = new SqlCommand(com, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (SerPassport == reader.GetValue(0).ToString())
                                    {
                                        Console.WriteLine($" ");
                                        Console.WriteLine($"{reader.GetValue(0).ToString()}\t{reader.GetValue(1).ToString()}\t{reader.GetValue(2).ToString()}\t{reader.GetValue(3).ToString()}\t{reader.GetValue(4).ToString()}\t{reader.GetValue(5).ToString()}");

                                    }
                                }
                            }
                        }
                        Console.ReadKey();
                        Console.Clear();
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
        public static void Graphic_Pog()
        {
            Console.Clear();
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string com = $"select * from GraphicPog";
            using (SqlCommand command = new SqlCommand(com, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (Customer.SerPassport == reader.GetValue(0).ToString())
                        {
                            Console.WriteLine($"{reader.GetValue(0).ToString()}\t{reader.GetValue(1).ToString()}\t{reader.GetValue(2).ToString()}\t{reader.GetValue(3).ToString()}\t{reader.GetValue(4).ToString()}\t{reader.GetValue(5).ToString()}");

                        }
                    }
                }
            }
            Console.ReadKey();
            Console.Clear();
        }
    }
}