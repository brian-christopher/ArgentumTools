using System.Text.Json;
using InitParser.Models;
using Shared;

Directory.CreateDirectory("in");
Directory.CreateDirectory("out");

var jsonOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = false
};

CargarAnimArmas();
CargarAnimEscudos();
CargarCabezas();
CargarCascos();
CargarCuerpos();
CargarFxs();

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