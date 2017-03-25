﻿// <author>Stefán Örvar Sigmundsson</author>
// <copyright company="eMedia Intellect℠" file="OnMouseLeave.cs">
//    Copyright © 2016–2017 eMedia Intellect℠
// </copyright>
// <licence>
//    This file is part of eMI User Controls Library.
//
//    eMI User Controls Library is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    eMI User Controls Library is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with eMI User Controls Library. If not, see http://www.gnu.org/licenses/.
// </licence>

namespace Emi.UserControls
{
    using System.Windows;
    using System.Windows.Input;

    /// <content>Contains the <see cref="OnMouseLeave"/> method.</content>
    public partial class CloseableTabItem
    {
        /// <summary>Makes the close button invisible when the mouse leaves.</summary>
        /// <param name="e">The state information of the event handler.</param>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            if (!this.IsSelected)
            {
                this.closeableHeader.closeButton.Visibility = Visibility.Hidden;
            }
        }
    }
}