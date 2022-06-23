namespace MDEngine;

public class UnorderedListTag
{
    private readonly string _inputString;

    private bool _isInList = false;

    public bool IsInList => _isInList;

    public UnorderedListTag(string inputString)
    {
        _inputString = inputString;
    }

    public string GenerateList(ref int indexPosition)
    {
        var returnList = "<ul>";

        bool isListItem = false;

        for (var i = indexPosition; i < _inputString.Length; i++)
        {
            if (_inputString[i] == '\n')
            {
                returnList += "</li>";
                isListItem = false;
                continue;
            }

            if (_inputString[i] == '\n' && _inputString[i + 1] == '\n')
            {
                returnList += "</ul>";
                break;
            }

            if (_inputString[i] == '-')
            {
                returnList += "<li>";
                isListItem = true;
                continue;
            }

            if (isListItem)
            {
                returnList += _inputString[i];
            }

            indexPosition = i;
        }

        return returnList;
    }
}