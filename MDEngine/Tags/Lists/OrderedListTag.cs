using System.Text.RegularExpressions;

namespace MDEngine.Tags.Lists;

public class OrderedListTag
{
    private readonly string _inputString;
    private bool _isActive;

    public OrderedListTag(string inputString)
    {
        _inputString = inputString;
    }
    
    

    public string Create(ref int index)
    {
        var regexString = "";
    
        var tempI = index;
    
        Regex rx = new(@"(\d+\.\s+)(.*)");
    
        try
        {
            while (_inputString[tempI] != '\n')
            {
                regexString += _inputString[tempI];
                tempI++;
            }
        }
        catch
        {
            // ignored
        }
    
    
        var matches = rx.Matches(regexString);

        if (matches.Count <= 0 || matches[0].Groups.Count <= 1) return string.Empty;
        if (matches[0].Groups[1].Value == "") return string.Empty;
        var returnString = "";
        if (!_isActive) returnString += "<ol>\n";
        _isActive = true;

        returnString += "<li>\n\t";

        index += 2;
        return returnString;


    }



    public string Close()
    {
        return CloseLi() + CloseList();
    }

    public string CloseLi()
    {
        return "\n</li>";
    }

    public string CloseList()
    {
        _isActive = false;
        return "\n</ol>";
    }

    public bool IsActive() => _isActive;

}