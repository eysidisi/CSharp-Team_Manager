<div id="top"></div>


<!-- PROJECT LOGO -->
<br />
<div align="center">
  <!-- <a href="https://github.com/github_username/repo_name">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
  </a> -->
  

<h3 align="center">Team Manager</h3>

  <p align="center">
    A team management system
    <!-- <br />
    <a href="https://github.com/github_username/repo_name"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/github_username/repo_name">View Demo</a>
    ·
    <a href="https://github.com/github_username/repo_name/issues">Report Bug</a>
    ·
    <a href="https://github.com/github_username/repo_name/issues">Request Feature</a> -->
  </p>
</div>



<!-- TABLE OF CONTENTS -->
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>



<!-- ABOUT THE PROJECT -->
## About The Project

This project is created as a dummy team management system. You can add/remove/view users and teams. You can also add/remove users to/from the teams and change the relationship between users and teams.

<p align="right">(<a href="#top">back to top</a>)</p>

### Built With
* [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
* [.Net 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0/)
* [Winforms](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/overview/?view=netdesktop-6.0/)
* [XUnit](https://xunit.net//)
* [Moq](https://github.com/moq/moq4)
* [SQLite](https://www.sqlite.org/index.html)
* [Dapper](https://github.com/DapperLib/Dapper/)


<p align="right">(<a href="#top">back to top</a>)</p>


<!-- GETTING STARTED -->
## Getting Started

### Prerequisites

To run the project properly using Visual Studio 2022 is recommended. 


### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/eysidisi/CSharp-Team_Manager.git
   ```
2. Open solution using Visual Studio

3. Set TeamManager.UI as startup project.

4. Run the application

<p align="right">(<a href="#top">back to top</a>)</p>


<!-- USAGE EXAMPLES -->
## Usage

The project has two main parts. Namely, "Wizard" and "Management". When you run it, It starts with  the Wizard window.

## Wizard
This is where a manager logs in to the system. It asks for a username and a password. 

![loginpage-screenshot]

When the manager is authenticated It also asks for the purpose of usage.

![purposepage-screenshot]

After saving the purpose of the manager Wizard form opens Management Form.

## Management
The management form comes with two panels on the same page. One is for team management and the other is for user management. 
![management-screenshot]

### Checking details of Users and Teams
After selecting the desired user or team you can see the details about the user/team by using the "Team Details" or "User Details" buttons.
![detailspage-screenshot]

### Adding/Removing Users and Teams

You can add new users and teams by entering the required information or you can delete users and teams with no user presented
![addingpage-screenshot]

### Editing Teams
You can add members to a team or remove members from a team by using the "Edit Team" button.
![editteampage-screenshot]

<!-- _For more examples, please refer to the [Documentation](https://example.com)_ -->

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- ROADMAP -->
## Roadmap

- [ ] Add ability to query users and teams by their name and creation date range

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- CONTACT -->
## Contact

Ali İhsan ELMAS - a.ihsan.elmas@gmail.com

[![LinkedIn][linkedin-shield]][linkedin-url]


<p align="right">(<a href="#top">back to top</a>)</p>




<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/github_username/repo_name.svg?style=for-the-badge
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/eysidisi/
[product-screenshot]: images/Wizard/LogInPage.PNG
[loginpage-screenshot]: images/Wizard/LogInPage.PNG
[purposepage-screenshot]: images/Wizard/PurposePage.PNG
[management-screenshot]: images/Management/ManagementWindow.PNG
[addingpage-screenshot]: images/Management/AddingPage.PNG
[detailspage-screenshot]: images/Management/DetailsPage.PNG
[editteampage-screenshot]: images/Management/EditTeamPage.PNG