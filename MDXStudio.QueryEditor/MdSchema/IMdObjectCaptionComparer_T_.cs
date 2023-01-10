using System;
using System.Collections.Generic;

namespace MDXStudio.MdSchema
{
	internal class IMdObjectCaptionComparer<T> : IComparer<T>
	where T : IMdObject
	{
		public IMdObjectCaptionComparer()
		{
		}

		int System.Collections.Generic.IComparer<T>.Compare(T x, T y)
		{
			return string.Compare(x.Caption, y.Caption, true);
		}
	}
}