using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MDXStudio.QueryEditor
{
	internal class ParserUtil
	{
		private readonly static Regex sRegExBlockComments;

		private readonly static Regex sRegExBrackets;

		private readonly static Regex sRegExCombination;

		private readonly static Regex sRegExKeyWords;

		private readonly static Regex sRegExLineComments;

		private readonly static Regex sRegExNumber;

		private readonly static Regex sRegExOperators;

		private readonly static Regex sRegExString;

		private readonly static Regex sRegExSysFunc;

		static ParserUtil()
		{
			ParserUtil.sRegExLineComments = new Regex("(?<linecomment>(?m:(//.*|--.*)))", RegexOptions.Compiled | RegexOptions.CultureInvariant);
			ParserUtil.sRegExBlockComments = new Regex("(?<blockcomment>(?sx:(?:((?<left>(/\\*))|(?<right-left>(\\*/)))|(?(left)((?!\\*/).)*|[$]))+)+)", RegexOptions.Compiled | RegexOptions.CultureInvariant);
			ParserUtil.sRegExString = new Regex("(?<string>(?s:((\"[^\"]*)(\"|$))))", RegexOptions.Compiled);
			ParserUtil.sRegExNumber = new Regex("(?<number>(?-sx:((^|\\s+|\\()\\d+)|(\\s*\\.\\d+\\)?)|((^|\\s+)\\d+\\.)|((^|\\s)+\\d+\\.)))", RegexOptions.Compiled | RegexOptions.CultureInvariant);
			ParserUtil.sRegExNumber = new Regex("(?x:(?<number>((\\d+)|((\\s|\\d*)\\.\\d+)|(\\d+\\.))))", RegexOptions.Compiled | RegexOptions.CultureInvariant);
			ParserUtil.sRegExOperators = new Regex("(?<operators1>(?x:(\\*)|(/)|(\\.|,|\\(|\\)|\\+|-|<|<=|=|>=|<>|>|\\^|%|!=|!<|!>|\\|~)+))", RegexOptions.Compiled | RegexOptions.CultureInvariant);
			ParserUtil.sRegExBrackets = new Regex("(?<brackets>(?xm:(?:((?<left>(\\[))|(?<right-left>(\\])))|(?(left)((?!\\]).)*|[$]))+)+)", RegexOptions.Compiled | RegexOptions.CultureInvariant);
			ParserUtil.sRegExKeyWords = new Regex("(?<keyword>(?x:(\\b((after|all|and|as|asc|axis|basc|bdesc|before|before_and_after|by|cell|columns|count|create|default_member|desc|description|dimensions|empty|end|excludeempty|false|for|freeze|from|includeempty|is|item|lag|measure|member|members|name|non|null|on|or|post|properties|property|recursive|rows|scope|select|self|self_and_after|self_and_before|self_before_after|session|set|this|true|type|uniquename|update|use|username|value|variance|where|with|xor|member_key|parent_unique_name|parent_level|dimension_unique_name|hierarchy_unique_name|cube_name)\\b))))", RegexOptions.Compiled | RegexOptions.CultureInvariant);
			ParserUtil.sRegExSysFunc = new Regex("(?<sysfunc>(?x:(\\b((ascendants|currentordinal|exists|order|ordinal|unknownmember|unorder|addcalculatedmembers|aggregate|allmembers|ancestor|ancestors|ascendants|avg|bottomcount|bottompercent|bottomsum|children|closingperiod|cousin|crossjoin|current|currentmember|defaultmember|descendants|dimension|distinct|distinctcount|drilldownlevel|except|extract|filter|firstchild|firstsibling|generate|head|hierarchy|iif|intersect|isancestor|isempty|isgeneration|isleaf|issibling|lastchild|lastperiods|lastsibling|lead|leaves|level|levels|linkmember|max|membertostr|min|mtd|nametoset|nextmember|nonemptycrossjoin|openingperiod|parallelperiod|parent|periodstodate|predict|prevmember|qtd|rank|rollupchildren|root|settoarray|settostr|sort|stripcalculatedmembers|strtomember|strtoset|strtotuple|strtoval|strtovalue|subset|sum|tail|topcount|toppercent|topsum|tupletostr|union|unique|validmeasure|visualtotals|wtd|ytd)\\b))))", RegexOptions.Compiled | RegexOptions.CultureInvariant);
			ParserUtil.sRegExCombination = new Regex("([A-Za-z]\\d+)", RegexOptions.Compiled | RegexOptions.CultureInvariant);
		}

		public ParserUtil()
		{
		}

		public static void CheckRegExsLoadList(string pText, List<TextSegment> pList)
		{
			pText = pText.ToLowerInvariant();
			MatchCollection matchCollections = ParserUtil.sRegExKeyWords.Matches(pText);
			MatchCollection matchCollections1 = ParserUtil.sRegExLineComments.Matches(pText);
			MatchCollection matchCollections2 = ParserUtil.sRegExBlockComments.Matches(pText);
			MatchCollection matchCollections3 = ParserUtil.sRegExBrackets.Matches(pText);
			MatchCollection matchCollections4 = ParserUtil.sRegExOperators.Matches(pText);
			MatchCollection matchCollections5 = ParserUtil.sRegExNumber.Matches(pText);
			MatchCollection matchCollections6 = ParserUtil.sRegExString.Matches(pText);
			MatchCollection matchCollections7 = ParserUtil.sRegExSysFunc.Matches(pText);
			MatchCollection matchCollections8 = ParserUtil.sRegExCombination.Matches(pText);
			ParserUtil.LoadListOfMatches(pList, matchCollections, eHighlightType.KeyWord);
			ParserUtil.LoadListOfMatches(pList, matchCollections7, eHighlightType.SystemFunction);
			ParserUtil.LoadListOfMatches(pList, matchCollections1, eHighlightType.LineComment);
			ParserUtil.LoadListOfMatches(pList, matchCollections2, eHighlightType.BlockComment);
			ParserUtil.LoadListOfMatches(pList, matchCollections3, eHighlightType.PlainText);
			ParserUtil.LoadListOfMatches(pList, matchCollections4, eHighlightType.Operator);
			ParserUtil.LoadListOfMatches(pList, matchCollections5, eHighlightType.Number);
			ParserUtil.LoadListOfMatches(pList, matchCollections6, eHighlightType.String);
			ParserUtil.LoadListOfMatches(pList, matchCollections8, eHighlightType.PlainText);
		}

		private static void LoadListOfMatches(IList<TextSegment> pTextSegmentList, MatchCollection pMatches, eHighlightType pHighlightType)
		{
			foreach (Match pMatch in pMatches)
			{
				if (!pMatch.Success || pMatch.Length <= 0)
				{
					continue;
				}
				TextSegment textSegment = new TextSegment(pHighlightType, pMatch.Index, pMatch.Length);
				pTextSegmentList.Add(textSegment);
			}
		}
	}
}