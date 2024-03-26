using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProtocol.Reader
{
    public interface IReader
    {
        RawPacket? ReadPacket(IByteBuffer buffer); // ? - moze byc NULLEM

        IByteBuffer GetPayload(RawPacket packet);

        IPacket? ParsePacket(RawPacket packet, params IPacket[] expectedPackets);

    }
}