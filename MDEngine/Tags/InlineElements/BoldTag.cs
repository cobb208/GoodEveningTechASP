using System;
namespace MDEngine.Tags.InlineElements
{
	public class BoldTag : ITag
	{
		private bool _isBold;

		public string Create()
        {
			_isBold = true;
			return "<strong>";
        }

		public string Close()
        {
			_isBold = false;
			return "</strong>";
        }

		public bool IsActive() => _isBold;
	}
}

