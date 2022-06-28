using System;
namespace MDEngine.Tags.InlineElements
{
	public class ItalicTag : ITag
	{
		private bool _isItalic;
		
		public string Create()
        {
			_isItalic = true;
			return "<em>";
        }

		public string Close()
        {
			_isItalic = false;
			return "</em>";
        }

		public bool IsActive() => _isItalic;

	}
}

