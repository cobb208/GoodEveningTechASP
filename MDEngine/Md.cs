using System.Text;
using MDEngine.Tags;

namespace MDEngine;

public class Md
{

    private readonly string _inputFilePath;
    private readonly string _outputFilePath;

    private readonly BlockQuoteTag _blockQuoteTag;
    private readonly UnorderedListTag _unorderedListTag;
    private readonly HeaderTag _headerTag;
    private readonly ParagraphTag _paragraphTag;
    private readonly ItalicTag _italicTag;
    private readonly BoldTag _boldTag;
    private readonly AnchorTag _anchorTag;
    private readonly HorizontalRuleTag _horizontalRuleTag;
    private readonly OrderedListTag _orderedListTag;
    private int _index;
    private readonly string _markdownString;
    private string _result;


    public Md(string inputFilePath, string outputFilePath)
    {
        _inputFilePath = inputFilePath;
        _outputFilePath = outputFilePath;
        _blockQuoteTag = new BlockQuoteTag();
        _paragraphTag = new ParagraphTag();
        _italicTag = new ItalicTag();
        _boldTag = new BoldTag();
        _horizontalRuleTag = new HorizontalRuleTag();
        _index = 0;
        _markdownString = CollectStringFromFile();
        _unorderedListTag = new UnorderedListTag(_markdownString);
        _headerTag = new HeaderTag(_markdownString);
        _anchorTag = new AnchorTag(_markdownString);
        _orderedListTag = new OrderedListTag(_markdownString);
        _result = "<div class='container'>\n";
    }



    public void GenerateMd()
    {
        ReadMd();
        WriteHtmlToFile();
    }
    
    private void ReadMd()
    {
        for (_index = 0; _index < _markdownString.Length; _index++)
        {
            if (!CheckAndValidateIfBlockListHeader(_markdownString[_index])) continue;
            //_result += _orderedListTag.Create(ref _index);
            GenerateParagraphTag();
            if (!CheckAndValidateIfAnchorBoldItalic(_markdownString[_index])) continue;
            _result += _markdownString[_index];
        }
        _result += "</div>";
    }
    
    private bool CheckAndValidateIfBlockListHeader(char character)
    {
        if (char.IsDigit(character))
        {
            // need to fix to ignore numbers.
            var tempResult = _orderedListTag.Create(ref _index);
            _result += _orderedListTag.Create(ref _index);
            return false;
        }
        
        switch (character)
        {
            case '>':
                if (!CheckAndValidateIfBlockQuote()) return false;
                break;
            case '-':
                if (!CheckAndValidateIfHorizontalRule()) return false;
                _result += _unorderedListTag.Create();
                return false;
            case '#':
                _result += _headerTag.Create(ref _index);
                break;
        }

        return true;
    }

    private bool CheckAndValidateIfAnchorBoldItalic(char character)
    {
        switch (character)
        {
            case '[':
                if (!CheckAndValidateIfAnchor()) return false;
                break;
            case '*':
                if (!CheckAndValidateIfBold()) return false;
                if (CheckAndValidateIfItalic()) return false;
                break;
            case '\n':
                CloseOutTag(_paragraphTag);
                CloseOutTag(_blockQuoteTag);
                CloseOutTag(_headerTag);
                CloseOutUnorderedListTag();
                CloseOutOrderedListTag();
                break;
        }

        return true;
    }
    

    private void GenerateParagraphTag()
    {
        if (!_headerTag.IsActive()
            && !_unorderedListTag.IsActive()
            && !_paragraphTag.IsActive()
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
        catch
        {
            // ignored
        }
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
        catch
        {
            // ignored
        }
        return true;
    }

    private bool CheckAndValidateIfAnchor()
    {
        var checkVal = _anchorTag.Create(ref _index);
        if(checkVal == string.Empty)
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
                if (_boldTag.IsActive())
                {
                    _result += _boldTag.Close();
                    _index++;
                    return false;
                }

                _result += _boldTag.Create();
                _index++;
                return false;
            }
        }
        catch
        {
            // ignored
        }

        return true;
    }

    

    private bool CheckAndValidateIfItalic()
    {
        if (_italicTag.IsActive())
        {
            _result += _italicTag.Close();
            return false;
        }

        _result += _italicTag.Create();
        return false;
    }

    private void CloseOutTag(ITag tag)
    {
        if (tag.IsActive()) _result += tag.Close();
    }
    private void CloseOutUnorderedListTag()
    {
        if (_unorderedListTag.IsActive())
        {
            _result += _unorderedListTag.CloseLi();
        }
        
        try
        {
            if (_markdownString[_index + 1] == '\n' && _unorderedListTag.IsActive())
            {
                _result += _unorderedListTag.CloseList();
            }
        }
        catch
        {
            // ignored
        }
    }
    
    private void CloseOutOrderedListTag()
    {
        if (_orderedListTag.IsActive())
        {
            _result += _orderedListTag.CloseLi();
        }
        
        try
        {
            if (_markdownString[_index + 1] == '\n' && _orderedListTag.IsActive())
            {
                _result += _orderedListTag.CloseList();
            }
        }
        catch
        {
            // ignored
        }
    }

    private string CollectStringFromFile()
    {
        var fs = File.Open(_inputFilePath, FileMode.Open);
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



    private void WriteHtmlToFile()
    {
        var fs = File.Create(_outputFilePath);
        var html = new UTF8Encoding(true)
            .GetBytes(_result);
        fs.Write(html, 0, html.Length);

        fs.Close();
    }
}