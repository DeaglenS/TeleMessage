using System;
using System.Collections.Generic;
namespace Server
{
    /// <summary>
    /// ////////////////////////////////////
    /// 
    /// 
    /// </summary>
    class Chat
    {

        List<Message> messages = new List<Message>();// массив сообщений
        public Bot bot; // массив ботов
        User user; // массив пользователей

        public Chat(User user)
        {
            //  bot = new Bot();
            this.user = user;

        }

        public void addBot(Bot bot)
        {
            this.bot = bot;
            //  this.bots.Add(bot);
        }

        public void work()
        {
            while (true)
            {
                //  Console.WriteLine("Hello World!");
                // string text = " d";
                Message message;
                string text = Console.ReadLine();
                // Console.WriteLine("Hello World!");
                message = user.write(text);
                message = bot.answer(message);
                text = message.write();
                Console.WriteLine(text);
            }
        }

    }
    class Bot
    {
        List<ICommand> commands = new List<ICommand>();

        public DateTime starttime;
        List<Message> messages = new List<Message>();

        public Bot()
        {
            starttime = DateTime.Now;
            Mood mood = new Mood();
            commands.Add(mood);
            Name name = new Name();
            commands.Add(name);
            Clean clean = new Clean();
            commands.Add(clean);
            Un un = new Un();
            commands.Add(un);
            GetTime getTime = new GetTime();
            commands.Add(getTime);
            WorkTime workTime = new WorkTime();
            commands.Add(workTime);
            starttime = DateTime.Now;
        }

        public Message answer(Message message)
        {
            Message new_message = new Message(message.get_text());
            MessageInterpreter messageInterpreter = new MessageInterpreter();
            new_message = messageInterpreter.catchComand(new_message, commands, messages, starttime);
            messages.Add(message);
            messages.Add(new_message);
            return new_message;
        }
    }

    class ICommand
    {
        public string command = "повтори";
        // реализация метода по умолчанию
        public Message work(Message message)
        {
            return message;
            //Console.WriteLine("Walking");
        }
    }
    class Mood : ICommand
    {
        public string command = "Как дела?";
        public Message work(Message message)
        {
            Message new_message = new Message();
            new_message.text = "SUPER GOOD!!";
            return new_message;
            //Console.WriteLine("Человек идет");
        }
    }
    class Name : ICommand
    {
        public string command = "Как тебя зовут?";
        public Message work(Message message)
        {
            Message new_message = new Message();
            new_message.text = "No name...";
            return new_message;
            //Console.WriteLine("Человек идет");
        }
    }
    class Clean : ICommand
    {
        public string command = "Очисти экран";
        public Message work(Message message)
        {
            Console.Clear();
            Message new_message = new Message();
            new_message.text = "It is clean";
            return new_message;
            //Console.WriteLine("Человек идет");
        }
    }

    class Un : ICommand
    {
        public string command = "Выведи три знака вопроса)";
        public Message work(Message message)
        {

            Message new_message = new Message();
            new_message.text = "???";
            return new_message;
            //Console.WriteLine("Человек идет");
        }
    }
    class GetTime : ICommand
    {
        public string command = "Сколько сейчас времени?";
        public Message work(Message message)
        {

            Message new_message = new Message();
            new_message.text = DateTime.Now.ToString("HH:mm:ss");
            return new_message;
            //Console.WriteLine("Человек идет");
        }
    }
    class WorkTime : ICommand
    {
        public string command = "Сколько времени ты работаешь?";
        public Message work(Message message, DateTime starttime)
        {

            //Ваши процедуры
            DateTime endtime = DateTime.Now;
            //Console.WriteLine(endtime - Chat.starttime);

            Message new_message = new Message();
            //endtime = endtime - starttime;
            new_message.text = (endtime - starttime).ToString();
            return new_message;
            //Console.WriteLine("Человек идет");
        }
    }
    class ShowMessages : ICommand
    {
        public string command = "Покажи нашу переписку";
        public Message work(Message message, List<Message> messages)
        {

            //Ваши процедуры
            //DateTime endtime = DateTime.Now;
            //Console.WriteLine(endtime - Chat.starttime);

            Message new_message = new Message();
            message.text = "\n";
            foreach (Message p in messages)
            {
                new_message.text = message.text + p.text + "\n";
            }
            //message.text = DateTime.Now.ToString("HH:mm:ss");
            return new_message;
            //Console.WriteLine("Человек идет");
        }
    }
    class Command
    {
        public Command()
        {

        }

        public Message process(Message message, List<ICommand> commands, List<Message> messages, DateTime starttime)
        {
            Message new_message = new Message();
            int check = 0;


            foreach (ICommand o in commands)
            {

                if (o.command == message.text)
                {
                    //Console.WriteLine("Человек идет");
                    //ICommand a = new ICommand();
                    new_message = o.work(message);
                    return message;
                    check = 1;
                }
            }

            //commands.IndexOf()

            //КОМАНДА a = commands[0];
            /// Каак то реализовать чтобы ифы не надо было писать так


            if (message.text == "Как дела?")
            {
                Mood hello = new Mood();
                hello.work(message);
            }

            else if (message.text == "Как тебя зовут?")
            {
                Name name = new Name();
                name.work(message);
            }
            else if (message.text == "Очисти экран")
            {
                Clean clean = new Clean();
                clean.work(message);
            }
            else if (message.text == "Сколько сейчас времени?")
            {
                GetTime getTime = new GetTime();
                getTime.work(message);
            }
            else if (message.text == "Сколько времени ты работаешь?")
            {
                WorkTime workTime = new WorkTime();
                workTime.work(message, starttime);
            }
            else if (message.text == "Покажи нашу переписку")
            {
                ShowMessages showMessages = new ShowMessages();
                showMessages.work(message, messages);
            }


            Un un = new Un();
            new_message = un.work(message);



            // if()
            //new_message = new Message();
            return new_message;
        }

    }



    class ConcreteCommand
    {
        public ConcreteCommand()
        {

        }

        /*public Message work(Message message)
        {

        }
        */

    }


    class CommandWriteHello : ConcreteCommand
    {
        public Message work(Message message)
        {
            return message;
        }
    }
    class MessageInterpreter
    {
        public MessageInterpreter()
        {

        }
        public Message catchComand(Message message, List<ICommand> commands, List<Message> messages, DateTime starttime)
        {
            Message new_message = new Message();
            Command command = new Command();
            new_message = command.process(message, commands, messages, starttime);
            return new_message;
        }
    }

    class Message
    {
        public string text = " ";
        public Message(string text)
        {
            this.text = text;
        }
        public Message()
        {

        }
        public string write()
        {
            return text;
        }

        public string get_text()
        {
            return text;
        }
    }
    class User
    {
        string name;
        public User(string name = "NoOne")
        {
            this.name = name;
        }

        public Message write(string text)
        {
            Message message = new Message(text);
            return message;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine("Hello World!");
            User user = new User("Ban");
            Bot bot = new Bot();
            Chat chat = new Chat(user);
            chat.addBot(bot);
            chat.work();

            //Chat.bot = new Bot();

        }
    }
    /*
    class UIChat
    {
        Chat chat;
        public UIChat(Chat chat)
        {
            Message[] messages;
            User user;
            Bot bot_first;

            //this.chat(messages, user, bot_first);
        }



    }
    */
}

/*
Задача: реализовать консольного чат бота
Требования:
Бот должен уметь отвечать на простые вопросы/команды:
*сколько сейчас времени?
* как тебя зовут?
* как дела?
* сколько времени ты работаешь?
* покажи нашу переписку?
* очисти экран
Бот должен корректно реагировать на неизвестные команды или вопросы
Бот должен быть разработан с применением ООП
*/