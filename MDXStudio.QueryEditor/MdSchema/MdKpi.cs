using System;
using System.Data;

namespace MDXStudio.MdSchema
{
	internal class MdKpi : IMdObject
	{
		private string mCubeName;

		private string mKpiUniqueName;

		private string mKpiName;

		private string mKpiCaption;

		private string mKpiDescription;

		private string mMeasureGroupName;

		private string mKpiDisplayFolder;

		private string mKpiValue;

		private string mKpiGoal;

		private string mKpiStatus;

		private string mKpiTrend;

		private string mKpiStatusGraphic;

		private string mKpiTrendGraphic;

		private string mKpiWeight;

		private string mKpiCurrentTimeMember;

		private string mKpiParentKpiName;

		private string mAnnotations;

		public string Annotations
		{
			get
			{
				return this.mAnnotations;
			}
		}

		public string Caption
		{
			get
			{
				return this.mKpiCaption;
			}
		}

		public string CubeName
		{
			get
			{
				return this.mCubeName;
			}
		}

		public string CurrentTimeMember
		{
			get
			{
				return this.mKpiCurrentTimeMember;
			}
		}

		public string Description
		{
			get
			{
				return this.mKpiDescription;
			}
		}

		public string DisplayFolder
		{
			get
			{
				return this.mKpiDisplayFolder;
			}
		}

		public string Goal
		{
			get
			{
				return this.mKpiGoal;
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
				return this.mKpiName;
			}
		}

		public string ParentKpiName
		{
			get
			{
				return this.mKpiParentKpiName;
			}
		}

		public string Status
		{
			get
			{
				return this.mKpiStatus;
			}
		}

		public string StatusGraphic
		{
			get
			{
				return this.mKpiStatusGraphic;
			}
		}

		public string Trend
		{
			get
			{
				return this.mKpiTrend;
			}
		}

		public string TrendGraphic
		{
			get
			{
				return this.mKpiTrendGraphic;
			}
		}

		public string UniqueName
		{
			get
			{
				return this.mKpiUniqueName;
			}
		}

		public string Value
		{
			get
			{
				return this.mKpiValue;
			}
		}

		public string Weight
		{
			get
			{
				return this.mKpiWeight;
			}
		}

		public MdKpi(DataRow pRow)
		{
			string str;
			string str1;
			string str2;
			string str3;
			this.mCubeName = (string)pRow["CUBE_NAME"];
			this.mKpiUniqueName = "";
			this.mKpiName = (string)pRow["KPI_NAME"];
			this.mKpiCaption = (string)pRow["KPI_CAPTION"];
			str = (pRow["KPI_DESCRIPTION"] is DBNull ? string.Empty : (string)pRow["KPI_DESCRIPTION"]);
			this.mKpiDescription = str;
			this.mKpiValue = (string)pRow["KPI_VALUE"];
			this.mKpiGoal = (string)pRow["KPI_GOAL"];
			this.mKpiStatus = (string)pRow["KPI_STATUS"];
			this.mKpiTrend = (string)pRow["KPI_TREND"];
			this.mKpiStatus = (string)pRow["KPI_STATUS"];
			this.mKpiTrendGraphic = (string)pRow["KPI_TREND_GRAPHIC"];
			this.mKpiStatusGraphic = (string)pRow["KPI_STATUS_GRAPHIC"];
			this.mKpiWeight = (string)pRow["KPI_WEIGHT"];
			str1 = (pRow["KPI_CURRENT_TIME_MEMBER"] is DBNull ? string.Empty : (string)pRow["KPI_CURRENT_TIME_MEMBER"]);
			this.mKpiCurrentTimeMember = str1;
			this.mKpiParentKpiName = (string)pRow["KPI_PARENT_KPI_NAME"];
			this.mAnnotations = (string)pRow["ANNOTATIONS"];
			str2 = (pRow["MEASUREGROUP_NAME"] is DBNull ? string.Empty : (string)pRow["MEASUREGROUP_NAME"]);
			this.mMeasureGroupName = str2;
			str3 = (pRow["KPI_DISPLAY_FOLDER"] is DBNull ? string.Empty : (string)pRow["KPI_DISPLAY_FOLDER"]);
			this.mKpiDisplayFolder = str3;
		}

		public override string ToString()
		{
			return this.mKpiCaption;
		}
	}
}