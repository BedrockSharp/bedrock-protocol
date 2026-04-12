# Information for AI Agents

Welcome, AI coder! If you are modifying `bedrock-protocol`, please read these rules carefully before running any tool calls.

1. **No Comments Rule**: This codebase employs a strict zero-comment policy. Do not add `//` or `/* */` XML style summary comments anywhere in the C# files. The code must be clean and self-documenting.
2. **Individual Packet Classes**: Each Minecraft packet must reside in its own `.cs` class under `src/BedrockProtocol/Packets/`. 
3. **No External System References**: Assume standard Little-Endian formatting and use the provided `BinaryStream` wrapper for encoding and decoding payload arrays.
4. Always build the project and execute xunit tests before completing the task. 
