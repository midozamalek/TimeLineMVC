TimeLineMVC
===========

Introduction
------------

TimeLineMVC is a modern, user-friendly web application designed to display and manage user-generated content in a timeline format. Built using ASP.NET MVC, it provides an intuitive interface for users to view posts, upload images, and interact with others. The application leverages MediatR and CQRS for clear, maintainable architecture, ensuring scalability and performance.

Features
--------

*   **User Authentication**: Secure access using OAuth 2.0 and JWT.
*   **Post Creation**: Users can create text-based posts with optional image uploads.
*   **Image Upload & Processing**: Supports JPG, PNG, and WebP formats, with automatic resizing and format conversion in the background.
*   **Timeline Display**: Displays posts in a chronological order, allowing users to view the latest content.
*   **Responsive Design**: Optimized for different screen sizes, providing a seamless experience on both desktop and mobile devices.

Technical Stack
---------------

*   **Backend**: ASP.NET MVC, .NET 8
*   **Authentication**: OAuth 2.0 and JWT
*   
Prerequisites
-------------

*   **Development Environment**: Visual Studio or any .NET-supported IDE
*   **Dependencies**: .NET 8 SDK

Setup
-----

1.  Clone the repository:
    
    ```bash
    git clone https://github.com/your-repo/timeline-mvc.git
    ```
    
2.  Navigate to the project directory:
    
    ```bash
    cd timeline-mvc
    ```
    
3.  Restore the necessary dependencies:
    
    ```bash
    dotnet restore
    ```
    
4.  Build the application:
    
    ```bash
    dotnet build
    ```
    
5.  Run the application:
    
    ```bash
    dotnet run
    ```
    



Authentication
--------------

*   Use hardcoded credentials ( Username == "admin" &&  Password == "password") for testing. The system uses OAuth 2.0 and JWT for securing endpoints.

Image Processing
----------------

*   The application processes image uploads in the background using Hangfire (not implemented).
*   Supported formats: JPG, PNG, WebP
*   Max image size: 2MB
*   Resizing and format conversion are handled automatically based on screen dimensions.

 
Contributing
------------

Contributions are welcome! Please read [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

License
-------

This project is licensed under the MIT License - see the LICENSE file for details.

 
