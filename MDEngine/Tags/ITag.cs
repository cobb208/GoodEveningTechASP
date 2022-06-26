using System;
namespace MDEngine.Tags
{
	public interface ITag
	{
		public string Create();

		public string Close();

		public bool IsActive();
	}
}

