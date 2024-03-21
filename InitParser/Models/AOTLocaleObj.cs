namespace InitParser.Models;

public class AOTLocaleObj
{
    public string Name { get; set; } = string.Empty;
    public string Desc { get; set; } = string.Empty;
    public int Type { get; set; }
    public int GrhIndex { get; set; }
    public int MinDef { get; set; }
    public int MaxDef { get; set; }
    public int MinHit { get; set; }
    public int MaxHit { get; set; }
    public string CreaLuz { get; set; } = string.Empty;
    public int RangoLuz { get; set; }
    public int Snd1 { get; set; } // Snd Equipar
    public int Snd2 { get; set; } // Snd Golpe
    public int Snd3 { get; set; } // Snd fallas
    public int Nivel { get; set; }
    public int CreaParticulaPiso { get; set; }
    public int QuitaStamina { get; set; }
    public List<int> ClasesProhibidas { get; set; } = [];
    public List<int> RazasProhibidas { get; set; } = [];
    public int Donador { get; set; }
    public int Faccion { get; set; }
    public int Aura { get; set; }
}
