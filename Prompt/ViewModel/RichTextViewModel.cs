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

using GalaSoft.MvvmLight.Messaging;

namespace Prompt.ViewModel
{
    using System;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;
    using GalaSoft.MvvmLight.Messaging;
    using View.Commands;

    public class RichTextViewModel : ViewModelBase
    {
        public RichTextViewModel()
        {
            Commands = new EditCommands
            {
                DecreaseFontSizeCommand = new RelayCommand(()=> Messenger.Default.Send(new CommandMessage(CommandMessage.Commands.DecreaseFont))),
                DeleteCommand = new RelayCommand(()=> Messenger.Default.Send(new CommandMessage(CommandMessage.Commands.Delete))),
                FontColorCommand = new RelayCommand(()=> Messenger.Default.Send(new CommandMessage(CommandMessage.Commands.FontColor))),
                FontHighlightCommand = new RelayCommand(()=> Messenger.Default.Send(new CommandMessage(CommandMessage.Commands.Highlight))),
                IncreaseFontSizeCommand = new RelayCommand(()=> Messenger.Default.Send(new CommandMessage(CommandMessage.Commands.IncreaseFont))),
                OpenCommand = new RelayCommand(()=> Messenger.Default.Send(new CommandMessage(CommandMessage.Commands.Open))),
                SaveCommand = new RelayCommand(()=> Messenger.Default.Send(new CommandMessage(CommandMessage.Commands.Save))),
                SaveAsCommand = new RelayCommand(()=> Messenger.Default.Send(new CommandMessage(CommandMessage.Commands.SaveAs))),
                SpellCheckCommand = new RelayCommand(()=> Messenger.Default.Send(new CommandMessage(CommandMessage.Commands.SpellCheck)))
            };
        }

        public EditCommands Commands { get; }
    }

    public class CommandMessage : MessageBase
    {
        public enum Commands
        {
            Open,
            Save,
            SaveAs,
            SpellCheck,
            Delete,
            IncreaseFont,
            DecreaseFont,
            FontColor,
            Highlight
        }

        public CommandMessage(Commands command)
        {
            Command = command;
        }

        public Commands Command { get; set; }

        public object Parameter { get; set; }
    }
}