using System;
using System.Text.RegularExpressions;
namespace MDEngine.Tags
{
	public class AnchorTag
	{
		private Regex _title;
		private Regex _anchor;
		private string _inputString;

		public AnchorTag(string InputString)
		{
			_inputString = InputString;
			_title = new(@"[\[].*[\]]");
			_anchor = new(@"[(].*[)]");
		}

		public string Create(ref int i)
        {
			string anchorTagString = "";

			int tempI = i;

			while(_inputString[tempI] != '\n')
            {
				anchorTagString += _inputString[tempI];
				tempI++;
            }

			var correctedTitle = CreateTitle(anchorTagString);
			var correctedAnchor = CreateAnchor(anchorTagString);

			if(correctedTitle == String.Empty || correctedAnchor == String.Empty)
            {
				return String.Empty;
            }

			i = tempI;

			return $"<a href='{correctedAnchor}'>{correctedTitle}</a>";

        }

		private string CreateTitle(string titleString)
        {
			var returnString = "";

			Match titleMatch = _title.Match(titleString);

			int leftSquareBracketIndex = titleMatch.Value.IndexOf("[");

			if(leftSquareBracketIndex == -1)
            {
				return String.Empty;
            }

			returnString += titleMatch.Value.Remove(leftSquareBracketIndex, 1);

			int rightSquareBracketIndex = returnString.IndexOf("]");

			if(rightSquareBracketIndex == -1)
            {
				return String.Empty;
            }

			return returnString.Remove(rightSquareBracketIndex, 1);
        }

		private string CreateAnchor(string anchorString)
        {
			var returnString = "";

			Match anchorMatch = _anchor.Match(anchorString);

			int leftBracketIndex = anchorMatch.Value.IndexOf("(");

			if(leftBracketIndex == -1)
            {
				return String.Empty;
            }

			returnString += anchorMatch.Value.Remove(leftBracketIndex, 1);

			int rightBracketIndex = returnString.IndexOf(")");

			if(rightBracketIndex == -1)
            {
				return String.Empty;
            }				

			return returnString.Remove(rightBracketIndex, 1);
        }

	}
}

