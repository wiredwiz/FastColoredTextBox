#region BSD 3-Clause License
// <copyright company="Edgerunner.org" file="FastColoredTextBox_TextProvider.cs">
// Copyright (c)  2022
// </copyright>
// 
// BSD 3-Clause License
// 
// Copyright (c) 2022,
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
// 
// 1. Redistributions of source code must retain the above copyright notice, this
//    list of conditions and the following disclaimer.
// 
// 2. Redistributions in binary form must reproduce the above copyright notice,
//    this list of conditions and the following disclaimer in the documentation
//    and/or other materials provided with the distribution.
// 
// 3. Neither the name of the copyright holder nor the names of its
//    contributors may be used to endorse or promote products derived from
//    this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion

// ReSharper disable once CheckNamespace

using FastColoredTextBoxNS.Types;
using System.Linq;
using System.Windows.Automation;
using System.Windows.Automation.Provider;

// ReSharper disable once CheckNamespace
namespace FastColoredTextBoxNS;

/// <summary>
/// ITextProvider implementation for FastColoredTextBox.
/// Implements the <see cref="System.Windows.Forms.UserControl" />
/// Implements the <see cref="IValueProvider" />
/// Implements the <see cref="System.ComponentModel.ISupportInitialize" />
/// Implements the <see cref="ITextProvider" />
/// Implements the <see cref="IRawElementProviderSimple" />
/// </summary>
/// <remarks>Important links for implementation
/// https://learn.microsoft.com/en-us/dotnet/framework/ui-automation/expose-a-server-side-ui-automation-provider
/// https://learn.microsoft.com/en-us/dotnet/framework/ui-automation/server-side-ui-automation-provider-implementation
/// https://learn.microsoft.com/en-us/windows/win32/winauto/uiauto-implementingtextandtextrange
/// https://learn.microsoft.com/en-us/windows/win32/winauto/uiauto-ui-automation-textpattern-overview
/// https://learn.microsoft.com/en-us/windows/win32/winauto/uiauto-about-text-and-textrange-patterns
/// https://learn.microsoft.com/en-us/dotnet/api/system.windows.automation.provider.itextprovider?view=netframework-4.8
/// https://learn.microsoft.com/en-us/dotnet/api/system.windows.automation.provider.itextrangeprovider?view=netframework-4.8
/// </remarks>
/// <seealso cref="System.Windows.Forms.UserControl" />
/// <seealso cref="IValueProvider" />
/// <seealso cref="System.ComponentModel.ISupportInitialize" />
/// <seealso cref="ITextProvider" />
/// <seealso cref="IRawElementProviderSimple" />
public partial class FastColoredTextBox : ITextProvider
{
   /// <summary>
   /// Gets a collection of text ranges that represents the currently selected text in a text-based control.
   /// </summary>
   /// <returns>An array of <see cref="ITextRangeProvider"/> representing current selections.</returns>
   public ITextRangeProvider[] GetSelection()
   {
      return new ITextRangeProvider[] { Selection };
   }

   /// <summary>
   /// Retrieves an array of disjoint text ranges from a text container where each text range begins with the first
   /// partially visible line through to the end of the last partially visible line.
   /// </summary>
   /// <returns>An array of <see cref="ITextRangeProvider"/> interfaces of the visible text ranges or an empty array.</returns>
   public ITextRangeProvider[] GetVisibleRanges()
   {
      var visible = Accessibility.AccessibilityHelper.GetVisibleLines(this);
      var result = new ITextRangeProvider[visible.Count];
      for (int i = 0; i < visible.Count; i++)
      {
         var line = visible[i];
         var range = new TextSelectionRange(this,
                                            line.StartX,
                                            line.SourceLineNo,
                                            line.Text.Length - line.StartX,
                                            line.SourceLineNo);
         result[i] = range;
      }

      return result;
   }

   /// <summary>
   /// Retrieves a text range that encloses the specified child element (for example, an image, hyperlink, or other embedded object).
   /// </summary>
   /// <param name="childElement">The UI Automation provider of the specified child element.</param>
   /// <returns>
   /// The text range that encloses the child element.
   /// </returns>
   /// <remarks>
   /// The returned range completely encloses the content of the child element such that:
   ///   1. ITextRangeProvider::GetEnclosingElement returns the child element itself, or the innermost descendant of the child element
   ///      that shares the same text range as the child element
   ///   2. ITextRangeProvider::GetChildren returns children of the element from (1) that are completely enclosed within the range
   ///   3. Both endpoints of the range are at the boundaries of the child element
   /// </remarks>
   public ITextRangeProvider RangeFromChild(IRawElementProviderSimple childElement)
   {
      return null;
   }

   /// <summary>
   /// Returns the degenerate (empty) text range nearest to the specified screen coordinates.
   /// </summary>
   /// <param name="screenLocation">The location in screen coordinates.</param>
   /// <returns>A degenerate (empty) text range nearest the specified location..</returns>
   public ITextRangeProvider RangeFromPoint(System.Windows.Point screenLocation)
   {
      var editorPlace = this.PointToPlace(new System.Drawing.Point((int)screenLocation.X, (int)screenLocation.Y));
      return new TextSelectionRange(this, editorPlace, editorPlace);
   }

   /// <summary>
   /// Gets a text range that encloses the main text of a document.
   /// </summary>
   /// <value>A <see cref="ITextRangeProvider"/> instance that contains the entire document text.</value>
   public ITextRangeProvider DocumentRange
   {
      get
      {
         var start = new Place(0, 0);
         var end = LinesCount != 0 ? new Place(this[LinesCount - 1].Count, LinesCount - 1) : start;
         return new TextSelectionRange(this, start, end);
      }
   }

   /// <summary>
   /// Gets a value that specifies the type of text selection that is supported by the control.
   /// </summary>
   /// <value>The supported text selection type(s) for the document.</value>
   public SupportedTextSelection SupportedTextSelection => SupportedTextSelection.Single;
}