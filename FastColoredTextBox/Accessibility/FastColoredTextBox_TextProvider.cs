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

using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Provider;

// ReSharper disable once CheckNamespace
namespace FastColoredTextBoxNS;

public partial class FastColoredTextBox : ITextProvider
{
   /// <summary>
   /// Gets the current selection.
   /// </summary>
   /// <returns>ITextRangeProvider[].</returns>
   /// <remarks>Important links for implementation
   /// https://learn.microsoft.com/en-us/dotnet/framework/ui-automation/expose-a-server-side-ui-automation-provider
   /// https://learn.microsoft.com/en-us/dotnet/framework/ui-automation/server-side-ui-automation-provider-implementation
   /// https://learn.microsoft.com/en-us/windows/win32/winauto/uiauto-implementingtextandtextrange
   /// https://learn.microsoft.com/en-us/windows/win32/winauto/uiauto-ui-automation-textpattern-overview
   /// https://learn.microsoft.com/en-us/windows/win32/winauto/uiauto-about-text-and-textrange-patterns
   /// https://learn.microsoft.com/en-us/dotnet/api/system.windows.automation.provider.itextprovider?view=netframework-4.8
   /// https://learn.microsoft.com/en-us/dotnet/api/system.windows.automation.provider.itextrangeprovider?view=netframework-4.8
   /// </remarks>
   public ITextRangeProvider[] GetSelection()
   {
      throw new System.NotImplementedException();
   }

   public ITextRangeProvider[] GetVisibleRanges()
   {
      throw new System.NotImplementedException();
   }

   public ITextRangeProvider RangeFromChild(IRawElementProviderSimple childElement)
   {
      throw new System.NotImplementedException();
   }

   public ITextRangeProvider RangeFromPoint(Point screenLocation)
   {
      throw new System.NotImplementedException();
   }

   public ITextRangeProvider DocumentRange => throw new System.NotImplementedException();

   public SupportedTextSelection SupportedTextSelection => throw new System.NotImplementedException();
}