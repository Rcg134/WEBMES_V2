# Define the source and destination paths
$sourcePath = ".\publish"
$destinationPath = "\\192.168.1.65\C$\inetpub\wwwroot\sampleAPI"

# Define the credentials (replace "username" and "password" with your actual credentials)
$username = "administrator"
$password = ConvertTo-SecureString "Dnhk$%07232022" -AsPlainText -Force
$credential = New-Object -TypeName System.Management.Automation.PSCredential -ArgumentList $username, $password

# Create the \publish directory if it doesn't exist
if (-not (Test-Path -Path $sourcePath -PathType Container)) {
    New-Item -Path $sourcePath -ItemType Directory
}

# Execute dotnet publish command
dotnet publish -o .\publish --force


# Create a temporary PSDrive with credentials
New-PSDrive -Name "TempDrive" -PSProvider FileSystem -Root $sourcePath -Credential $credential | Out-Null

# Copy the files to the destination using the temporary PSDrive
Copy-Item -Path "TempDrive:\*" -Destination $destinationPath -Recurse -Force

# Remove the temporary PSDrive
Remove-PSDrive -Name "TempDrive"

# Remove the \publish directory after copying
Remove-Item -Path $sourcePath -Force -Recurse
