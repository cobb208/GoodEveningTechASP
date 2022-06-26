using System;
namespace MDEngine.Tags
{
	public class HorizontalRuleTag : ITag
	{
		public HorizontalRuleTag()
		{
		}

		public string Create() => "<hr>";

		public string Close() => "";

		public bool IsActive() => false;
	}
}

