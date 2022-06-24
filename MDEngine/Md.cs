using System.Text;
using System.Text.RegularExpressions;
using MDEngine.Tags;

namespace MDEngine;

public class Md
{

    private readonly string _inputFilePath;
    private readonly string _outputFilePath;

    private BlockQuoteTag _blockQuoteTag;
    private ListTag _listTag;
    private HeaderTag _headerTag;
    private ParagraphTag _paragraphTag;
    private ItalicTag _italicTag;
    private BoldTag _boldTag;
    private AnchorTag _anchorTag;
    private HorizontalRuleTag _horizontalRuleTag;
    private int _index;
    private string _markdownString;
    private string _result;
    private bool _shouldContinue;


    public Md(string inputFilePath, string outputFilePath)
    {
        _inputFilePath = inputFilePath;
        _outputFilePath = outputFilePath;
        _blockQuoteTag = new();
        _listTag = new();
        _paragraphTag = new();
        _italicTag = new();
        _boldTag = new();
        _horizontalRuleTag = new();
        _index = 0;
        _markdownString = CollectStringFromFile();
        _headerTag = new(_markdownString);
        _anchorTag = new(_markdownString);
        _result = "<div class='container'>\n";
        _shouldContinue = false;

    }



    public void NewGenerateMd()
    {
        for (_index = 0; _index < _markdownString.Length; _index++)
        {
            _shouldContinue = true;

            switch(_markdownString[_index])
            {
                case '>':
                    _shouldContinue = CheckAndValidateIfBlockQuote();
                    if (!_shouldContinue) continue;
                    break;

                case '-':
                    _shouldContinue = CheckAndValidateIfHorizontalRule();
                    if (!_shouldContinue) continue;

                    _result += _listTag.Create();
                    continue;

                case '#':
                    _result += _headerTag.Create(ref _index);
                    break;
            }

            GenerateParagraphTag();


            switch(_markdownString[_index])
            {
                case '[':
                    

                    _shouldContinue = CheckAndValidateIfAnchor();
                    if (!_shouldContinue) continue;
                    break;

                case '*':
                    _shouldContinue = CheckAndValidateIfBold();
                    if (!_shouldContinue) continue;
                    _shouldContinue = CheckAndValidateIfItalic();
                    if (!_shouldContinue) continue;
                    break;


                case '\n':
                    CloseOutParagraphTag();
                    CloseOutBlockQuoteTag();
                    CloseOutHeaderTag();
                    CloseOutUnorderedListTag();
                    break;
            }

            _result += _markdownString[_index];

        }

        _result += "</div>";

        WriteToFile();
    }


    private void GenerateParagraphTag()
    {
        if (!_headerTag.IsHeader
            && !_listTag.IsList
            && !_paragraphTag.IsParagraph
            && (_markdownString[_index] != '\n')
        )
        {
            _result += _paragraphTag.Create();
        }
    }

    private bool CheckAndValidateIfBlockQuote()
    {
        if (_index == 0)
        {
            _result += _blockQuoteTag.Create();
            return false;
        }
        try
        {
            if (_markdownString[_index - 1] == '\n')
            {
                _result += _blockQuoteTag.Create();
                return false;
            }
        }
        catch { }

        return true;
    }

    private bool CheckAndValidateIfHorizontalRule()
    {
        try
        {
            if (_markdownString[_index + 1] == '-' && _markdownString[_index + 2] == '-')
            {
                _result += _horizontalRuleTag.Create();
                _index += 3;
                return false;
            }
        }
        catch { }

        return true;
    }

    private bool CheckAndValidateIfAnchor()
    {
        var checkVal = _anchorTag.Create(ref _index);
        if(checkVal == String.Empty)
        {
            _result += _markdownString[_index];
            return false;
        }
        _result += checkVal;
        return true;
    }

    private bool CheckAndValidateIfBold()
    {
        try
        {
            if (_markdownString[_index + 1] == '*')
            {
                if (_boldTag.IsBold)
                {
                    _result += _boldTag.Close();
                    _index++;
                    return false;
                }
                else
                {
                    _result += _boldTag.Create();
                    _index++;
                    return false;
                }
            }
        }
        catch { }

        return true;
    }

    private bool CheckAndValidateIfItalic()
    {
        if (_italicTag.IsItalic)
        {
            _result += _italicTag.Close();
            return false;
        }

        _result += _italicTag.Create();
        return false;
    }

    private void CloseOutParagraphTag()
    {
        if (_paragraphTag.IsParagraph)
        {
            _result += _paragraphTag.Close();
        }
    }

    private void CloseOutBlockQuoteTag()
    {
        if (_blockQuoteTag.IsBlockQuote)
        {
            _result += _blockQuoteTag.Close();
        }
    }

    private void CloseOutHeaderTag()
    {
        if (_headerTag.IsHeader)
        {
            _result += _headerTag.Close();
        }
    }

    private void CloseOutUnorderedListTag()
    {
        if (_listTag.IsList)
        {
            _result += _listTag.CloseLi();
        }

        try
        {
            if (_markdownString[_index + 1] == '\n' && _listTag.IsList)
            {
                _result += _listTag.CloseUl();
            }
        }
        catch { }
    }


    private string CollectStringFromFile()
    {
        using var fs = File.Open(_inputFilePath, FileMode.Open);
        var b = new byte[fs.Length];

        var returnString = "";

        var temp = new UTF8Encoding(true);

        while (fs.Read(b, 0, b.Length) > 0)
        {
            returnString = temp.GetString(b);
        }

        fs.Close();


        return returnString;
    }



    private void WriteToFile()
    {
        using var fs = File.Create(_outputFilePath);
        var html = new UTF8Encoding(true)
            .GetBytes(_result);
        fs.Write(html, 0, html.Length);

        fs.Close();
    }
}