using HeroALpha;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

public class GameData

{
    [JsonIgnore]
    public Dictionary<string, Monster> MonstersFile { get; set; } = new(); //список всех монстров
    public Dictionary<string, string> HeroesFiles { get; set; } = new();   //список всех персонажей
    
    [JsonIgnore]
    public string SaveFolder { get; set; }
    public GameData(string saveFolder)
    {
        SaveFolder = saveFolder;
        LoadMonsters();
    }
    [JsonConstructor]
    private GameData() { }

    private void LoadMonsters()
    {
        string monsterFilePath = Path.Combine(SaveFolder, "monsters.json");
        if (!File.Exists(monsterFilePath))
            throw new FileNotFoundException($"Monster file not found: {monsterFilePath}");

        string json = File.ReadAllText(monsterFilePath);
        MonstersFile = JsonSerializer.Deserialize<Dictionary<string, Monster>>(json);
    }
    public bool AddHero(Hero hero)
    {
        foreach (string existingName in HeroesFiles.Keys)
        {
            if (string.Equals(existingName, hero.Name, StringComparison.OrdinalIgnoreCase))
                return false;
        }

        string safeName = MakeSafeFileName(hero.Name);//делаем имя файла которое не сломает к херам все, метод будет чуть позже
        string heroFilePath = Path.Combine(SaveFolder, "heroes", $"{safeName}.json");//собираем путь к файлу

        string heroesDir = Path.GetDirectoryName(heroFilePath);
        if (!Directory.Exists(heroesDir))
            Directory.CreateDirectory(heroesDir);

        hero.Save(heroFilePath);
        HeroesFiles[hero.Name] = heroFilePath;
        SaveGameData();
        return true;
    }
    public Hero? GetHero(string heroName)
    {
        if (HeroesFiles.TryGetValue(heroName, out string filePath) && File.Exists(filePath))
        {
            return Hero.Read(filePath);
        }
        return null;
    }
    public Monster GetMonster(string monsterName)
    {
        if (MonstersFile.TryGetValue(monsterName, out Monster monster))
        {
            return monster;
        }
        return null;
    }
    public void RemoveHero(string heroName)
    {
        if (HeroesFiles.TryGetValue(heroName, out string filePath))
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            HeroesFiles.Remove(heroName);
            SaveGameData();
        }
    }
    public void SaveGameData()
    {
        string gameDataPath = Path.Combine(SaveFolder, "game_data.json");
        string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(gameDataPath, json);
    }

    public static GameData Load(string saveFolder)
    {
        string gameDataPath = Path.Combine(saveFolder, "game_data.json");
        GameData data;
        if (File.Exists(gameDataPath))
        {
            string json = File.ReadAllText(gameDataPath);
            data = JsonSerializer.Deserialize<GameData>(json);
        }
        else
        {
            data = new GameData(saveFolder);
        }
        data.SaveFolder = saveFolder;

        try
        {
            data.LoadMonsters();
        }
        catch (FileNotFoundException)
        {
            data.MonstersFile = new Dictionary<string, Monster>();
        }
        return data;
    }
    private string MakeSafeFileName(string name)
    {
        name = Transliterate(name);
        char[] invalidChars = Path.GetInvalidFileNameChars();
        string safeName = name;
        foreach (char c in invalidChars)
        {
            safeName = safeName.Replace(c, '_');
        }
        safeName = safeName.Replace(' ', '_');
        return safeName.ToLowerInvariant();
    }
    private string Transliterate(string text)//заменяем русские буквы латиницей, я хз почему, но мне кажется что это крутая фича
    {
        var translit = new Dictionary<char, string>
    {
        {'а', "a"}, {'б', "b"}, {'в', "v"}, {'г', "g"},
        {'д', "d"}, {'е', "e"}, {'ё', "yo"}, {'ж', "zh"},
        {'з', "z"}, {'и', "i"}, {'й', "y"}, {'к', "k"},
        {'л', "l"}, {'м', "m"}, {'н', "n"}, {'о', "o"},
        {'п', "p"}, {'р', "r"}, {'с', "s"}, {'т', "t"},
        {'у', "u"}, {'ф', "f"}, {'х', "kh"}, {'ц', "ts"},
        {'ч', "ch"}, {'ш', "sh"}, {'щ', "shch"}, {'ъ', ""},
        {'ы', "y"}, {'ь', ""}, {'э', "e"}, {'ю', "yu"},
        {'я', "ya"},
        // Заглавные буквы
        {'А', "a"}, {'Б', "b"}, {'В', "v"}, {'Г', "g"},
        {'Д', "d"}, {'Е', "e"}, {'Ё', "yo"}, {'Ж', "zh"},
        {'З', "z"}, {'И', "i"}, {'Й', "y"}, {'К', "k"},
        {'Л', "l"}, {'М', "m"}, {'Н', "n"}, {'О', "o"},
        {'П', "p"}, {'Р', "r"}, {'С', "s"}, {'Т', "t"},
        {'У', "u"}, {'Ф', "f"}, {'Х', "kh"}, {'Ц', "ts"},
        {'Ч', "ch"}, {'Ш', "sh"}, {'Щ', "shch"}, {'Ъ', ""},
        {'Ы', "y"}, {'Ь', "`"}, {'Э', "e"}, {'Ю', "yu"},
        {'Я', "ya"}
    };

        var result = new StringBuilder();
        foreach (char c in text)
        {
            if (translit.TryGetValue(c, out string translitChar))
                result.Append(translitChar);
            else
                result.Append(c);
        }
        return result.ToString();
    }
    public List<Hero> GetAllHeroes()
    {
        var heroes = new List<Hero>();
        foreach (string Name in HeroesFiles.Keys)
        {
            Hero hero = GetHero(Name);
            if (hero != null)
                heroes.Add(hero);
        }
        return heroes;
    }
}