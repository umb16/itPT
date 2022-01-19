using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WarpedbConvertor
{
    /*private static string[] translit = { "a","b","v","g","d","e","yo","zh","z","i","j","k","l","m","n","o","p","r","s","t","u",
                                         "f","h","c","ch","sh","shh","tvz","y","mjz","je","ju","ja"};*/
    private static Dictionary<char, string[]> _allSymbols = new Dictionary<char, string[]>()
    {
        { 'à', new string[]{"a"} },
        { 'á', new string[]{"b"} },
        { 'â', new string[]{"v"} },
        { 'ã', new string[]{"g"} },
        { 'ä', new string[]{"d"} },
        { 'å', new string[]{"e"} },
        { '¸', new string[]{"yo"} },
        { 'æ', new string[]{"zh"} },
        { 'ç', new string[]{"z"} },
        { 'è', new string[]{"i"} },
        { 'é', new string[]{"j"} },
        { 'ê', new string[]{"k"} },
        { 'ë', new string[]{"l"} },
        { 'ì', new string[]{"m"} },
        { 'í', new string[]{"n"} },
        { 'î', new string[]{"o"} },
        { 'ï', new string[]{"p"} },
        { 'ð', new string[]{"r"} },
        { 'ñ', new string[]{"s"} },
        { 'ò', new string[]{"t"} },
        { 'ó', new string[]{"u"} },
        { 'ô', new string[]{"f"} },
        { 'õ', new string[]{"h"} },
        { 'ö', new string[]{"c"} },
        { '÷', new string[]{"ch"} },
        { 'ø', new string[]{"sh"} },
        { 'ù', new string[]{"shh"} },
        { 'ú', new string[]{"tvz"}},
        { 'û', new string[]{"y"}},
        { 'ü', new string[]{"mjz"}},
        { 'ý', new string[]{"je"}},
        { 'þ', new string[]{"ju"}},
        { 'ÿ', new string[]{"ja"}},
        { '_', new string[]{"cursor"} },
        { 'H', new string[]{"hand1","hand2"}},
        { 'M', new string[]{"moon1","moon2"}},
        { ' ', new string[]{"space"}},
        { ',', new string[]{","}},
        { '.', new string[]{"."}},
        { ':', new string[]{"doubledot"}},
        { 'D', new string[]{"drop1","drop2"}},
        { 'h', new string[]{"heart1","heart2"}},
        { 'S', new string[]{"snow1","snow2"}},
        { 's', new string[]{"star1","star2"}},
        { 'E', new string[]{"energy1","energy2"}},
        { '|', new string[]{"brick2"}},
        { 'i', new string[]{"brick1"}},
        { 'l', new string[]{"brick3"}},
        { 'b', new string[]{"brick4"}},
        { 'B', new string[]{"brick5"}},
    };

    public static string[] ConvertStringToKeys(string value)
    {
        List<string> result = new List<string>();
        var chars = value.ToCharArray();
        foreach (var item in chars)
        {
           var x = ConvertCyrToTranslit(item);
            if (x != null)
                result.AddRange(x);
        }
        return result.ToArray();
    }

    public static string[] ConvertCyrToTranslit(char character)
    {
        if (_allSymbols.TryGetValue(character, out var result))
            return result;
        return null;
    }

    public static string[] ConvertNumberToKeys(int number)
    {
        List<string> result = new List<string>();
        List<int> numbers = new List<int>();
        if (number == 0)
            numbers.Add(0);
        while (number > 0)
        {
            numbers.Add(number % 10);
            number /= 10;
        }
        numbers.Reverse();
        if (numbers.Count % 2 == 1)
        {
            result.Add(numbers[0].ToString());
            numbers.RemoveAt(0);
        }
        for (int i = 0; i < numbers.Count; i += 2)
        {
            int n1 = numbers[i];
            int n2 = numbers[i + 1];
            if (n1 == 0)
                n1 = -1;
            if (n2 == 0)
                n2 = -1;
            result.Add($"{n1}0");
            result.Add($"0{n2}");
        }
        return result.ToArray();
    }

    public static string GenerateBar(int lenght, float max, char[] elements, float value)
    {
        string result = "";
        float step = max / lenght;

        for (int i = 0; i < lenght; i++)
        {
            if (i * step >= value)
            {
                result+=" ";
            }
            else
            {
                int elementIndex = Mathf.Min(elements.Length - 1, Mathf.FloorToInt((value - i * step) / step * elements.Length));

                result += elements[elementIndex];
            }
        }
        return result;
    }

}
