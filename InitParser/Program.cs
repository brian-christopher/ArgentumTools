using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using InitParser.Models;
using Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

Directory.CreateDirectory("in");
Directory.CreateDirectory("out");

var jsonOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = false,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};

//CargarAnimArmas();
//CargarAnimEscudos();
//CargarCabezas();
//CargarCascos();
//CargarCuerpos();
//CargarFxs();
CargarLocaleObject();

int ParseIntOrDefault(string valor, int valorPorDefecto)
{
    int resultado;
    return int.TryParse(valor, out resultado) ? resultado : valorPorDefecto;
}


/*OBJData(i).Name & "|" & OBJData(i).desc & "|" & OBJData(i).GrhIndex & "|" & OBJData(i).tipe & "|" & OBJData(i).MaxDef & "|" & OBJData(i).MinDef & 
 * "|" & OBJData(i).MaxHit & "|" & OBJData(i).MinHit & "|" & OBJData(i).CreaLuz & "|" & OBJData(i).RangoLuz & "|" & OBJData(i).Snd1 & "|" & OBJData(i).Snd2 & "|" & OBJData(i).Snd3 & 
 * "|" & OBJData(i).Nivel & "|" & OBJData(i).CreaParticulaPiso & "|" & OBJData(i).QuitarStamina & "|" & OBJData(i).ClasePROHIBIDA & "|" & OBJData(i).RazaProhibida 
 * & "|" & OBJData(i).Donador & "|" & OBJData(i).faccion & "|" & OBJData(i).aura)
 */
void CargarLocaleObject()
{
    AOTLocaleObj ParseLocaleObj(string str)
    {
        List<int> StrToList(string list)
        {
            try
            {
                var ints = list.Split(',')
                    .Select(int.Parse)
                    .ToList();

                if (ints.Count == 1 && ints[0] == 0) return [];
                return ints;
            }
            catch 
            {
                return [];
            }
        }

        var retval = new AOTLocaleObj();
        var keys = str.Split('|');  

        retval.Name = keys[0];
        retval.Desc = keys[1];
        retval.GrhIndex = ParseIntOrDefault(keys[2], 0);
        retval.Type = ParseIntOrDefault(keys[3], 0);
        retval.MaxDef = ParseIntOrDefault(keys[4], 0);
        retval.MinDef = ParseIntOrDefault(keys[5], 0);
        retval.MaxHit = ParseIntOrDefault(keys[6], 0);
        retval.MinHit = ParseIntOrDefault(keys[7], 0);
        retval.CreaLuz = keys[8];
        retval.RangoLuz = ParseIntOrDefault(keys[9], 0);
        retval.Snd1 = ParseIntOrDefault(keys[10], 0);
        retval.Snd2 = ParseIntOrDefault(keys[11], 0);
        retval.Snd3 = ParseIntOrDefault(keys[12], 0);

        retval.Nivel = ParseIntOrDefault(keys[13], 0);
        retval.CreaParticulaPiso = ParseIntOrDefault(keys[14], 0);
        retval.QuitaStamina = ParseIntOrDefault(keys[15], 0);
        retval.ClasesProhibidas = StrToList(keys[16]);
        retval.RazasProhibidas = StrToList(keys[17]);
        retval.Donador = ParseIntOrDefault(keys[18], 0);
        retval.Faccion = ParseIntOrDefault(keys[19], 0);
        retval.Aura = ParseIntOrDefault(keys[20], 0);

        return retval;
    }


    var filename = PathList.GetPath(PathType.LocaleObject);
    var lines = File.ReadAllLines(filename, Encoding.Latin1);

    var data = lines.Select(ParseLocaleObj)
        .ToList();

    File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "out", "locale_obj_es.json"),
        JsonSerializer.Serialize(data, jsonOptions), Encoding.UTF8);
}

void CargarFxs()
{
    var filename = PathList.GetPath(PathType.Fxs);
    using var ms = new MemoryStream(File.ReadAllBytes(filename));
    using var reader = new BinaryReader(ms);
    
    ms.Position += 4 + 4 + 255;
    var count = reader.ReadInt16();
    var data = new GrhFx[count + 1];

    for (int i = 1; i <= count; i++)
    {
        data[i] = new GrhFx
        {
            GrhId = reader.ReadInt16(),
            OffsetX = reader.ReadInt16(),
            OffsetY = reader.ReadInt16()
        };
    }
    
    File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "out", "fxs.json"),
        JsonSerializer.Serialize(data, jsonOptions));
}

void CargarCuerpos()
{
    var filename = PathList.GetPath(PathType.Body);
    using var ms = new MemoryStream(File.ReadAllBytes(filename));
    using var reader = new BinaryReader(ms);

    ms.Position += 4 + 4 + 255;

    var count = reader.ReadInt16();
    var data = new GrhAnimation[count + 1];

    for (var i = 1; i <= count; i++)
    {
        data[i].Up = reader.ReadInt16();
        data[i].Right = reader.ReadInt16();
        data[i].Down = reader.ReadInt16();
        data[i].Left = reader.ReadInt16();

        data[i].OffsetX = reader.ReadInt16();
        data[i].OffsetY = reader.ReadInt16();
    }

    File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "out", "bodies.json"),
        JsonSerializer.Serialize(data, jsonOptions));
}

void CargarCascos()
{
    var filename = PathList.GetPath(PathType.Helmet);
    using var ms = new MemoryStream(File.ReadAllBytes(filename));
    using var reader = new BinaryReader(ms);

    ms.Position += 4 + 4 + 255;

    var count = reader.ReadInt16();
    var data = new GrhAnimation[count + 1];

    for (var i = 1; i <= count; i++)
    {
        data[i].Up = reader.ReadInt16();
        data[i].Right = reader.ReadInt16();
        data[i].Down = reader.ReadInt16();
        data[i].Left = reader.ReadInt16();
    }

    File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "out", "helmets.json"),
        JsonSerializer.Serialize(data, jsonOptions));
}

void CargarCabezas()
{
    var filename = PathList.GetPath(PathType.Head);
    using var ms = new MemoryStream(File.ReadAllBytes(filename));
    using var reader = new BinaryReader(ms);

    ms.Position += 4 + 4 + 255;

    var count = reader.ReadInt16();
    var data = new GrhAnimation[count + 1];

    for (var i = 1; i <= count; i++)
    {
        data[i].Up = reader.ReadInt16();
        data[i].Right = reader.ReadInt16();
        data[i].Down = reader.ReadInt16();
        data[i].Left = reader.ReadInt16();
    }

    File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "out", "heads.json"),
        JsonSerializer.Serialize(data, jsonOptions));
}

void CargarAnimArmas()
{
    var filename = PathList.GetPath(PathType.Weapon);
    var reader = new InitReader(); 
    reader.Load(filename);

    var count = reader.GetValueInt("INIT", "NumArmas");
    var data = new GrhAnimation[count + 1];

    for (var i = 1; i <= count; i++)
    { 
        data[i].Up = reader.GetValueInt($"ARMA{i}", "Dir1", 0);
        data[i].Right = reader.GetValueInt($"ARMA{i}", "Dir2", 0);
        data[i].Down = reader.GetValueInt($"ARMA{i}", "Dir3", 0);
        data[i].Left = reader.GetValueInt($"ARMA{i}", "Dir4", 0);
    }

    File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "out", "weapons.json"),
        JsonSerializer.Serialize(data, jsonOptions));
}

void CargarAnimEscudos()
{
    var filename = PathList.GetPath(PathType.Shield);
    var reader = new InitReader(); 
    reader.Load(filename);

    var count = reader.GetValueInt("INIT", "NumEscudos");
    var data = new GrhAnimation[count + 1];

    for (var i = 1; i <= count; i++)
    { 
        data[i].Up = reader.GetValueInt($"ESC{i}", "Dir1", 0);
        data[i].Right = reader.GetValueInt($"ESC{i}", "Dir2", 0);
        data[i].Down = reader.GetValueInt($"ESC{i}", "Dir3", 0);
        data[i].Left = reader.GetValueInt($"ESC{i}", "Dir4", 0);
    }

    File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "out", "shields.json"),
        JsonSerializer.Serialize(data, jsonOptions));
}