using MarketApp.DataAccess;
using MarketApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Serveces
{
    public class BuyingProcess
    {
        public static void Input()
        {
            Console.WriteLine("Добро пожаловать в магазин, для совершения покупок Вам необходимо войти в систему.\n" +
                "Для входа в систему введите email:");
                string email=Console.ReadLine();
            Console.WriteLine("Введите пароль:");
                string password=Console.ReadLine();
            Autorization(email, password);
        }

        static void Autorization(string email, string password)
        {
            User user = new User();
            using (var context = new MarketContext())
            {
                bool isEntry = false;
                foreach (var u in context.Users.ToList())
                {
                    if (u.Email==email)
                    {
                        isEntry = true;
                        break;
                    }
                }

                if (isEntry==false)
                {
                    Console.WriteLine("Этой учетной записи в системе нет. \n" +
                        "Хотите зарегистрироваться?\n1.- да\n2.- нет");
                    int choice = 0;
                    while (choice==0)
                    {
                        int.TryParse(Console.ReadLine(), out choice);
                        if (choice <= 0 || choice > 2) choice = 0;
                        else break;
                    }

                    switch (choice)
                    {
                        case 1:
                            Registration(ref user, email, password);
                            Console.WriteLine("Добро пожаловать в систему!");
                            Purchase(user);

                            break;
                        case 2:
                            Console.WriteLine("До свидания!");
                            break;
                    }
                }

                else
                {
                    user = context.Users.FirstOrDefault(u => u.Email == email);
                    if (user.Password==password)
                    {
                        Console.WriteLine("Добро пожаловать в систему!");
                    }
                    else
                    {
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Не верно введен пароль попробуйте снова: ");

                            password = Console.ReadLine();
                            if (user.Password == password)
                            {
                                Console.WriteLine("Добро пожаловать в систему!");
                                break;
                            }
                        }

                    }
                        Purchase(user);
                }

            }
            
        }

        static void Registration(ref User user, string email, string password)
        {
            using (var context=new MarketContext())
            {
                user = new User { Email = email, Password = password };
                context.Users.Add(user);                   
                context.SaveChanges();

                user = context.Users.FirstOrDefault(u => u.Email == email);
            }
        }

        static void ShowAssortimentsList()
        {
            using (var context=new MarketContext())
            {
                    Console.WriteLine("Ассортимент:");
                foreach (var p in context.Markets.ToList())
                {
                    Console.WriteLine($"{p.Id}. {p.Product}: цена - {p.Price}, колличество - {p.Quantity}");
                }
            }
        }

        static void Purchase(User user)
        {
            Console.Clear();
            ShowAssortimentsList();
            Console.WriteLine("*********************************\n" +
                "Добавьте в корзину товар для пoкупки по его номеру: ");
            int choice = 0;

            Market market = new Market();

            using (var context=new MarketContext())
            {
                while (choice==0)
                {
                    int.TryParse(Console.ReadLine(), out choice);
                    if (choice < 0 || choice > context.Markets.Count())
                    {
                        choice = 0;
                    }
                    else
                    {
                        market = context.Markets.FirstOrDefault(m => m.Id == choice);
                        break;
                    }
                }

               
                Basket basket = new Basket
                {
                    UserId=user.Id,
                    User=user,
                    MarketId= market.Id,
                    Market=market
                };

                context.Baskets.Add(basket);
                context.SaveChanges();

                List<Basket> baskets = new List<Basket>();

                Console.WriteLine("Просмотр корзины: ");
                foreach (var b in context.Baskets.ToList())
                {
                    Console.WriteLine($"Пользователь: {b.User.Email}," +
                        $" Товар: {b.Market.Product}," +
                        $" Цена: {b.Market.Price}," +
                        $" Вес(кг.): {b.Market.Quantity},");
                    baskets.Add(b);
                }

                foreach (var b in baskets)
                {
                    if (b != null)
                    {
                        context.Baskets.Remove(b);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
