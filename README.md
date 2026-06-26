# CyberGuard AI - Cybersecurity Assistant

**Student Name:** Ovayo Maholwana  
**Student Number:** ST10490913  
**Project:** Part 3 - Final POE (PROG6221)

---

## Project Overview
**CyberGuard AI** is a professional WPF desktop application that features an intelligent cybersecurity chatbot combined with useful tools including a Task Assistant, Cybersecurity Quiz, and Activity Log.

This is the final submission for Part 3, building upon the chatbot developed in Part 2.

---

## Changes from Part 2

### Major Features Added in Part 3:

1. **Task Assistant with Reminders** (New)
   - Full CRUD functionality (Create, Read, Update, Delete)
   - Persistent storage using **JSON file** (`tasks.json`)
   - Tasks include title, description, reminder, and completion status

2. **Cybersecurity Mini-Game (Quiz)** (New)
   - 12+ multiple-choice questions on cybersecurity topics
   - Immediate feedback after each answer
   - Final score with performance message

3. **Activity Log System** (New)
   - Automatically records all user actions and chatbot responses
   - Viewable in a dedicated tab with timestamped entries

4. **Enhanced NLP & Intent Detection**
   - Better natural language understanding for new features
   - Supports commands such as "add task", "start quiz", "show activity log", etc.

5. **Modern Professional User Interface**
   - Dark cybersecurity-themed design
   - Tabbed navigation
   - Chat interface with message bubbles (User on right, Bot on left)

---

## Implementation Highlights

### JSON File Storage (`tasks.json`)
- Installed **Newtonsoft.Json** NuGet package
- Created `CyberTask.cs` model class
- Developed `TaskStorageHelper.cs` to handle all file operations (Load, Save, Add, Update, Delete)
- JSON file is automatically created in the output directory when the first task is added
- Implemented proper error handling and data persistence

### Other Key Classes Added:
- `TaskManager.cs` – Business logic layer
- `QuizManager.cs` – Manages quiz questions and scoring
- `ActivityLogger.cs` – Handles logging of all activities
- Updated `ChatBot.cs`, `KeywordResponder.cs`, and `MainWindow.xaml.cs`

---

## Technologies Used
- **WPF** (.NET 8.0)
- **C#**
- **Newtonsoft.Json** (for data persistence)
- Visual Studio 2022

---

## How to Run
1. Open the solution in Visual Studio 2022
2. Build the project (`Ctrl + Shift + B`)
3. Run the application (`F5`)
4. Use the different tabs to explore all features

---

**Submitted by:** Ovayo Maholwana (ST10490913)  
**Part 3 Final Submission**

---

