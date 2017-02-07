﻿using System;
using System.Collections.Generic;
using System.IO;

namespace AntShares.VM
{
    public class ScriptContext : IDisposable
    {
        public readonly byte[] Script;
        internal readonly bool PushOnly;
        internal readonly BinaryReader OpReader;
        internal readonly HashSet<uint> BreakPoints;

        public int InstructionPointer
        {
            get
            {
                return (int)OpReader.BaseStream.Position;
            }
            set
            {
                OpReader.BaseStream.Seek(value, SeekOrigin.Begin);
            }
        }

        internal ScriptContext(byte[] script, bool push_only, HashSet<uint> break_points = null)
        {
            this.Script = script;
            this.PushOnly = push_only;
            this.OpReader = new BinaryReader(new MemoryStream(script, false));
            this.BreakPoints = break_points ?? new HashSet<uint>();
        }

        public ScriptContext Clone()
        {
            return new ScriptContext(Script, PushOnly, BreakPoints)
            {
                InstructionPointer = InstructionPointer
            };
        }

        public void Dispose()
        {
            OpReader.Dispose();
        }
    }
}
