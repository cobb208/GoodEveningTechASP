using System.Text;
using System.Text.RegularExpressions;
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


    public void NewGenerateMd()
    {
        var result = "<div class='container'>\n";
        using var fs = File.Open(_inputFilePath, FileMode.Open);
        var b = new byte[fs.Length];
        var temp = new UTF8Encoding(true);

        while(fs.Read(b, 0, b.Length) > 0)
        {
            var inputString = temp.GetString(b);

            BlockQuoteTag blockQuoteTag = new();
            ListTag listTag = new();
            HeaderTag headerTag = new(inputString);
            ParagraphTag paragraphTag = new();
            ItalicTag italicTag = new();
            BoldTag boldTag = new();
            AnchorTag anchorTag = new(inputString);

            for(var i = 0; i < inputString.Length; i++)
            {

                if(inputString[i] == '>')
                {
                    if(i == 0)
                    {
                        result += blockQuoteTag.Create();
                        continue;
                    }
                    if(inputString[i - 1] == '\n')
                    {
                        result += blockQuoteTag.Create();
                        continue;
                    }
                    
                }

                if(inputString[i] == '-')
                {
                    result += listTag.Create();
                    continue;
                }

                if(inputString[i] == '#')
                {
                    result += headerTag.Create(ref i);

                }

                if(!headerTag.IsHeader
                    && !listTag.IsList
                    && !paragraphTag.IsParagraph
                    && (inputString[i] != '\n'))
                {
                    result += paragraphTag.Create();
                }

                if(inputString[i] == '[')
                {

                    var checkVal = anchorTag.Create(ref i);

                    if(checkVal == String.Empty)
                    {
                        result += inputString[i];
                        continue;
                    } else
                    {
                        result += checkVal;
                    }

                }



                if(inputString[i] == '*')
                {
                    try
                    {
                        if (inputString[i + 1] == '*')
                        {
                            if (boldTag.IsBold)
                            {
                                result += boldTag.Close();
                                i++;
                                continue;
                            }
                            else
                            {
                                result += boldTag.Create();
                                i++;
                                continue;
                            }

                        }
                    }
                    catch { }


                    if(italicTag.IsItalic)
                    {
                        result += italicTag.Close();
                        continue;
                    } else
                    {
                        result += italicTag.Create();
                        continue;
                    }

                }

                if(inputString[i] == '\n')
                {

                    if(paragraphTag.IsParagraph)
                    {
                        result += paragraphTag.Close();
                    }

                    if (blockQuoteTag.IsBlockQuote)
                    {
                        result += blockQuoteTag.Close();
                    }

                    if (headerTag.IsHeader)
                    {
                        result += headerTag.Close();
                    }


                    if(listTag.IsList)
                    {
                        result += listTag.CloseLi();
                    }
                    try
                    {
                        if (inputString[i + 1] == '\n' && listTag.IsList)
                        {
                            result += listTag.CloseUl();
                        }
                    }
                    catch { }

                }

                result += inputString[i];
            }
        }

        result += "</div>";

        WriteToFile(result);
    }


    private void WriteToFile(string htmlString)
    {
        using var fs = File.Create(_outputFilePath);
        var html = new UTF8Encoding(true)
            .GetBytes(htmlString);
        fs.Write(html, 0, html.Length);
    }
}