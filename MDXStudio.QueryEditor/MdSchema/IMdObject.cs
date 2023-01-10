using System;

namespace MDXStudio.MdSchema
{
	internal interface IMdObject
	{
		string Caption
		{
			get;
		}

		string Name
		{
			get;
		}

		string UniqueName
		{
			get;
		}
	}
}