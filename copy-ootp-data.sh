#!/bin/bash

# OOTP Data Copy Script
# This script finds OOTP 26 in the Steam library and copies ODB files to ./test-data for testing

set -e  # Exit on any error

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Function to print colored output
print_status() {
    echo -e "${BLUE}[INFO]${NC} $1" >&2
}

print_success() {
    echo -e "${GREEN}[SUCCESS]${NC} $1" >&2
}

print_warning() {
    echo -e "${YELLOW}[WARNING]${NC} $1" >&2
}

print_error() {
    echo -e "${RED}[ERROR]${NC} $1" >&2
}

# Function to find Steam library directories
find_steam_libraries() {
    local steam_libs=()
    
    # Common Steam library locations
    local common_paths=(
        "$HOME/.steam/steam/steamapps"
        "$HOME/.local/share/Steam/steamapps"
        "/usr/share/steam/steamapps"
        "/opt/steam/steamapps"
    )
    
    # Check for additional library folders from libraryfolders.vdf
    local libraryfolders="$HOME/.steam/steam/steamapps/libraryfolders.vdf"
    if [ -f "$libraryfolders" ]; then
        print_status "Found Steam library configuration at: $libraryfolders"
        
        # Extract library paths from libraryfolders.vdf
        local lib_paths=$(grep -o '"[^"]*"' "$libraryfolders" | grep -v "libraryfolders" | grep -v "apps" | sed 's/"//g' | grep -v "^[0-9]*$")
        
        for path in $lib_paths; do
            if [ -d "$path/steamapps" ]; then
                steam_libs+=("$path/steamapps")
                print_status "Found Steam library: $path/steamapps"
            fi
        done
    fi
    
    # Add common paths if they exist
    for path in "${common_paths[@]}"; do
        if [ -d "$path" ]; then
            steam_libs+=("$path")
            print_status "Found Steam library: $path"
        fi
    done
    
    echo "${steam_libs[@]}"
}

# Function to find OOTP 26 installation
find_ootp26() {
    local steam_libs=($(find_steam_libraries))
    
    if [ ${#steam_libs[@]} -eq 0 ]; then
        print_error "No Steam libraries found!"
        return 1
    fi
    
    for lib in "${steam_libs[@]}"; do
        print_status "Searching in Steam library: $lib"
        
        # Look for OOTP 26 app manifest
        local ootp_manifest=$(find "$lib" -name "appmanifest_*.acf" -exec grep -l "Out of the Park Baseball 26\|OOTP Baseball 26" {} \; 2>/dev/null | head -1)
        
        if [ -n "$ootp_manifest" ]; then
            print_success "Found OOTP 26 manifest: $ootp_manifest"
            
            # Extract app ID from manifest filename
            local app_id=$(basename "$ootp_manifest" | sed 's/appmanifest_\([0-9]*\)\.acf/\1/')
            print_status "OOTP 26 App ID: $app_id"
            
            # Find the actual game directory
            local game_dir="$lib/common/Out of the Park Baseball 26"
            if [ ! -d "$game_dir" ]; then
                game_dir="$lib/common/OOTP Baseball 26"
            fi
            
            if [ -d "$game_dir" ]; then
                print_success "Found OOTP 26 installation: $game_dir"
                echo "$game_dir"
                return 0
            else
                print_warning "Found manifest but game directory not found at expected location"
            fi
        fi
    done
    
    print_error "OOTP 26 not found in any Steam library!"
    return 1
}

# Function to copy ODB files
copy_odb_files() {
    local source_dir="$1"
    local target_dir="./test-data"
    
    print_status "Looking for ODB files in: $source_dir"
    
    # Debug: Check if source directory exists
    if [ ! -d "$source_dir" ]; then
        print_error "Source directory does not exist: $source_dir"
        return 1
    fi
    
    # Create target directory if it doesn't exist
    if [ ! -d "$target_dir" ]; then
        mkdir -p "$target_dir"
        print_status "Created target directory: $target_dir"
    fi
    
    print_status "Copying ODB files..."
    
    # Use find to locate all .odb files and copy them directly to target directory
    if find "$source_dir" -name "*.odb" -type f -exec cp {} "$target_dir/" \; 2>/dev/null; then
        local copied_count=$(find "$target_dir" -name "*.odb" -type f | wc -l)
        print_success "Successfully copied $copied_count ODB file(s) to $target_dir"
        
        # List copied files
        echo
        print_status "Copied files:"
        ls -la "$target_dir"/*.odb 2>/dev/null || print_warning "No ODB files in target directory"
        
        return 0
    else
        print_error "Failed to copy ODB files"
        return 1
    fi
}

# Function to show usage
show_usage() {
    echo "Usage: $0 [OPTIONS]"
    echo
    echo "Options:"
    echo "  -h, --help     Show this help message"
    echo "  -f, --force    Force copy even if files already exist"
    echo "  -v, --verbose  Verbose output"
    echo
    echo "This script will:"
    echo "  1. Search for Steam libraries"
    echo "  2. Find OOTP 26 installation"
    echo "  3. Copy all *.odb files to ./test-data directory"
    echo
    echo "Example:"
    echo "  $0"
    echo "  $0 --verbose"
}

# Main script
main() {
    print_status "OOTP Data Copy Script"
    print_status "====================="
    
    # Parse command line arguments
    FORCE_COPY=false
    VERBOSE=false
    
    while [[ $# -gt 0 ]]; do
        case $1 in
            -h|--help)
                show_usage
                exit 0
                ;;
            -f|--force)
                FORCE_COPY=true
                shift
                ;;
            -v|--verbose)
                VERBOSE=true
                shift
                ;;
            *)
                print_error "Unknown option: $1"
                show_usage
                exit 1
                ;;
        esac
    done
    
    # Check if we're in the right directory
    if [ ! -f "OOTPDatabaseConverter.Core/OOTPDatabaseConverter.Core.csproj" ]; then
        print_error "This script must be run from the OOTPDBTools root directory!"
        exit 1
    fi
    
    # Find OOTP 26
    print_status "Searching for OOTP 26..."
    local ootp_dir
    ootp_dir=$(find_ootp26)
    
    if [ $? -ne 0 ]; then
        print_error "Could not find OOTP 26 installation!"
        print_status "Please ensure OOTP 26 is installed via Steam and try again."
        exit 1
    fi
    
    print_status "OOTP directory found: '$ootp_dir'"
    
    # Copy ODB files
    print_status "Copying ODB files..."
    copy_odb_files "$ootp_dir"
    
    if [ $? -eq 0 ]; then
        print_success "Data copy completed successfully!"
        print_status "You can now use the ODB files in ./test-data for testing."
    else
        print_warning "No ODB files were copied. Please check the OOTP 26 installation."
        exit 1
    fi
}

# Run main function
main "$@" 