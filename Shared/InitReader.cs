namespace Shared;

using InitDictionary = Dictionary<string, Dictionary<string, string>>;

public class InitReader
{
     private readonly InitDictionary _data = new();

     public void SetValue(string main, string key, string value)
    {
        _data[main.ToLower()][key.ToLower()] = value;
    }

    public string GetValue(string main, string key)
    {
        return _data[main.ToLower()][key.ToLower()];
    }

    public string GetValue(string main, string key, string defaultValue)
    {
        try
        {
            return GetValue(main, key);
        }
        catch
        {
            return defaultValue;
        }
    }

    public int GetValueInt(string main, string key)
    {
        return int.Parse(GetValue(main, key));
    }

    public int GetValueInt(string main, string key, int defaultValue)
    {
        try
        {
            return int.Parse(GetValue(main, key));
        }
        catch
        {
            return defaultValue; 
        }
    }

    public void Load(string path)
    {
        var lines = File.ReadAllLines(path);
        var main = string.Empty;

        foreach (var line in lines)
        {
            if (line.Length == 0)
                continue;

            if (line[0] == '\\' || line[0] == '#' || line[0] == '|')
                continue;

            if (line[0] == '[')
            {
                var index = line.IndexOf(']');
                if (index != -1)
                {
                    main = line.Substring(1, index - 1).ToLower();
                    _data.Add(main, new Dictionary<string, string>());
                }
            }
            else
            {
                var index = line.IndexOf('=');
                if (index != -1 && !string.IsNullOrEmpty(main))
                {
                    var key = line.Substring(0, index).ToLower();
                    var value = line.Substring(index + 1);

                    _data[main][key] = value;
                }
            }
        }
    }
}