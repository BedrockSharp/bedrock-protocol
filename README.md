<p align="center">
  <h1 align="center">bedrock-protocol</h1>
  <p align="center"><i>A C# implementation of the Minecraft: Bedrock Edition network protocol.</i></p>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-9.0-512bd4?style=for-the-badge&logo=.net" alt=".NET 9.0">
  <img src="https://img.shields.io/badge/License-MIT-green?style=for-the-badge" alt="License MIT">
  <img src="https://img.shields.io/github/actions/workflow/status/BedrockSharp/bedrock-protocol/build.yml?style=for-the-badge" alt="Build Status">
  <img src="https://img.shields.io/badge/PRs-welcome-cyan?style=for-the-badge" alt="PRs Welcome">
</p>

**bedrock-protocol** is an C# implementation of the Minecraft: Bedrock Edition network protocol. It is built on top of [raknet-cs](https://github.com/BedrockSharp/raknet-cs) for robust RakNet networking.

---

## 🛠️ Getting Started

### Installation
Clone the repository and build the project:
```bash
git clone https://github.com/BedrockSharp/bedrock-protocol.git
cd bedrock-protocol
dotnet build
```

## 📦 Defining Custom Packets

Use the attribute-based system to register your packets effortlessly:

```csharp
public class ExamplePacket : Packet
{
    public override uint PacketId => (uint)PacketIds.ExamplePacket;

    public string Message { get; set; } = string.Empty;

    public override void Encode(BinaryStream stream)
    {
        stream.WriteString(Message);
    }

    public override void Decode(BinaryStream stream)
    {
        Message = stream.ReadString();
    }
}
```

## 🤝 Contributing

Contributions are what make the open-source community such an amazing place!
1. **Fork** the project.
2. **Create** your Feature Branch (`git checkout -b feature/AmazingFeature`).
3. **Commit** your changes (`git commit -m 'Add some AmazingFeature'`).
4. **Push** to the Branch (`git push origin feature/AmazingFeature`).
5. **Open** a Pull Request.

Please see [CONTRIBUTING.md](CONTRIBUTING.md) for details.

## 📄 License

Distributed under the MIT License. See [LICENSE](LICENSE) for more information.

---
<p align="center">
  Maintained with ❤️ by the BedrockSharp team.
</p>