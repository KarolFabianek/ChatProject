using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace ChatProtocol.Writer
{
    public class PacketWriter : IWriter
    {
        private IByteBuffer _byteBuffer;
        private PooledByteBufferAllocator _allocator;

        public PacketWriter()
        {
            _allocator = new PooledByteBufferAllocator();
            _byteBuffer = _allocator.Buffer();
        }
        
        public void WritePacket(IPacket packet)
        {
            lock (_byteBuffer)
            {
                if (packet is RawPacket)
                {
                    var raw = (RawPacket)packet;
                    _byteBuffer.WriteByte(raw.PackedId());
                    _byteBuffer.WriteInt(Convert.ToInt32(raw.DataLength));
                    _byteBuffer.WriteBytes(raw.Payload);
                    return;
                }

                var raw2 = PacketToRaw(packet);
                _byteBuffer.WriteByte(raw2.PackedId());
                _byteBuffer.WriteInt(Convert.ToInt32(raw2.DataLength));
                _byteBuffer.WriteBytes(raw2.Payload);
            }
        }
        public RawPacket PacketToRaw(IPacket packet)
        {
            RawPacket packetToRaw = new RawPacket();
            packetToRaw.Id = packet.PackedId();
            var byteBuffor = _allocator.Buffer();
            packet.Serialize(byteBuffor);
            packetToRaw.Payload = byteBuffor.Copy();
            packetToRaw.DataLength = byteBuffor.ReadableBytes;
            return packetToRaw;
        }

        public int PacketControl()
        {
            return _byteBuffer.ReadableBytes;
        }

        public void Flush(IChannelHandlerContext context)
        {
            lock (_byteBuffer) // lock  - blokuje zmienna dla danego watku
            {
                context.WriteAndFlushAsync(Unpooled.WrappedBuffer(_byteBuffer));
                _byteBuffer = _allocator.Buffer(PacketConstrains.Threshold);
            }
        }
    }
}
