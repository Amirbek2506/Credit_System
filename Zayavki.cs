using System;
using System.Data;
using System.Data.SqlClient;

namespace Credit_System
{
    public static class Zayavki
    {
        public static string SerPassport;
        public static int SummCredit;
        public static int SummOtpl = 0;
        public static DateTime DataZayavk;
        public static DateTime DataOtpl;
        public static int StatusCredit = 0;
        public static decimal OstatokCredit;
        public static int OpshDokhod;
        public static int Prosrochka = 0;
        public static string SelCredit;
        public static int SrokCredit;
        static SqlConnection connection = new SqlConnection(Connect.StrConnection);
        public static void TableZayavki()
        {
            Console.Clear();
        P1: Console.WriteLine("1.Посмотреть все\n2.Посмотреть по серии паспорта\n3.Назад\nВыберите команду: ");
            switch (Console.ReadLine())
            {
                case "1":
                    {
                        Console.Clear();
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();
                        string com = $"select LastName,FirstName,SelCredit,OpshSumm,OpshDokhod,Srok,Status,Data from Zayavki join Customer on Customer.SeriesPassport = Zayavki.SeriesPassport";
                        using (SqlCommand command = new SqlCommand(com, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine($"{reader.GetValue(0).ToString()}\t{reader.GetValue(1).ToString()}\t{reader.GetValue(2).ToString()}\t{reader.GetValue(3).ToString()}\t{reader.GetValue(4).ToString()}\t{reader.GetValue(5).ToString()}\t{reader.GetValue(6).ToString()}\t{reader.GetValue(7).ToString()}");
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
                        Console.WriteLine("Введите серии паспорта");
                        SerPassport = Console.ReadLine();
                        Console.Clear();
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();
                        string com = $"select LastName,FirstName,SelCredit,OpshSumm,OpshDokhod,Srok,Status,Data from Zayavki join Customer on Customer.SeriesPassport = Zayavki.SeriesPassport Where GraphicPog.SeriesPassport='{SerPassport}'";
                        using (SqlCommand command = new SqlCommand(com, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (SerPassport == reader.GetValue(0).ToString())
                                    {
                                        Console.WriteLine($"{reader.GetValue(0).ToString()}\t{reader.GetValue(1).ToString()}\t{reader.GetValue(2).ToString()}\t{reader.GetValue(3).ToString()}\t{reader.GetValue(4).ToString()}\t{reader.GetValue(5).ToString()}\t{reader.GetValue(6).ToString()}\t{reader.GetValue(7).ToString()}");
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
            Console.Write("Сумма кредита: ");
            SummCredit = int.Parse(Console.ReadLine());
            Console.Write("Общий доход: "); OpshDokhod = int.Parse(Console.ReadLine());
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
            Console.Clear();
        S2: Console.Write("Срок кредита(на месяц): "); SrokCredit = int.Parse(Console.ReadLine());
            if (!(SrokCredit > 3 && SrokCredit <= 60))
            {
                Console.WriteLine("Cрок кредита от 3 до 60 месяц!!!");
                goto S2;
            }
            DataZayavk = DateTime.Now;
            if (Calculation.ConculationZayavok())
            {
                OstatokCredit = SummCredit - SummOtpl;
                StatusCredit = 1;
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                string com = $"insert into Zayavki([SeriesPassport],[SelCredit],[OpshSumm],[OpshDokhod],[Srok],[Status],[Data]) Values ('{Customer.SerPassport}','{SelCredit}',{SummCredit},{OpshDokhod},{SrokCredit},{StatusCredit},'{DateTime.Now}')";
                SqlCommand command = new SqlCommand(com, connection);
                command.ExecuteNonQuery();
                Console.Clear();
                Console.WriteLine("Вы можете взять кредит\nДля оформления нажмите 1");
                if (Console.ReadLine() == "1")
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    for (int i = 0; i < SrokCredit; i++)
                    {
                        string comm = $"Insert Into GraphicPog([SeriesPassport],[SummPay],[DatePay],[Prosrochka],[SummOpl],[DateOpl]) Values ('{Customer.SerPassport}','{Math.Round(((SummCredit + (SummCredit * 0.2)) / SrokCredit), 0)}','{DataZayavk}','{Prosrochka}',{SummOtpl},null)";
                        SqlCommand commandI = new SqlCommand(comm, connection);
                        commandI.ExecuteNonQuery();
                        DataZayavk = DataZayavk.AddMonths(1);
                    }
                    Credit_History.AddCreditHistory();
                    Console.Clear();
                    Console.WriteLine($"Вы взяли кредит в размере {SummCredit} сомони на срок {SrokCredit} месяцев.\n\tУ вас ест график погашения!");
                    Console.ReadKey();
                    Console.Clear();
                }

            }
            else
            {
                Console.WriteLine("Вы не можите взять кредит");
                Console.ReadKey();
                Console.Clear();
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                string com = $"Insert Into Zayavki([SeriesPassport],[SelCredit],[OpshSumm],[OpshDokhod],[Srok],[Status],[Data]) Values ('{Customer.SerPassport}','{SelCredit}',{SummCredit},{OpshDokhod},{SrokCredit},{Zayavki.StatusCredit},'{DateTime.Now}')";
                SqlCommand command = new SqlCommand(com, connection);
                command.ExecuteNonQuery();
            }
            return true;
        }
        public static void HistoryZayavk()
        {
            Console.Clear();
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string com = $"select LastName,FirstName,SelCredit,OpshSumm,OpshDokhod,Srok,Status,Data from Zayavki join Customer on Customer.SeriesPassport = Zayavki.SeriesPassport Where Zayavki.SeriesPassport='{Customer.SerPassport}'";
            using (SqlCommand command = new SqlCommand(com, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                            Console.WriteLine($"{reader.GetValue(0).ToString()}\t{reader.GetValue(1).ToString()}\t{reader.GetValue(2).ToString()}\t{reader.GetValue(3).ToString()}\t{reader.GetValue(4).ToString()}\t{reader.GetValue(5).ToString()}\t{reader.GetValue(6).ToString()}\t{reader.GetValue(7).ToString()}");
                    }
                }
            }
            Console.ReadKey();
        }
    }
}