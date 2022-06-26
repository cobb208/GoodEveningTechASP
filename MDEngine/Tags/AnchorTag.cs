using System;
using System.Text.RegularExpressions;
namespace MDEngine.Tags
{
	public class AnchorTag
	{
		private readonly string _inputString;

		public AnchorTag(string inputString)
		{
			_inputString = inputString;
		}

		public string Create(ref int i)
		{
			var anchorTagString = "";

			var tempI = i;

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


				if (title == string.Empty || anchor == string.Empty)
				{
					return string.Empty;
				}

				i = tempI;

				return $"<a href='{anchor}'>{title}</a>";
			}

			return string.Empty;

		}

	}
}

