1.Introduction
The "Manager Notes" web application is designed to create, delete, update and view notes.
The application was written using the .Net Core 2.1. platform and MVC pattern. 
"Manager Notes" has an adaptive design.


2.Initial configuration of the application in VS
To work correctly, write the following commands in the NuGet command line of VS (Tools -> NuGet Package Manager -> Package Manager Console):
a) Add-Migration "InitialCreate"
b) Update-Database


3.description of the client side
The main task of this website is to create necessary notes and various operations with them. In the primary 
The start up page displays the main panel with :
a list of existing notes, if not displayed 
a message that the user needs to create them,
when you click on a note from the menu, its content will be displayed, there are 2 buttons (icons) in this field with the content of the note.
with which you can either delete the current note or modify it.
When you change a note, the user is redirected to another page with 2 text messages. 
fields that display the title and text of the note.
Also the current page contains 2 buttons ("Submit", "Back to List") with the help of which 
the user applies the changes made or returns to the list of notes.
Also on the main page of the site there is a button "Add" ("Добавить"), which is 
redirects the user to the same page as when changing the note, only to add a new note with empty text fields.
The navigation bar contains two controls:
a) a link (Name of the site - "Manager Notes") that redirects the user to 
home page
b) drop-down list for language switching (RUS-EN).



4.Project errors/problems
The problem with the project at the moment is to reload the entire page, not its content. You can fix this problem using Ajax, JS and others.

