using System.Diagnostics;

namespace InitParser.Models;

public enum PathType{
    Head,
    Body,
    Helmet,
    Weapon,
    Shield,
    Graphics,
    Fxs,
    LocaleObject
}
 
public static class PathList
{
    public static string GetPath(PathType type)
    {
        var filename = type switch
        {       
            PathType.Body => "Personajes.ind",
            PathType.Fxs => "Fxs.ind",
            PathType.Graphics => "Graficos3.ind",
            PathType.Head => "Cabezas.ind",
            PathType.Helmet => "Cascos.ind",
            PathType.Weapon => "Armas.dat",
            PathType.Shield => "Escudos.dat",
            PathType.LocaleObject => "locale_obj_es.ind",
            _ => throw new ArgumentException(nameof(type))
        };

        return Path.Combine(Environment.CurrentDirectory, "in", filename);
    } 
}