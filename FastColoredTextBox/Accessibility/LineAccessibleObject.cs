#region BSD 3-Clause License
// <copyright company="Edgerunner.org" file="LineAccessibleObject.cs">
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

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using FastColoredTextBoxNS.Types;

namespace FastColoredTextBoxNS;


public partial class FastColoredTextBox
{
   /// <summary>
   /// A class representing an accessibility object for a text line.
   /// </summary>
   /// <seealso cref="AccessibleObject" />
   public class LineAccessibleObject : AccessibleObject
   {
      public LineAccessibleObject(FastColoredTextBox textBox, int lineNo, int literalIndexNo, string text,
         Rectangle bounds)
      {
         TextBox = textBox;
         Name = text;
         _LineNo = lineNo;
         this.literalIndexNo = literalIndexNo;
         _Bounds = bounds;
      }


      private readonly int _LineNo;

      private readonly int literalIndexNo;

      protected bool Selected
      {
         get => selected;
         set
         {
            selected = value;
            TextBox.AccessibilityNotifyClients(
               selected ? AccessibleEvents.SelectionAdd : AccessibleEvents.SelectionRemove, literalIndexNo);
         }
      }

      protected FastColoredTextBox TextBox { get; set; }

      /// <summary>
      /// The bounds of the object.
      /// </summary>
      protected Rectangle _Bounds;

      private bool selected;

      /// <summary>
      /// Gets the location and size of the accessible object.
      /// </summary>
      public override Rectangle Bounds => _Bounds;

      /// <summary>
      /// Gets a string that describes the default action of the object. Not all objects have a default action.
      /// </summary>
      public override string DefaultAction => "Edit";

      /// <summary>
      /// Gets a string that describes the visual appearance of the specified object. Not all objects have a description.
      /// </summary>
      public override string Description => "Text line";

      /// <summary>
      /// Gets a description of what the object does or how the object is used.
      /// </summary>
      public override string Help => "Edit";

      /// <summary>
      /// Gets or sets the object name.
      /// </summary>
      public override string Name { get; set; }

      /// <summary>
      /// Gets the parent of an accessible object.
      /// </summary>
      public override AccessibleObject Parent => TextBox.AccessibilityObject;

      /// <summary>
      /// Gets the role of this accessible object.
      /// </summary>
      public override AccessibleRole Role => AccessibleRole.Text;

      /// <summary>
      /// Gets the state of this accessible object.
      /// </summary>
      public override AccessibleStates State
      {
         get
         {
            var state = AccessibleStates.Selectable;
            if (Selected)
               state |= AccessibleStates.Selected;
            return state;
         }
      }

      public override AccessibleObject Navigate(AccessibleNavigation navdir)
      {
         Debug.WriteLine("line navigation triggered");
         var fctbAccess = Parent as FastColoredTextBoxAccessibleObject;
         var indx = -1;
         indx = fctbAccess.Lines.IndexOf(this);
         switch (navdir)
         {
            case AccessibleNavigation.Down:
            case AccessibleNavigation.Next:
               if (indx == -1)
                  return base.Navigate(navdir);
               if (indx != fctbAccess.Lines.Count - 1)
                  indx++;
               return fctbAccess.Lines[indx];
            case AccessibleNavigation.Up:
            case AccessibleNavigation.Previous:
               if (indx == -1)
                  return base.Navigate(navdir);
               if (indx != 0)
                  indx--;
               return fctbAccess.Lines[indx];
            default:
               return base.Navigate(navdir);
         }
      }

      public override void Select(AccessibleSelection flags)
      {
         if (flags.HasFlag(AccessibleSelection.RemoveSelection))
            Selected = false;
         else if (flags.HasFlag(AccessibleSelection.AddSelection) || flags.HasFlag(AccessibleSelection.TakeSelection))
            Selected = true;
      }
   }
}