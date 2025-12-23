namespace HeroALpha
{
    public class Battle 
    {
        
        public void Start(Hero hero,GameData game)
        {
            Random randomMonster = new();
            int randomIndex = randomMonster.Next(0, game.MonstersFile.Count);
            ConsoleKeyInfo key;
            int actionPoint = 2;//пока костыль, потом перенеси как параметр для Hero
            bool turnEnd = false;
            int selectedAction = 0;

           

            Monster monster = game.MonstersFile.Values.ToArray()[randomIndex].SpawnMonster();
            
            Console.WriteLine("Бой!");
            List<Skill> battleSkills = new();
            List<string> commonAction = ["побег","закончить ход"];
            for (int i = 0; i < hero.Skills.Count; i++)
            {
                battleSkills.Add(hero.Skills[i]); 
            }
            while (hero.HP > 0 && monster.HP > 0)
            {
                
                while (turnEnd == false)// Ход героя
                {
                    
                    turnEnd = false;
                    Console.Clear();
                    
                    Console.WriteLine("\nход игрока:");

                    Console.WriteLine($"{hero.Name} HP: {hero.HP},Mana: {hero.Mana}");
                    Console.WriteLine($"{monster.Name} HP: {monster.HP}");
                    
                    for (int i = 0; i < battleSkills.Count; i++)
                    {  
                        if (i == selectedAction)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("=>");
                        }
                        Console.WriteLine($"{i + 1}: {battleSkills[i].Name} Перезарядка: {battleSkills[i].CurrentCooldown}");
                        Console.ResetColor();
                    }
                    for(int i = 0; i < commonAction.Count; i++)
                    {
                        if (i == selectedAction - battleSkills.Count)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("=>");
                        }
                        Console.WriteLine(commonAction[i]);
                        Console.ResetColor();
                    }

                    //данный для тестировки
                    Console.WriteLine("\ntest data");
                    Console.WriteLine($"Очки действий:{actionPoint}");
                    Console.WriteLine($"selectedAction:{selectedAction}");
                    Console.WriteLine($"выбрано действие:{ selectedAction + 1}");
                    Console.WriteLine($"Batle skill count + 1 = {battleSkills.Count + 1}");
                    Console.WriteLine($"Common action 0 = {commonAction[0]}");
                    Console.WriteLine($"battleSkills.Count + commonAction.Count = {battleSkills.Count + commonAction.Count}");
                    
                    //выбор строки стрелочками
                    key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.DownArrow && selectedAction < battleSkills.Count + commonAction.Count - 1)
                        selectedAction++;
                    else if (key.Key == ConsoleKey.UpArrow && selectedAction > 0)
                        selectedAction--;

                    else if (key.Key == ConsoleKey.Enter )//переделать//использование скилов в бою
                    {
                        
                        if (selectedAction >= battleSkills.Count)
                        {
                            //Console.Clear();
                            //Console.WriteLine("true");
                            //key = Console.ReadKey(true);
                            if (selectedAction == battleSkills.Count) //написать логику побега//?сделать методы победы/поражения/победы? 
                            {
                                Console.Clear();
                                Console.WriteLine("run");
                                key = Console.ReadKey(true);
                            }
                            else if(selectedAction == battleSkills.Count + 1)//завершение хода игрока
                            {
                                Console.Clear();
                                Console.WriteLine("turn end");
                                turnEnd = true;
                                key = Console.ReadKey(true);
                            }
                            //asd[selectedAction-BattleSKills.Count].
                        }
                        else 
                        {
                            if (battleSkills[selectedAction].CanUse(hero.Mana) == true && battleSkills[selectedAction].CurrentCooldown == 0 && actionPoint > 0)
                            {
                                
                                hero.UseSkill(battleSkills[selectedAction], monster);
                                actionPoint -= 1;

                            }
                            else
                                Console.WriteLine("NO");
                        }
                    }
                   
                        
                    

                }
                //выход из хода игрока

                // Проверка смерти монстра
                if (monster.HP <= 0)
                {
                    Console.WriteLine("Победа!");
                    hero.Exp += monster.GivenExp;
                    //здесть надо прописать получение награды после боя
                    break;
                }

                // Ход монстра
                Console.WriteLine("\nход врага:");
                actionPoint += 2;
                int monsterDamage = monster.ClawAttack();
                hero.HP -= monsterDamage;
                Console.WriteLine($"враг наносит {monsterDamage} урона!");
                turnEnd = false;
                Console.WriteLine("люабая клавиша что бы продолжить");
                key = Console.ReadKey(true);
                
                // Проверка смерти героя
                if (hero.HP <= 0)
                {
                    Console.WriteLine("Помер!");
                    break;
                }
            }
        }

        public void StartElite(Hero hero, GameData game)
        {
            Random randomMonster = new();
            int randomIndex = randomMonster.Next(0, game.MonstersFile.Count);
            ConsoleKeyInfo key;
            int actionPoint = 2;//пока костыль, потом перенеси как параметр для Hero
            bool turnEnd = false;
            int selectedAction = 0;



            Monster monster = game.MonstersFile.Values.ToArray()[randomIndex].SpawnMonster();

            Console.WriteLine("Бой!");
            List<Skill> battleSkills = new();
            List<string> commonAction = ["побег", "закончить ход"];
            for (int i = 0; i < hero.Skills.Count; i++)
            {
                battleSkills.Add(hero.Skills[i]);
            }
            while (hero.HP > 0 && monster.HP > 0)
            {

                while (turnEnd == false)// Ход героя
                {

                    turnEnd = false;
                    Console.Clear();

                    Console.WriteLine("\nход игрока:");

                    Console.WriteLine($"{hero.Name} HP: {hero.HP},Mana: {hero.Mana}");
                    Console.WriteLine($"Элитный {monster.Name} HP: {monster.HP}");

                    for (int i = 0; i < battleSkills.Count; i++)
                    {
                        if (i == selectedAction)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("=>");
                        }
                        Console.WriteLine($"{i + 1}: {battleSkills[i].Name} Перезарядка: {battleSkills[i].CurrentCooldown}");
                        Console.ResetColor();
                    }
                    for (int i = 0; i < commonAction.Count; i++)
                    {
                        if (i == selectedAction - battleSkills.Count)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("=>");
                        }
                        Console.WriteLine(commonAction[i]);
                        Console.ResetColor();
                    }

                    //данный для тестировки//потом точно пригодится
                    //Console.WriteLine("\ntest data");
                    //Console.WriteLine($"Очки действий:{actionPoint}");
                    //Console.WriteLine($"selectedAction:{selectedAction}");
                    //Console.WriteLine($"выбрано действие:{selectedAction + 1}");
                    //Console.WriteLine($"Batle skill count + 1 = {battleSkills.Count + 1}");
                    //Console.WriteLine($"Common action 0 = {commonAction[0]}");
                    //Console.WriteLine($"battleSkills.Count + commonAction.Count = {battleSkills.Count + commonAction.Count}");

                    //выбор строки стрелочками
                    key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.DownArrow && selectedAction < battleSkills.Count + commonAction.Count - 1)
                        selectedAction++;
                    else if (key.Key == ConsoleKey.UpArrow && selectedAction > 0)
                        selectedAction--;

                    else if (key.Key == ConsoleKey.Enter)//переделать//использование скилов в бою
                    {

                        if (selectedAction >= battleSkills.Count)
                        {
                            //Console.Clear();
                            //Console.WriteLine("true");
                            //key = Console.ReadKey(true);
                            if (selectedAction == battleSkills.Count) //написать логику побега
                            {
                                Console.Clear();
                                Console.WriteLine("run");
                                key = Console.ReadKey(true);
                            }
                            else if (selectedAction == battleSkills.Count + 1)//завершение хода игрока
                            {
                                Console.Clear();
                                Console.WriteLine("turn end");
                                turnEnd = true;
                                key = Console.ReadKey(true);
                            }
                            //asd[selectedAction-BattleSKills.Count].
                        }
                        else
                        {
                            if (battleSkills[selectedAction].CanUse(hero.Mana) == true && battleSkills[selectedAction].CurrentCooldown == 0 && actionPoint > 0)
                            {
                                //hero.Skills[selectedAction].Use(monster);
                                hero.UseSkill(battleSkills[selectedAction], monster);
                                actionPoint -= 1;
                                //turnEnd = true;
                                //Console.WriteLine(monster.HP);
                                // key = Console.ReadKey(true);
                            }
                            else
                                Console.WriteLine("NO");
                        }
                    }
                    // else if (selectedAction == BattleSKills.Count && key.Key == ConsoleKey.Enter)



                }
                //выход из хода игрока

                // Проверка смерти монстра
                if (monster.HP <= 0)
                {
                    Console.WriteLine("Победа!");
                    hero.Exp += monster.GivenExp;
                    //здесть надо прописать получение награды после боя
                    break;
                }

                // Ход монстра
                Console.WriteLine("\nход врага:");
                actionPoint += 2;
                int monsterDamage = monster.ClawAttack();
                hero.HP -= monsterDamage;
                Console.WriteLine($"враг наносит {monsterDamage} урона!");
                turnEnd = false;
                Console.WriteLine("люабая клавиша что бы продолжить");
                key = Console.ReadKey(true);

                // Проверка смерти героя
                if (hero.HP <= 0)
                {
                    Console.WriteLine("Помер!");
                    break;
                }
            }
        }
        public void StartBoss(Hero hero, GameData game)
        {
            Random randomMonster = new();
            int randomIndex = randomMonster.Next(0, game.MonstersFile.Count);
            ConsoleKeyInfo key;
            int actionPoint = 2;//пока костыль, потом перенеси как параметр для Hero
            bool turnEnd = false;
            int selectedAction = 0;



            Monster monster = game.MonstersFile["Fake Vekna"].SpawnMonster();

            Console.WriteLine("Бой!");
            List<Skill> battleSkills = new();
            List<string> commonAction = ["побег", "закончить ход"];
            for (int i = 0; i < hero.Skills.Count; i++)
            {
                battleSkills.Add(hero.Skills[i]);
            }
            while (hero.HP > 0 && monster.HP > 0)
            {

                while (turnEnd == false)// Ход героя
                {

                    turnEnd = false;
                    Console.Clear();

                    Console.WriteLine("\nход игрока:");

                    Console.WriteLine($"{hero.Name} HP: {hero.HP},Mana: {hero.Mana}");
                    Console.WriteLine($"Элитный {monster.Name} HP: {monster.HP}");

                    for (int i = 0; i < battleSkills.Count; i++)
                    {
                        if (i == selectedAction)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("=>");
                        }
                        Console.WriteLine($"{i + 1}: {battleSkills[i].Name} Перезарядка: {battleSkills[i].CurrentCooldown}");
                        Console.ResetColor();
                    }
                    for (int i = 0; i < commonAction.Count; i++)
                    {
                        if (i == selectedAction - battleSkills.Count)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("=>");
                        }
                        Console.WriteLine(commonAction[i]);
                        Console.ResetColor();
                    }

                    //данный для тестировки
                    Console.WriteLine("\ntest data");
                    Console.WriteLine($"Очки действий:{actionPoint}");
                    Console.WriteLine($"selectedAction:{selectedAction}");
                    Console.WriteLine($"выбрано действие:{selectedAction + 1}");
                    Console.WriteLine($"Batle skill count + 1 = {battleSkills.Count + 1}");
                    Console.WriteLine($"Common action 0 = {commonAction[0]}");
                    Console.WriteLine($"battleSkills.Count + commonAction.Count = {battleSkills.Count + commonAction.Count}");

                    //выбор строки стрелочками
                    key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.DownArrow && selectedAction < battleSkills.Count + commonAction.Count - 1)
                        selectedAction++;
                    else if (key.Key == ConsoleKey.UpArrow && selectedAction > 0)
                        selectedAction--;

                    else if (key.Key == ConsoleKey.Enter)//переделать//использование скилов в бою
                    {

                        if (selectedAction >= battleSkills.Count)
                        {
                            //Console.Clear();
                            //Console.WriteLine("true");
                            //key = Console.ReadKey(true);
                            if (selectedAction == battleSkills.Count) //написать логику побега
                            {
                                Console.Clear();
                                Console.WriteLine("run");
                                key = Console.ReadKey(true);
                            }
                            else if (selectedAction == battleSkills.Count + 1)//завершение хода игрока
                            {
                                Console.Clear();
                                Console.WriteLine("turn end");
                                turnEnd = true;
                                key = Console.ReadKey(true);
                            }
                            //asd[selectedAction-BattleSKills.Count].
                        }
                        else
                        {
                            if (battleSkills[selectedAction].CanUse(hero.Mana) == true && battleSkills[selectedAction].CurrentCooldown == 0 && actionPoint > 0)
                            {
                                //hero.Skills[selectedAction].Use(monster);
                                hero.UseSkill(battleSkills[selectedAction], monster);
                                actionPoint -= 1;
                                //turnEnd = true;
                                //Console.WriteLine(monster.HP);
                                // key = Console.ReadKey(true);
                            }
                            else
                                Console.WriteLine("NO");
                        }
                    }
                    // else if (selectedAction == BattleSKills.Count && key.Key == ConsoleKey.Enter)



                }
                //выход из хода игрока

                // Проверка смерти монстра
                if (monster.HP <= 0)
                {
                    Console.WriteLine("Победа!");
                    Console.WriteLine("Конец Альфа Версии!");
                    hero.Exp += monster.GivenExp;
                    //здесть надо прописать получение награды после боя
                    break;
                }

                // Ход монстра
                Console.WriteLine("\nход врага:");
                actionPoint += 2;
                int monsterDamage = monster.ClawAttack();
                hero.HP -= monsterDamage;
                Console.WriteLine($"враг наносит {monsterDamage} урона!");
                turnEnd = false;
                Console.WriteLine("люабая клавиша что бы продолжить");
                key = Console.ReadKey(true);

                // Проверка смерти героя
                if (hero.HP <= 0)
                {
                    Console.WriteLine("Помер!");
                    break;
                }
            }
        }
    }

}
