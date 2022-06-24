using System;
using System.Text.RegularExpressions;
namespace MDEngine.Tags
{
	public class AnchorTag
	{
		private string _inputString;

		public AnchorTag(string InputString)
		{
			_inputString = InputString;
		}

		public string Create(ref int i)
		{
			string anchorTagString = "";

			int tempI = i;

			Regex rx = new(@"[\[](.*)[\]][(](.*)[)]");

			while (_inputString[tempI] != '\n')
			{
				anchorTagString += _inputString[tempI];
				tempI++;
			}

			var matches = rx.Matches(anchorTagString);

			if (matches.Count > 0 && matches[0].Groups.Count > 1)
			{

				var title = matches[0].Groups[1].Value;
				var anchor = matches[0].Groups[2].Value;


				if (title == String.Empty || anchor == String.Empty)
				{
					return String.Empty;
				}

				i = tempI;

				return $"<a href='{anchor}'>{title}</a>";
			}

			return String.Empty;

		}

	}
}

