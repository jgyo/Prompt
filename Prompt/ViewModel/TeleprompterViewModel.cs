// Copyright (c) 2017 Gil Yoder <gil.yoder@oabs.org>
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
// * Redistributions of source code must retain the above copyright notice, 
//   this list of conditions and the following disclaimer. 
// 
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution. 
// 
// * Neither the name of Prompt nor the names of its 
//   contributors may be used to endorse or promote products derived from this
//   software without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.

namespace Prompt.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;
    using GalaSoft.MvvmLight;

    public class TeleprompterViewModel : ViewModelBase
    {
        private Color _backgroundColor;

        private TimeSpan _elapsedTime;
        private double _eyelineOpacity;

        private EyelinePosition _eyelinePosition;

        private FontFamily _fontFamily;

        private double _fontSize;

        private Color _foregroundColor;

        private bool _isMirrored;

        private bool _isPrompting;
        private bool _isTalentWindowOpened;
        private double _lineSpacing;

        private TimeSpan _timeRemaining;

        public TeleprompterViewModel()
        {
            FontFamilies = new List<FontFamily>();
            foreach (var family in Fonts.SystemFontFamilies)
                if (family.Baseline < 2)
                    FontFamilies.Add(family);
        }

        public Color BackgroundColor
        {
            get => _backgroundColor;
            set => Set(ref _backgroundColor, value);
        }

        public TimeSpan ElapsedTime
        {
            get => _elapsedTime;
            set => Set(ref _elapsedTime, value);
        }

        public double EyelineOpacity
        {
            get => _eyelineOpacity;
            set => Set(ref _eyelineOpacity, value);
        }

        public EyelinePosition EyelinePosition
        {
            get => _eyelinePosition;
            set => Set(ref _eyelinePosition, value);
        }

        public List<FontFamily> FontFamilies { get; }

        public FontFamily FontFamily
        {
            get => _fontFamily;
            set => Set(ref _fontFamily, value);
        }

        public double FontSize
        {
            get => _fontSize;
            set => Set(ref _fontSize, value);
        }

        public Color ForegroundColor
        {
            get => _foregroundColor;
            set => Set(ref _foregroundColor, value);
        }

        public bool IsMirrored
        {
            get => _isMirrored;
            set => Set(ref _isMirrored, value);
        }

        public bool IsPrompting
        {
            get => _isPrompting;
            set => Set(ref _isPrompting, value);
        }

        public bool IsTalentWindowOpened
        {
            get => _isTalentWindowOpened;
            set => Set(ref _isTalentWindowOpened, value);
        }

        public double LineSpacing
        {
            get => _lineSpacing;
            set => Set(ref _lineSpacing, value);
        }

        public TimeSpan TimeRemaining
        {
            get => _timeRemaining;
            set => Set(ref _timeRemaining, value);
        }
    }
}