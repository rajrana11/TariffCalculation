#!/bin/bash

# Set GitHub repository URL
REPO_URL="https://github.com/rajrana11/TariffCalculation.git"
PROJECT_DIR="your-repository"  # Change this to match the name of your repo

# Install .NET SDK 8.0 if not installed
if ! command -v dotnet &> /dev/null
then
    echo ".NET SDK 8.0 not found, installing..."
    
    # Download .NET SDK 8.0 (replace with the correct version if needed)
    wget https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-8.0.405-linux-x64-binaries -O dotnet-sdk-8.0.0-linux-x64.tar.gz
    
    # Extract the tar file to /usr/local/dotnet
    sudo mkdir -p /usr/local/dotnet
    sudo tar -zxvf dotnet-sdk-8.0.0-linux-x64.tar.gz -C /usr/local/dotnet
    
    # Add dotnet to the PATH
    echo "export PATH=\$PATH:/usr/local/dotnet" >> ~/.bashrc
    source ~/.bashrc
    
    echo ".NET SDK 8.0 installed successfully."
else
    echo ".NET SDK 8.0 already installed."
fi

# Clone the GitHub repository (if not already cloned)
if [ ! -d "$PROJECT_DIR" ]; then
    echo "Cloning the repository..."
    git clone $REPO_URL
else
    echo "Repository already cloned."
fi

# Navigate to the project directory
cd $PROJECT_DIR

# Restore the project dependencies
echo "Restoring project dependencies..."
dotnet restore

# Build the project
echo "Building the project..."
dotnet build

# Run the project
echo "Running the project..."
dotnet run

