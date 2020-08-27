using System;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace Ponovlenya
{
    class Program
    {
        static void DoCommand(SqlCommand command)
        {
            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Команда выполнено успешно");
            }
            catch
            {
                Console.WriteLine("Вы ввели некоректные данные. попробуйте еще раз.");
            }
        }
        static void Main(string[] args)
        {
            SqlConnection connection = SQL.GetInstance();
            CommandMaker maker = new CommandMaker();
            SqlCommand command = new SqlCommand() ;

            while (true)
            {
                Console.WriteLine("Выберите таблицу с которой будете работать\nHuman - 1\nMail - 2\n" +
                    "Give - 3\nЗакончить работу с таблицами - другое");
                string choise = Console.ReadLine();
                string table;
                if (choise == "1") table = "Human";
                else if (choise == "2") table = "Mail";
                else if (choise == "3") table = "Give";
                else break;
                while (true)
                {
                    Console.WriteLine("Какую команду используем\nInsert - 1\nUpdate - 2\n" +
                        "Delete - 3\nЗакончить работу с данной таблицей - другое");
                    choise = Console.ReadLine();
                    if (choise == "1")
                    {
                        if (table == "Human") command = maker.GetCommand(new InsertHuman(table));
                        else if (table == "Mail") command = maker.GetCommand(new InsertMail(table));
                        else if (table == "Give") command = maker.GetCommand(new InsertGive(table));
                    }
                    else if (choise == "2")
                    {
                        if (table == "Human") command = maker.GetCommand(new UpdateHuman(table));
                        else if (table == "Mail") command = maker.GetCommand(new UpdateMail(table));
                        else if (table == "Give") command = maker.GetCommand(new UpdateGive(table));
                    }
                    else if (choise == "3")
                    {
                        command = maker.GetCommand(new Delete(table));
                    }
                    else break;
                    command.Connection = connection;
                    DoCommand(command);
                }
            }


            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();

            adapter.SelectCommand = new SqlCommand("Select Human.Name, Human.Birth from Human " +
                "join Mail on Human.ID = Mail.Send" +
                "where length(mail.text) = (Select min(length(mail.text)) from mail)", connection);

            adapter.Fill(ds, "1");

            adapter.SelectCommand = new SqlCommand("Select Human.Name, Human.Birth, count(Mail.Send) as Send, count(Give.Give) as Give from Human " +
                "join Mail on Human.ID = Mail.Send" +
                "join Give on Human.ID = Give.Give" +
                "group by Human.ID", connection);

            adapter.Fill(ds, "2");

            DataSet CloseSet = new DataSet();

            adapter.SelectCommand = new SqlCommand("Select * From Human", connection);
            adapter.Fill(CloseSet, "Human");
            adapter.SelectCommand = new SqlCommand("Select * From Mail", connection);
            adapter.Fill(CloseSet, "Mail");
            adapter.SelectCommand = new SqlCommand("Select * From Give", connection);
            adapter.Fill(CloseSet, "Give");

            connection.Close();


            foreach (DataTable dt in ds.Tables)
            {
                Console.WriteLine(dt.TableName);
                foreach (DataColumn column in dt.Columns)
                    Console.Write("\t\t\t{0}", column.ColumnName);
                Console.WriteLine();
                foreach (DataRow row in dt.Rows)
                {
                    var cells = row.ItemArray;
                    foreach (object cell in cells)
                        Console.Write("\t\t\t{0}", cell);
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Введите тему");
            string theme = Console.ReadLine();

            var Three = (from Hum in CloseSet.Tables["Human"].AsEnumerable()
                        join Give in CloseSet.Tables["Give"].AsEnumerable() on Hum.Field<int>("ID") equals Give.Field<int>("Give")
                        join Mail in CloseSet.Tables["Mail"].AsEnumerable() on Give.Field<int>("Mail") equals Mail.Field<int>("ID")
                        where Mail.Field<string>("Theme") == theme
                        select new { name = Hum.Field<string>("Name"), second = Hum.Field<string>("Second") }).Distinct();

            Console.WriteLine("Получатели заданной темы");
            foreach (var item in Three)
            {
                Console.WriteLine("{0} {1}", item.name, item.second);
            }

        }
    }
}
