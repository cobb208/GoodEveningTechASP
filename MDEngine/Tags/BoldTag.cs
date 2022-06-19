namespace MDEngine.Tags;

public class BoldTag : ITag
{
    private bool _isBold;

    public BoldTag()
    {
        _isBold = false;
    }

    public string Toggle()
    {
        if (_isBold)
        {
            _isBold = !_isBold;
            return "</strong>";
        }

        _isBold = !_isBold;
        return "<strong>";
    }
}