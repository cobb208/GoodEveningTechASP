namespace MDEngine;

public class ParagraphTagSetter
{
    private readonly string _inputString;
    private int HeaderCounter { get; set; } = 0;

    private bool _headerFlag = false;

    private bool _tagSet = false;

    public bool HeaderFlag => _headerFlag;

    public bool TagSet => _tagSet;

    public ParagraphTagSetter(string inputString)
    {
        _inputString = inputString;
    }

    public string OpenParagraphTag(ref int indexPosition)
    {
        for (var i = indexPosition; i < _inputString.Length; i++)
        {
            if (_inputString[i] == '#')
            {
                HeaderCounter++;
                _headerFlag = true;
            }
            else
            {
                indexPosition = i;
                break;
            }
        }

        if (_headerFlag && _tagSet == false)
        {
            _tagSet = true;
            return $"<h{HeaderCounter}>";
        }

        if (!_tagSet)
        {
            _tagSet = true;
            return "<p>";
        }

        return "";
    }

    public string CloseParagraphTag()
    {
        if (_headerFlag)
        {
            var temp = HeaderCounter;
            ResetHeaderValues();
            return $"</h{temp}>";
        }
        ResetHeaderValues();
        return "</p>";
    }

    private void ResetHeaderValues()
    {
        _headerFlag = false;
        _tagSet = false;
        HeaderCounter = 0;
    }
    
}