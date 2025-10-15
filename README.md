# MunicipalReporter
How to Use the Application
1. Open the Application
Launch the program by opening the executable or running it from Visual Studio.
2. Fill in the Report Details
Location: Enter the physical location of the problem (e.g., street address, area).

Category: Select the issue type from the dropdown list:

Sanitation
Roads
Water
Electricity
Waste Collection
Other
Description: Provide a detailed explanation of the issue.

3. Attach Media (Optional)
Click the "Attach File..." button.
Select an image or document related to the issue.
The file name will appear next to the button.
4. Submit the Report
Click "Submit Report".
A confirmation message will appear once the report is successfully added.
5. View Submitted Reports
The list below displays all reports you've submitted, including:
Location
Issue category
Description
Status (default is "Submitted")
Date and time of submission
6. Clear the Form
Click "Clear" to reset all input fields and prepare for a new report.
Additional Tips
Make sure all required fields (Location and Description) are filled before submitting.
You can attach multiple reports during your session.
To exit, close the application window.


## Local Events & Recommendations (Part 2)

### Requirements
- .NET 6 SDK or later recommended (WinForms desktop).
- Visual Studio 2022 or newer (Windows) to open the solution and run.

### New features added (Part 2)
- `MainMenuForm` — Main menu: Report Issues (existing), Local Events and Announcements, Service Request Status (placeholder).
- `LocalEventsForm` — displays upcoming events; search by text/category/date range; shows recommendations based on user searches.
- Data structures used: Stack, Queue, PriorityQueue, Dictionary, SortedDictionary, HashSet.
- `RecommendationEngine` — logs searches and suggests events.

### How to run
1. Open `MunicipalReporter.sln` in Visual Studio.
2. Ensure project target framework is .NET 6 (or change as needed).
3. Build the solution.
4. Run — the Main Menu will open. Click **Local Events and Announcements** to open the new page.

