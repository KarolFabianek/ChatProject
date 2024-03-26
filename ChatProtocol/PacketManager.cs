using ChatProtocol.Reader;

namespace ChatProtocol;

public class PacketManager
{
    public delegate void PacketHandler<T>(T packet, EventArgs eventArgs) where T : IPacket; //pakiet typu t, event delegata typu T przyjmujaca 2 argumenty, where - ograniczenie gdy T dziedziczy po IPacket
    private readonly PacketReader _reader;
    private readonly Dictionary<Byte, Delegate> _handlers = new();
    private readonly Dictionary<Byte, Type> _packets = new();

    public PacketManager()
    {
        _reader = new PacketReader();
    }

    public void RegisterHandler<T>(byte packetId ,PacketHandler<T> handler) where T : IPacket
    {
        _handlers.Add(packetId , handler);
    }

    public void UnRegisterHandler(byte packetId)
    {
        _handlers.Remove(packetId);
    }

    public async void CallAndForgetHandler(RawPacket? rawPacket, EventArgs eventArgs)
    {
        if (rawPacket == null)
        {
            return; // dalej sie nie wykonuje
        }
        
        if (!_packets.ContainsKey(rawPacket.Id))
        {
            return;  // dalej sie nie wykonuje
        }

        var normalPacket = _reader.ParsePacket(rawPacket, Activator.CreateInstance(_packets[rawPacket.Id]) as IPacket); //    wyciagamy typ z pakietow o danym ID. Activator.CreateInstance wywoluje domyslny konstruktor i wyznacza typ
        
        if (normalPacket == null) 
        {
            return;
        }
        
        this.CallAndForgetHandler(normalPacket, eventArgs);
    }

    public async void CallAndForgetHandler(IPacket? packet, EventArgs eventArgs)
    {
        if (packet == null)
        {
            return;
        }

        if (!_packets.ContainsKey(packet.PackedId()))
        {
            return;
        }

        await Task.Factory.StartNew(() =>
        {
            foreach (var dic in _handlers)
            {
                if (dic.Key != packet.PackedId())
                {
                    continue;
                }

                dic.Value.DynamicInvoke(packet, eventArgs);  //wywolujemy delegate
            }
        });
    }
                            
    public void RegisterPacket<T>(byte packetId) where T : IPacket
    {
        _packets.Add(packetId, typeof(T));
    }
    
    public void DeletePacket(byte packetId)
    {
        _packets.Remove(packetId);
    }
}