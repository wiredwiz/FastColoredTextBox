#region BSD 3-Clause License
// <copyright company="Edgerunner.org" file="FastColoredTextBox_RawElementProviderSimple.cs">
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

using System.Windows.Automation;
using System.Windows.Automation.Provider;

// ReSharper disable once CheckNamespace
namespace FastColoredTextBoxNS;

public partial class FastColoredTextBox : IRawElementProviderSimple
{
   /// <summary>
   /// Retrieves a pointer to an object that provides support for a control pattern on a Microsoft UI Automation element.
   /// </summary>
   /// <param name="patternId">The identifier of the control pattern. For a list of control pattern IDs, see Control Pattern Identifiers.</param>
   /// <returns>Returns a reference to the object that supports the control pattern, or NULL if the control pattern is not supported.</returns>
   public object GetPatternProvider(int patternId)
   {
      if (patternId == TextPatternIdentifiers.Pattern.Id || 
          patternId == ValuePatternIdentifiers.Pattern.Id ||
          patternId == RangeValuePatternIdentifiers.Pattern.Id)
         return this;

      return null;
   }

   /// <summary>
   /// Retrieves the value of a property supported by the Microsoft UI Automation provider.
   /// </summary>
   /// <param name="propertyId">The property identifier. For a list of property IDs, see Property Identifiers.</param>
   /// <returns>Returns the property value, or <c>null</c> if the property is not supported by this provider.</returns>
   public object GetPropertyValue(int propertyId)
   {
      if (propertyId == AutomationElementIdentifiers.AutomationIdProperty.Id)
         return "FastColoredTextBox";

      if (propertyId == AutomationElementIdentifiers.ClickablePointProperty.Id)
         return PlaceToPoint(Selection.Start);

      if (propertyId == AutomationElementIdentifiers.IsKeyboardFocusableProperty.Id)
         return true;

      if (propertyId == AutomationElementIdentifiers.HasKeyboardFocusProperty.Id)
         return Focused;

      if (propertyId == AutomationElementIdentifiers.NameProperty.Id)
         return "FastColoredTextBox";

      if (propertyId == AutomationElementIdentifiers.LocalizedControlTypeProperty.Id)
         return "edit";

      if (propertyId == AutomationElementIdentifiers.BoundingRectangleProperty.Id)
         return ClientRectangle;

      if (propertyId == AutomationElementIdentifiers.IsContentElementProperty.Id)
         return true;

      if (propertyId == AutomationElementIdentifiers.IsControlElementProperty.Id)
         return true;

      if (propertyId == AutomationElementIdentifiers.IsPasswordProperty.Id)
         return false;

      return null;
   }

   /// <summary>
   /// Specifies the type of Microsoft UI Automation provider; for example, whether it is a client-side (proxy) or server-side provider.
   /// </summary>
   /// <value>The provider options.</value>
   public ProviderOptions ProviderOptions => ProviderOptions.ServerSideProvider | ProviderOptions.UseComThreading;

   /// <summary>
   /// Specifies the host provider for this element.
   /// </summary>
   /// <value>The host raw element provider.</value>
   public IRawElementProviderSimple HostRawElementProvider => AutomationInteropProvider.HostProviderFromHandle(this.Handle);
}