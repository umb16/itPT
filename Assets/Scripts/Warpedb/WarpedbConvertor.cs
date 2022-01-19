using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WarpedbConvertor
{
    /*private static string[] translit = { "a","b","v","g","d","e","yo","zh","z","i","j","k","l","m","n","o","p","r","s","t","u",
                                         "f","h","c","ch","sh","shh","tvz","y","mjz","je","ju","ja"};*/
    private static Dictionary<char, string[]> _allSymbols = new Dictionary<char, string[]>()
    {
        { '�', new string[]{"a"} },
        { '�', new string[]{"b"} },
        { '�', new string[]{"v"} },
        { '�', new string[]{"g"} },
        { '�', new string[]{"d"} },
        { '�', new string[]{"e"} },
        { '�', new string[]{"yo"} },
        { '�', new string[]{"zh"} },
        { '�', new string[]{"z"} },
        { '�', new string[]{"i"} },
        { '�', new string[]{"j"} },
        { '�', new string[]{"k"} },
        { '�', new string[]{"l"} },
        { '�', new string[]{"m"} },
        { '�', new string[]{"n"} },
        { '�', new string[]{"o"} },
        { '�', new string[]{"p"} },
        { '�', new string[]{"r"} },
        { '�', new string[]{"s"} },
        { '�', new string[]{"t"} },
        { '�', new string[]{"u"} },
        { '�', new string[]{"f"} },
        { '�', new string[]{"h"} },
        { '�', new string[]{"c"} },
        { '�', new string[]{"ch"} },
        { '�', new string[]{"sh"} },
        { '�', new string[]{"shh"} },
        { '�', new string[]{"tvz"}},
        { '�', new string[]{"y"}},
        { '�', new string[]{"mjz"}},
        { '�', new string[]{"je"}},
        { '�', new string[]{"ju"}},
        { '�', new string[]{"ja"}},
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
