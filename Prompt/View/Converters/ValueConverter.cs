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
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;
    using JetBrains.Annotations;

    /// <summary>
    /// Class ValueConverter.
    /// </summary>
    /// <typeparam name="TInput">The type of the t input.</typeparam>
    /// <typeparam name="TOutput">The type of the t output.</typeparam>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    public class ValueConverter<TInput, TOutput> : IValueConverter
    {
        private readonly Func<ConverterParams<TOutput>, object> _convertBackFunction;
        private readonly Func<ConverterParams<TInput>, TOutput> _convertFunction;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueConverter{TInput, TOutput}"/> class.
        /// </summary>
        /// <param name="convertFunction">The convert function.</param>
        public ValueConverter(Func<ConverterParams<TInput>, TOutput> convertFunction)
        {
            _convertFunction = convertFunction;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueConverter{TInput, TOutput}"/> class.
        /// </summary>
        /// <param name="convertFunction">The convert function.</param>
        /// <param name="convertBackFunction">The convert back function.</param>
        public ValueConverter(Func<ConverterParams<TInput>, TOutput> convertFunction,
            Func<ConverterParams<TOutput>, object> convertBackFunction)
        {
            _convertFunction = convertFunction;
            _convertBackFunction = convertBackFunction;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TInput inputValue;
            if (value is TInput)
                inputValue = (TInput)value;
            else
                return value;

            if (!targetType.IsAssignableFrom(typeof(TOutput)))
                return value;

            var converterParams = new ConverterParams<TInput>(inputValue, parameter, culture);
            return _convertFunction(converterParams);
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TOutput outputValue;
            if (value is TOutput)
                outputValue = (TOutput)value;
            else
                return value;

            if (!targetType.IsAssignableFrom(typeof(TInput)))
                return value;

            var converterParams = new ConverterParams<TOutput>(outputValue, parameter, culture);
            return _convertBackFunction(converterParams);
        }

        /// <summary>
        /// Creates the specified convert function.
        /// </summary>
        /// <param name="convertFunc">The convert function.</param>
        /// <returns>IValueConverter.</returns>
        public static IValueConverter Create(Func<ConverterParams<TInput>, TOutput> convertFunc)
        {
            return new ValueConverter<TInput, TOutput>(convertFunc);
        }

        /// <summary>
        /// Creates the specified convert function.
        /// </summary>
        /// <param name="convertFunc">The convert function.</param>
        /// <param name="convertBackFunc">The convert back function.</param>
        /// <returns>IValueConverter.</returns>
        public static IValueConverter Create(Func<ConverterParams<TInput>, TOutput> convertFunc,
            Func<ConverterParams<TOutput>, object> convertBackFunc)
        {
            return new ValueConverter<TInput, TOutput>(convertFunc, convertBackFunc);
        }

        /// <summary>
        /// Class ConverterParams.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class ConverterParams<T>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ConverterParams`1"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="targetType">Type of the target.</param>
            /// <param name="parameter">The parameter.</param>
            /// <param name="culture">The culture.</param>
            public ConverterParams(T value, object parameter, CultureInfo culture)
            {
                Value = value;
                Parameter = parameter;
                Culture = culture;
            }

            /// <summary>
            /// Gets the culture.
            /// </summary>
            /// <value>The culture.</value>
            [CanBeNull]
            public CultureInfo Culture { get; }

            /// <summary>
            /// Gets the parameter.
            /// </summary>
            /// <value>The parameter.</value>
            [CanBeNull]
            public object Parameter { get; }

            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <value>The value.</value>
            [CanBeNull]
            public T Value { get; }
        }
    }

    public class MultiValueConverter<TInput, TOutput> : IMultiValueConverter
    {
        private readonly Func<MultiValueConverterParams<TInput>, TOutput> _convertFunction;

        public MultiValueConverter(Func<MultiValueConverterParams<TInput>, TOutput> convertFunction)
        {
            _convertFunction = convertFunction;
        }

        public object Convert([NotNull] object[] values, [NotNull] Type targetType, [CanBeNull] object parameter,
            [CanBeNull] CultureInfo culture)
        {
            if(values.Length == 0)
                return default(TOutput);

            if(values.Any(e => e.GetType() != typeof(TInput)))
                return default(TOutput);

            if (targetType != typeof(TOutput))
                return null;

            return _convertFunction(new MultiValueConverterParams<TInput>(values, parameter, culture));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MultiValueConverterParams<Tinput>
    {
        public List<Tinput> Values { get; }
        public object Parameter { get; }
        public CultureInfo Culture { get; }

        public MultiValueConverterParams(object[] values, object parameter, CultureInfo culture)
        {
            Values  = new List<Tinput>();
            foreach (var value in values)
            {
                Values.Add((Tinput)value);
            }

            this.Parameter = parameter;
            this.Culture = culture;
        }
    }
}