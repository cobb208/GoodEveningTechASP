using System;
namespace MDEngine.Tags
{
	public class BlockQuoteTag : ITag
	{
		private bool _isBlockQuote = false;

		public BlockQuoteTag()
		{
		}


		public string Create()
        {
			if(!_isBlockQuote)
            {
				_isBlockQuote = true;
				return "<blockquote>";
            }

			return "";
        }

		public string Close()
        {
			if (_isBlockQuote)
			{
				_isBlockQuote = false;
				return "</blockquote>";
			}
			return "";
        }

		public bool IsActive() => _isBlockQuote;
	}
}

