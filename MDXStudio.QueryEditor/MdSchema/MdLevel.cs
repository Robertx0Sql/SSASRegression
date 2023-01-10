using System;
using System.Data;

namespace MDXStudio.MdSchema
{
	internal class MdLevel : IMdObject
	{
		private string mCubeName;

		private string mDimensionUniqueName;

		private string mHierarchyUniqueName;

		private string mLevelUniqueName;

		private string mLevelName;

		private string mLevelCaption;

		private string mDescription;

		private int mLevelNumber;

		private int mLevelCardinality;

		private eLevelType mLevelType;

		private bool mIsVisible;

		private string mAttributeHierarchyName;

		private int mKeyCardinality;

		private int mOrigin;

		private MdObjectList<MdProperty> mPropertyList;

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
				return this.mLevelCaption;
			}
		}

		public int Cardinality
		{
			get
			{
				return this.mLevelCardinality;
			}
		}

		public string CubeName
		{
			get
			{
				return this.mCubeName;
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

		public int KeyKardinality
		{
			get
			{
				return this.mKeyCardinality;
			}
		}

		public eLevelType LevelType
		{
			get
			{
				return this.mLevelType;
			}
		}

		public string Name
		{
			get
			{
				return this.mLevelName;
			}
		}

		public int Number
		{
			get
			{
				return this.mLevelNumber;
			}
		}

		public int Origin
		{
			get
			{
				return this.mOrigin;
			}
		}

		public MdObjectList<MdProperty> Properties
		{
			get
			{
				return this.mPropertyList;
			}
		}

		public string UniqueName
		{
			get
			{
				return this.mLevelUniqueName;
			}
		}

		public MdLevel(string pCubeName, string pDimensionUniqueName, string pHierarchyUniqueName, string pUniqueName, string pName, string pCaption, string pDescription, int pNumber, int pCardinality, eLevelType pType, bool pIsVisible, string pAttributeHierarchyName, int pKeyKardinality, int pOrigin)
		{
			this.mCubeName = pCubeName;
			this.mDimensionUniqueName = pDimensionUniqueName;
			this.mHierarchyUniqueName = pHierarchyUniqueName;
			this.mLevelUniqueName = pUniqueName;
			this.mLevelName = pName;
			this.mLevelCaption = pCaption;
			this.mDescription = pDescription;
			this.mLevelNumber = pNumber;
			this.mLevelCardinality = pCardinality;
			this.mLevelType = pType;
			this.mIsVisible = pIsVisible;
			this.mAttributeHierarchyName = pAttributeHierarchyName;
			this.mKeyCardinality = pKeyKardinality;
			this.mOrigin = pOrigin;
			this.mPropertyList = new MdObjectList<MdProperty>();
		}

		public MdLevel(DataRow pRow)
		{
			string str;
			string str1;
			int num;
			int num1;
			this.mCubeName = (string)pRow["CUBE_NAME"];
			this.mDimensionUniqueName = (string)pRow["DIMENSION_UNIQUE_NAME"];
			this.mHierarchyUniqueName = (string)pRow["HIERARCHY_UNIQUE_NAME"];
			this.mLevelUniqueName = (string)pRow["LEVEL_UNIQUE_NAME"];
			this.mLevelName = (string)pRow["LEVEL_NAME"];
			this.mLevelCaption = (string)pRow["LEVEL_CAPTION"];
			str = (pRow["DESCRIPTION"] is DBNull ? string.Empty : (string)pRow["DESCRIPTION"]);
			this.mDescription = str;
			this.mLevelNumber = (int)pRow["LEVEL_NUMBER"];
			this.mLevelCardinality = (int)pRow["LEVEL_CARDINALITY"];
			this.mLevelType = (eLevelType)((int)pRow["LEVEL_TYPE"]);
			this.mIsVisible = (bool)pRow["LEVEL_IS_VISIBLE"];
			str1 = (pRow.Table.Columns.IndexOf("LEVEL_ATTRIBUTE_HIERARCHY_NAME") < 0 || pRow["LEVEL_ATTRIBUTE_HIERARCHY_NAME"] is DBNull ? string.Empty : (string)pRow["LEVEL_ATTRIBUTE_HIERARCHY_NAME"]);
			this.mAttributeHierarchyName = str1;
			num = (pRow.Table.Columns.IndexOf("LEVEL_KEY_CARDINALITY") < 0 ? 0 : (int)((ushort)pRow["LEVEL_KEY_CARDINALITY"]));
			this.mKeyCardinality = num;
			num1 = (pRow.Table.Columns.IndexOf("LEVEL_KEY_CARDINALITY") < 0 ? 0 : (int)((ushort)pRow["LEVEL_ORIGIN"]));
			this.mOrigin = num1;
			this.mPropertyList = new MdObjectList<MdProperty>();
		}

		public override string ToString()
		{
			return this.mLevelCaption;
		}
	}
}