namespace HeroALpha
{
    public class Map
    {
        private GameData gameData;
        private char[] rooms = new char[10];
        private int playerPosition = 0;
        private int selectedRoom = 0;
        private Hero hero;

        private Random random = new Random();
        public Map(Hero hero)
        {
            gameData = GameData.Load("game_data");
            InitilizeRooms();
            this.hero = hero;
        }

        private void InitilizeRooms()
        {
            rooms[0] = '^'; // Стартовая комната
            rooms[9] = '¤'; // Комната босса
            
            for(int i = 1;  i < 9; i++)
            {
                rooms[i] = RoomGeneratorV01();
            }
        }

        public void DrawMapV2()
        {
            
            ConsoleKeyInfo key;
            do
            {
                Console.Clear();
                Console.WriteLine("       КАРТА ПОДЗЕМЕЛЬЯ");
                Console.WriteLine("       (↑↓ - выбор, Enter - перейти, ESC - выход)");
                Console.WriteLine();

                DrawMapWithSelection();

                Console.WriteLine($"\nВыбрана комната: {selectedRoom}");
                Console.WriteLine($"Игрок в комнате: {playerPosition}");

               

                List<int> availableRooms = GetVerticalNeighbors(playerPosition);
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow && availableRooms.Count > 0)
                {
                    selectedRoom = (availableRooms[0]);
                    //MoveToRoom(availableRooms[0]);
                }
                else if (key.Key == ConsoleKey.DownArrow && availableRooms.Count > 1)
                {
                    selectedRoom = availableRooms[1];
                    //MoveToRoom(availableRooms[1]);

                    //selectedRoom = (selectedRoom - 1 + 10) % 10;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    MoveToRoom(selectedRoom);
                }
            } while (key.Key != ConsoleKey.Escape);
            

        }
        //пока пробуем костылями
        //ВАЖНОЕ попробовать сделать перемещение по уровням т.е.  3 комната - 1 уровень(верхний на карте) 1,2 - 2 уровень и тд. Для более удобного перемещение и получения комнат ПОВТОРИТЬ ТЕОРИЮ ГРАФОВ И КВАНТОРНЫЕ АВТОМАТЫ
        private List<int> GetVerticalNeighbors(int currentRoom)
        {
            Dictionary<int, List<int>> verticalMap = new()
            {
                {0, new List<int>{1,6}},         // Стартовая
                {1, new List<int>{3, 4}},
                {2, new List<int>{4, 5}},
                {3, new List<int>{2,2}},          //самая верхняя   
                {4, new List<int>{2, 7}},        //центальная
                {5, new List<int>{9}},
                {6, new List<int>{4, 8}},
                {7, new List<int>{5}},
                {8, new List<int>{7}},          // Самая нижняя
                {9, new List<int>{}}
            };
            return verticalMap.ContainsKey(currentRoom) ? verticalMap[currentRoom] : new List<int>();

        }

        private void DrawMapWithSelection()
        {
            Console.WriteLine($"       {FormatRoom(3)}");
            Console.WriteLine($"       / \\");
            Console.WriteLine($"      /   \\");
            Console.WriteLine($"     /     \\");
            Console.WriteLine($"   {FormatRoom(1)}     {FormatRoom(2)}");
            Console.WriteLine($"   / \\     / \\");
            Console.WriteLine($"  /   \\   /   \\");
            Console.WriteLine($" /     \\ /     \\");
            Console.WriteLine($"{FormatRoom(0)}    {FormatRoom(4)}     {FormatRoom(5)}═══{FormatRoom(9)}");
            Console.WriteLine($" \\     / \\     /");
            Console.WriteLine($"  \\   /   \\   /");
            Console.WriteLine($"   \\ /     \\ /");
            Console.WriteLine($"   {FormatRoom(6)}     {FormatRoom(7)}");
            Console.WriteLine($"     \\     /");
            Console.WriteLine($"      \\   /");
            Console.WriteLine($"       \\ /");
            Console.WriteLine($"       {FormatRoom(8)}");
            Console.ResetColor();
        }

        private string FormatRoom(int roomIndex)
        {
            if(roomIndex == selectedRoom)
            {
                //Console.ForegroundColor = ConsoleColor.Magenta;
                return $"[{rooms[roomIndex]}]";
            }
            else
            {
                Console.ResetColor();
                return $" {rooms[roomIndex]} ";
            }

        }

        private void MoveToRoom(int targetRoom)
        {
            if (CanMoveTo(targetRoom))
            {
                RoomEnter(targetRoom);
                //меняем значок зачищенной комнаты
                rooms[playerPosition] = '▓';
                //перемещаем игрока
                playerPosition = targetRoom;
                rooms[playerPosition] = '@';

                Console.WriteLine($"Переместились в комнату {targetRoom}");

                
            }
        }
        private bool CanMoveTo(int targetRoom)
        {
            Dictionary<int, List<int>> connections = new()
            {                              
                {0, new List<int>{1,6}},         // Стартовая
                {1, new List<int>{3, 4}},      
                {2, new List<int>{4, 5}},       
                {3, new List<int>{2}},          //самая верхняя   
                {4, new List<int>{2, 7}},       //центальная
                {5, new List<int>{9}},          
                {6, new List<int>{4, 8}},       
                {7, new List<int>{4, 5}},    
                {8, new List<int>{7}},          // Самая нижняя
                {9, new List<int>{}}           // Босс (тупик)
            };
            return connections.ContainsKey(playerPosition) && connections[playerPosition].Contains(targetRoom);
        }
        private void RoomEnter(int roomIndex)
        {
            string buferRoomType = "default";
            Rooms room = new(buferRoomType);
            char roomType = rooms[roomIndex];
            switch (roomType)
            {
                case '^':
                    Console.WriteLine("Вы в стартовой комнате");
                    buferRoomType = ("startRoom");
                    Console.ReadKey(true);
                    break;
                case '#':
                    Console.Clear();
                    Console.WriteLine("Бой с монстром!");
                    Console.ReadKey(true);
                    buferRoomType = "FightRoom";
                    room.RoomEvent(buferRoomType, hero, gameData);
                    break;
                case '$':
                    Console.Clear();
                    Console.WriteLine("Элитный враг!");
                    Console.ReadKey(true);
                    buferRoomType = "EliteFightRoom";
                    room.RoomEvent(buferRoomType, hero, gameData);
                    break;
                case '*':
                    Console.WriteLine("Сундук с сокровищами!");
                    Console.ReadKey(true);
                    buferRoomType = "TreasureRoom";
                    room.RoomEvent(buferRoomType, hero, gameData);
                    break;
                case '?':
                    Console.WriteLine("Случайное событие!");
                    Console.ReadKey(true);
                    buferRoomType = "RandomEvent";
                    room.RoomEvent(buferRoomType, hero, gameData);
                    break;
                case '¤':
                    Console.WriteLine("БОСС!");
                    Console.ReadKey(true);
                    buferRoomType = "BossRoom";
                    room.RoomEvent(buferRoomType, hero, gameData);
                    break;
                case '▓':
                    Console.WriteLine("Исследованная комната");
                    Console.ReadKey(true);
                    break;
                default:
                    Console.WriteLine("char not capture");
                    Console.ReadKey(true);
                    break;
            }

        }

        public void DrawMap()
        {
            ConsoleKeyInfo key;
            char roomChar1 = RoomGeneratorV01();
            char roomChar2 = RoomGeneratorV01();
            char roomChar3 = RoomGeneratorV01();
            char roomChar4 = RoomGeneratorV01();
            char roomChar5 = RoomGeneratorV01();
            char roomChar6 = RoomGeneratorV01();
            char roomChar7 = RoomGeneratorV01();
            char roomChar8 = RoomGeneratorV01();
            char roomChar9 = RoomGeneratorV01();
            char bossRoomChar = '¤';
            char startRoomChar = '@';
            char clearRoomChar = '▓';
            int selectedRoom = 0;

            Console.WriteLine($"       [{roomChar1}]              \r\n       / \\              \r\n      /   \\             \r\n     /     \\            \r\n   [{roomChar2}]     [{roomChar3}]          \r\n   / \\     / \\          \r\n  /   \\   /   \\         \r\n /     \\ /     \\        \r\n[{startRoomChar}]    [{roomChar5}]     [{roomChar6}]═══[{bossRoomChar}]\r\n \\     / \\     /        \r\n  \\   /   \\   /         \r\n   \\ /     \\ /          \r\n   [{roomChar8}]     [{roomChar9}]          \r\n     \\     /            \r\n      \\   /             \r\n       \\ /              \r\n       [{roomChar4}]");

            Console.WriteLine(selectedRoom);
            key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.DownArrow)
            {
                selectedRoom++;
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                selectedRoom--;

            }


           

            //описание значков//Console.WriteLine("/* \r\n $ - Элитный враг\r\n # - Бой \r\n * - Награда/Сундук\r\n ? - Событие\r\n @ - Персонаж\r\n ¤ - Босс\r\n*/");
        }//архивный метод
        public char RoomGeneratorV01()
        {

            char[] maybeChars = { '$', '#', '#', '?', '*' };
            int randomChar = random.Next(0, maybeChars.Length);
            return maybeChars[randomChar];

        }
    }
}
