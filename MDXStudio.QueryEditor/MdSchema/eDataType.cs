using System;

namespace MDXStudio.MdSchema
{
	internal enum eDataType
	{
		DBTYPE_EMPTY = 0,
		DBTYPE_NULL = 1,
		DBTYPE_I2 = 2,
		DBTYPE_I4 = 3,
		DBTYPE_R4 = 4,
		DBTYPE_R8 = 5,
		DBTYPE_CY = 6,
		DBTYPE_DATE = 7,
		DBTYPE_BSTR = 8,
		DBTYPE_IDISPATCH = 9,
		DBTYPE_ERROR = 10,
		DBTYPE_BOOL = 11,
		DBTYPE_VARIANT = 12,
		DBTYPE_IUNKNOWN = 13,
		DBTYPE_DECIMAL = 14,
		DBTYPE_I1 = 16,
		DBTYPE_UI1 = 17,
		DBTYPE_UI2 = 18,
		DBTYPE_UI4 = 19,
		DBTYPE_I8 = 20,
		DBTYPE_UI8 = 21,
		DBTYPE_GUID = 72,
		DBTYPE_BYTES = 128,
		DBTYPE_STR = 129,
		DBTYPE_WSTR = 130,
		DBTYPE_NUMERIC = 131,
		DBTYPE_UDT = 132,
		DBTYPE_DBDATE = 133,
		DBTYPE_DBTIME = 134,
		DBTYPE_DBTIMESTAMP = 135,
		DBTYPE_VECTOR = 4096,
		DBTYPE_ARRAY = 8192,
		DBTYPE_BYREF = 16384,
		DBTYPE_RESERVED = 32768
	}
}