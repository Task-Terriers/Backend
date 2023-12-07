## Setting Up and Running the NancyFX Server

Follow these steps to set up and run the NancyFX server in the TaskTerriers/BackEnd repository:

### Prerequisites
- Make sure you have [.NET](https://dotnet.microsoft.com/download) installed for your specific operating system.
- NancyFX package needs to be installed.

### Installation and Configuration

1. **Clone the Repository and Change Directory:**
   ```bash
    https://github.com/Task-Terriers/Backend
    cd BackEnd/NancyFX


2. **Install NancyFX:**
    ```bash 
    dotnet add package Nancy --version 2.0.0

3. **Configure Firebase in `program.cs`:**
- Open `program.cs`.
- Locate the comment lines 22-23 and add your Firebase Base Key and Secret Auth.

4. **Set the Correct IP Address:**
- In `program.cs`, change line 44 (and 47 if necessary) to use your machine's IP address.
- Example: `new Uri("http://your.ip.address.here:1234")`.
- Note: `localhost` will not work with the Android Simulator, keep `1234` as the port.

### Running the Server

- Run the server using the following command:
    ```bash 
    dotnet run

