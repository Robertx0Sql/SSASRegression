using System;

namespace MDXStudio.MdSchema
{
	[Flags]
	internal enum ePropertyType
	{
		MDPROP_MEMBER = 1,
		MDPROP_CELL = 2,
		MDPROP_SYSTEM = 4,
		MDPROP_BLOB = 8
	}
}