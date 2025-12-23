namespace HeroALpha
{
    //конструктор скилов
    public class Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int PhysicalDamage { get; set; }
        public int MagicDamage { get; set; }
        public int ManaCost { get; set; }


        public int Cooldown { get; set; }
        public int CurrentCooldown { get; set; }
        public Skill() { }

        public Skill(string name, string description, int physicalDamage, int magicDamage, int manaCost, int cooldown, int currentCooldown)
        {
            Name = name;

            PhysicalDamage = physicalDamage;
            MagicDamage = magicDamage;
            ManaCost = manaCost;
            Description = description;
            Cooldown = cooldown;
            CurrentCooldown = currentCooldown;
        }
        public void PrintDescription()
        {
            Console.WriteLine($"{Name}");
            Console.WriteLine($"Физический урон:{PhysicalDamage}");
            Console.WriteLine($"Магический урон:{MagicDamage}");
            Console.WriteLine($"Расход маны:{ManaCost}");
            Console.WriteLine($"Перезарядка:{Cooldown} ходов");
            Console.WriteLine($"Описание:\n{Description}");

        }

        public bool CanUse(int currentMana)
        {
            if (CurrentCooldown == 0 && currentMana >= ManaCost)
                return true;
            else
                return false;
            Console.WriteLine("use");
           
        }
        public void Use(Monster target)
        {
            CurrentCooldown = Cooldown;
            //target.HP -= MagicDamage + PhysicalDamage;
            //target.TakeDamage();
            Console.WriteLine($"используется {Name}!");                      
        }
        public void Use(Hero target)
        {
            CurrentCooldown = Cooldown;
            Console.WriteLine($"используется {Name}!");

        }
    }

}
