using System;
namespace MDEngine.Tags
{
	public class ListTag
	{

		private bool _isList;

		public bool IsList => _isList;

		public ListTag()
		{
			_isList = false;
		}


		public string Create()
        {
			var result = "";
			if(!_isList)
            {
				_isList = true;
				result += "<ul>\n";
            }
			return result + "<li>\n";

        }

		public string CloseLi()
        {
			return "\n</li>";
        }

		public string CloseUl()
        {
			_isList = false;
			return "\n</ul>";
        }
	}
}

