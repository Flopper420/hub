using System.Text.Json;


namespace HeroALpha
{
    //конструктор персонажа
    public class Hero
    {
        public List<Skill> Skills { get; set; } = new List<Skill>();

        public int HP { get; set; }
        public int Mana { get; set; }
        public int Attack { get; set; }
        public int MagicAttack { get; set; }

        //добавит параметр очков развития
        //добавить очки действий(уже есть в батл но надо доработать механику)
        public int Streght { get; set; }
        public int Intelligence { get; set; }
        public int Dexterity { get; set; }
        
        public string Name { get; set; }
        public string GameClass { get; set; }
        public int Level { get; set; }
        public int ExpToNewLevel { get; set; }
        public int Exp { get; set; }
        public Hero() { }

        public Hero(string name,string gameClass, int intelligence, int streght, int dexterity, int exp, int expToNewLevel, int level)
        {
            HP = streght * 10;
            Mana = intelligence * 20;
            Attack = streght * dexterity * 2;
            MagicAttack = intelligence * 3;

            Name = name;
            GameClass = gameClass;

            Level = level;
            ExpToNewLevel = level * 100;
            Exp = exp;
        }
        //архивные методы сохранения и чтения, мб пригодятся
        public void Save(string path)
        {
            //Console.WriteLine($"Сохранение героя {Name} по пути: {path}");
            //Console.ReadKey(true);
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(this, options);
            File.WriteAllText(path, json);
        }
        public static Hero Read(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Hero>(json);
        }

        public void LearnSkill(Skill skill)
        {
            Skills.Add(skill);
        }
        public void UseSkill(Skill skill, Monster target)
        {
            skill.CurrentCooldown = skill.Cooldown;
            if(skill.CurrentCooldown == 0)
            {
                Mana -= skill.ManaCost;
                target.TakeDamage(skill);
            }           
        }
        public void PrintHeroStat()
        {
            Console.WriteLine($"Здоровье: {HP}");
            Console.WriteLine($"Атака: {Attack}");
            Console.WriteLine($"Мана: {Mana}");
            Console.WriteLine($"Уровень:{Level}");
            Console.WriteLine($"Опыт: {Exp}");
            Console.WriteLine($"До нового уровня: {ExpToNewLevel}");
            
        }
        public void PrintHeroSkills(int selectedSkill)
        {
            //ConsoleKeyInfo key;
            
     
            Console.WriteLine("\n===НАВЫКИ===");

            //int selectedSkill = 0;

            for (int i = 0; i < Skills.Count; i++)
            {
                if(i == selectedSkill)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("=>");
                }
                Console.WriteLine($"{i + 1}: {Skills[i].Name}");
                Console.ResetColor();
            }
        }
        public void PrintHeroSkillDescription(Skill skillDesc)
        {
            Console.WriteLine($"{Name}");
            Console.WriteLine($"Физический урон:{skillDesc.PhysicalDamage}");
            Console.WriteLine($"Магический урон:{skillDesc.MagicDamage}");
            Console.WriteLine($"Расход маны:{skillDesc.ManaCost}");
            Console.WriteLine($"Перезарядка:{skillDesc.Cooldown} ходов");
            Console.WriteLine($"Описание:\n{skillDesc.Description}");
            //skillDesc.PrintDescription(skillDesc);
        }
        public void TakeDamage(Skill skill)
        {
            HP -= skill.MagicDamage + skill.PhysicalDamage; ;
        }

    }

}
