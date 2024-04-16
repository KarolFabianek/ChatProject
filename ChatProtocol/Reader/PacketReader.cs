using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProtocol.Reader
{
    public class PacketReader : IReader
    {
        private IByteBuffer _byteBuffer;
        private PooledByteBufferAllocator _allocator;

        public PacketReader()
        {
            _allocator = new PooledByteBufferAllocator();
            _byteBuffer = _allocator.Buffer(0);
        }

        public RawPacket? ReadPacket(IByteBuffer buffer)
        {
            var rawPacket = new RawPacket();
            var byteBuffer = buffer;
            
            if (_byteBuffer.ReadableBytes > 0)
            {
                _byteBuffer = new CompositeByteBuffer(_allocator, false, 2, _byteBuffer, byteBuffer);
                buffer.SetReaderIndex(buffer.ReadableBytes);
                byteBuffer = _byteBuffer;
            }

            var packedId = byteBuffer.ReadByte();
            rawPacket.Id = packedId;
            if (rawPacket.Id > PacketConstrains.MaxPacketId)
            {
                throw new Exception("ID wykracza poza zakres znanych ID");
            }
            
            var dataLength = byteBuffer.ReadInt();
            rawPacket.DataLength = dataLength;
            if (rawPacket.DataLength > PacketConstrains.MaxPacketLength)
            {
                throw new Exception("Wiadomość jest zbyt długa, by było możliwe jej wysłanie");
            }

            if (dataLength > buffer.ReadableBytes)
            {
                byteBuffer.SetReaderIndex(byteBuffer.ReaderIndex - 5);
                _byteBuffer = new CompositeByteBuffer(_allocator, false, 2, _byteBuffer, byteBuffer);
                byteBuffer.SetReaderIndex(byteBuffer.ReadableBytes);
                return null;
            }

            rawPacket.Payload = byteBuffer.Copy();
            byteBuffer.SetReaderIndex(byteBuffer.ReaderIndex + dataLength);

            return rawPacket;
        }

        public IByteBuffer GetPayload(RawPacket packet)
        {
            return packet.Payload;
        }

        public IPacket? ParsePacket(RawPacket packet, params IPacket[] expectedPackets)
        {
            if (packet == null)
            {
                return null;
            }

            foreach (var expectedPacket in expectedPackets)
            {
                if (expectedPacket.PackedId() != packet.PackedId())
                {
                    continue;
                }
                var payload = GetPayload(packet);
                expectedPacket.Parse(payload);
                return expectedPacket;
            }

            throw new Exception($"Niespodziewane ID pakietu! {packet.PackedId()}");
        }

        public IByteBuffer ByteBuffer
        {
            get { return _byteBuffer; }
            set { _byteBuffer = value; }
        }

        public PooledByteBufferAllocator Allocator
        {
            get { return _allocator; }
            set { _allocator = value; }
        }
    }
}
