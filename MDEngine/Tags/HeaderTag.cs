using System;
namespace MDEngine.Tags
{
	public class HeaderTag : ITag
	{
		private readonly string _inputString;
		private int _headerCounter;
		private bool _isHeader;

		public HeaderTag(string inputString)
		{
			_inputString = inputString;
			_headerCounter = 1;
			_isHeader = false;
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
			int tempHeader = _headerCounter;
			_headerCounter = 0;
			_isHeader = false;
			return $"\n</h{tempHeader}>";
        }

        public bool IsActive() => _isHeader;
        
	}
}

