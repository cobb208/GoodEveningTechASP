using System;
namespace MDEngine.Tags
{
	public class ParagraphTag
	{
		private bool _isParagraph;

		public bool IsParagraph => _isParagraph;

		public ParagraphTag()
		{
			_isParagraph = false;
		}

		public string Create()
        {
			_isParagraph = true;
			return "<p>";
        }

		public string Close()
        {
			_isParagraph = false;
			return "</p>";
        }


	}
}

