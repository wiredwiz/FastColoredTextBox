#region BSD 3-Clause License
// <copyright company="Edgerunner.org" file="TextSelectionRange_TextRangeProvider.cs">
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

using System;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Automation.Text;

// ReSharper disable once CheckNamespace
namespace FastColoredTextBoxNS.Types;

public partial class TextSelectionRange : ITextRangeProvider
{
   ITextRangeProvider ITextRangeProvider.Clone()
   {
      return this.Clone();
   }

   bool ITextRangeProvider.Compare(ITextRangeProvider range)
   {
      return range == this;
   }

   /// <summary>
   /// Returns a value that specifies whether two text ranges have identical endpoints.
   /// </summary>
   /// <param name="endpoint">The Start or End endpoint of the caller.</param>
   /// <param name="targetRange">The target range for comparison.</param>
   /// <param name="targetEndpoint">The Start or End endpoint of the target.</param>
   /// <returns>
   /// Returns a negative value if the caller's endpoint occurs earlier in the text than the target endpoint.
   /// Returns zero if the caller's endpoint is at the same location as the target endpoint.
   /// Returns a positive value if the caller's endpoint occurs later in the text than the target endpoint.</returns>
   /// <exception cref="System.ArgumentException">Endpoints are from different text range sources.</exception>
   int ITextRangeProvider.CompareEndpoints(TextPatternRangeEndpoint endpoint, ITextRangeProvider targetRange,
      TextPatternRangeEndpoint targetEndpoint)
   {
      var target = targetRange as TextSelectionRange;
      if (target == null || tb != target.tb)
         throw new ArgumentException("Endpoints are from different text range sources.");

      if (endpoint == TextPatternRangeEndpoint.Start)
         return Start.CompareTo(target.Start);
      if (endpoint == TextPatternRangeEndpoint.End)
         return End.CompareTo(target.End);

      throw new ArgumentException("Unhandled endpoint enumeration type");
   }

   /// <summary>Expands the text range to the specified text unit.</summary>
   /// <param name="unit">The textual unit.</param>
   /// <remarks>
   ///   <para>
   /// If the range is already an exact quantity of the specified units then it remains unchanged.
   /// There is a series of steps are involved behind the scenes in order for the Move method to execute successfully.
   /// </para>
   ///   <list type="number">
   ///     <item>The text range is normalized; that is, the text range is collapsed to a degenerate range at the Start endpoint, which makes the End endpoint superfluous. This step is necessary to remove ambiguity in situations where a text range spans unit boundaries; for example, "{The U}RL https://www.microsoft.com/ is embedded in text" where "{" and "}" are the text range endpoints. </item>
   ///     <item>The resulting range is moved backward in the DocumentRange to the beginning of the requested unit boundary. </item>
   ///     <item>The range is moved forward or backward in the DocumentRange by the requested number of unit boundaries. </item>
   ///     <item>The range is then expanded from a degenerate range state by moving the End endpoint by one requested unit boundary. </item>
   ///   </list>
   ///   <para>
   /// ExpandToEnclosingUnit respects both hidden and visible text.
   /// ExpandToEnclosingUnit defers to the next largest TextUnit supported if the given TextUnit is not supported by the control.
   /// The order, from smallest unit to largest, is listed below.
   /// </para>
   ///   <list type="bullet">
   ///     <item>Character</item>
   ///     <item>Format</item>
   ///     <item>Word</item>
   ///     <item>Line</item>
   ///     <item>Paragraph</item>
   ///     <item>Page</item>
   ///     <item>Document
   /// </item>
   ///   </list>
   /// </remarks>
   void ITextRangeProvider.ExpandToEnclosingUnit(TextUnit unit)
   {
      TextSelectionRange range = this;
      switch (unit)
      {
         case TextUnit.Character: 
            range = tb.GetCharacterSelection(Start);
            break;
         case TextUnit.Word:
         case TextUnit.Format:
         case TextUnit.Line:
            range = tb.GetLineSelection(Start);
            break;
         case TextUnit.Paragraph:
         case TextUnit.Page:
         case TextUnit.Document:
            range = tb.GetDocumentSelection(Start);
            break;
      }

      tb.Selection = range;
   }

   ITextRangeProvider ITextRangeProvider.FindAttribute(int attribute, object value, bool backward)
   {
      throw new System.NotImplementedException();
   }

   ITextRangeProvider ITextRangeProvider.FindText(string text, bool backward, bool ignoreCase)
   {
      throw new System.NotImplementedException();
   }

   object ITextRangeProvider.GetAttributeValue(int attribute)
   {
      if (attribute == TextPatternIdentifiers.IsReadOnlyAttribute.Id)
         return ReadOnly;

      return null;
   }

   double[] ITextRangeProvider.GetBoundingRectangles()
   {
      var NumberOfLines = (End.iLine - Start.iLine) + 1;
      var xAdjust = tb.CharWidth * Start.iChar;
      var height = tb.CharHeight * NumberOfLines;
      // TODO: bad logic, needs fixing, but is a temp fix for working
      return new double[4] { tb.ClientRectangle.X, tb.ClientRectangle.Y, tb.ClientRectangle.Width, tb.ClientRectangle.Height };
   }

   IRawElementProviderSimple ITextRangeProvider.GetEnclosingElement()
   {
      throw new System.NotImplementedException();
   }

   string ITextRangeProvider.GetText(int maxLength)
   {
      return Text != null && Text.Length > maxLength ? Text.Substring(0, maxLength) : Text;
   }

   /// <summary>
   /// Moves the text range the specified number of text units.
   /// </summary>
   /// <param name="unit">The text unit boundary.</param>
   /// <param name="count">The number of text units to move.</param>
   /// <returns>
   /// The number of units actually moved. This can be less than the number requested
   /// if either of the new text range endpoints is greater than or less than the DocumentRange endpoints.</returns>
   int ITextRangeProvider.Move(TextUnit unit, int count)
   {
      var actual = count;
      TextSelectionRange range = this;
      switch (unit)
      {
         case TextUnit.Character: 
            range = tb.GetNextCharacterSelection(End, ref actual);
            break;
         case TextUnit.Word:
         case TextUnit.Format:
         case TextUnit.Line:
            range = tb.GetNextLineSelection(End, ref actual);
            break;
         case TextUnit.Paragraph:
         case TextUnit.Page:
         case TextUnit.Document:
            range = tb.GetDocumentSelection(End);
            actual = 0;
            break;
      }

      tb.Selection = range;
      return actual;
   }

   int ITextRangeProvider.MoveEndpointByUnit(TextPatternRangeEndpoint endpoint, TextUnit unit, int count)
   {
      throw new System.NotImplementedException();
   }

   /// <summary>
   /// Moves one endpoint of a text range to the specified endpoint of a second text range.
   /// </summary>
   /// <param name="endpoint">The endpoint to move.</param>
   /// <param name="targetRange">Another range from the same text provider.</param>
   /// <param name="targetEndpoint">An endpoint on the other range.</param>
   /// <remarks>
   /// If the endpoint being moved crosses the other endpoint of the same text range then that other
   /// endpoint is moved also, resulting in a degenerate range and ensuring the correct ordering of the endpoints
   /// (that is, Start is always less than or equal to End).
   /// </remarks>
   void ITextRangeProvider.MoveEndpointByRange(TextPatternRangeEndpoint endpoint, ITextRangeProvider targetRange,
      TextPatternRangeEndpoint targetEndpoint)
   {
      var target = targetRange as TextSelectionRange;
      if (target == null)
         throw new ArgumentException($"{nameof(targetRange)} is from a different ITextRangeProvider");

      if (endpoint == TextPatternRangeEndpoint.Start)
      {
         Start = targetEndpoint == TextPatternRangeEndpoint.Start ? target.Start : target.End;
         if (End < Start)
            End = Start;
         return;
      }
      if (endpoint == TextPatternRangeEndpoint.End)
      {
         End = targetEndpoint == TextPatternRangeEndpoint.Start ? target.Start : target.End;
         if (End < Start)
            Start = End;
         return;
      }

      throw new ArgumentException("Unsupported EndPoint enumeration");
   }

   /// <summary>
   /// Selects this instance.
   /// </summary>
   /// <exception cref="System.NotImplementedException"></exception>
   void ITextRangeProvider.Select()
   {
      tb.Selection = this;
   }

   /// <summary>
   /// Adds to selection.
   /// </summary>
   /// <exception cref="System.NotImplementedException"></exception>
   void ITextRangeProvider.AddToSelection()
   {
      throw new System.NotImplementedException();
   }

   /// <summary>
   /// Removes from selection.
   /// </summary>
   /// <exception cref="System.NotImplementedException"></exception>
   void ITextRangeProvider.RemoveFromSelection()
   {
      throw new System.NotImplementedException();
   }

   /// <summary>
   /// Scrolls the into view.
   /// </summary>
   /// <param name="alignToTop">if set to <c>true</c> [align to top].</param>
   /// <exception cref="System.NotImplementedException"></exception>
   void ITextRangeProvider.ScrollIntoView(bool alignToTop)
   {
      throw new System.NotImplementedException();
   }

   /// <summary>
   /// Gets the children.
   /// </summary>
   /// <returns>IRawElementProviderSimple[].</returns>
   /// <exception cref="System.NotImplementedException"></exception>
   IRawElementProviderSimple[] ITextRangeProvider.GetChildren()
   {
      return Array.Empty<IRawElementProviderSimple>();
   }
}