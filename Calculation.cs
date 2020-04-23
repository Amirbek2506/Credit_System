using System;

namespace Credit_System
{
    public static class Calculation
    {
        public static bool ConculationZayavok()
        {
            int Bal=0;
           if(Customer.Gender=="Жен")Bal+=2;
           else Bal+=1;
           if(Customer.MaritalStatus=="холостой")Bal+=1;
           else if(Customer.MaritalStatus=="семеянин")Bal+=2;
           else if(Customer.MaritalStatus=="вразводе")Bal+=1;
           else if(Customer.MaritalStatus=="вдовец/вдова")Bal+=2;
           int Age=DateTime.Now.Year-Customer.BirthDate.Year;
           if(Age<=25)Bal+=0;
           if(Age>25 && Age<=35)Bal+=1;
           if(Age>=36 && Age<=62)Bal+=2;
           if(Age>=63)Bal+=1;
           if(Customer.Nationality =="Таджикистан")Bal+=1;
           Decimal SummOOD=(Zayavki.SummCredit*100)/Zayavki.OpshDokhod;
           if(SummOOD<=80)Bal+=4;
           else if(SummOOD>80 && SummOOD<=150)Bal+=3;
           else if(SummOOD>150 && SummOOD<=250)Bal+=2;
           else if(SummOOD>250)Bal+=1;
           

            return true;
        }
    }
}