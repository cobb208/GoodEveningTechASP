using System;
namespace MDEngine.Tags
{
	public class BlockQuoteTag : ITag
	{
		private bool _isBlockQuote;

		public string Create()
		{
			if (_isBlockQuote) return "";
			_isBlockQuote = true;
			return "<blockquote>";
		}

		public string Close()
		{
			if (!_isBlockQuote) return "";
			_isBlockQuote = false;
			return "</blockquote>";
		}

		public bool IsActive() => _isBlockQuote;
	}
}

