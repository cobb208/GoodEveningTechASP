using System;
namespace MDEngine.Tags
{
	public class BoldTag : ITag
	{
		private bool _isBold;

		public bool IsBold => _isBold;

		public BoldTag()
		{
			_isBold = false;
		}

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

		public bool IsOpen() => _isBold;
	}
}

