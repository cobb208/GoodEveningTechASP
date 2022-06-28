using System;
namespace MDEngine.Tags.InlineElements
{
	public class HeaderTag : ITag
	{
		private readonly string _inputString;
		private int _headerCounter = 1;
		private bool _isHeader;

		public HeaderTag(string inputString)
		{
			_inputString = inputString;
		}


        public string Create(ref int i)
        {
			_headerCounter = 0;
			while(_inputString[i] == '#')
            {
				_headerCounter++;
				i++;
            }

			_isHeader = true;
			return $"<h{_headerCounter}>\n";
        }

        public string Create()
        {
	        return "<h1>";
        }

        public string Close()
        {
			var tempHeader = _headerCounter;
			_headerCounter = 0;
			_isHeader = false;
			return $"\n</h{tempHeader}>";
        }

        public bool IsActive() => _isHeader;
        
	}
}

