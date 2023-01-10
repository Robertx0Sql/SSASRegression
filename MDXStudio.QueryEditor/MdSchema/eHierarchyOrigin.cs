using System;

namespace MDXStudio.MdSchema
{
	internal enum eHierarchyOrigin
	{
		UserDefined = 1,
		SystemEnabled = 2,
		ParentChild = 3,
		SystemInternal = 4,
		UserDefinedSystemInternal = 5,
		Key = 6
	}
}