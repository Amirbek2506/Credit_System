using System;
using System.Data;
using System.Data.SqlClient;

namespace Credit_System
{
    public static class Calculation
    {
        static SqlConnection connection = new SqlConnection(Connect.StrConnection);

        public static bool ConculationZayavok()
        {

            //Количество кредитную историю
            int CCrHistory = 0;
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string com = $"Select * from Credit_History";
            using (SqlCommand command = new SqlCommand(com, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (Customer.SerPassport == reader.GetValue(0).ToString())
                        {
                            if (reader.GetValue(7).ToString()== "1")
                            {   Console.Clear();
                                Console.WriteLine("\tУ вас есть не закритий кредит!!!");
                                Console.ReadKey();
                                return false;
                            }
                            else CCrHistory++;
                            break;
                        }
                    }
                }
            }
            //количество просрочки
            int ColProsrochka = 0;
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string commandStr = $"Select * from Credit_History";
            using (SqlCommand command = new SqlCommand(commandStr, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (Customer.SerPassport == reader.GetValue(0).ToString())
                        {
                            ColProsrochka += int.Parse(reader.GetValue(4).ToString());
                            break;
                        }
                    }
                }
            }
            int Bal = 1;            // Bal=Bal+1 Для срок кредитов
            if (CCrHistory == 0) Bal--;
            else if (CCrHistory == 1 || CCrHistory == 2) Bal++;
            else if (CCrHistory >= 3) Bal += 2;
            if (ColProsrochka == 4) Bal--;
            else if (ColProsrochka >= 5 && ColProsrochka <= 7) Bal -= 2;
            else if (ColProsrochka > 7) Bal -= 3;
            if (Zayavki.SelCredit == "Бытовая техника") Bal += 2;
            else if (Zayavki.SelCredit == "Ремонт") Bal++;
            else if (Zayavki.SelCredit == "Прочее") Bal--;
            if (Customer.Gender == "Жен") Bal += 2;
            else Bal += 1;
            if (Customer.MaritalStatus == "холостой") Bal += 1;
            else if (Customer.MaritalStatus == "семеянин") Bal += 2;
            else if (Customer.MaritalStatus == "вразводе") Bal += 1;
            else if (Customer.MaritalStatus == "вдовец/вдова") Bal += 2;
            int Age = DateTime.Now.Year - Customer.BirthDate.Year;
            if (Age <= 25) Bal += 0;
            if (Age > 25 && Age <= 35) Bal += 1;
            if (Age >= 36 && Age <= 62) Bal += 2;
            if (Age >= 63) Bal += 1;
            if (Customer.Nationality == "Таджикистан") Bal += 1;
            Decimal SummOOD = ((Zayavki.SummCredit * 100) / Zayavki.OpshDokhod);
            if (SummOOD <= 80) Bal += 4;
            else if (SummOOD > 80 && SummOOD <= 150) Bal += 3;
            else if (SummOOD > 150 && SummOOD <= 250) Bal += 2;
            else if (SummOOD > 250) Bal += 1;
            if (Bal > 11) return true;
            else return false;
        }
    }
}