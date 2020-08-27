using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Ponovlenya
{
    class CommandMaker
    {
        public SqlCommand GetCommand(Command cmd)
        {
            SqlCommand command = new SqlCommand();
            cmd.Step1(command);
            cmd.Step2();
            cmd.Step3(command);
            cmd.Step4(command);
            return command;
        }

    }

    class Command
    {
        public Command(string table) { this.table = table; }
        protected string table;
        protected string id;
        virtual public void Step1(SqlCommand command) { }
        virtual public void Step2()
        {
            Console.WriteLine("Введите ID записи");
            id = Console.ReadLine();
        }
        virtual public void Step3(SqlCommand command) { }
        virtual public void Step4(SqlCommand command)
        {
            command.CommandText += "Where ID = @id";
            SqlParameter ID = new SqlParameter("@id", id);
            command.Parameters.Add(ID);
        }
    }
    class Insert : Command
    {
        public Insert(string table) : base(table) { }
        override public void Step1(SqlCommand command)
        {
            command.CommandText = "INSERT INTO " + table + " VALUES ";
        }
        override public void Step2() { }
        override public void Step4(SqlCommand command) { }
    }
    class Update : Command
    {
        public Update(string table) : base(table) { }
        override public void Step1(SqlCommand command)
        {
            command.CommandText = "UPDATE " + table + " SET ";
        }
        override public void Step3(SqlCommand command)
        {
        }
    }
    class Delete : Command
    {
        public Delete(string table) : base(table) { }
        override public void Step1(SqlCommand command)
        {
            command.CommandText = "DELETE FROM " + table + " ";
        }
    }

    class InsertHuman : Insert
    {
        public InsertHuman(string table) : base(table) { }
        override public void Step3(SqlCommand command)
        {
            command.CommandText += "(@name, @second, @middle, @date)";
            Console.WriteLine("Введите имя");
            SqlParameter name = new SqlParameter("@name", Console.ReadLine());
            Console.WriteLine("Введите фамилию");
            SqlParameter second = new SqlParameter("@second", Console.ReadLine());
            Console.WriteLine("Введите отчество");
            SqlParameter middle = new SqlParameter("@middle", Console.ReadLine());
            Console.WriteLine("Введите дату рождения в формате ГГГГ-ММ-ДД");
            SqlParameter date = new SqlParameter("@date", Console.ReadLine());
            command.Parameters.Add(name);
            command.Parameters.Add(second);
            command.Parameters.Add(middle);
            command.Parameters.Add(date);
        }
    }
    class UpdateHuman : Update
    {
        public UpdateHuman(string table) : base(table) { }
        override public void Step3(SqlCommand command)
        {
            command.CommandText += "Name = @name, SecondName = @second, MiddleName = @middle, Birth = @date ";
            Console.WriteLine("Введите имя");
            SqlParameter name = new SqlParameter("@name", Console.ReadLine());
            Console.WriteLine("Введите фамилию");
            SqlParameter second = new SqlParameter("@second", Console.ReadLine());
            Console.WriteLine("Введите отчество");
            SqlParameter middle = new SqlParameter("@middle", Console.ReadLine());
            Console.WriteLine("Введите дату рождения в формате ГГГГ-ММ-ДД");
            SqlParameter date = new SqlParameter("@date", Console.ReadLine());
            command.Parameters.Add(name);
            command.Parameters.Add(second);
            command.Parameters.Add(middle);
            command.Parameters.Add(date);
        }
    }
    class InsertMail : Insert
    {
        public InsertMail(string table) : base(table) { }
        override public void Step3(SqlCommand command)
        {
            command.CommandText += "(@send, @theme, @text, @date)";
            Console.WriteLine("Введите Отправителя");
            SqlParameter send = new SqlParameter("@send", Console.ReadLine());
            Console.WriteLine("Введите Тему");
            SqlParameter theme = new SqlParameter("@theme", Console.ReadLine());
            Console.WriteLine("Введите Текст");
            SqlParameter text = new SqlParameter("@text", Console.ReadLine());
            Console.WriteLine("Введите дату отправки в формате ГГГГ-ММ-ДД чч:мм:сс");
            SqlParameter date = new SqlParameter("@date", Console.ReadLine());
            command.Parameters.Add(send);
            command.Parameters.Add(theme);
            command.Parameters.Add(text);
            command.Parameters.Add(date);
        }
    }
    class UpdateMail : Update
    {
        public UpdateMail(string table) : base(table) { }
        override public void Step3(SqlCommand command)
        {
            command.CommandText += "Send = @send, Theme = @theme, Text = @text, Date = @date ";
            Console.WriteLine("Введите отправителя");
            SqlParameter send = new SqlParameter("@send", Console.ReadLine());
            Console.WriteLine("Введите тему");
            SqlParameter theme = new SqlParameter("@theme", Console.ReadLine());
            Console.WriteLine("Введите текст");
            SqlParameter text = new SqlParameter("@text", Console.ReadLine());
            Console.WriteLine("Введите дату отправки в формате ГГГГ-ММ-ДД чч:мм:сс");
            SqlParameter date = new SqlParameter("@date", Console.ReadLine());
            command.Parameters.Add(send);
            command.Parameters.Add(theme);
            command.Parameters.Add(text);
            command.Parameters.Add(date);
        }
    }

    class InsertGive : Insert
    {
        public InsertGive(string table) : base(table) { }
        override public void Step3(SqlCommand command)
        {
            command.CommandText += "(@give, @mail)";
            Console.WriteLine("Введите Получателя");
            SqlParameter give = new SqlParameter("@give", Console.ReadLine());
            Console.WriteLine("Введите Письмо");
            SqlParameter mail = new SqlParameter("@mail", Console.ReadLine());
            command.Parameters.Add(give);
            command.Parameters.Add(mail);
        }
    }
    class UpdateGive : Update
    {
        public UpdateGive(string table) : base(table) { }
        override public void Step3(SqlCommand command)
        {
            command.CommandText += "Give = @give, Mail = @mail)";
            Console.WriteLine("Введите Получателя");
            SqlParameter give = new SqlParameter("@give", Console.ReadLine());
            Console.WriteLine("Введите Письмо");
            SqlParameter mail = new SqlParameter("@mail", Console.ReadLine());
            command.Parameters.Add(give);
            command.Parameters.Add(mail);
        }
    }
}
