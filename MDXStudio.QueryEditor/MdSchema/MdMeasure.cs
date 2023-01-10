using System;
using System.Data;

namespace MDXStudio.MdSchema
{
	internal class MdMeasure : IMdObject
	{
		private string mCubeName;

		private string mMeasureUniqueName;

		private string mMeasureName;

		private string mMeasureCaption;

		private string mDescription;

		private eMeasureAggregator mMeasureAggregator;

		private int mDataType;

		private int mNumericPrecision;

		private int mNumericScale;

		private string mExpression;

		private bool mIsVisible;

		private string mMeasureGroupName;

		private string mDisplayFolder;

		private string mFormatString;

		public string Caption
		{
			get
			{
				return this.mMeasureCaption;
			}
		}

		public string CubeName
		{
			get
			{
				return this.mCubeName;
			}
		}

		public int DataType
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

		public string DisplayFolder
		{
			get
			{
				return this.mDisplayFolder;
			}
		}

		public string Expression
		{
			get
			{
				return this.mExpression;
			}
		}

		public string FormatString
		{
			get
			{
				return this.mFormatString;
			}
		}

		public bool IsVisible
		{
			get
			{
				return this.mIsVisible;
			}
		}

		public eMeasureAggregator MeasureAggregator
		{
			get
			{
				return this.mMeasureAggregator;
			}
		}

		public string MeasureGroupName
		{
			get
			{
				return this.mMeasureGroupName;
			}
		}

		public string Name
		{
			get
			{
				return this.mMeasureName;
			}
		}

		public int NumericPrecision
		{
			get
			{
				return this.mNumericPrecision;
			}
		}

		public int NumericScale
		{
			get
			{
				return this.mNumericScale;
			}
		}

		public string UniqueName
		{
			get
			{
				return this.mMeasureUniqueName;
			}
		}

		public MdMeasure(string pCubeName, string pUniqueName, string pName, string pCaption, string pDescription, eMeasureAggregator pMeasureAggregator, int pDataType, int pNumericPrecision, int pNumericScale, string pExpression, bool pIsVisible, string pMeasureGroupName, string pDisplayFolder, string pFormatString)
		{
			this.mCubeName = pCubeName;
			this.mMeasureUniqueName = pUniqueName;
			this.mMeasureName = pName;
			this.mMeasureCaption = pCaption;
			this.mDescription = pDescription;
			this.mMeasureAggregator = pMeasureAggregator;
			this.mDataType = pDataType;
			this.mNumericPrecision = pNumericPrecision;
			this.mNumericScale = pNumericScale;
			this.mExpression = pExpression;
			this.mIsVisible = pIsVisible;
			this.mMeasureGroupName = pMeasureGroupName;
			this.mDisplayFolder = pDisplayFolder;
			this.mFormatString = pFormatString;
		}

		public MdMeasure(DataRow pRow)
		{
			string str;
			string str1;
			string str2;
			string str3;
			this.mCubeName = (string)pRow["CUBE_NAME"];
			this.mMeasureUniqueName = (string)pRow["MEASURE_UNIQUE_NAME"];
			this.mMeasureName = (string)pRow["MEASURE_NAME"];
			this.mMeasureCaption = (string)pRow["MEASURE_CAPTION"];
			str = (pRow["DESCRIPTION"] is DBNull ? string.Empty : (string)pRow["DESCRIPTION"]);
			this.mDescription = str;
			this.mMeasureAggregator = (eMeasureAggregator)((int)pRow["MEASURE_AGGREGATOR"]);
			this.mDataType = (ushort)pRow["DATA_TYPE"];
			this.mNumericPrecision = (ushort)pRow["NUMERIC_PRECISION"];
			this.mNumericScale = (short)pRow["NUMERIC_SCALE"];
			this.mExpression = pRow["EXPRESSION"] as string;
			this.mIsVisible = (bool)pRow["MEASURE_IS_VISIBLE"];
			str1 = (pRow.Table.Columns.IndexOf("MEASUREGROUP_NAME") < 0 || pRow["MEASUREGROUP_NAME"] is DBNull ? string.Empty : (string)pRow["MEASUREGROUP_NAME"]);
			this.mMeasureGroupName = str1;
			str2 = (pRow.Table.Columns.IndexOf("MEASURE_DISPLAY_FOLDER") < 0 || pRow["MEASURE_DISPLAY_FOLDER"] is DBNull ? string.Empty : (string)pRow["MEASURE_DISPLAY_FOLDER"]);
			this.mDisplayFolder = str2;
			str3 = (pRow.Table.Columns.IndexOf("DEFAULT_FORMAT_STRING") < 0 || pRow["DEFAULT_FORMAT_STRING"] is DBNull ? string.Empty : (string)pRow["DEFAULT_FORMAT_STRING"]);
			this.mFormatString = str3;
		}

		public override string ToString()
		{
			return this.mMeasureCaption;
		}
	}
}