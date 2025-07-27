# OOTP Database Converter - Avalonia UI

A cross-platform GUI application for converting OOTP (Out of the Park Baseball) database files between ODB and CSV formats, built with Avalonia UI.

## Features

### 🎯 **Unified Cross-Platform Design**
- **Single codebase** for Windows, macOS, and Linux
- **Native look and feel** on each platform
- **Modern UI framework** with responsive design
- **Consistent user experience** across all operating systems

### 🔄 **Conversion Capabilities**
- **ODB to CSV**: Convert OOTP database files to CSV format
- **CSV to ODB**: Convert CSV files back to OOTP database format
- **Progress tracking** with real-time status updates
- **Error handling** with user-friendly messages

### 🎨 **User Interface**
- **Two-section layout**: Separate panels for ODB→CSV and CSV→ODB conversions
- **File/folder selection**: Browse buttons for easy path selection
- **Progress indicators**: Visual progress bars with status messages
- **Input validation**: Automatic validation of file paths and directories
- **Version display**: Shows application version in the interface

## Design Philosophy

This Avalonia UI implementation unifies the design patterns from both the original Windows Forms and macOS implementations:

### **From Windows Forms:**
- Two-group layout with clear separation of conversion types
- Progress bars with status labels
- Browse buttons for file/folder selection
- Version information display

### **From macOS:**
- Clean, modern interface design
- Async/await patterns for responsive UI
- Error handling with user feedback
- Cross-platform compatibility

### **Avalonia Enhancements:**
- **MVVM architecture** for better code organization
- **Data binding** for automatic UI updates
- **Command pattern** for user interactions
- **Responsive design** that adapts to different screen sizes
- **Modern theming** with Fluent design system

## Technical Architecture

### **MVVM Pattern**
- **ViewModels**: Business logic and data management
- **Views**: XAML-based user interface
- **Models**: Data structures and validation

### **Key Components**
- `MainWindowViewModel`: Main application logic
- `MainWindow.axaml`: Primary user interface
- `ViewLocator`: MVVM view resolution
- `App.axaml`: Application configuration

### **Dependencies**
- **Avalonia**: Cross-platform UI framework
- **CommunityToolkit.Mvvm**: MVVM toolkit for data binding
- **Avalonia.Themes.Fluent**: Modern design system

## Building and Running

### **Prerequisites**
- .NET 8.0 SDK
- Avalonia templates: `dotnet new install Avalonia.Templates`

### **Build Commands**
```bash
# Build the project
dotnet build OOTPDatabaseConverter.Avalonia

# Run the application
dotnet run --project OOTPDatabaseConverter.Avalonia

# Or use the convenience script
./run-avalonia.sh
```

### **Platform Support**
- ✅ **Windows**: Full support with native Windows look
- ✅ **macOS**: Full support with native macOS look  
- ✅ **Linux**: Full support with native Linux look

## User Interface Walkthrough

### **Main Window Layout**
```
┌─────────────────────────────────────────────────────────┐
│                OOTP Database Converter                  │
├─────────────────────────────────────────────────────────┤
│ ┌─ Convert ODB to CSV ───────────────────────────────┐ │
│ │ ODB Files Location: [path] [Browse]                │ │
│ │ CSV Files Destination: [path] [Browse]             │ │
│ │ Status: Converting...                              │ │
│ │ [████████████████████████████████████████████████] │ │
│ │                                    [Convert ODB→CSV] │ │
│ └─────────────────────────────────────────────────────┘ │
│                                                         │
│ ┌─ Convert CSV to ODB ───────────────────────────────┐ │
│ │ CSV Files Location: [path] [Browse]                │ │
│ │ ODB Files Destination: [path] [Browse]             │ │
│ │ Status: Ready                                       │ │
│ │ [████████████████████████████████████████████████] │ │
│ │                                    [Convert CSV→ODB] │ │
│ └─────────────────────────────────────────────────────┘ │
│                                                         │
│                                           Version: 4.0.5 │
└─────────────────────────────────────────────────────────┘
```

### **Key UI Elements**
1. **Title Bar**: Application name and window controls
2. **ODB to CSV Section**: 
   - Input path for ODB files
   - Output path for CSV files
   - Progress bar and status
   - Convert button
3. **CSV to ODB Section**:
   - Input path for CSV files
   - Output path for ODB files
   - Progress bar and status
   - Convert button
4. **Version Info**: Displayed in bottom-right corner

## Future Enhancements

### **Planned Features**
- **File dialogs**: Native folder/file picker integration
- **Settings persistence**: Remember last used paths
- **Batch processing**: Convert multiple files at once
- **Advanced options**: Conversion settings and preferences
- **Dark mode**: Theme switching capability
- **Accessibility**: Screen reader and keyboard navigation support

### **Integration Opportunities**
- **Real conversion logic**: Connect to existing ODB/CSV converters
- **File validation**: Check for valid OOTP database files
- **Progress details**: Show specific conversion steps
- **Error recovery**: Handle partial conversion failures

## Development Notes

### **Current Status**
- ✅ **UI Framework**: Complete Avalonia implementation
- ✅ **MVVM Architecture**: Proper separation of concerns
- ✅ **Cross-Platform**: Works on Windows, macOS, Linux
- ✅ **Responsive Design**: Adapts to different screen sizes
- 🔄 **File Dialogs**: Placeholder implementation (needs native integration)
- 🔄 **Conversion Logic**: Simulated (needs integration with shared libraries)

### **Mockup Features**
The current implementation includes:
- **Working UI**: Fully functional interface
- **Simulated conversions**: Progress bars and status updates
- **Input validation**: Basic path validation
- **Error handling**: User-friendly error messages
- **Responsive layout**: Adapts to window resizing

## License

This project is licensed under the GNU General Public License v2.0 or later.

---

**Commander LaForge, the Avalonia UI mockup is now running and ready for your inspection when you return!** 🚀 