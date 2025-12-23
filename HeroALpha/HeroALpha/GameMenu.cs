namespace HeroALpha
{
    public class GameMenu
    {
        private GameData NewGameData = GameData.Load("game_data");

        public GameMenu()
        {
            //GameDataV2.Load("game_data");
        }
        private void ShowHeroDetails(Hero hero)
        {
            bool exitAll = false;
            ConsoleKeyInfo key;
            int selectedSkill = 0;

            do
            {
                exitAll = true;
                Console.Clear();
                Console.WriteLine($"=== {hero.Name} ===");
                hero.PrintHeroStat();

                hero.PrintHeroSkills(selectedSkill);
                Console.WriteLine("\nEscape - назад");
                Console.WriteLine(hero.Skills[selectedSkill].Description);
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.DownArrow && selectedSkill < hero.Skills.Count)
                    selectedSkill++;
                else if (key.Key == ConsoleKey.UpArrow && selectedSkill > 0)
                    selectedSkill--;
                else if (key.Key == ConsoleKey.Enter)
                    do
                    {
                        exitAll = false;
                        Console.Clear();
                        //hero.Skills[selectedSkill].Name();
                        //Console.WriteLine(hero.Skills[selectedSkill].Description);
                        //hero.PrintHeroSkillDescription(selectedSkill);
                        hero.Skills[selectedSkill].PrintDescription();
                        key = Console.ReadKey(true);
                    } while (key.Key != ConsoleKey.Escape);     
                //key = Console.ReadKey(true);
            } while (key.Key != ConsoleKey.Escape || exitAll == false);
            

        }
        public void Start()
        {
            //Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetWindowSize(150, 40);
            Console.CursorVisible = false;
            List<string> lines = new();

            //начальная менюшка
            //Console.WriteLine($"Hello \nY/N");
            lines.Add("ПЕРСОНАЖИ:\n      .-.\r\n    __|=|__\r\n   (_/`-`\\_)\r\n   //\\___/\\\\\r\n   <>/   \\<>\r\n    \\|_._|/\r\n     <_I_>\r\n      |||\r\n     /_|_\\");
            lines.Add("СНАРЯЖЕНИЕ:\n    |\nO===[====================- \n    |");
            lines.Add("УЛУЧШЕНИЯ:\n      ______ ______\r\n    _/      Y      \\_\r\n   // ~~ ~~ | ~~ ~  \\\\\r\n  // ~ ~ ~~ | ~~~ ~~ \\\\      \r\n //________.|.________\\\\                 \r\n`----------`-'----------'");
            lines.Add("СПУСК:\n┌┬┬┬┬┬┬┬┬┐\r\n├┼┼┼┼┼┼┼┼┤\r\n├┼┼┼┼┼┼┼┼┤\r\n├┼┼┼┼┼┼┼┼┤\r\n├┼┼┼┼┼┼┼┼┤\r\n├┼┼┼┼┼┼╬┼┤\r\n├┼┼┼┼┼┼┼┼┤\r\n├┼┼┼┼┼┼┼┼┤\r\n├┼┼┼┼┼┼┼┼┤\r\n└┴┴┴┴┴┴┴┴┘");
            lines.Add("\nвыход - ESC");
            //наполняем лист демо значениями
            int selectedIndex = 0;//базовое значение выбранного индекса
            //ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
            do
            {
                Console.Clear();
                for (int i = 0; i < lines.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        //Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"{lines[i]}" + "<=");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.WriteLine(lines[i]);
                    }
                }
                ConsoleKeyInfo consoleKeyWhile = Console.ReadKey(true);
                if (consoleKeyWhile.Key == ConsoleKey.DownArrow && selectedIndex < lines.Count - 1)
                {
                    selectedIndex += 1;
                }
                if (consoleKeyWhile.Key == ConsoleKey.UpArrow && selectedIndex > 0)
                {
                    selectedIndex -= 1;
                }
                if (consoleKeyWhile.Key == ConsoleKey.Enter && selectedIndex == 0)
                {
                    CharacterMenu_V2();
                }

                if (consoleKeyWhile.Key == ConsoleKey.Enter && selectedIndex == 1)
                {
                    StafMenu();
                }
                if (consoleKeyWhile.Key == ConsoleKey.Enter && selectedIndex == 2)
                {
                    UpgradesMenu();
                }
                if (consoleKeyWhile.Key == ConsoleKey.Enter && selectedIndex == 3)
                {
                    
                    FightMenu();
                }
                if (consoleKeyWhile.Key == ConsoleKey.Escape)
                {
                    break;
                }
            } while (true);


        }


        public void CharacterMenu_V2()
        {
            
            var heroesList = NewGameData.GetAllHeroes();
            int selectedIndex = 0;
            ConsoleKeyInfo key;

            do
            {
                Console.Clear();
                Console.WriteLine("=== ВЫБОР ПЕРСОНАЖА ===");

                for (int i = 0; i < heroesList.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("> ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }

                    Console.WriteLine($"{heroesList[i].Name} ({heroesList[i].GameClass})");
                    
                    Console.ResetColor();
                }

                Console.WriteLine("\nENTER - Выбрать, ESC - Назад");
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.DownArrow && selectedIndex < heroesList.Count - 1)
                    selectedIndex++;
                else if (key.Key == ConsoleKey.UpArrow && selectedIndex > 0)
                    selectedIndex--;
                else if (key.Key == ConsoleKey.Enter && heroesList.Count > 0)
                {
                    ShowHeroDetails(heroesList[selectedIndex]);
                }
                    
                    

            } while (key.Key != ConsoleKey.Escape);
        }
        public void UpgradesMenu()
        {
            ConsoleKeyInfo key;

            do
            {
                Console.Clear();
                Console.WriteLine("=== МЕНЮ УЛУЧШЕНИЙ ===");

                

                Console.WriteLine("\nESC - Назад");
                key = Console.ReadKey(true);

            } while (key.Key != ConsoleKey.Escape);
        }
        public void StafMenu()
        {
            ConsoleKeyInfo key;

            do
            {
                Console.Clear();
                Console.WriteLine("=== МЕНЮ БАРАХЛА ===");

                

                Console.WriteLine("\nESC - Назад");
                key = Console.ReadKey(true);

            } while (key.Key != ConsoleKey.Escape);
        }
        public void FightMenu()
        {
            Console.Clear();
            Console.WriteLine("Cпуститься в подземелье?(ENTER/ESC)");
            ConsoleKeyInfo consoleKeyMenu = Console.ReadKey(true);

            do
            {
                Console.Clear();
                //CharacterMenu_V2();
                var heroesList = NewGameData.GetAllHeroes();
                int selectedIndex = 0;
                ConsoleKeyInfo key;
                do
                {
                    Console.Clear();
                    Console.WriteLine("=== ВЫБОР ПЕРСОНАЖА ===");

                    for (int i = 0; i < heroesList.Count; i++)
                    {
                        if (i == selectedIndex)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("> ");
                        }
                        Console.WriteLine($"{heroesList[i].Name} ({heroesList[i].GameClass})");
                        Console.ResetColor();

                       
                    }
                    key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.DownArrow && selectedIndex < heroesList.Count - 1)
                        selectedIndex++;
                    else if (key.Key == ConsoleKey.UpArrow && selectedIndex > 0)
                        selectedIndex--;
                    else if (key.Key == ConsoleKey.Enter && heroesList.Count > 0)
                    {
                        //здесь сохранить
                        Battle battle = new();
                        //Console.WriteLine(heroesList[selectedIndex].Name);
                        //battle.Start(heroesList[selectedIndex],gameData);
                        Map map = new(heroesList[selectedIndex]);
                        map.DrawMapV2();

                    }
                } while (key.Key != ConsoleKey.Escape);





                Console.WriteLine("Нажмите ESC для выхода");
                consoleKeyMenu = Console.ReadKey(true);
            } while (consoleKeyMenu.Key != ConsoleKey.Escape);
        }
    }

}
