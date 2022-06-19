namespace MDEngine;

public class TagSetter
{
    private string InputString { get; set; }
    private int HeaderCounter { get; set; } = 0;

    private bool _headerFlag = false;

    private bool _tagSet = false;

    public bool HeaderFlag => _headerFlag;

    public bool TagSet => _tagSet;

    public TagSetter()
    {
        InputString = "";
    }

    public TagSetter(string inputString)
    {
        InputString = inputString;
    }

    public string OpenParagraphTag(ref int indexPosition)
    {
        for (var i = indexPosition; i < InputString.Length; i++)
        {
            if (InputString[i] == '#')
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
        
        Console.WriteLine("adding a closing p tag");
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