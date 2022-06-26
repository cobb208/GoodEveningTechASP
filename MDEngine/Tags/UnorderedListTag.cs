using System;
using System.Text.RegularExpressions;

namespace MDEngine.Tags
{
	public class UnorderedListTag : ITag
	{
		private readonly string _inputString;
		private bool _isActive;

		public UnorderedListTag(string inputString)
		{
			_inputString = inputString;
		}

		public string Create()
        {
			var result = "";
			if (_isActive) return result + "<li>\n";
			_isActive = true;
			result += "<ul>\n";
			return result + "<li>\n";

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
			return "\n</ul>";
        }

		public bool IsActive() => _isActive;
	}
}

