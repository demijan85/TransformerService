using System.Globalization;
using System.Text;
using Transformer.Api.Attributes;

namespace Transformer.Api.Services.Implementations;

[Transformer("Group1", "CyrillicGreekToLatin")]
public class CyrillicGreekToLatinTransformer : BaseTransformer
{
    protected override string PerformTransformation(string input, Dictionary<string, string> parameters)
    {
        var normalizedString = input.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);

            if (unicodeCategory == UnicodeCategory.NonSpacingMark)
            {
                continue;
            }

            stringBuilder.Append(ConvertChar(c));
        }

        var result = stringBuilder.ToString();
        return result;
    }

    private static string ConvertChar(char c)
    {
        var charMap = new Dictionary<char, string>
        {
            ['А'] = "A", ['Б'] = "B", ['В'] = "V", ['Г'] = "G",
            ['Д'] = "D", ['Е'] = "E", ['Ё'] = "Yo", ['Ж'] = "Zh",
            ['З'] = "Z", ['И'] = "I", ['Й'] = "Y", ['К'] = "K",
            ['Л'] = "L", ['М'] = "M", ['Н'] = "N", ['О'] = "O",
            ['П'] = "P", ['Р'] = "R", ['С'] = "S", ['Т'] = "T",
            ['У'] = "U", ['Ф'] = "F", ['Х'] = "Kh", ['Ц'] = "Ts",
            ['Ч'] = "Ch", ['Ш'] = "Sh", ['Щ'] = "Shch", ['Ъ'] = "",
            ['Ы'] = "Y", ['Ь'] = "", ['Э'] = "E", ['Ю'] = "Yu",
            ['Я'] = "Ya",
            
            ['а'] = "a", ['б'] = "b", ['в'] = "v", ['г'] = "g",
            ['д'] = "d", ['е'] = "e", ['ё'] = "yo", ['ж'] = "zh",
            ['з'] = "z", ['и'] = "i", ['й'] = "y", ['к'] = "k",
            ['л'] = "l", ['м'] = "m", ['н'] = "n", ['о'] = "o",
            ['п'] = "p", ['р'] = "r", ['с'] = "s", ['т'] = "t",
            ['у'] = "u", ['ф'] = "f", ['х'] = "kh", ['ц'] = "ts",
            ['ч'] = "ch", ['ш'] = "sh", ['щ'] = "shch", ['ъ'] = "",
            ['ы'] = "y", ['ь'] = "", ['э'] = "e", ['ю'] = "yu",
            ['я'] = "ya",
            
            ['Α'] = "A", ['Β'] = "V", ['Γ'] = "G", ['Δ'] = "D",
            ['Ε'] = "E", ['Ζ'] = "Z", ['Η'] = "I", ['Θ'] = "Th",
            ['Ι'] = "I", ['Κ'] = "K", ['Λ'] = "L", ['Μ'] = "M",
            ['Ν'] = "N", ['Ξ'] = "X", ['Ο'] = "O", ['Π'] = "P",
            ['Ρ'] = "R", ['Σ'] = "S", ['Τ'] = "T", ['Υ'] = "Y",
            ['Φ'] = "F", ['Χ'] = "Ch", ['Ψ'] = "Ps", ['Ω'] = "O",

            ['α'] = "a", ['β'] = "v", ['γ'] = "g", ['δ'] = "d",
            ['ε'] = "e", ['ζ'] = "z", ['η'] = "i", ['θ'] = "th",
            ['ι'] = "i", ['κ'] = "k", ['λ'] = "l", ['μ'] = "m",
            ['ν'] = "n", ['ξ'] = "x", ['ο'] = "o", ['π'] = "p",
            ['ρ'] = "r", ['σ'] = "s", ['ς'] = "s", ['τ'] = "t",
            ['υ'] = "y", ['φ'] = "f", ['χ'] = "ch", ['ψ'] = "ps",
            ['ω'] = "o",
        };

        return charMap.TryGetValue(c, out var convertChar) 
            ? convertChar 
            : c.ToString();
    }
}