<body>
    <h1>Flight Management System - Installation Guide</h1>

    <h2>1. System Requirements</h2>
    <ul>
        <li>Operating System: Windows 10 or later</li>
        <li>.NET Framework 4.7.2 or higher</li>
        <li>SQL Server Express or full version</li>
        <li>Visual Studio (latest version recommended)</li>
    </ul>

    <h2>2. Installation and Running the Project</h2>
    <h3>Step 1: Clone the Repository</h3>
    <pre><code>git clone https://github.com/haothach/FlightManagementSystem.git</code></pre>

    <h3>Step 2: Configure the Database</h3>
    <p>Import the SQL file into SQL Server:</p>
    <pre><code>1. Open SQL Server Management Studio (SSMS)
2. Connect to your SQL Server
3. Execute the <code>QLChuyenBay.sql</code> file to create the database</code></pre>

    <h3>Step 3: Configure the Connection String</h3>
    <p>Edit the connection string in the C# code:</p>
    <pre><code>string cnStr = "Data Source=.;Initial Catalog=FlightManagement;Integrated Security=True";</code></pre>

    <h3>Step 4: Run the Application</h3>
    <pre><code>1. Open Visual Studio
2. Open the <code>FlightManagement.sln</code> project
3. Press F5 to start the application</code></pre>

    <h2>3. Extended Features</h2>
    <ul>
        <li>📧 <strong>Email Sending:</strong> Automatically send ticket confirmation emails and flight change notifications.</li>
        <li>📊 <strong>Statistics & Charts:</strong> Visualize ticket sales and revenue over time using charts.</li>
        <li>🧾 <strong>Report Exporting:</strong> Export reports to PDF or Excel files.</li>
        <li>🤖 <strong>Basic Chatbot:</strong> Integrated simple chatbot for quick user support.</li>
    </ul>

    <h2>4. Contact</h2>
    <p>If you have any questions or need support, feel free to contact: 
        <a href="mailto:haonhut.thach@gmail.com">haonhut.thach@gmail.com</a>
    </p>
</body>
