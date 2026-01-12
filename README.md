# Dainiki - Cross-Platform Journal App

![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-9.0-blue)
![C#](https://img.shields.io/badge/C%23-13.0-purple)
![License](https://img.shields.io/badge/license-MIT-green)

A modern, cross-platform journaling application built with .NET MAUI and Blazor, allowing users to capture their thoughts, moods, and life experiences across Windows, Android, iOS, and macOS.

## Features

### Authentication System
- **User Registration** - Secure account creation with validation
- **Login/Logout** - Session management with state persistence
- **Password Recovery** - "Forgot Password" functionality

### Journal Entry Management
- **Rich Text Editor** - Powered by Quill.js for formatted content
- **Mood Tracking** - Primary and secondary mood selection
- **Life Phase Categorization** - Track different phases of your life
- **Custom Tags** - Organize entries with custom or pre-built tags
- **CRUD Operations** - Create, read, update, and delete journal entries

### Dashboard
- **Statistics Overview** - Total entries, weekly/monthly counts
- **Streak Tracking** - Monitor your journaling consistency
- **Recent Entries** - Quick access to your latest writings
- **Quick Actions** - Fast navigation to common tasks

### UI/UX
- **Material Design** - Built with MudBlazor components
- **Responsive Layout** - Optimized for all screen sizes
- **Dark Mode Support** - Theme customization capabilities
- **Cross-Platform** - Native look and feel on every platform

## Tech Stack

### Frameworks & Libraries
- **.NET 9** - Latest .NET platform
- **.NET MAUI** - Multi-platform App UI framework
- **Blazor** - Interactive web UI framework
- **MudBlazor 8.15.0** - Material Design component library
- **SQLite (sqlite-net-pcl 1.9.172)** - Local database storage
- **Quill.js 2.0.3** - Rich text editor

### Platforms Supported
- Windows 10/11 (Build 19041+)


## Getting Started

### Prerequisites
- Visual Studio 2022 (17.8 or later) with .NET MAUI workload
- .NET 9 SDK
- Platform-specific SDKs:
  - Windows: Windows 10 SDK (10.0.19041.0)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/karkisaroj/Dainiki.git
   cd Dainiki
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Run the application**
   
   For Windows:
   ```bash
   dotnet run -f net9.0-windows10.0.19041.0
   ```
   
   For Android:
   ```bash
   dotnet run -f net9.0-android
   ```
   
   For iOS:
   ```bash
   dotnet run -f net9.0-ios
   ```
   
   For macOS:
   ```bash
   dotnet run -f net9.0-maccatalyst
   ```

## Project Structure

```
Dainiki/
├── Components/
│   ├── Database/
│   │   └── JournalDatabase.cs          # SQLite database operations
│   ├── Layout/
│   │   ├── MainLayout.razor            # Main application layout
│   │   └── NavMenu.razor               # Navigation menu
│   ├── Models/
│   │   ├── User.cs                     # User entity model
│   │   ├── LoginModel.cs               # Login form model
│   │   └── RegisterModel.cs            # Registration form model
│   ├── Pages/
│   │   ├── Home.razor                  # Landing page
│   │   ├── Login.razor                 # Login page
│   │   ├── Register.razor              # Registration page
│   │   ├── Dashboard.razor             # User dashboard
│   │   ├── JournalEntry.razor          # Journal entry editor
│   │   ├── ForgotPassword.razor        # Password recovery
│   │   └── Theme.razor                 # Theme settings
│   ├── Services/
│   │   ├── AuthService.cs              # Authentication service
│   │   └── Constants.cs                # Application constants
│   └── _Imports.razor                  # Global using directives
├── Resources/
│   ├── AppIcon/                        # Application icons
│   ├── Fonts/                          # Custom fonts
│   ├── Images/                         # Image assets
│   └── Splash/                         # Splash screen
├── wwwroot/
│   └── index.html                      # Root HTML with Quill.js
├── MauiProgram.cs                      # App configuration & DI
├── App.xaml                            # Application resources
└── Dainiki.csproj                      # Project file
```

## Configuration

### Database
The app uses SQLite for local data storage. The database path is configured in `Constants.cs`:

```csharp
public static class Constants
{
    public const string DatabaseFilename = "dainiki.db3";
    public static string DatabasePath => 
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
}
```

### Dependency Injection
Services are registered in `MauiProgram.cs`:

```csharp
builder.Services.AddMauiBlazorWebView();
builder.Services.AddMudServices();
builder.Services.AddSingleton<JournalDatabase>();
builder.Services.AddScoped<AuthService>();
```

## Features Deep Dive

### Rich Text Editor Integration
The journal entry page uses Quill.js for rich text editing with the following features:
- Headers (H1, H2, H3)
- Text formatting (bold, italic, underline, strikethrough)
- Blockquotes and code blocks
- Ordered and unordered lists
- Text color and background color
- Hyperlinks

### Mood Tracking System
Users can select moods from predefined categories:
- **Primary Moods**: Happy, Sad, Calm, Angry, Excited
- **Secondary Moods**: Work, Health, Travel, Relationships, Study

### Life Phase Categorization
Organize entries by life phase:
- School life
- College life
- Work life
- Family life

## Roadmap

- [ ] Entry search and filtering
- [ ] Export entries (PDF, TXT, JSON)
- [ ] Cloud synchronization
- [ ] Entry attachments (images, files)
- [ ] Mood analytics and insights
- [ ] Reminder notifications
- [ ] Entry encryption
- [ ] Multiple journal notebooks
- [ ] Social sharing capabilities

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Developer

**Saroj Karki**
- GitHub: [@karkisaroj](https://github.com/karkisaroj)

## Acknowledgments

- [MudBlazor](https://mudblazor.com/) - Material Design components for Blazor
- [Quill.js](https://quilljs.com/) - Rich text editor
- [.NET MAUI](https://dotnet.microsoft.com/apps/maui) - Cross-platform framework
- [SQLite](https://www.sqlite.org/) - Embedded database engine

## Support

If you have any questions or need help, please open an issue in the GitHub repository.

---

Star this repository if you find it helpful!
