#!/bin/bash

echo "Building Portable OOTP Database Converter..."
echo "=========================================="

# Create output directory
OUTPUT_DIR="portable-builds"
mkdir -p "$OUTPUT_DIR"

# Build self-contained versions for different platforms
PLATFORMS=("win-x64" "linux-x64" "osx-x64")

for platform in "${PLATFORMS[@]}"; do
    echo "Building for $platform..."
    
    # Build Avalonia app
    dotnet publish OOTPDatabaseConverter.Avalonia/OOTPDatabaseConverter.Avalonia.csproj \
        -c Release \
        -r $platform \
        --self-contained true \
        -p:PublishSingleFile=true \
        -p:PublishTrimmed=true \
        -o "$OUTPUT_DIR/$platform-avalonia"
    
    # Build Console app
    dotnet publish OOTPDatabaseConverter.Console/OOTPDatabaseConverter.Console.csproj \
        -c Release \
        -r $platform \
        --self-contained true \
        -p:PublishSingleFile=true \
        -p:PublishTrimmed=true \
        -o "$OUTPUT_DIR/$platform-console"
    
    # Create portable package
    PACKAGE_NAME="OOTPDatabaseConverter-$platform-portable"
    mkdir -p "$OUTPUT_DIR/$PACKAGE_NAME"
    
    # Copy executables
    cp "$OUTPUT_DIR/$platform-avalonia/OOTPDatabaseConverter.Avalonia"* "$OUTPUT_DIR/$PACKAGE_NAME/"
    cp "$OUTPUT_DIR/$platform-console/OOTPDatabaseConverter"* "$OUTPUT_DIR/$PACKAGE_NAME/"
    
    # Copy scripts and documentation
    cp copy-ootp-data.sh "$OUTPUT_DIR/$PACKAGE_NAME/" 2>/dev/null || cp copy-ootp-data.bat "$OUTPUT_DIR/$PACKAGE_NAME/"
    cp README.md "$OUTPUT_DIR/$PACKAGE_NAME/" 2>/dev/null || echo "README.md not found"
    
    # Create simple usage instructions
    cat > "$OUTPUT_DIR/$PACKAGE_NAME/USAGE.txt" << EOF
OOTP Database Converter - Portable Version
=========================================

This is a portable version that requires no installation.

USAGE:
- GUI Version: Run OOTPDatabaseConverter.Avalonia (or .exe on Windows)
- Console Version: Run OOTPDatabaseConverter (or .exe on Windows)

The application will create test-data and test-csv-output directories
in the same folder when you use the "Copy OOTP Data" feature.

No installation required - just run the executable!
EOF
    
    # Create ZIP package
    cd "$OUTPUT_DIR"
    zip -r "$PACKAGE_NAME.zip" "$PACKAGE_NAME"
    cd ..
    
    echo "Created $OUTPUT_DIR/$PACKAGE_NAME.zip"
done

echo ""
echo "Portable builds complete!"
echo "Check the $OUTPUT_DIR directory for the packages."
echo ""
echo "Each package contains:"
echo "- Self-contained executable (no .NET runtime needed)"
echo "- Copy script for OOTP data"
echo "- Usage instructions"
echo "- No installation required" 