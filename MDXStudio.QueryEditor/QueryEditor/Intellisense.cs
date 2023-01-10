using MDXParser;
using MDXStudio.QueryEditor;
using Microsoft.AnalysisServices.AdomdClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MDXStudio
{
	internal class Intellisense : IntellisenseOptions
	{
		internal AutoCompleteList m_AutoCompleteList;

		private KeyEventArgs m_KeyDown;

		private MdxEditor m_Editor;

		public Intellisense()
		{
		}

		internal void listBoxAutoComplete_DoubleClick(object sender, EventArgs e)
		{
			AutoCompleteList autoCompleteList = (AutoCompleteList)sender;
			if (autoCompleteList.SelectedItems.Count == 1)
			{
				this.selectItem();
				autoCompleteList.Hide();
				this.m_Editor.Focus();
			}
		}

		internal void listBoxAutoComplete_KeyDown(object sender, KeyEventArgs e)
		{
			this.m_Editor.Focus();
		}

		internal void listBoxAutoComplete_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.m_Editor.Focus();
		}

		private void OnIntellisenseMenu_KeyPress(object sender, KeyPressEventArgs e)
		{
		}

		private void OnIntellisenseMenuClick(object sender, EventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
			if (char.IsLetter(this.m_Editor.Text[this.m_Editor.SelectionStart - 1]))
			{
				MdxEditor mEditor = this.m_Editor;
				mEditor.SelectionStart = mEditor.SelectionStart - 1;
				this.m_Editor.SelectionLength = 1;
			}
			this.m_Editor.SelectedText = toolStripMenuItem.Text;
		}

		internal void ProcessKeyDownEvent(object sender, KeyEventArgs e)
		{
			this.m_KeyDown = e;
			this.m_Editor = sender as MdxEditor;
			if (this.m_AutoCompleteList.Visible)
			{
				if (e.KeyCode == Keys.Up)
				{
					this.m_Editor.HideToolTip();
					if (this.m_AutoCompleteList.SelectedIndex > 0)
					{
						AutoCompleteList mAutoCompleteList = this.m_AutoCompleteList;
						mAutoCompleteList.SelectedIndex = mAutoCompleteList.SelectedIndex - 1;
					}
					e.Handled = true;
					return;
				}
				if (e.KeyCode == Keys.Down)
				{
					this.m_Editor.HideToolTip();
					if (this.m_AutoCompleteList.Visible)
					{
						if (this.m_AutoCompleteList.SelectedIndex < this.m_AutoCompleteList.Items.Count - 1)
						{
							AutoCompleteList selectedIndex = this.m_AutoCompleteList;
							selectedIndex.SelectedIndex = selectedIndex.SelectedIndex + 1;
						}
						e.Handled = true;
						return;
					}
				}
				else if (e.KeyValue < 48 || e.KeyValue >= 58 && e.KeyValue <= 64 || e.KeyValue >= 91 && e.KeyValue <= 96 || e.KeyValue > 122)
				{
					if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab || e.KeyCode == Keys.Space || e.KeyCode == Keys.OemPeriod || e.KeyCode == Keys.D9 || e.KeyCode == Keys.Oemcomma)
					{
						this.m_Editor.HideToolTip();
						this.selectItem();
						if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab)
						{
							e.Handled = true;
						}
					}
					if (e.KeyCode != Keys.ShiftKey && e.KeyCode != Keys.ControlKey)
					{
						this.m_AutoCompleteList.Hide();
						return;
					}
				}
				else
				{
					string currentWord = this.m_Editor.GetCurrentWord();
					currentWord = string.Concat(currentWord, (char)((ushort)e.KeyValue));
					this.m_AutoCompleteList.SelectedIndex = -1;
					for (int i = 0; i < this.m_AutoCompleteList.Items.Count; i++)
					{
						if (this.m_AutoCompleteList.Items[i].ToString().StartsWith(currentWord, StringComparison.InvariantCultureIgnoreCase))
						{
							this.m_AutoCompleteList.SelectedIndex = i;
							return;
						}
					}
				}
			}
		}

		internal void ProcessKeyPressEvent(object sender, KeyPressEventArgs eKeyPress, Connection cn)
		{
			MDXObject[] sObjects;
			int i;
			DimensionCollection.Enumerator enumerator;
			HierarchyCollection.Enumerator enumerator1;
			LevelCollection.Enumerator enumerator2;
			Size clientSize;
			int num;
			MdxEditor str = sender as MdxEditor;
			this.m_Editor = str;
			eKeyPress.Handled = this.m_KeyDown.Handled;
			bool flag = false;
			char keyChar = eKeyPress.KeyChar;
			KeyEventArgs mKeyDown = this.m_KeyDown;
			if (this.m_AutoCompleteList.Visible && keyChar != '(' && keyChar != ',')
			{
				return;
			}
			if (mKeyDown.KeyCode == Keys.Back)
			{
				this.m_Editor.HideToolTip();
				if (this.m_Editor.SelectionStart <= 1 || !char.IsLetterOrDigit(this.m_Editor.Text[this.m_Editor.SelectionStart - 1]))
				{
					this.m_AutoCompleteList.Hide();
				}
			}
			else if (41 == keyChar || 125 == keyChar || 93 == keyChar)
			{
				char chr = '\0';
				char chr1 = keyChar;
				if (chr1 == ')')
				{
					chr = '(';
				}
				else if (chr1 == ']')
				{
					chr = '[';
				}
				else if (chr1 == '}')
				{
					chr = '{';
				}
				int selectionStart = str.SelectionStart;
				int num1 = str.FindMatch(keyChar, chr);
				if (num1 >= 0)
				{
					str.SelectedText = keyChar.ToString();
					str.SelectionStart = num1;
					str.SelectionLength = 1;
					str.SelectionBackColor = Color.Silver;
					str.SelectionStart = selectionStart;
					str.SelectionLength = 1;
					str.SelectionBackColor = Color.Silver;
					str.SelectionStart = selectionStart + 1;
					str.SelectionLength = 0;
					eKeyPress.Handled = true;
					str.HideToolTip();
				}
			}
			else if (Keys.OemPeriod == mKeyDown.KeyCode)
			{
				string precedingWord = str.GetPrecedingWord();
				string precedingWord1 = str.GetPrecedingWord(2);
				if (precedingWord1 != null && precedingWord1.Equals("MEMBER", StringComparison.InvariantCultureIgnoreCase))
				{
					return;
				}
				MDXDataType mDXDataType = MDXDataType.Unknown;
                MDXParser.CubeInfo cubeInfo = null;
				if (cn.MetadataConnection == null || string.IsNullOrEmpty(cn.CurrentCube))
				{
                    cubeInfo = new MDXParser.CubeInfo();
				}
				else
				{
					cubeInfo = new AdomdClientCubeInfo(cn.CurrentCube, cn.MetadataConnection);
				}
				if (precedingWord != null)
				{
					mDXDataType = cubeInfo.DetermineType(precedingWord);
				}
				this.m_AutoCompleteList.Items.Clear();
				flag = true;
				sObjects = MDXParserObjects.s_Objects;
				for (i = 0; i < (int)sObjects.Length; i++)
				{
					MDXObject mDXObject = sObjects[i];
					if ((mDXObject.SyntaxForm == MDXSyntaxForm.Method || mDXObject.SyntaxForm == MDXSyntaxForm.Property) && mDXObject.ThisType == mDXDataType)
					{
						num = (mDXObject.SyntaxForm == MDXSyntaxForm.Method ? this.m_AutoCompleteList.ImageList.Images.IndexOfKey("Function.bmp" /*.ico*/) : this.m_AutoCompleteList.ImageList.Images.IndexOfKey("Property.bmp" /*.ico*/));
						int num2 = num;
						this.m_AutoCompleteList.Items.Add(new AutoCompleteListItem(mDXObject.CanonicalName, num2));
					}
				}
				if (MDXDataType.Level == mDXDataType)
				{
					this.m_AutoCompleteList.Items.Add(new AutoCompleteListItem("MEMBERS", this.m_AutoCompleteList.ImageList.Images.IndexOfKey("Property.bmp" /*.ico*/)));
					this.m_AutoCompleteList.Items.Add(new AutoCompleteListItem("ALLMEMBERS", this.m_AutoCompleteList.ImageList.Images.IndexOfKey("Property.bmp" /*.ico*/)));
				}
				if (precedingWord != null && (precedingWord.Equals("Measures", StringComparison.InvariantCultureIgnoreCase) || precedingWord.Equals("[Measures]", StringComparison.InvariantCultureIgnoreCase)))
				{
					if (cn != null && !string.IsNullOrEmpty(cn.CurrentCube))
					{
						CubeDef item = cn.MetadataConnection.Cubes[cn.CurrentCube];
						MeasureCollection.Enumerator enumerator3 = item.Measures.GetEnumerator();
						while (enumerator3.MoveNext())
						{
							Measure current = enumerator3.Current;
							this.m_AutoCompleteList.Items.Add(new AutoCompleteListItem(current.Name, this.m_AutoCompleteList.ImageList.Images.IndexOfKey("Measure.bmp" /*.ico*/), current.UniqueName.Replace("[Measures].", "")));
						}
					}
				}
				else if (MDXDataType.Level == mDXDataType)
				{
					if (cn != null && !string.IsNullOrEmpty(cn.CurrentCube))
					{
						CubeDef cubeDef = cn.MetadataConnection.Cubes[cn.CurrentCube];
						bool flag1 = false;
						enumerator = cubeDef.Dimensions.GetEnumerator();
						while (enumerator.MoveNext())
						{
							Dimension dimension = enumerator.Current;
							enumerator1 = dimension.Hierarchies.GetEnumerator();
						Label1:
							while (enumerator1.MoveNext())
							{
								Hierarchy hierarchy = enumerator1.Current;
								enumerator2 = hierarchy.Levels.GetEnumerator();
								while (enumerator2.MoveNext())
								{
									Level level = enumerator2.Current;
									if (!level.UniqueName.Equals(precedingWord, StringComparison.InvariantCultureIgnoreCase))
									{
										continue;
									}
									flag1 = true;
									MemberCollection members = level.GetMembers((long)0, (long)20);
									MemberCollection.Enumerator enumerator4 = members.GetEnumerator();
									while (enumerator4.MoveNext())
									{
										Member member = enumerator4.Current;
										this.m_AutoCompleteList.Items.Add(new AutoCompleteListItem(member.Name, this.m_AutoCompleteList.ImageList.Images.IndexOfKey("Member.bmp" /*.ico*/), string.Format("[{0}]", member.Name)));
									}
									goto Label1;
								}
							}
							if (flag1)
							{
								goto Label0;
							}
						}
					}
				}
				else if (MDXDataType.Hierarchy == mDXDataType)
				{
					if (cn != null && !string.IsNullOrEmpty(cn.CurrentCube))
					{
						CubeDef item1 = cn.MetadataConnection.Cubes[cn.CurrentCube];
						DimensionCollection.Enumerator enumerator5 = item1.Dimensions.GetEnumerator();
						while (enumerator5.MoveNext())
						{
							Dimension current1 = enumerator5.Current;
							HierarchyCollection.Enumerator enumerator6 = current1.Hierarchies.GetEnumerator();
							while (enumerator6.MoveNext())
							{
								Hierarchy hierarchy1 = enumerator6.Current;
								if (!hierarchy1.UniqueName.Equals(precedingWord, StringComparison.InvariantCultureIgnoreCase))
								{
									continue;
								}
								enumerator2 = hierarchy1.Levels.GetEnumerator();
								while (enumerator2.MoveNext())
								{
									Level level1 = enumerator2.Current;
									this.m_AutoCompleteList.Items.Add(new AutoCompleteListItem(level1.Name, this.m_AutoCompleteList.ImageList.Images.IndexOfKey("Level2.bmp" /*.ico*/), string.Format("[{0}]", level1.Name)));
								}
							}
						}
					}
				}
				else if (MDXDataType.Dimension == mDXDataType)
				{
					if (cn != null && !string.IsNullOrEmpty(cn.CurrentCube))
					{
						CubeDef cubeDef1 = cn.MetadataConnection.Cubes[cn.CurrentCube];
						enumerator = cubeDef1.Dimensions.GetEnumerator();
						while (enumerator.MoveNext())
						{
							Dimension dimension1 = enumerator.Current;
							if (!dimension1.UniqueName.Equals(precedingWord, StringComparison.InvariantCultureIgnoreCase))
							{
								continue;
							}
							enumerator1 = dimension1.Hierarchies.GetEnumerator();
							while (enumerator1.MoveNext())
							{
								Hierarchy current2 = enumerator1.Current;
								this.m_AutoCompleteList.Items.Add(new AutoCompleteListItem(current2.Name, this.m_AutoCompleteList.ImageList.Images.IndexOfKey("Hierarchy.bmp" /*.ico*/), string.Format("[{0}]", current2.Name)));
							}
						}
					}
				}
				else if (cn == null)
				{
				}
			}
			else if (44 == keyChar)
			{
				if (this.m_AutoCompleteList.Visible)
				{
					this.selectItem();
				}
			}
			else if (40 == keyChar)
			{
				if (this.m_AutoCompleteList.Visible)
				{
					this.selectItem();
				}
				string currentWord = str.GetCurrentWord();
				if (currentWord != null)
				{
					MDXObject mDXObject1 = MDXParserObjects.FindMDXObject(currentWord);
					if (mDXObject1 != null && mDXObject1.SyntaxTip != null)
					{
						str.ShowToolTip(string.Format("{0}({1})", mDXObject1.CanonicalName, mDXObject1.SyntaxTip));
					}
				}
			}
			else if (91 == keyChar)
			{
				this.m_AutoCompleteList.Items.Clear();
				flag = true;
				if (cn == null || string.IsNullOrEmpty(cn.CurrentCube))
				{
					this.m_AutoCompleteList.Items.Add(new AutoCompleteListItem("Measures", this.m_AutoCompleteList.ImageList.Images.IndexOfKey("DbDimension.bmp" /*.ico*/), "Measures]"));
				}
				else
				{
					CubeDef item2 = cn.MetadataConnection.Cubes[cn.CurrentCube];
					enumerator = item2.Dimensions.GetEnumerator();
					while (enumerator.MoveNext())
					{
						Dimension dimension2 = enumerator.Current;
						this.m_AutoCompleteList.Items.Add(new AutoCompleteListItem(dimension2.Name, this.m_AutoCompleteList.ImageList.Images.IndexOfKey("DbDimension.bmp" /*.ico*/), string.Format("{0}]", dimension2.Name)));
					}
				}
			}
			else if (Keys.Return == mKeyDown.KeyCode)
			{
				str.HideToolTip();
			}
			else if (Keys.Space == mKeyDown.KeyCode)
			{
				this.m_AutoCompleteList.Items.Clear();
				flag = true;
				string str1 = str.GetPrecedingWord();
				string precedingWord2 = str.GetPrecedingWord(2);
				if (str1 != null)
				{
					if (str1.Equals("FROM", StringComparison.InvariantCultureIgnoreCase))
					{
						if (cn.MetadataConnection == null || cn.MetadataConnection.Cubes.Count <= 0)
						{
							str.ShowToolTip("If you were connected to the server, you would've seen the list of cubes now", 1500);
						}
						else
						{
							CubeCollection.Enumerator enumerator7 = cn.MetadataConnection.Cubes.GetEnumerator();
							while (enumerator7.MoveNext())
							{
								CubeDef cubeDef2 = enumerator7.Current;
                                
                                if (cubeDef2.Type != CubeType.Cube)
								{
									continue;
								}
								this.m_AutoCompleteList.Items.Add(new AutoCompleteListItem(cubeDef2.Name, this.m_AutoCompleteList.ImageList.Images.IndexOfKey("Cube.bmp" /*.ico*/), string.Format("[{0}]", cubeDef2.Name)));
							}
						}
					}
					else if (str1.Equals("ON", StringComparison.InvariantCultureIgnoreCase))
					{
						int num3 = this.m_AutoCompleteList.ImageList.Images.IndexOfKey("Keyword.bmp" /*.ico*/);
						this.m_AutoCompleteList.Items.Add(new AutoCompleteListItem("0", num3));
						this.m_AutoCompleteList.Items.Add(new AutoCompleteListItem("1", num3));
						this.m_AutoCompleteList.Items.Add(new AutoCompleteListItem("COLUMNS", num3));
						this.m_AutoCompleteList.Items.Add(new AutoCompleteListItem("ROWS", num3));
						this.m_AutoCompleteList.Items.Add(new AutoCompleteListItem("PAGES", num3));
					}
					else if (precedingWord2 != null && precedingWord2.Equals("FROM", StringComparison.InvariantCultureIgnoreCase))
					{
						this.m_AutoCompleteList.Items.Add(new AutoCompleteListItem("WHERE", this.m_AutoCompleteList.ImageList.Images.IndexOfKey("Keyword.bmp" /*.ico*/)));
						this.m_AutoCompleteList.Items.Add(new AutoCompleteListItem("CELL PROPERTIES", this.m_AutoCompleteList.ImageList.Images.IndexOfKey("Keyword.bmp" /*.ico*/)));
					}
				}
			}
			else if (!mKeyDown.Control && !mKeyDown.Alt && char.IsLetter(keyChar))
			{
				bool flag2 = false;
				if (this.m_Editor.SelectionStart > 0 && char.IsLetter(this.m_Editor.Text[this.m_Editor.SelectionStart - 1]))
				{
					flag2 = true;
				}
				if (!flag2)
				{
					this.m_AutoCompleteList.Items.Clear();
					flag = true;
					string currentWord1 = this.m_Editor.GetCurrentWord();
					currentWord1 = string.Concat(currentWord1, keyChar);
					string[] sKeywords = MDXParserObjects.s_Keywords;
					for (i = 0; i < (int)sKeywords.Length; i++)
					{
						string str2 = sKeywords[i];
						if (str2.StartsWith(currentWord1, StringComparison.InvariantCultureIgnoreCase))
						{
							this.m_AutoCompleteList.Items.Add(new AutoCompleteListItem(str2, this.m_AutoCompleteList.ImageList.Images.IndexOfKey("Keyword.bmp" /*.ico*/)));
						}
					}
					sObjects = MDXParserObjects.s_Objects;
					for (i = 0; i < (int)sObjects.Length; i++)
					{
						MDXObject mDXObject2 = sObjects[i];
						if (mDXObject2.SyntaxForm == MDXSyntaxForm.Function && mDXObject2.Optimization != MDXFunctionOpt.Deprecated && mDXObject2.CanonicalName.StartsWith(currentWord1, StringComparison.InvariantCultureIgnoreCase))
						{
							this.m_AutoCompleteList.Items.Add(new AutoCompleteListItem(mDXObject2.CanonicalName, this.m_AutoCompleteList.ImageList.Images.IndexOfKey("Function.bmp" /*.ico*/)));
						}
					}
					if (cn != null && !string.IsNullOrEmpty(cn.CurrentCube))
					{
						CubeDef item3 = cn.MetadataConnection.Cubes[cn.CurrentCube];
						enumerator = item3.Dimensions.GetEnumerator();
						while (enumerator.MoveNext())
						{
							Dimension current3 = enumerator.Current;
							if (!current3.Name.StartsWith(currentWord1, StringComparison.InvariantCultureIgnoreCase))
							{
								continue;
							}
							this.m_AutoCompleteList.Items.Add(new AutoCompleteListItem(current3.Name, this.m_AutoCompleteList.ImageList.Images.IndexOfKey("DbDimension.bmp" /*.ico*/), current3.UniqueName));
						}
					}
				}
			}
		Label0:
			if (flag && this.m_AutoCompleteList.Items.Count > 0)
			{
				bool flag3 = false;
				bool text = false;
				ColorCoding colorCoding = this.m_Editor.ColorCoding;
				if (colorCoding != null)
				{
					ColorSegment colorSegment = colorCoding.FindSegment(str.SelectionStart, str.SelectionStart);
					if (colorSegment == null)
					{
						if (str.SelectionStart == str.TextLength)
						{
							int selectionStart1 = str.SelectionStart - 1;
							while (selectionStart1 >= 0 && str.Text[selectionStart1] != '[' && str.Text[selectionStart1] != ']')
							{
								selectionStart1--;
							}
							if (selectionStart1 >= 0)
							{
								text = str.Text[selectionStart1] == '[';
							}
						}
					}
					else if (colorSegment.Color == Color.Green)
					{
						flag3 = true;
					}
					else if (colorSegment.Color == Color.Maroon)
					{
						text = string.IsNullOrEmpty(str.GetCurrentWord());
					}
				}
				if (!flag3 && !text)
				{
					this.m_Editor.HideToolTip();
					Point positionFromCharIndex = str.GetPositionFromCharIndex(str.SelectionStart);
					if (positionFromCharIndex.Y >= str.ClientSize.Height / 2)
					{
						int y = positionFromCharIndex.Y;
						clientSize = this.m_AutoCompleteList.ClientSize;
						positionFromCharIndex.Y = y - clientSize.Height;
					}
					else
					{
						positionFromCharIndex.Y = positionFromCharIndex.Y + (int)Math.Ceiling((double)str.Font.GetHeight()) + 4;
					}
					positionFromCharIndex.X = positionFromCharIndex.X + 4;
					if (positionFromCharIndex.X + this.m_AutoCompleteList.ClientSize.Width > str.ClientRectangle.Right)
					{
						int right = str.ClientRectangle.Right;
						clientSize = this.m_AutoCompleteList.ClientSize;
						positionFromCharIndex.X = right - clientSize.Width;
					}
					Point screen = str.PointToScreen(positionFromCharIndex);
					this.m_AutoCompleteList.Location = this.m_AutoCompleteList.Parent.PointToClient(screen);
					this.m_AutoCompleteList.BringToFront();
					this.m_AutoCompleteList.Show();
				}
			}
		}
		internal void ProcessMouseDownEvent(object sender, MouseEventArgs e)
		{
			this.m_Editor = sender as MdxEditor;
			this.m_AutoCompleteList.Hide();
			this.m_Editor.HideToolTip();
		}

		private void selectItem()
		{
			AutoCompleteListItem selectedItem = this.m_AutoCompleteList.SelectedItem as AutoCompleteListItem;
			if (selectedItem != null)
			{
				string text = selectedItem.Text;
				if (selectedItem.Tag != null)
				{
					text = selectedItem.Tag.ToString();
				}
				string currentWord = this.m_Editor.GetCurrentWord();
				MdxEditor mEditor = this.m_Editor;
				mEditor.SelectionStart = mEditor.SelectionStart - currentWord.Length;
				this.m_Editor.SelectionLength = currentWord.Length;
				this.m_Editor.SelectedText = "";
				this.m_Editor.SelectionLength = 0;
				this.m_Editor.SelectedText = text;
			}
			this.m_AutoCompleteList.Hide();
		}
	}
}