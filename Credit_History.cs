using System;
using System.Data;
using System.Data.SqlClient;

namespace Credit_System
{
    public static class Credit_History
    {
        static SqlConnection connection = new SqlConnection(Connect.StrConnection);
        public static void AddCreditHistory()
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string comm = $"Insert Into Credit_History([SeriesPassport],[SelCredit],[SummCredit],[SrokCredit],[Prosrochka],[BeginDate],[EndDate],[Status],[OstatokCredit]) Values ('{Customer.SerPassport}','{Zayavki.SelCredit}',{Zayavki.SummCredit},{Zayavki.SrokCredit},{Zayavki.Prosrochka},'{Zayavki.DataZayavk}',null,{Zayavki.StatusCredit},{Zayavki.OstatokCredit})";
            SqlCommand commandI = new SqlCommand(comm, connection);
            commandI.ExecuteNonQuery();
        }
        public static void TableCreditHistory()
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
                        string com = $"select LastName,FirstName,SelCredit,SummCredit,SrokCredit,Prosrochka,BeginDate,EndDate,Status,OstatokCredit from Credit_History join Customer on Customer.SeriesPassport = Credit_History.SeriesPassport";
                        using (SqlCommand command = new SqlCommand(com, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine($"{reader.GetValue(0).ToString()}\t{reader.GetValue(1).ToString()}\t{reader.GetValue(2).ToString()}\t{reader.GetValue(3).ToString()}\t{reader.GetValue(4).ToString()}\t{reader.GetValue(5).ToString()}\t{reader.GetValue(6).ToString()}\t{reader.GetValue(7).ToString()}\t{reader.GetValue(8).ToString()}\t{reader.GetValue(9).ToString()}");
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
                        Console.WriteLine("Введите серии паспорта"); string SerPassport = Console.ReadLine();
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();
                        string com = $"select LastName,FirstName,SelCredit,SummCredit,SrokCredit,Prosrochka,BeginDate,EndDate,Status,OstatokCredit from Credit_History join Customer on Customer.SeriesPassport = Credit_History.SeriesPassport Where Credit_History.SeriesPassport={SerPassport}";
                        using (SqlCommand command = new SqlCommand(com, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (SerPassport == reader.GetValue(0).ToString())
                                    {
                                        Console.WriteLine($"{reader.GetValue(0).ToString()}\t{reader.GetValue(1).ToString()}\t{reader.GetValue(2).ToString()}\t{reader.GetValue(3).ToString()}\t{reader.GetValue(4).ToString()}\t{reader.GetValue(5).ToString()}\t{reader.GetValue(6).ToString()}\t{reader.GetValue(7).ToString()}\t{reader.GetValue(8).ToString()}\t{reader.GetValue(9).ToString()}");
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
                        Console.ReadKey();
                        Console.Clear();
                        goto P1;
                    }
            }
        }
        public static void CreditHistory()
        {
            Console.Clear();
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string com = $"select LastName,FirstName,SelCredit,SummCredit,SrokCredit,Prosrochka,BeginDate,EndDate,Status,OstatokCredit from Credit_History join Customer on Customer.SeriesPassport = Credit_History.SeriesPassport Where Credit_History.SeriesPassport={Customer.SerPassport}";
            using (SqlCommand command = new SqlCommand(com, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (Customer.SerPassport == reader.GetValue(0).ToString())
                        {
                            Console.WriteLine($"{reader.GetValue(0).ToString()}\t{reader.GetValue(1).ToString()}\t{reader.GetValue(2).ToString()}\t{reader.GetValue(3).ToString()}\t{reader.GetValue(4).ToString()}\t{reader.GetValue(5).ToString()}\t{reader.GetValue(6).ToString()}\t{reader.GetValue(7).ToString()}\t{reader.GetValue(8).ToString()}\t{reader.GetValue(9).ToString()}");
                        }
                    }
                }
            }
            Console.ReadKey();

        }

    }
}