using Microsoft.AnalysisServices.AdomdClient;
using System;

namespace MDXStudio.MdSchema
{
	public class MdMember
	{
		private const string cCol_CATALOG_NAME = "CATALOG_NAME";

		private const string cCol_CUBE_NAME = "CUBE_NAME";

		private const string cCol_DIMENSION_UNIQUE_NAME = "DIMENSION_UNIQUE_NAME";

		private const string cCol_HIERARCHY_UNIQUE_NAME = "HIERARCHY_UNIQUE_NAME";

		private const string cCol_LEVEL_UNIQUE_NAME = "LEVEL_UNIQUE_NAME";

		private const string cCol_LEVEL_NUMBER = "LEVEL_NUMBER";

		private const string cCol_MEMBER_NAME = "MEMBER_NAME";

		private const string cCol_MEMBER_UNIQUE_NAME = "MEMBER_UNIQUE_NAME";

		private const string cCol_CHILDREN_CARDINALITY = "CHILDREN_CARDINALITY";

		private const string cCol_PARENT_LEVEL = "PARENT_LEVEL";

		private const string cCol_PARENT_UNIQUE_NAME = "PARENT_UNIQUE_NAME";

		private const string cCol_MEMBER_KEY = "MEMBER_KEY";

		private const string cCol_KEY0 = "KEY0";

		private string mCubeName = string.Empty;

		private string mDimensionUniqueName = string.Empty;

		private string mHierarchyUniqueName = string.Empty;

		private string mLevelUniqueName = string.Empty;

		private int mLevelNumber;

		private string mUniqueName = string.Empty;

		private string mName = string.Empty;

		private string mParentUniqueName = string.Empty;

		private int mParentLevelNumber;

		private string mKey = string.Empty;

		private bool mHasChildren;

		public string CubeName
		{
			get
			{
				return this.mCubeName;
			}
			set
			{
				this.mCubeName = value;
			}
		}

		public string DimensionUniqueName
		{
			get
			{
				return this.mDimensionUniqueName;
			}
		}

		public bool HasChildren
		{
			get
			{
				return this.mHasChildren;
			}
		}

		public string HierarchyUniqueName
		{
			get
			{
				return this.mHierarchyUniqueName;
			}
		}

		public string Key
		{
			get
			{
				return this.mKey;
			}
		}

		public string LevelName
		{
			get
			{
				return this.mLevelUniqueName;
			}
		}

		public int LevelNumber
		{
			get
			{
				return this.mLevelNumber;
			}
		}

		public string Name
		{
			get
			{
				return this.mName;
			}
		}

		public int ParentLevelNumber
		{
			get
			{
				return this.mParentLevelNumber;
			}
		}

		public string ParentUniqueName
		{
			get
			{
				return this.mParentUniqueName;
			}
		}

		public string UniqueName
		{
			get
			{
				return this.mUniqueName;
			}
		}

		public MdMember(string pCubeName, string pUniqueName)
		{
			this.mCubeName = pCubeName;
			this.mUniqueName = pUniqueName;
		}

		public MdMember(string pCubeName, string pDimensionUniqueName, string pHierarchyUniqueName, string pLevelUniqueName, int pLevelNumber, string pUniqueName, string pName, string pParentUniqueName, int pParentLevelNumber, string pKey, bool pHasChildren)
		{
			this.mCubeName = pCubeName;
			this.mDimensionUniqueName = pDimensionUniqueName;
			this.mHierarchyUniqueName = pHierarchyUniqueName;
			this.mLevelUniqueName = pLevelUniqueName;
			this.mLevelNumber = pLevelNumber;
			this.mUniqueName = pUniqueName;
			this.mName = pName;
			this.mParentUniqueName = pParentUniqueName;
			this.mParentLevelNumber = pParentLevelNumber;
			this.mKey = pKey;
			this.mHasChildren = pHasChildren;
		}

		public static MdMember CreateFromMember(Member pMember)
		{
			string str;
			string str1;
			string str2;
			string str3;
			int num;
			string str4;
			string uniqueName = pMember.UniqueName;
			string caption = pMember.Caption;
			string levelName = pMember.LevelName;
			int levelDepth = pMember.LevelDepth;
			bool childCount = pMember.ChildCount > (long)0;
			MdMember.ReadManadatoryProperties(pMember, out str, out str1, out str2, out str4, out str3, out num);
			MdMember mdMember = new MdMember(str, str1, str2, levelName, levelDepth, uniqueName, caption, str3, num, str4, childCount);
			return mdMember;
		}

		private static void ReadManadatoryProperties(Member pMember, out string oCubeName, out string oDName, out string oHName, out string oKey, out string oParUName, out int oParLNumber)
		{
			object value;
			MemberPropertyCollection memberProperties = pMember.MemberProperties;
			oCubeName = string.Empty;
			oDName = string.Empty;
			oHName = string.Empty;
			oKey = string.Empty;
			oParUName = string.Empty;
			oParLNumber = 0;
			for (int i = 0; i < memberProperties.Count; i++)
			{
				MemberProperty item = memberProperties[i];
				string name = item.Name;
				if (name.IndexOf("CUBE_NAME") >= 0)
				{
					value = item.Value;
					if (value != null && !(value is DBNull))
					{
						oCubeName = (string)value;
					}
				}
				else if (name.IndexOf("DIMENSION_UNIQUE_NAME") >= 0)
				{
					value = item.Value;
					if (value != null && !(value is DBNull))
					{
						oDName = (string)value;
					}
				}
				else if (name.IndexOf("HIERARCHY_UNIQUE_NAME") >= 0)
				{
					value = item.Value;
					if (value != null && !(value is DBNull))
					{
						oHName = (string)value;
					}
				}
				else if (name.IndexOf("MEMBER_KEY") >= 0)
				{
					value = item.Value;
					if (value != null && !(value is DBNull))
					{
						oKey = (string)value;
					}
				}
				else if (name.IndexOf("PARENT_UNIQUE_NAME") >= 0)
				{
					value = item.Value;
					if (value != null && !(value is DBNull))
					{
						oParUName = (string)value;
					}
				}
				else if (name.IndexOf("PARENT_LEVEL") >= 0)
				{
					value = item.Value;
					if (value != null && !(value is DBNull))
					{
						if (!(value is string))
						{
							oParLNumber = (int)value;
						}
						else
						{
							oParLNumber = int.Parse((string)value);
						}
					}
				}
			}
		}

		public override string ToString()
		{
			return this.Name;
		}
	}
}