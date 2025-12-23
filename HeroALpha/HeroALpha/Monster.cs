namespace HeroALpha
{
    //конструктор монстра
    public class Monster
    {
        public List<Skill> MonsterSkills { get; set; } = new List<Skill>();
        public int HP { get; set; }
        public int Mana { get; set; }
        public int Attack { get; set; }
        public int GivenExp { get; set; }
        public string Name { get; set; }

        public Monster() { }
        public Monster(string name,int hp, int mana, int attack, int givenExp)
        {
            HP = hp;
            Mana = mana;
            Attack = attack;
            GivenExp = givenExp;
            Name = name;
        }
        public void TakeDamage(Skill skill)
        {
            HP -= skill.MagicDamage + skill.PhysicalDamage; ;
        }

        public int ClawAttack()
        {
            Console.WriteLine("Monster uses Claw Attack!");
            return Attack;
        }
        public void UseSkill(Skill skill, Hero target)
        {
            skill.CurrentCooldown = skill.Cooldown;
            if (skill.CurrentCooldown == 0)
            {
                Mana -= skill.ManaCost;
                target.TakeDamage(skill);
            }
        }

        public Monster SpawnMonster()
        {
            
            return new Monster(Name, HP, Mana, Attack, GivenExp);
        }
    }
}
