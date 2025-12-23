namespace HeroALpha
{
    public class Rooms
    {
        string RoomType { get; set; }
        public Rooms(string roomType)
        {
            RoomType = roomType;
        }
        public void RoomEvent(string roomType,Hero hero,GameData gameData)
        {
            switch (roomType)
            {
                case "FightRoom":
                    
                    Battle battle = new();
                    battle.Start(hero, gameData);
                    break;
                case "EliteFightRoom":
                    
                    Battle eliteBattle = new();
                    eliteBattle.StartElite(hero, gameData);
                    break;
                case "TreasureRoom":
                    Console.WriteLine("Сделай вид что нашел че то крутое, я еще не добавил предметы");
                    Console.ReadKey(true);
                    break;
                    GetRandomEvent(hero, gameData);
                    Console.ReadKey(true);
                    break;
                case "BossRoom":
                    Console.WriteLine("бой с боссом. Напиши в Battle метод StartBoss который будет активировать босс с Векной(?)");
                    Battle bossBattle = new();
                    bossBattle.StartBoss(hero, gameData);
                    Console.ReadKey(true);
                    break;

            }
        }
        public void GetRandomEvent(Hero hero, GameData gameData)
        {
            Random random = new();
            int randomEvent = random.Next(0, 3);
            switch (randomEvent)
            {
                case 0:
                    Battle battle = new();
                    battle.Start(hero, gameData);
                    break;
                case 1:
                    Console.WriteLine("Сделай вид что нашел че то крутое, я еще не добавил предметы");
                    Console.ReadKey(true);
                    break;
                case 2:
                    RandomEvent1();
                    break;
            }
        }
        public void RandomEvent1()
        {
            List<string> DialogeVariant = new(["свали с дороги,плесень", "Старче, будь добр,дай пройти"]);
            int selectedIndex = 0;
            ConsoleKeyInfo key;
            do
            {
                Console.Clear();


                Console.WriteLine("ты встретил каког то то деда");
                Console.WriteLine("Что скажешь ему?");
                for (int i = 0; i < DialogeVariant.Count; i++)
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
                    Console.WriteLine(DialogeVariant[i]);

                    Console.ResetColor();
                }

                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.DownArrow && selectedIndex < DialogeVariant.Count - 1)
                    selectedIndex++;
                else if (key.Key == ConsoleKey.UpArrow && selectedIndex > 0)
                    selectedIndex--;
                else if (key.Key == ConsoleKey.Enter)
                {
                    if (selectedIndex == 0)
                    {
                        Console.WriteLine("Тебя избили клюкой");
                        Console.ReadKey(true);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Он дал тебе пройти и дал в дорогу странный мешочек");
                        Console.ReadKey(true);
                        break;
                    }
                }
            } while (true);
        }

    }
}
