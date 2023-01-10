using System;
using System.Data;

namespace MDXStudio.MdSchema
{
	internal class MdProperty : IMdObject
	{
		private string mCubeName;

		private string mDimensionUniqueName;

		private string mHierarchyUniqueName;

		private string mLevelUniqueName;

		private string mPropertyName;

		private string mPropertyCaption;

		private string mDescription;

		private ePropertyType mPropertyType;

		private eDataType mDataType;

		private int mContentType;

		private int mOrigin;

		private string mAttributeHierarchyName;

		private eRelationCardinality mCardinality;

		private bool mIsVisible;

		public string AttributeHierarchyName
		{
			get
			{
				return this.mAttributeHierarchyName;
			}
		}

		public string Caption
		{
			get
			{
				return this.mPropertyCaption;
			}
		}

		public eRelationCardinality Cardinality
		{
			get
			{
				return this.mCardinality;
			}
		}

		public int ContentType
		{
			get
			{
				return this.mContentType;
			}
		}

		public string CubeName
		{
			get
			{
				return this.mCubeName;
			}
		}

		public eDataType DataType
		{
			get
			{
				return this.mDataType;
			}
		}

		public string Description
		{
			get
			{
				return this.mDescription;
			}
		}

		public string DimensionUniqueName
		{
			get
			{
				return this.mDimensionUniqueName;
			}
		}

		public string HierarchyUniqueName
		{
			get
			{
				return this.mHierarchyUniqueName;
			}
		}

		public bool IsVisible
		{
			get
			{
				return this.mIsVisible;
			}
		}

		public string LevelUniqueName
		{
			get
			{
				return this.mLevelUniqueName;
			}
		}

		public string Name
		{
			get
			{
				return this.mPropertyName;
			}
		}

		public int Origin
		{
			get
			{
				return this.mOrigin;
			}
		}

		public ePropertyType PropertyType
		{
			get
			{
				return this.mPropertyType;
			}
		}

		public string UniqueName
		{
			get
			{
				return this.mPropertyName;
			}
		}

		public MdProperty(string pCubeName, string pDimensionUniqueName, string pHierarchyUniqueName, string pLevelUniqueName, string pName, string pCaption, string pDescription, ePropertyType pPropertyType, int pDataType, int pContentType, int pOrigin, string pAttributeHierarchyName, eRelationCardinality pCardinality, bool pIsVisible)
		{
			this.mCubeName = pCubeName;
			this.mDimensionUniqueName = pDimensionUniqueName;
			this.mHierarchyUniqueName = pHierarchyUniqueName;
			this.mLevelUniqueName = pLevelUniqueName;
			this.mPropertyName = pName;
			this.mPropertyCaption = pCaption;
			this.mDescription = pDescription;
			this.mPropertyType = pPropertyType;
			this.mDataType = (eDataType)pDataType;
			this.mContentType = pContentType;
			this.mOrigin = pOrigin;
			this.mAttributeHierarchyName = pAttributeHierarchyName;
			this.mCardinality = pCardinality;
			this.mIsVisible = pIsVisible;
		}

		public MdProperty(DataRow pRow)
		{
			string str;
			int num;
			string str1;
			string str2;
			eRelationCardinality _eRelationCardinality;
			this.mCubeName = (string)pRow["CUBE_NAME"];
			this.mDimensionUniqueName = (string)pRow["DIMENSION_UNIQUE_NAME"];
			this.mHierarchyUniqueName = (string)pRow["HIERARCHY_UNIQUE_NAME"];
			this.mLevelUniqueName = (string)pRow["LEVEL_UNIQUE_NAME"];
			this.mPropertyName = (string)pRow["PROPERTY_NAME"];
			this.mPropertyCaption = (string)pRow["PROPERTY_CAPTION"];
			str = (pRow["DESCRIPTION"] is DBNull ? string.Empty : (string)pRow["DESCRIPTION"]);
			this.mDescription = str;
			this.mPropertyType = (ePropertyType)((short)pRow["PROPERTY_TYPE"]);
			this.mDataType = (eDataType)((ushort)pRow["DATA_TYPE"]);
			num = (pRow["PROPERTY_CONTENT_TYPE"] is DBNull ? 0 : (int)((short)pRow["PROPERTY_CONTENT_TYPE"]));
			this.mContentType = num;
			this.mOrigin = (ushort)pRow["PROPERTY_ORIGIN"];
			str1 = (pRow["PROPERTY_ATTRIBUTE_HIERARCHY_NAME"] is DBNull ? string.Empty : (string)pRow["PROPERTY_ATTRIBUTE_HIERARCHY_NAME"]);
			this.mAttributeHierarchyName = str1;
			str2 = (pRow["PROPERTY_CARDINALITY"] is DBNull ? string.Empty : (string)pRow["PROPERTY_CARDINALITY"]);
			_eRelationCardinality = (str2 == "MANY" ? eRelationCardinality.Many : eRelationCardinality.One);
			this.mCardinality = _eRelationCardinality;
			this.mIsVisible = (bool)pRow["PROPERTY_IS_VISIBLE"];
		}

		public MdProperty SetName(string pName)
		{
			return new MdProperty(this.mCubeName, this.mDimensionUniqueName, this.mHierarchyUniqueName, this.mLevelUniqueName, pName, pName, string.Empty, ePropertyType.MDPROP_SYSTEM, 130, this.mContentType, this.mOrigin, this.mAttributeHierarchyName, this.mCardinality, this.mIsVisible);
		}

		public override string ToString()
		{
			return this.mPropertyCaption;
		}
	}
}