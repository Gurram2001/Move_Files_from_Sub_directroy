# Folder File Mover

## 📌 Overview
This C# console application moves files from subdirectories of a **source directory** to a **destination directory** while keeping a log of copied files. The program ensures that limit of files are copied in one execution and implements a retry mechanism for failed copies.

## 🔧 Features
- Copies files from all sub-directories within the source folder.
- Stops copying once the file limit (**5 files**) is reached. Hardcode, you can keep limit
- Skips files that already exist in the destination.
- Implements a **retry mechanism (3 attempts)** for failed file copies.
- Logs file transfer details in `FileCopyLog.txt`.

## 🛠️ How to Use
1. **Run the application**  
   Open the terminal or command prompt and execute the compiled '.exe` file.
   
2. **Provide directory paths**  
   - Enter the **source directory path** (where files are located).  
   - Enter the **destination directory path** (where files should be moved).  

3. **File copying process begins**  
   - The application checks each subdirectory inside the source directory.
   - It copies files to the destination directory while respecting the copy limit.
   - A log file (`FileCopyLog.txt`) is created/updated in the destination directory.

4. **Completion Message**  
   - If the limit is reached, it displays: `"File copy limit reached. Stopping process."`
   - If successful, it displays: `"File extraction and movement completed."`

## 🚀 Requirements
- **.NET SDK** installed  
- Windows, Linux, or MacOS with **C# runtime**  

## ⚠️ Error Handling
- If the **source directory is invalid**, it exits with an error message.
- If the **destination directory does not exist**, it is automatically created.
- If a **file already exists** in the destination, it is skipped.
- If a file copy **fails**, it retries **3 times** before logging the error.

## 📝 Log File (`FileCopyLog.txt`)
Every successful or failed file copy attempt is recorded in `FileCopyLog.txt`, stored inside the **destination folder**.

## 🏗️ Future Enhancements
- Add a **GUI version** for easier user interaction.
- Allow users to **set a custom file copy limit**.
- Implement **multi-threading** for better performance.

---

**📝 Author:** G Mani Karthik
📅 **Last Updated:** _March 2025_  
