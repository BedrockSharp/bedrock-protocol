# Contributing

When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change. 

### Rules
1. **Packet implementation**: Add each new packet as a separate class inside the `Packets/` folder. Do not place multiple packets in one file.
2. Update `PacketIds.cs` with the correct enumerator id.

### Pull Request Process
1. Ensure the CI workflows (`dotnet build` and `dotnet test`) pass before you submit.
2. Update the README.md with details of changes to the protocol version.
3. You may merge the Pull Request in once you have the sign-off of two other developers, or if you do not have permission to do that, you may request the second reviewer to merge it for you.
