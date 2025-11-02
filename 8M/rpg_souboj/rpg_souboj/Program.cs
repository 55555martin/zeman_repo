using System.Runtime.InteropServices.Marshalling;

namespace rpg_souboj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Army[] armies = new Army[2] { new Army(CreateArmy()), new Army(CreateArmy()) };
            LinkArmyEnemies(armies[0], armies[1]);
        }

        public static List<Entity> CreateArmy()
        {
            Random rnd = new Random();
            List<Entity> army = new List<Entity>();
            int n = rnd.Next(1, 4);
            for (int i = 0; i < n; i++)
            {
                army.Add(new Wizard(i.ToString()));
            }
            Random rnd2 = new Random();
            for (int i = 0; i < rnd2.Next(2, 6); i++)
            {
                army.Add(new Warrior(i.ToString() + n));
            }
            for (int i = army.Count(); i < 10; i++)
            {
                army.Add(new Archer(i.ToString()));
            }
            return army.OrderBy(x => new Random().Next()).ToList();
        }

        public static void LinkArmyEnemies(Army army1, Army army2)
        {
            for (int i = 0; i < Math.Min(army1.list.Count(), army2.list.Count()); i++) 
            {
                army1.list[i].enemy = army2.list[i].enemy;
                army2.list[i].enemy = army2.list[i].enemy;
            }
        }

        public static void Attacks(Army army1, Army army2)
        {
            for (int i = 0; i < Math.Min(army1.list.Count(), army2.list.Count()); i++)
            {
                Console.WriteLine(army1.list[i].ToString(), " vs. ", army2.list[i].ToString());
                army1.list[i].Attack();
                army2.list[i].Attack();
            }
        }
    }

    public abstract class Entity
    {
        protected string Name { get; set; }
        public int Health { get; set; }
        protected int Power { get; set; }
        protected string Hlaska {  get; set; }
        public Entity enemy { get; set; }
        public virtual void Attack()
        {
            Console.WriteLine(Hlaska);
            enemy.TakeDamage(Power);
            if (!enemy.IsAlive())
            {
                Power += 1;
            }
        }
        public virtual void TakeDamage(int amount)
        {
            Health -= amount;
        }
        public bool IsAlive()
        {
            return Health > 0;
        }
        public override string ToString()
        {
            return $"{this.GetType().Name} {Name} ({Health}/{Power})";
        }
    }

    public class Wizard : Entity
    {
        public Wizard(string name)
        {
            Name = name;
            Power = 2;
            Health = 5;
            Hlaska = "Vyčaruji z tebe mrtvolu!!!";
        }
        public override void TakeDamage(int amount)
        {
            base.TakeDamage(amount);
            enemy.Health -= amount / 2;
        }
    }
    
    public class Warrior : Entity
    {
        public Warrior(string name)
        {
            Name = name;
            Power = 1;
            Health = 10;
            Hlaska = "Do boje!!!";
        }
    }

    public class Archer : Entity
    {
        public Archer(string name)
        {
            Name = name;
            Power = 5;
            Health = 3;
            Hlaska = "Síla šípu!";
        }
    }

    public class Army
    {
        public List<Entity> list { get; }
        public Army(List<Entity> list)
        {
            this.list = list;
        }

        public void RemoveDead()
        {
            foreach (Entity e in list)
            {
                if(!e.IsAlive())
                {
                    list.Remove(e);
                }
            }
        }
    }
}
