#!/bin/bash

DOTNET_SDK_VERSION=3.1.100-preview1-014459
INSTALL_DIR=TerraformIoUtility

# Install .NET CLI dependencies
sudo apt-get update
sudo apt-get install -y --no-install-recommends \
  libc6 \
  libgcc1 \
  libgssapi-krb5-2 \
  libicu60 \
  libssl1.1 \
  libstdc++6 \
  zlib1g \
  p7zip-full
sudo rm -rf /var/lib/apt/lists/*

# Install .NET Core SDK
curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/$DOTNET_SDK_VERSION/dotnet-sdk-$DOTNET_SDK_VERSION-linux-x64.tar.gz
dotnet_sha512='2d0d4c4af775d46a0a3bf25d1ebb1f6ee51df07a82e53176efb1055cef746ca5074ab95e6dc65ae8f738c34f6a45eae42941c342b24efac5e04fa82ccbcf27d2'
echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c -
sudo mkdir -p /usr/share/dotnet
sudo tar -zxf dotnet.tar.gz -C /usr/share/dotnet
sudo rm dotnet.tar.gz
sudo ln -sf /usr/share/dotnet/dotnet /usr/bin/dotnet

# Install .NET Core SDK
curl -SL --output TerraformIoUtility.7z https://github.com/ivanfarkas2/ceres-terraformio-utility/releases/download/1.0/linux-x64.7z
sudo mkdir -p $INSTALL_DIR
sudo 7z x TerraformIoUtility.7z -o$INSTALL_DIR
sudo chmod 755 $INSTALL_DIR/*.*
sudo chmod 755 $INSTALL_DIR/TerraformIoUtility
sudo rm TerraformIoUtility.7z
cd TerraformIoUtility

echo 'Running: TerraformIoUtility help'

TerraformIoUtility help
