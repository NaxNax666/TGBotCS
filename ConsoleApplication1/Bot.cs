using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleApplication1
{
    /// <summary>
    /// Основной модуль бота
    /// </summary>
    public class Bot
    {
        /// <summary>
        /// Клиент Telegram
        /// </summary>
        private TelegramBotClient client;

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Bot()
        {
            // Создание клиента для Telegram
            client = new TelegramBotClient("....");
            client.OnMessage += MessageProcessor;
        }

        /// <summary>
        /// Обработка входящего сообщения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageProcessor(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            switch (e.Message.Type)
            {
                

                case Telegram.Bot.Types.Enums.MessageType.Text: // текстовое сообщение
                    if (e.Message.Text.Substring(0, 1) == "/")
                    {
                        CommandProcessor(e.Message);
                    }
                    else
                    {
                        client.SendTextMessageAsync(e.Message.Chat.Id, $"Ты сказал мне: {e.Message.Text}");
                        Console.WriteLine(e.Message.Text);
                    }
                    break;
                case MessageType.Sticker:
                    if (e.Message.Sticker.IsAnimated)
                    {
                        client.SendTextMessageAsync(e.Message.Chat.Id, text: "ну ты и анимечник епта ");    
                    }
                    else
                    {
                        client.SendTextMessageAsync(e.Message.Chat.Id, text: "ну ты и лох статичный епта ");
                    }
                    break;

                default:
                    client.SendTextMessageAsync(e.Message.Chat.Id, $"Ты прислал мне {e.Message.Type}, но я это пока не понимаю");
                    Console.WriteLine(e.Message.Type);
                    break;
            }
        }

        /// <summary>
        /// Обработка команды
        /// </summary>
        /// <param name="message"></param>
        private void CommandProcessor(Telegram.Bot.Types.Message message)
        {
            // Отрезаем первый символ (который должен быть '/')
            string command = message.Text.Substring(1).ToLower();

            switch (command)
            {
                case "start":
                    client.SendTextMessageAsync(message.Chat.Id, $"Мать ебал {message.Chat.FirstName} \r\n Этот Бот может:\r\n Ебать мать \r\n говорить что ты сказал\r\n по /randpic кинет картинку \r\n назовет тебя анимешником за стикосы \r\n А ТЫ И ДАЛЬШЕ ЖДИ БОТА ДЛЯ NoFAPa.");
                    break;
                case "randpic":
                    
                    client.SendPhotoAsync(message.Chat.Id, "https://www.nasa.gov/sites/default/files/styles/full_width_feature/public/thumbnails/image/iss063e053998.jpg");
                    break;

                default:
                    client.SendTextMessageAsync(message.Chat.Id, $"Я пока не понимаю команду {command}");
                    break;
            }
        }

        /// <summary>
        /// Запуск бота
        /// </summary>
        public void Run()
        {
            // Запуск приема сообщений
            client.StartReceiving();
        }
    }
}