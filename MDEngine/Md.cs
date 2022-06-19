using System.Text;
using MDEngine.Tags;

namespace MDEngine;

public class Md
{

    private readonly string _inputFilePath;
    private readonly string _outputFilePath;

    public Md(string inputFilePath, string outputFilePath)
    {
        _inputFilePath = inputFilePath;
        _outputFilePath = outputFilePath;
    }

    enum SentenceCase
    {
        Italic,
        Bold,
        ItalicBold
    }

    public void GenerateMd()
    {
        var result = "<div class=\"container\">\n";
        using var fs = File.Open(_inputFilePath, FileMode.Open);
        var b = new byte[fs.Length];
        var temp = new UTF8Encoding(true);

        ITag italicTag = new ItalicTag();
        ITag boldTag = new BoldTag();

        while (fs.Read(b, 0, b.Length) > 0)
        {
            var inputString = temp.GetString(b);

            ParagraphTagSetter paragraphTagSetter = new(inputString);
            UnorderedListTag unorderedListTag = new(inputString);

            for (var i = 0; i < inputString.Length; i++)
            {
                if (!paragraphTagSetter.TagSet)
                {
                    result += paragraphTagSetter.OpenParagraphTag(ref i);
                    continue;
                }
                
                
                
                if (inputString[i] == '-')
                {
                    result += unorderedListTag.GenerateList(ref i);
                    continue;
                }


                switch (inputString[i])
                {
                    case '*':
                        var sentenceCase = SetSentenceCase(inputString, i);
                        
                        switch (sentenceCase)
                        {
                            case SentenceCase.Italic:
                                result += italicTag.Toggle();
                                continue;
                            case SentenceCase.Bold:
                                result += boldTag.Toggle();
                                i++;
                                continue;
                            case SentenceCase.ItalicBold:
                                result += italicTag.Toggle();
                                result += boldTag.Toggle();
                                i += 2;
                                continue;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    case '\n':
                        result += paragraphTagSetter.CloseParagraphTag();
                        result += '\n';
                        break;
                    
                }

                result += inputString[i];
            }

            result += paragraphTagSetter.CloseParagraphTag();
        }

        result += "\n</div>";
        WriteToFile(result);
    }

    private void WriteToFile(string htmlString)
    {
        using var fs = File.Create(_outputFilePath);
        var html = new UTF8Encoding(true)
            .GetBytes(htmlString);
        fs.Write(html, 0, html.Length);
    }

    private static SentenceCase SetSentenceCase(string inputString, int index)
    {
        try
        {
            if (inputString[index + 1] == '*')
            {
                return SentenceCase.Bold;
            }

            if (inputString[index + 2] == '*')
            {
                return SentenceCase.ItalicBold;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error processing: " + e);
            return SentenceCase.Italic;
        }
        return SentenceCase.Italic;
    }
}