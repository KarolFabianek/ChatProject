﻿using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProtocol.Writer
{
    public interface IWriter
    {
        void WritePacket(IPacket packet);

        RawPacket PacketToRaw(IPacket packet);

        int PacketControl();

        void Flush(IChannelHandlerContext context);

    }
}