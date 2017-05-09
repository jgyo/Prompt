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
namespace Prompt.View.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class ValueConverter<TInput, TOutput> : IValueConverter
    {
        private readonly Func<ConverterParams<TOutput>, TInput> _convertBackFunction;
        private readonly Func<ConverterParams<TInput>, TOutput> _convertFunction;

        public ValueConverter(Func<ConverterParams<TInput>, TOutput> convertFunction)
        {
            _convertFunction = convertFunction;
        }

        public ValueConverter(Func<ConverterParams<TInput>, TOutput> convertFunction,
            Func<ConverterParams<TOutput>, TInput> convertBackFunction)
        {
            _convertFunction = convertFunction;
            _convertBackFunction = convertBackFunction;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TInput inputValue;
            if (value is TInput)
                inputValue = (TInput)value;
            else
                return value;

            if (!targetType.IsAssignableFrom(typeof(TOutput)))
                return value;

            var converterParams = new ConverterParams<TInput>(inputValue, targetType, parameter, culture);
            return _convertFunction(converterParams);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TOutput outputValue;
            if (value is TOutput)
                outputValue = (TOutput)value;
            else
                return value;

            if (!targetType.IsAssignableFrom(typeof(TInput)))
                return value;

            var converterParams = new ConverterParams<TOutput>(outputValue, targetType, parameter, culture);
            return _convertBackFunction(converterParams);
        }

        public static IValueConverter Create(Func<ConverterParams<TInput>, TOutput> convertFunc)
        {
            return new ValueConverter<TInput, TOutput>(convertFunc);
        }

        public static IValueConverter Create(Func<ConverterParams<TInput>, TOutput> convertFunc,
            Func<ConverterParams<TOutput>, TInput> convertBackFunc)
        {
            return new ValueConverter<TInput, TOutput>(convertFunc, convertBackFunc);
        }

        public class ConverterParams<T>
        {
            public ConverterParams(T value, Type targetType, object parameter, CultureInfo culture)
            {
                Value = value;
                TargetType = targetType;
                Parameter = parameter;
                Culture = culture;
            }

            public CultureInfo Culture { get; }

            public object Parameter { get; }

            public Type TargetType { get; }

            public T Value { get; }
        }
    }
}