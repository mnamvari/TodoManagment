<h2>ASP.NET Assessment</h2>

<p>In this assessment, you will be given a set of requirements for a simple web application, and your task will be to implement the application using ASP.NET and MS SQL SERVER.</p>

<h3>Requirements</h3>

<p>Your task is to create a simple web application that allows users to view and manage a list of tasks. The application should have the following functionality:</p>

<ol>
<li>Users should be able to view a list of all tasks.</li>
<li>Users should be able to add new tasks to the list.</li>
<li>Users should be able to edit existing tasks.</li>
<li>Users should be able to delete tasks from the list.</li>
</ol>

<h3>Instructions</h3>

<ol>
<li>You should create a new ASP.NET web application project.</li>
<li>You should use MS SQL SERVER as the database for this project.</li>
<li>You should create a database table to store the tasks. The table should have the following columns:</li>
<ul>
<li><code>Id</code> (integer, primary key)</li>
<li><code>Title</code> (string)</li>
<li><code>Description</code> (string)</li>
<li><code>DueDate</code> (datetime)</li>
</ul>
<li>You should create a web page that displays a list of all tasks. The page should display the following information for each task:</li>
<ul>
<li><code>Title</code></li>
<li><code>Description</code></li>
<li><code>DueDate</code></li>
</ul>
<li>You should create a web page that allows users to add new tasks to the list. The page should have a form with the following fields:</li>
<ul>
<li><code>Title</code></li>
<li><code>Description</code></li>
<li><code>DueDate</code></li>
</ul>
<li>You should create a web page that allows users to edit existing tasks. The page should have a form with the following fields:</li>
<ul>
<li><code>Title</code></li>
<li><code>Description</code></li>
<li><code>DueDate</code></li>
</ul>
<p>The page should pre-fill the form with the current values for the task.</p>
<li>You should create a web page that allows users to delete tasks from the list. The page should prompt the user to confirm the deletion before proceeding.</li>
<li>You should add testing to ensure that it works as expected.</li>
</ol>

<h3>Input Data</h3>

<p>You can use the following sample data to test your application:</p>

<pre>
Tasks table:

| Id | Title             | Description                          | DueDate             |
|----|-------------------|--------------------------------------|---------------------|
| 1  | Complete project  | Finish project report and presentation| 2023-03-15 09:00:00 |
| 2  | Call client       | Follow up with client on project status| 2023-03-12 14:00:00 |
| 3  | Prepare for meeting | Research and prepare for project meeting| 2023-03-18 11:00:00 |
</pre>

<h3>How to Submit</h3>

<ol>
<li>Include a READEME file that has detailed instructions to run/test your project.</li>
<li>Upload the source to GitHub and share the link with the Hiring Manager.</li>
</ol>