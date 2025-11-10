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

    public class Player
    {
        public int Health { get; private set; } = 100;
        public Weapon Weapon { get; private set; }
        public Armor Armor { get; private set; }

        public bool IsAlive => Health > 0;

        public int Defense => Armor?.Defense ?? 0;

        public void EquipWeapon(Weapon weapon)
        {
            Weapon = weapon;
        }

        public void EquipArmor(Armor armor)
        {
            Armor = armor;
        }

        public int Attack()
        {
            return Weapon?.Damage ?? 10; // Базовый урон, если нет оружия
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public void Heal()
        {
            Health = 100; // Полное исцеление
        }
    }

    public abstract class Enemy
    {
        public int Health { get; protected set; }
        public abstract bool IsAlive { get; }

        public abstract int Attack();

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public static Enemy CreateRandomEnemy()
        {
            Random random = new Random();
            int type = random.Next(3); // 0 - Гоблин, 1 - Скелет, 2 - Маг

            switch (type)
            {
                case 0: return new Goblin();
                case 1: return new Skeleton();
                case 2: return new Mage();
                default: throw new Exception("Unknown enemy type");
            }
        }
    }

    public class Goblin : Enemy
    {
        private static Random random = new Random();

        public Goblin()
        {
            Health = 30;
        }

        public override bool IsAlive => Health > 0;

        public override int Attack()
        {
            int baseDamage = 5;
            if (random.Next(100) < 20) // 20% шанс критического удара
            {
                baseDamage *= 2;
                Console.WriteLine("Гоблин наносит критический удар!");
            }

            return baseDamage;
        }
    }

    public class Skeleton : Enemy
    {
        public Skeleton()
        {
            Health = 40;
        }

        public override bool IsAlive => Health > 0;

        public override int Attack()
        {
            return 7; // Игнорирует защиту игрока
        }
    }

    public class Mage : Enemy
    {
        private static Random random = new Random();

        public Mage()
        {
            Health = 25;
        }

        public override bool IsAlive => Health > 0;

        public override int Attack()
        {
            int baseDamage = 6;

            if (random.Next(100) < 30) // 30% шанс заморозки
            {
                Console.WriteLine("Маг накладывает заморозку на игрока! Вы пропустите следующий ход."); return baseDamage;
            }

            return baseDamage;
        }
    }

    public abstract class Boss : Enemy
    {
        public static Boss CreateRandomBoss()
        {
            Random random = new Random();

            int type = random.Next(2); // 0 - ВВГ, 1 - Ковальский

            switch (type)
            {
                case 0: return new VVG();
                case 1: return new Kovalskiy();
                default: throw new Exception("Unknown boss type");
            }
        }
    }

    public class VVG : Boss
    {
        public VVG()
        {
            Health = (int)(30 * 2.0); // Здоровье ×2 от базового гоблина
        }

        public override bool IsAlive => Health > 0;

        public override int Attack()
        {
            return (int)(5 * 1.5); // Атака ×1.5
        }
    }

    public class Kovalskiy : Boss
    {
        public Kovalskiy()
        {
            Health = (int)(40 * 2.5); // Здоровье ×2.5 от базового скелета
        }

        public override bool IsAlive => Health > 0;

        public override int Attack()
        {
            return (int)(7 * 1.3); // Атака ×1.3, игнорирует защиту игрока
        }
    }

    public abstract class Item
    {
        public string Name { get; protected set; }

        public static Item CreateRandomItem()
        {
            Random random = new Random();

            int type = random.Next(3); // 0 - Лечебное зелье, 1 - Оружие, 2 - Броня

            switch (type)
            {
                case 0: return new HealingPotion();
                case 1: return new Weapon("Меч", 10);
                case 2: return new Armor("Кожаная броня", 5);
                default: throw new Exception("Unknown item type");
            }
        }
    }

    public class HealingPotion : Item
    {
        public HealingPotion()
        {
            Name = "Лечебное зелье";
        }
    }

    public class Weapon : Item
    {
        public int Damage { get; private set; }

        public Weapon(string name, int damage)
        {
            Name = name;
            Damage = damage;
        }
    }

    public class Armor : Item
    {
        public int Defense { get; private set; }

        public Armor(string name, int defense)
        {
            Name = name;
            Defense = defense;
        }
    }
}
