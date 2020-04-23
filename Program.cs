using System;
using System.Data.SqlClient;
namespace Credit_System
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.Write("\t\tДобро пожаловать!\n");
        start: System.Console.Write("Выберите команда\n1.Админ\n2.Клиент\nВаш роль: ");
            switch (Console.ReadLine())
            {
                case "1":
                    {
                    FindAd: if (Admin.FindAdmin())
                        {
                        Table: Console.Clear();
                            Console.Write("1.Посмотреть список слиента\n2.Добавит админ\n3.Посмотреть список зайавок\n4.Посмотреть список кредитной истории\n5.Посмотреть график погощении\n6.Назад\nВыберите команду: ");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    {
                                        Customer.TableCustomer();
                                        goto Table;
                                    }
                                case "2":
                                    {
                                        Admin.AddAdmin();
                                        goto Table;
                                    }
                                case "3":
                                    {
                                        Zayavki.TableZayavki();
                                        goto Table;
                                    }
                                case "4":
                                    {
                                        Credit_History.TableCreditHistory();
                                        goto Table;
                                    }
                                case "5":
                                    {
                                        GraphicPog.TableGraphicPog();
                                        goto Table;
                                    }
                                case "6":
                                    {
                                        goto FindAd;
                                    }
                                default:
                                    {
                                        System.Console.WriteLine("\tНеверная команда!");
                                        goto Table;
                                    }
                            }
                        }
                        Console.Clear();
                        Console.WriteLine("Логин или пароль не совпадает!");
                        Console.ReadKey();
                        goto start;
                    }
                case "2":
                    {
                        Console.Clear();
                    main: Console.WriteLine("1.Вход\n2.Регистрация\n3.Назад\nВыберите команду: ");
                        switch (Console.ReadLine())
                        {
                            case "2":
                                {
                                    if (Customer.Registration())
                                    {
                                        goto main;
                                    }
                                    else
                                    {
                                        Console.ReadKey();
                                        Console.Clear();
                                        goto main;
                                    }
                                }

                            case "1":
                                {
                                FindCust: if (Customer.FindCustomer())
                                    {
                                        Console.Clear();
                                        Console.WriteLine($"Добро пожаловать {Customer.FirstName} {Customer.LastName}");
                                    Table: Console.Write("\tВыберите действия\n1.Оставит заявку на кредит\n2.Посмотреть история заявок\n3.Посмотреть личние данных\n4.Посмотреть кредитную историю\n5.Посмотреть график погощенност\n6.Отплатит\n7.Изменит личние данных\n8.Назад\nВыберите команду: ");
                                        switch (Console.ReadLine())
                                        {
                                            case "1":
                                                {
                                                    if (Zayavki.AddZayavka())
                                                    {
                                                        Console.Clear();
                                                        System.Console.WriteLine("\tВаша зайавка успешно отправлен!");
                                                        Console.ReadKey();
                                                        Console.Clear();
                                                    }
                                                    goto Table;
                                                }
                                            case "2":
                                                {
                                                    Admin.AddAdmin();
                                                    goto Table;
                                                }
                                            case "3":
                                                {
                                                    Zayavki.TableZayavki();
                                                    goto Table;
                                                }
                                            case "4":
                                                {
                                                    Credit_History.TableCreditHistory();
                                                    goto Table;
                                                }
                                            case "5":
                                                {
                                                    GraphicPog.TableGraphicPog();
                                                    goto Table;
                                                }
                                            case "6":
                                                {
                                                    goto FindCust;
                                                }
                                            case "7":
                                                {
                                                    goto FindCust;
                                                }
                                            case "8":
                                                {
                                                    goto FindCust;
                                                }
                                            default:
                                                {
                                                    System.Console.WriteLine("\tНеверная команда!");
                                                    goto Table;
                                                }
                                        }
                                    }

                                    goto FindCust;
                                }

                            default:
                                {
                                    Console.Clear();
                                    Console.WriteLine("Неверная команда!");
                                    goto start;
                                }
                        }
                    }
            }
        }
    }
}
