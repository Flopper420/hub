using System.IO;


namespace HeroALpha
{
    /*
     * Сделать:
     * лут и инвентарь
     * админ панель(если время лишнее будет)
     * повышение уровня
     * экипировка(не то же самое что инвентарь но связано с ним)
     * создание персов(внутри игры а не через код)
     * 
     * ^модифицировать сохранение гейм даты - сделал 
    */
    internal class Program
    { //Hero(string name,string gameClass, int intelligence, int streght, int dexterity, int exp, int expToNewLevel, int level)
      //Monster(string name,int hp, int mana, int attack, int givenExp)

        static void Main(string[] args)
        {
            GameMenu game = new();
            GameData gameData = GameData.Load("game_data");
            game.Start();
        }
    }
}
