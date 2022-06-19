namespace MDEngine.Tags;

public class ItalicTag : ITag
{
    private bool _isItalic;

    public ItalicTag()
    {
        _isItalic = false;
    }

    public string Toggle()
    {
        if (_isItalic)
        {
            _isItalic = !_isItalic;
            return "</em>";
        }
        _isItalic = !_isItalic;
        return "<em>";
    }
    
}