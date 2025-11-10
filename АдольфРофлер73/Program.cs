using System;
using System.Collections.Generic;

namespace RoguelikeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }

    public class Game
    {
        private Player player;
        private int turnCount;
        private Random random;

        public Game()
        {
            player = new Player();
            turnCount = 0;
            random = new Random();
        }

        public void Start()
        {
            Console.WriteLine("Добро пожаловать в текстовую рогалик-игру!");
            while (true)
            {
                turnCount++;
                Console.WriteLine($"\nХод {turnCount}");
                if (turnCount % 10 == 0)
                {
                    EncounterBoss();
                }
                else
                {
                    EncounterEvent();
                }

                if (player.IsAlive == false)
                {
                    Console.WriteLine("Вы погибли. Игра окончена.");
                    break;
                }
            }
        }

        private void EncounterEvent()
        {
            if (random.Next(2) == 0)
            {
                EncounterEnemy();
            }
            else
            {
                OpenChest();
            }
        }

        private void EncounterEnemy()
        {
            Enemy enemy = Enemy.CreateRandomEnemy();
            Console.WriteLine($"Вы встретили {enemy.GetType().Name}!");

            while (enemy.IsAlive && player.IsAlive)
            {
                PlayerTurn(enemy);
                if (enemy.IsAlive)
                {
                    EnemyTurn(enemy);
                }
            }
        }

        private void PlayerTurn(Enemy enemy)
        {
            Console.WriteLine("Ваш ход: (1) Атаковать (2) Защита");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Attack(enemy);
            }
            else if (choice == "2")
            {
                Defend(enemy);
            }
        }

        private void Attack(Enemy enemy)
        {
            int damage = player.Attack();
            enemy.TakeDamage(damage);
            Console.WriteLine($"Вы нанесли {damage} урона {enemy.GetType().Name}. Осталось здоровья: {enemy.Health}");
        }

        private void Defend(Enemy enemy)
        {
            bool dodged = random.Next(100) < 40; // 40% шанс уклонения
            if (dodged)
            {
                Console.WriteLine("Вы успешно уклонились от атаки!");
                return;
            }

            int damage = enemy.Attack();
            damage = Math.Max(damage - player.Defense, 0); // Учитываем защиту игрока
            player.TakeDamage(damage);
            Console.WriteLine($"Вы получили {damage} урона. Осталось здоровья: {player.Health}");
        }

        private void EnemyTurn(Enemy enemy)
        {
            int damage = enemy.Attack();
            player.TakeDamage(damage);
            Console.WriteLine($"Враг атакует и наносит {damage} урона. Осталось здоровья: {player.Health}");
        }

        private void OpenChest()
        {
            Item item = Item.CreateRandomItem();
            Console.WriteLine($"Вы открыли сундук и нашли: {item.Name}!");

            if (item is HealingPotion)
            {
                player.Heal();
                Console.WriteLine("Вы исцелены!");
            }
            else if (item is Weapon weapon)
            {
                EquipItem(weapon);
            }
            else if (item is Armor armor)
            {
                EquipItem(armor);
            }
        }

        private void EquipItem(Weapon weapon)
        {
            Console.WriteLine($"Новое оружие: {weapon.Name}, Урон: {weapon.Damage}"); Console.WriteLine($"Текущее оружие: {player.Weapon?.Name ?? "Нет"}");

            Console.WriteLine("Хотите заменить текущее оружие? (y/n)");
            if (Console.ReadLine().ToLower() == "y")
            {
                player.EquipWeapon(weapon);
                Console.WriteLine("Оружие заменено.");
            }
        }

        private void EquipItem(Armor armor)
        {
            Console.WriteLine($"Новая броня: {armor.Name}, Защита: {armor.Defense}");
            Console.WriteLine($"Текущая броня: {player.Armor?.Name ?? "Нет"}");

            Console.WriteLine("Хотите заменить текущую броню? (y/n)");
            if (Console.ReadLine().ToLower() == "y")
            {
                player.EquipArmor(armor);
                Console.WriteLine("Броня заменена.");
            }
        }

        private void EncounterBoss()
        {
            Boss boss = Boss.CreateRandomBoss();
            Console.WriteLine($"Вы встретили босса: {boss.GetType().Name}!");

            while (boss.IsAlive && player.IsAlive)
            {
                PlayerTurn(boss);
                if (boss.IsAlive)
                {
                    EnemyTurn(boss);
                }
            }
        }
    }

    